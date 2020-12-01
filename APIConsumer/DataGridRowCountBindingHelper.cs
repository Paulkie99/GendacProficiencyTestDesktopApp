using System.ComponentModel;
using System.Windows.Forms;

namespace APIConsumer
{
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
