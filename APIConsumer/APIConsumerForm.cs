using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace APIConsumer
{
    public partial class APIConsumerForm : Form
    {
        private List<Product> ProductList = new List<Product>();
        private HttpClient consumer = new HttpClient();
        private string endpoint = "http://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/";
        private int NumProducts = 0;

        public APIConsumerForm()
        {
            InitializeComponent();
            InitializeConsumer(new Uri(endpoint));
            GetProductListAsync();
        }

        private void InitializeConsumer(Uri uri)
        {
            this.consumer.BaseAddress = uri;
            this.consumer.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Refresh the list of products by performing a new GET request
        private async Task GetProductListAsync()
        {
            DisableUI();

            ProductList.Clear();

            Task<HttpResponseMessage> GetResponse = consumer.GetAsync("");
            string JsonProducts = "";
            try
            {
                HttpResponseMessage GetResponseResult = await GetResponse;
                GetResponseResult.EnsureSuccessStatusCode();
                JsonProducts = await GetResponseResult.Content.ReadAsStringAsync();
            }
            catch
            {
                //response error 
                //TODO: add notification
                JsonProducts = "";
            }

            if (JsonProducts != "")
            {
                ProductList = JsonConvert.DeserializeObject<List<Product>>(JsonProducts);
                if (ProductList.Any())
                {
                    NumProducts = ProductList.Count;
                    foreach (Product product in ProductList)
                    {
                        if(product.Category < ProductCategory.CategoryA || product.Category > ProductCategory.CategoryC)
                        {
                            product.Category = ProductCategory.Invalid;
                        }
                    }
                    //Done adding all products to list
                    this.ProductGrid.AutoGenerateColumns = true;
                    this.ProductGrid.DataSource = ProductList;
                    this.CountLabel.Text = NumProducts.ToString() + " items";
                }
                else
                {
                    //product list is empty for some reason
                }
            }
            else
            {
                //could not parse response
                //TODO: add notification
            }

            EnableUI();
        }

        private void DisableUI()
        {
            this.Enabled = false;
        }

        private void EnableUI()
        {
            this.Enabled = true;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void GetButton_Click(object sender, EventArgs e)
        {

        }

        private void GetSorted_Click(object sender, EventArgs e)
        {

        }

        private void ProductGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {

        }

        private void GetIdButton_Click(object sender, EventArgs e)
        {

        }
    }
}
