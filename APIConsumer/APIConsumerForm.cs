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
        private Dictionary<int, Product> ProductDict = new Dictionary<int, Product>();
        private List<Product> ProductList = new List<Product>();
        private HttpClient consumer = new HttpClient();
        private string endpoint = "http://gendacproficiencytest.azurewebsites.net/API/ProductsAPI/";

        private async Task UpdateProductListAsync()
        {
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
                JsonProducts = "";
            }

            if(JsonProducts != "")
            {
                ProductList = JsonConvert.DeserializeObject<List<Product>>(JsonProducts);
            }

            string b = "";
        }

        public APIConsumerForm()
        {
            InitializeComponent();
            InitializeConsumer(new Uri(endpoint));
            UpdateProductListAsync();
        }

        private void ProductListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EditButton_Click(object sender, EventArgs e)
        {

        }
    }
}
