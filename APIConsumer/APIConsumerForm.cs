using System;
using System.Windows.Forms;


namespace APIConsumer
{
    // Main form containing all functional buttons  
    public partial class APIConsumerForm : Form
    {
        public Consumer consumer; // Reference required to call relevant communication functions

        public APIConsumerForm()
        {
            consumer = new Consumer(this);
            InitializeComponent();
            consumer.GetAsync(""); // get list of all products on startup
        }

        // Edit product of selected row
        private void EditButton_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm(this);
            editForm.ShowDialog();
        }

        // Delete product of selected row
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

        // Get entire product list
        private void GetButton_Click(object sender, EventArgs e)
        {
            consumer.GetAsync("");
        }

        // Get sorted, filtered, ordered list
        private void GetSorted_Click(object sender, EventArgs e)
        {
            GetSortedListForm sortedForm = new GetSortedListForm(this);
            sortedForm.ShowDialog();
        }

        // Add product
        private void AddButton_Click(object sender, EventArgs e)
        {
            AddProductForm addForm = new AddProductForm(this);
            addForm.ShowDialog();
        }

        // Get product by Id
        private void GetIdButton_Click(object sender, EventArgs e)
        {
            GetIdForm getIdForm = new GetIdForm(this);
            getIdForm.ShowDialog();
        }
    }
}
