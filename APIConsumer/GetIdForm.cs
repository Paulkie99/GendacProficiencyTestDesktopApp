using System;
using System.Windows.Forms;

namespace APIConsumer
{
    // Form used to get product by Id
    public partial class GetIdForm : Form
    {
        private APIConsumerForm parent;
        public GetIdForm(APIConsumerForm parent)
        {
            InitializeComponent();
            IdTextBox.KeyPress += CheckEnterKeyPress;
            this.parent = parent;
        }

        private async void OKButton_ClickAsync(object sender, EventArgs e)
        {
            int id;
            if(int.TryParse(this.IdTextBox.Text, out id))
            {
                if (id <= 0) // Open Issue: should Id of 0 be allowed?
                {
                    MessageBox.Show("Invalid Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                parent.ProductGrid.Rows.Clear();
                await parent.consumer.GetAsync(id.ToString());
                parent.BindSources(); //Bind products to grid
                parent.ProductGrid.Update();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Invalid Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                OKButton_ClickAsync(sender, e);
                e.Handled = true;
            }
        }
    }
}
