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
        public async Task GetProductListAsync(string method, bool isSorted = false)
        {
            DisableUI(); // Disable UI to avoid spamming Get requests (and to force the user to wait for a product list before attempting other operations)

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
            catch //Catch occurs if either the response status code is not successful, or the response content could not be read as string
            {
                //response error 

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
                if(isSorted)
                {
                    GetSortedResponse response = JsonConvert.DeserializeObject<GetSortedResponse>(JsonProducts);
                    ProductList = response.Results;
                }
                else if (method.Length == 0) // get list of all products
                    ProductList = JsonConvert.DeserializeObject<List<Product>>(JsonProducts);
                else // get one product
                    ProductList.Add(JsonConvert.DeserializeObject<Product>(JsonProducts));

                foreach(Product product in ProductList)
                {
                    ProductNameDict.Add(product.Name, product);
                    ProductIdDict.Add(product.Id, product);
                }

                //Bind products to grid
                BindSources();
            }

            EnableUI();
        }
        
        // Add new product
        public async Task PostAsync(Product addProduct)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(addProduct), Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> GetResponse = client.PostAsync("", content);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
            }
            catch
            {
                //response error 

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
                MessageBox.Show("Product Added Successfully, retrieving updated list...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddProductToDatastructures(addProduct);
                GetProductListAsync(""); // Necessary because the API seems to ignore requested Id and instead uses incremented largest Id, therefore the product list must be updated to ensure the DataGrid reflects the Id assigned by the API
            }

        }

        // Delete product with specified Id (method) at rowIndex in DataGrid
        public async Task DeleteAsync(string method, int rowIndex)
        {
            //DisableUI();

            Task<HttpResponseMessage> GetResponse = client.DeleteAsync(method);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
            }
            catch
            {
                //response error 

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
                MessageBox.Show("Product Deleted Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DeleteProductFromDatastructures(rowIndex);
            }

            //EnableUI();
        }

        // Update product based on addProduct, at specified rowIndex in DataGrid
        public async Task PutAsync(Product addProduct, int rowIndex)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(addProduct), Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> GetResponse = client.PutAsync(addProduct.Id.ToString(), content);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
            }
            catch
            {
                //response error 

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
                MessageBox.Show("Product Edited Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateProductToDatastructures(rowIndex, addProduct);
            }
        }

        private void UpdateProductToDatastructures(int gridIndex, Product addProduct)
        {
            DeleteProductFromDatastructures(gridIndex);
            AddProductToDatastructures(addProduct);
        }

        private void DeleteProductFromDatastructures(int gridIndex)
        {
            ProductNameDict.Remove(Form.ProductGrid.Rows[gridIndex].Cells["Name"].Value.ToString());
            ProductIdDict.Remove((int) Form.ProductGrid.Rows[gridIndex].Cells["Id"].Value);
            Form.ProductGrid.Rows.RemoveAt(gridIndex);
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


        private void AddProductToDatastructures(Product addProduct)
        {
            ProductList.Add(addProduct);
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
