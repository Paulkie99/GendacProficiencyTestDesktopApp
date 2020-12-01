using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace APIConsumer
{
    public partial class GetIdForm : Form
    {
        private APIConsumerForm parent;
        public GetIdForm(APIConsumerForm parent)
        {
            InitializeComponent();
            IdTextBox.KeyPress += CheckEnterKeyPress;
            this.parent = parent;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            int id;
            if(int.TryParse(this.IdTextBox.Text, out id))
            {
                parent.consumer.GetProductListAsync(id.ToString());
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
                OKButton_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
