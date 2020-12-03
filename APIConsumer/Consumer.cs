using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIConsumer
{
    // This class encapsulates all communication functions between the external API and the project
    public class Consumer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger(); // log to Log.txt

        private HttpClient client = new HttpClient();
        private string endpoint = "http://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/";

        private APIConsumerForm Form; // The class requires a reference to the main form in order to manipulate the DataGridView

        public List<Product> ProductList = new List<Product>();

        // Dictionaries are used to quickly determine whether a product with a given Name or Id exists
        public Dictionary<int, Product> ProductIdDict = new Dictionary<int, Product>();
        public Dictionary<string, Product> ProductNameDict = new Dictionary<string, Product>();

        public InputValidator validator;

        public Consumer(APIConsumerForm form)
        {
            validator = new InputValidator(this);
            this.Form = form;
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Json-formatted responses and requests to be expected
        }

        //Refresh the list of products by performing a new GET request, if 'method' is an Id (formatted as string), get a product with the given Id, otherwise if 'method' is empty retrieve a list of all products
        public async Task GetAsync(string method, bool isSorted = false)
        {
            Logger.Info("GET " + endpoint + method);

            DisableUI(); // Disable UI to avoid spamming Get requests (and to force the user to wait for a product list before attempting other operations)
            Form.CountLabel.Text = "Retrieving...";

            // Clear datastructures to ensure they all reflect the latest products obtained from the API
            Form.ProductGrid.Rows.Clear(); //also clears ProductList once bound
            ProductNameDict.Clear();
            ProductIdDict.Clear();

            Task<HttpResponseMessage> GetResponse = client.GetAsync(method);
            string JsonProducts = "";
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse; // Async tasks may throw exceptions
                GetResponseResult.EnsureSuccessStatusCode(); // Throws exception if status code is not success
                JsonProducts = await GetResponseResult.Content.ReadAsStringAsync(); // Async tasks may throw exceptions
            }
            catch (Exception e) //Catch occurs if either the response status code is not successful, or the response content could not be read as string
            {
                //response error 
                Logger.Error(e, "Exception caught in GetAsync function " + endpoint + method);

                if (GetResponseResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Id not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else //response content could not be read as string or response has unspecified status code
                {
                    MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                JsonProducts = ""; //ensure string is empty to reflect the error
            }

            if (JsonProducts != "") // Catch block did not execute, i.e. response status was successful and result could be read as string
            {
                try
                {
                    if (isSorted)
                    {
                        GetSortedResponse response = JsonConvert.DeserializeObject<GetSortedResponse>(JsonProducts);
                        ProductList = response.Results;
                    }
                    else if (method.Length == 0) // get list of all products
                        ProductList = JsonConvert.DeserializeObject<List<Product>>(JsonProducts);
                    else // get one product
                        ProductList.Add(JsonConvert.DeserializeObject<Product>(JsonProducts));
                }
                catch(Exception e)
                {
                    MessageBox.Show("Could not interpret API result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.Error(e, " Exception caught during deserialization of " + JsonProducts);
                }

                foreach (Product product in ProductList)
                {
                    ProductNameDict.Add(product.Name, product);
                    ProductIdDict.Add(product.Id, product);
                }

                //Bind products to grid
                BindSources();

                Logger.Info("GET " + endpoint + method + " executed successfully");
            }
            else
            {
                MessageBox.Show("Empty API result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("JsonConverter could not parse GET result or exception occurred " + endpoint + method);
            }

            EnableUI();
        }
        
        // Add new product
        public async Task PostAsync(Product addProduct)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(addProduct), Encoding.UTF8, "application/json");
            
            Logger.Info("POST " + addProduct.ToString());
         
            Task<HttpResponseMessage> GetResponse = client.PostAsync("", content);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                //response error 
                Logger.Error(e, "Exception caught in PostAsync function " + addProduct.ToString());

                if (GetResponseResult.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Product Properties Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(GetResponseResult.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    MessageBox.Show("Duplicate Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                {
                    MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if(GetResponseResult.IsSuccessStatusCode)
            {
                Logger.Info("POST " + addProduct.ToString() + " completed successfully");

                MessageBox.Show("Product added successfully, retrieving updated list...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddProductToDatastructures(addProduct);
                //GetAsync(""); // Necessary because the API seems to ignore requested Id and instead uses incremented largest Id, therefore the product list must be updated to ensure the DataGrid reflects the Id assigned by the API
                // Open Issue: should the API be behaving in this way? Update: Id is now auto-generated instead of user-specified (see AddProductForm)
            }
            else
            {
                MessageBox.Show("Could not add product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("POST " + addProduct.ToString() + " response status code is not successful");
            }

        }

        // Delete product with specified Id (method) at rowIndex in DataGrid
        public async Task DeleteAsync(string method, int rowIndex)
        {
            Logger.Info("DELETE " + method);

            Task<HttpResponseMessage> GetResponse = client.DeleteAsync(method);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                //response error 
                Logger.Error(e, "Exception caught in DeleteAsync " + method);

                if (GetResponseResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Id does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (GetResponseResult.IsSuccessStatusCode)
            {
                Logger.Info("DELETE " + method + " successful");
                MessageBox.Show("Product Deleted Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DeleteProductFromDatastructures(rowIndex);
            }
            else
            {
                MessageBox.Show("Could not delete product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("DELETE " + method + " response status code is not successful");
            }
        }

        // Update product based on addProduct, at specified rowIndex in DataGrid
        public async Task PutAsync(Product addProduct, int rowIndex)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(addProduct), Encoding.UTF8, "application/json");

            Logger.Info("PUT " + addProduct.ToString());

            Task<HttpResponseMessage> GetResponse = client.PutAsync(addProduct.Id.ToString(), content);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
            }
            catch(Exception e)
            {
                //response error 
                Logger.Error(e, "Exception caught during PutAsync " + addProduct.ToString());

                if (GetResponseResult.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Product Properties Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (GetResponseResult.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    MessageBox.Show("Duplicate Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(GetResponseResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Id does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (GetResponseResult.IsSuccessStatusCode)
            {
                Logger.Info("PUT " + addProduct.ToString() + " successful");
                MessageBox.Show("Product Edited Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateProductToDatastructures(rowIndex, addProduct);
            }
            else
            {
                MessageBox.Show("Could not edit product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("PUT " + addProduct.ToString() + " response status code is not successful");
            }
        }

        private void UpdateProductToDatastructures(int gridIndex, Product addProduct)
        {
            DeleteProductFromDatastructures(gridIndex);
            AddProductToDatastructures(addProduct, gridIndex);
        }

        private void DeleteProductFromDatastructures(int gridIndex)
        {
            ProductNameDict.Remove(Form.ProductGrid.Rows[gridIndex].Cells["Name"].Value.ToString());
            ProductIdDict.Remove((int) Form.ProductGrid.Rows[gridIndex].Cells["Id"].Value);
            Form.ProductGrid.Rows.RemoveAt(gridIndex);
            Form.ProductGrid.Update();
        }

        private void BindSources()
        {
            //Bind grid to product list
            Form.ProductGrid.AutoGenerateColumns = true;
            var bindingList = new BindingList<Product>(ProductList);
            var source = new BindingSource(bindingList, null);
            Form.ProductGrid.DataSource = source;

            //Bind count label to grid row count
            Binding countBinding = new Binding("Text", new DataGridRowCountBindingHelper(Form.ProductGrid), "Count", true);
            countBinding.Format += (sender, e) => e.Value = string.Format("{0} items", e.Value);
            Form.CountLabel.DataBindings.Clear(); //avoid adding the same binding twice if the label text has been bound before
            Form.CountLabel.DataBindings.Add(countBinding);
        }


        private void AddProductToDatastructures(Product addProduct, int gridIndex = 0)
        {
            ProductList.Insert(gridIndex, addProduct);
            Form.ProductGrid.DataSource = new BindingSource(new BindingList<Product>(ProductList), null);
            ProductNameDict.Add(addProduct.Name, addProduct);
            ProductIdDict.Add(addProduct.Id, addProduct);
        }

        private void DisableUI()
        {
            Form.Enabled = false;
        }

        private void EnableUI()
        {
            Form.Enabled = true;
        }
    }
}
