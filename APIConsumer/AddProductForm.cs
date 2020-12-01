using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace APIConsumer
{
    public partial class AddProductForm : Form
    {
        private APIConsumerForm parent;

        public AddProductForm(APIConsumerForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(IdTB.Text, out id))
            {
                MessageBox.Show("Cannot parse id as integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!parent.consumer.validator.IsValidId(id))
            {
                MessageBox.Show("Id already exists or is less than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = NameTB.Text;
            if (!parent.consumer.validator.IsValidName(name))
            {
                MessageBox.Show("Name is empty or already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int category = CatCB.SelectedIndex + 1;
            //if(!Enum.TryParse<ProductCategory>(CatCB.SelectedValue.ToString(), out category))
            if (!parent.consumer.validator.IsValidCategory(category))
            {
                MessageBox.Show("Invalid Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            float price;
            if (!float.TryParse(PriceTB.Text, out price))
            {
                MessageBox.Show("Cannot parse price as float", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!parent.consumer.validator.IsValidPrice(price))
            {
                MessageBox.Show("Price cannot be less than or equal to zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Product addProduct = new Product(id, name, (ProductCategory)category, price);
            parent.consumer.PostAsync(addProduct);
            this.Dispose();
        }
    }
}
