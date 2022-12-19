using System.Windows.Controls;
using StoreManager.Presenters;
using StoreManager.Model;
using System.Windows;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for ProductsSearchResult.xaml
    /// </summary>
    public partial class ProductsListView : UserControl
    {
        public ProductsListView()
        {
            InitializeComponent();
        }

        private ProductsListPresenter Presenter { get { return DataContext as ProductsListPresenter; } }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var product = ((Button)sender).DataContext as Product;
            if (product != null)
            {
                Presenter.OpenProduct(product);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Close();
        }
    }
}
