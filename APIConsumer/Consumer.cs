using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public List<Product> ProductList = new List<Product>();

        // Dictionaries are used to quickly determine whether a product with a given Name or Id exists
        public Dictionary<int, Product> ProductIdDict = new Dictionary<int, Product>();
        public Dictionary<string, Product> ProductNameDict = new Dictionary<string, Product>();

        public InputValidator validator;

        public int MaxId = 0;

        public bool IsSuccess;

        public Consumer()
        {
            validator = new InputValidator(this);
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Json-formatted responses and requests to be expected
        }

        //Refresh the list of products by performing a new GET request, if 'method' is an Id (formatted as string), get a product with the given Id, otherwise if 'method' is empty retrieve a list of all products
        public async Task GetAsync(string method, bool isSorted = false)
        {
            IsSuccess = true; // return value to indicate whether the GET request was successful

            Logger.Info("GET " + endpoint + method);

            Task<HttpResponseMessage> GetResponse = client.GetAsync(method); // Send GET
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

                JsonProducts = ""; // reflect the error
            }

            if (JsonProducts != "") // Catch block did not execute, i.e. response status was successful and result could be read as (non-empty) string
            {
                try
                {
                    if (isSorted)
                    {
                        GetSortedResponse response = JsonConvert.DeserializeObject<GetSortedResponse>(JsonProducts);
                        ProductList = response.Results;
                    }
                    else if (method.Length == 0) // get list of all products
                    {
                        ProductList = JsonConvert.DeserializeObject<List<Product>>(JsonProducts);

                        // Clear datastructures to ensure they all reflect the latest changes obtained from the API
                        ProductNameDict.Clear();
                        ProductIdDict.Clear();

                        foreach (Product product in ProductList)
                        {
                            ProductNameDict.Add(product.Name, product);
                            ProductIdDict.Add(product.Id, product);
                        }

                        UpdateMaxId();
                    }
                    else // get one product
                        ProductList.Add(JsonConvert.DeserializeObject<Product>(JsonProducts));
                }
                catch(Exception e)
                {
                    MessageBox.Show("Could not interpret API result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.Error(e, " Exception caught during deserialization of " + JsonProducts);
                    IsSuccess = false;
                }

                Logger.Info("GET " + endpoint + method + " executed successfully");
            }
            else // either an exception was caught, or the string returned from ReadAsStringAsync was empty. In both cases an "Empty API result" message is appropriate, since the user will be presented with an empty list
            {
                MessageBox.Show("Empty API result", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("JsonConverter could not parse GET result or exception occurred " + endpoint + method);
                IsSuccess = false;
            }
        }
        
        // Add new product
        public async Task PostAsync(Product addProduct)
        {
            IsSuccess = true;

            StringContent content = new StringContent(JsonConvert.SerializeObject(addProduct), Encoding.UTF8, "application/json");
            
            Logger.Info("POST " + addProduct.ToString());
         
            Task<HttpResponseMessage> GetResponse = client.PostAsync("", content);
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode(); // throws exception if status code is not success
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

                MessageBox.Show("Product added successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //GetAsync(""); // Necessary because the API seems to ignore requested Id and instead uses incremented largest Id, therefore the product list must be updated to ensure the DataGrid reflects the Id assigned by the API
                // Open Issue: should the API be behaving in this way? Update: Id is now auto-generated instead of user-specified (see AddProductForm)

                AddProductToDatastructures(addProduct);
            }
            else
            {
                MessageBox.Show("Could not add product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("POST " + addProduct.ToString() + " response status code is not successful");
                IsSuccess = false;
            }
        }

        // Delete product with specified Id (method) at rowIndex in DataGrid
        public async Task DeleteAsync(string method)
        {
            IsSuccess = true;

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

                DeleteProductFromDatastructures(method);
            }
            else
            {
                MessageBox.Show("Could not delete product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("DELETE " + method + " response status code is not successful");
                IsSuccess = false;
            }
        }

        // Update product based on addProduct, at specified rowIndex in DataGrid
        public async Task PutAsync(Product addProduct)
        {
            IsSuccess = true;

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

                UpdateProductInDatastructures(addProduct);
            }
            else
            {
                MessageBox.Show("Could not edit product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Warn("PUT " + addProduct.ToString() + " response status code is not successful");
                IsSuccess = false;
            }
        }

        // Delete a product from consumer dictionaries. Since the product list is bound to the APIConsumerForm's DataGrid, deleting the selected row from the APIConsumerForm class will remove the relevant Product from
        // the Consumer's ProductList.
        private void DeleteProductFromDatastructures(string id)
        {
            int.TryParse(id, out int RemovedId);

            ProductNameDict.Remove(ProductIdDict[RemovedId].Name);
            ProductIdDict.Remove(RemovedId);

            UpdateMaxId();
        }

        // Add a new product to the consumer dictionaries and product list
        private void AddProductToDatastructures(Product addProduct, int gridIndex = 0)
        {
            ProductList.Insert(gridIndex, addProduct);
            ProductNameDict.Add(addProduct.Name, addProduct);
            ProductIdDict.Add(addProduct.Id, addProduct);

            UpdateMaxId();
        }

        // Both dictionaries and the product list should reference the same Product instances, changing one instance's properties will change all
        private void UpdateProductInDatastructures(Product addProduct)
        {
            ProductIdDict[addProduct.Id].Name = addProduct.Name;
            ProductIdDict[addProduct.Id].Category = addProduct.Category;
            ProductIdDict[addProduct.Id].Price = addProduct.Price;
        }

        // Update the maximum known Id 
        private void UpdateMaxId()
        {
            try { MaxId = ProductIdDict.Keys.Max(); } catch { MaxId = 0; }
        }
    }
}
