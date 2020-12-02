using System.ComponentModel;
using System.Windows.Forms;

namespace APIConsumer
{
    // Helper class to bind item count label to the number of rows in the DataGridView
    class DataGridRowCountBindingHelper : INotifyPropertyChanged
    {
        private DataGridView _view;

        public DataGridRowCountBindingHelper(DataGridView view)
        {
            this._view = view;
            view.Rows.CollectionChanged += new CollectionChangeEventHandler(Rows_CollectionChanged);
        }

        private void Rows_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            OnPropertyChanged("Count");
        }

        private void SelectedRowChanged(object sender, SelectedGridItemChangedEventArgs e)
        {

        }

        public int Count
        {
            get
            {
                return this._view.Rows.Count;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
