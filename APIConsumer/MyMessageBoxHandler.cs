using System.Windows.Forms;

namespace APIConsumer
{
    public class MyMessageBoxHandler
    {
        public bool ShowMessages = true;

        public void NewErrorBox(string message) 
        {
            if (!ShowMessages)
                return;

            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void NewInfoBox(string message)
        {
            if (!ShowMessages)
                return;

            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
