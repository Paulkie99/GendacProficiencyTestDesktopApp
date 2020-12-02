using System;
using System.Windows.Forms;

namespace APIConsumer
{
    // Form used to edit a product, inherits from AddProductForm since the controls are identical.
    // Editform overrides OKButton_Click function to ensure product is edited ("PUT" instead of "POST")
    public class EditForm : AddProductForm
    {
        public EditForm(APIConsumerForm parent) : base(parent)
        {
            this.Text = "Edit Form"; // Change form title from "Add product form"

            // Pre-populate fields with selected product properties
            this.IdTB.Text = parent.ProductGrid.SelectedRows[0].Cells["Id"].Value.ToString();
            this.IdTB.Enabled = false; // Open issue: user should not be able to edit Id?
            this.NameTB.Text = parent.ProductGrid.SelectedRows[0].Cells["Name"].Value.ToString();
            this.CatCB.SelectedIndex = CatCB.FindStringExact(parent.ProductGrid.SelectedRows[0].Cells["Category"].Value.ToString());
            this.PriceTB.Text = parent.ProductGrid.SelectedRows[0].Cells["Price"].Value.ToString();
        }
        protected override void OKButton_Click(object sender, EventArgs e)
        {
            int id;
            if (!int.TryParse(IdTB.Text, out id))
            {
                MessageBox.Show("Cannot parse id as integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!parent.consumer.validator.IsIdExists(id))
            {
                MessageBox.Show("Id does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string name = NameTB.Text;
            if (!parent.consumer.validator.IsValidName(name)) // Open issue: should name be allowed to be non-unique when editing?
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
            parent.consumer.PutAsync(addProduct, this.parent.ProductGrid.SelectedRows[0].Index);
            this.Dispose();
        }
    }
}
