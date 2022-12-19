using StoreManager.Presenters;
namespace StoreManager.UserControls
{
    /// <summary>
    /// Interaction logic for SimpleSearch.xaml
    /// </summary>
    public partial class SimpleSearch : System.Windows.Controls.UserControl
    {
        public SimpleSearch()
        {
            InitializeComponent();
        }

        public string SearchText { get { return txtSearch.Text; } }
        public TableName TableForSearch { get { return (TableName)cmbTablesForSearch.SelectedItem; } }
    }
}
