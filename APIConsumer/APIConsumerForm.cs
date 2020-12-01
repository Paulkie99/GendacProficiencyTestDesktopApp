using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace APIConsumer
{
    public partial class APIConsumerForm : Form
    {
        public Consumer consumer;

        public APIConsumerForm()
        {
            consumer = new Consumer(this);
            InitializeComponent();
            consumer.GetProductListAsync(""); // get list of all products on startup
        }

        private void EditButton_Click(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ProductGrid.SelectedRows)
            {
                if (row.Index >= 0)
                {
                    consumer.DeleteAsync(((int) row.Cells[0].Value).ToString(), row.Index);
                }
            }
        }

        private void GetButton_Click(object sender, EventArgs e)
        {
            consumer.GetProductListAsync("");
        }

        private void GetSorted_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddProductForm addForm = new AddProductForm(this);
            addForm.ShowDialog();
        }

        private void GetIdButton_Click(object sender, EventArgs e)
        {
            GetIdForm getIdForm = new GetIdForm(this);
            getIdForm.ShowDialog();
        }
    }
}
