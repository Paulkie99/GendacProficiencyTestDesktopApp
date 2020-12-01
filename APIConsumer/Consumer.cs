using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIConsumer
{
    public class Consumer
    {
        private HttpClient client = new HttpClient();
        private string endpoint = "http://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/";
        private APIConsumerForm Form;

        public List<Product> ProductList = new List<Product>();
        public Dictionary<int, Product> ProductIdDict = new Dictionary<int, Product>();
        public Dictionary<string, Product> ProductNameDict = new Dictionary<string, Product>();
        public InputValidator validator;

        public Consumer(APIConsumerForm form)
        {
            validator = new InputValidator(this);
            this.Form = form;
            client.BaseAddress = new Uri(endpoint);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Refresh the list of products by performing a new GET request
        public async Task GetProductListAsync(string method)
        {
            DisableUI();

            Form.ProductGrid.Rows.Clear(); //also clears productlist once bound
            ProductNameDict.Clear();
            ProductIdDict.Clear();

            Task<HttpResponseMessage> GetResponse = client.GetAsync(method);
            string JsonProducts = "";
            HttpResponseMessage GetResponseResult = new HttpResponseMessage();
            try
            {
                GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
                JsonProducts = await GetResponseResult.Content.ReadAsStringAsync();
            }
            catch
            {
                //response error 

                if (GetResponseResult.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Id not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Unknown Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                JsonProducts = ""; //ensure string is empty to reflect the error
            }

            if (JsonProducts != "")
            {
                if (method.Length == 0) // get list of all products
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
                MessageBox.Show("Product Added Successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddProductToDatastructures(addProduct);
                GetProductListAsync("");
            }

        }

        public async Task DeleteAsync(string method, int rowIndex)
        {
            DisableUI();

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

            EnableUI();
        }

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
