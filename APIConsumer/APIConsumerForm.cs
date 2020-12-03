using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace APIConsumer
{
    // Main form containing all functional buttons  
    public partial class APIConsumerForm : Form
    {
        public Consumer consumer; // Reference required to call relevant communication functions

        public APIConsumerForm()
        {
            consumer = new Consumer();
            InitializeComponent();
        }

        private async void Init(object sender, EventArgs e)
        {
            await GetEntireProductList();
        }

        // Edit product of selected row
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ProductGrid.SelectedRows.Count == 1)
            {
                EditForm editForm = new EditForm(this);
                editForm.ShowDialog();
            }
            else if(ProductGrid.SelectedRows.Count > 1)
            {
                MessageBox.Show("Only one product may be edited at a time", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // None selected
            {
                MessageBox.Show("Please select a product to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete product of selected row
        private async void DeleteButton_ClickAsync(object sender, EventArgs e)
        {
            if(this.ProductGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (DataGridViewRow row in this.ProductGrid.SelectedRows) // for loop here will allow multiple rows to be deleted if multiple selections are allowed
            {
                if (row.Index >= 0)
                {
                    await consumer.DeleteAsync(((int) row.Cells[0].Value).ToString());
                    if (consumer.IsSuccess)
                        RemoveRowFromGrid(row.Index);
                }
            }
        }

        // Get entire product list
        private async void GetButton_ClickAsync(object sender, EventArgs e)
        {
            await GetEntireProductList();
        }

        // Procedure followed to get entire product list
        private async Task GetEntireProductList()
        {
            DisableUI(); // Disable UI to avoid spamming Get requests (and to force the user to wait for a product list before attempting other operations)
            CountLabel.DataBindings.Clear();
            CountLabel.Text = "Retrieving...";
            ProductGrid.Rows.Clear(); //also clears ProductList once bound
            await consumer.GetAsync("");
            EnableUI();
            BindSources(); //Bind products to grid
            ProductGrid.Update();
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

        protected override void OnFormClosing(System.Windows.Forms.FormClosingEventArgs e) // Ensure logger is flushed when the app is closed
        {
            base.OnFormClosing(e);
            NLog.LogManager.Shutdown();
        }

        private void DisableUI()
        {
            Enabled = false;
        }

        private void EnableUI()
        {
            Enabled = true;
        }

        public void BindSources()
        {
            //Bind grid to product list
            ProductGrid.AutoGenerateColumns = true;
            var bindingList = new BindingList<Product>(consumer.ProductList);
            var source = new BindingSource(bindingList, null);
            ProductGrid.DataSource = source;

            //Bind count label to grid row count
            Binding countBinding = new Binding("Text", new DataGridRowCountBindingHelper(ProductGrid), "Count", true);
            countBinding.Format += (sender, e) => e.Value = string.Format("{0} items", e.Value);
            CountLabel.DataBindings.Clear(); //avoid adding the same binding twice if the label text has been bound before
            CountLabel.DataBindings.Add(countBinding);
        }

        public void UpdateRow(int gridIndex)
        {
            BindGridToConsumerList(); // binding the source again seems to improve response time
            ProductGrid.Rows[gridIndex].Selected = true;
            ProductGrid.FirstDisplayedScrollingRowIndex = gridIndex;
        }

        private void RemoveRowFromGrid(int gridIndex)
        {
            ProductGrid.Rows.RemoveAt(gridIndex); // this also removes the product from the consumer's product list
            ProductGrid.Update();
        }

        public void BindGridToConsumerList()
        {
            ProductGrid.DataSource = new BindingSource(new BindingList<Product>(consumer.ProductList), null);
        }
    }
}
