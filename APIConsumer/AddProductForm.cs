using System;
using System.Linq;
using System.Windows.Forms;

namespace APIConsumer
{
    // Form used to add a product
    public partial class AddProductForm : Form
    {
        protected APIConsumerForm parent;

        public AddProductForm(APIConsumerForm parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.IdTB.Enabled = false;
            this.IdTB.Text = (parent.consumer.MaxId + 1).ToString(); // Increment max Id by one to obtain new product Id
            this.CatCB.SelectedIndex = 0; // Default is CategoryA
        }

        protected virtual async void OKButton_ClickAsync(object sender, EventArgs e)
        {
            // Open Issue: should the user be allowed to specify Id?
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


            int category = CatCB.SelectedIndex + 1; // Combobox indexing starts at zero, but ProductCategory enum starts at 1, therefore increasea the selectedindex by one
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

            Enabled = false; // avoid more than one request at a time
            await parent.consumer.PostAsync(addProduct);

            if(parent.consumer.IsSuccess)
                parent.BindGridToConsumerList();

            Enabled = true;
            this.Dispose();
        }
    }
}
