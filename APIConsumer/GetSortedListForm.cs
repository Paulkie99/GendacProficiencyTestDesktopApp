using System;
using System.Windows.Forms;

namespace APIConsumer
{
    // Form used to get sorted, ordered, filtered product list
    public partial class GetSortedListForm : Form
    {
        private APIConsumerForm parent;
        public GetSortedListForm(APIConsumerForm parent)
        {
            this.parent = parent;
            InitializeComponent();
            OrderByCB.SelectedIndex = 0;
        }

        private async void OKButton_ClickAsync(object sender, EventArgs e)
        {
            int page;
            if(!int.TryParse(PageTB.Text, out page))
            {
                MessageBox.Show("Page could not be parsed as int", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(page <= 0)
            {
                MessageBox.Show("Invalid Page", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int pageSize;
            if(!int.TryParse(PageSizeTB.Text, out pageSize))
            {
                MessageBox.Show("Page Size could not be parsed as int", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(pageSize <= 0)
            {
                MessageBox.Show("Invalid Page Size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string OrderBy = OrderByCB.SelectedItem.ToString();

            bool ascending = AscendingCheckBox.Checked;

            string Filter = FilterTB.Text;

            string methodString = "?page=" + page.ToString() + "&pageSize=" + pageSize.ToString() + "&orderBy=" + OrderBy
                                    + "&ascending=" + (ascending ? "true" : "false") + "&filter=" + Filter;


            Enabled = false; // avoid more than one request at a time
            parent.ProductGrid.Rows.Clear(); 
            await parent.consumer.GetAsync(methodString, true);
            parent.BindSources(); 
            parent.ProductGrid.Update();

            Enabled = true;
            this.Dispose();
        }
    }
}
