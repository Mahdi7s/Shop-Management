using StoreManager.Views;
using StoreManager.Model;
using System.Collections.ObjectModel;

namespace StoreManager.Presenters
{
    public sealed class ProductsListPresenter : Presenter<ProductsListView>
    {
        #region fields

        private readonly ApplicationPresenter m_AppPresenter;
        private readonly ObservableCollection<Product> m_Products;

        #endregion

        #region ctr/dtr

        public ProductsListPresenter(ApplicationPresenter appPresenter, ProductsListView view, ObservableCollection<Product> products)
            : base(view, "TabHeader")
        {
            m_AppPresenter = appPresenter;
            m_Products = products;
        }

        #endregion

        #region properties

        public string TabHeader { get { return "نتیجه جستجوی کالاها"; } }

        public ObservableCollection<Product> Products { get { return m_Products; } }

        #endregion

        #region methods

        public void Close() { m_AppPresenter.RemoveTab(this); }

        public void OpenProduct(Product model)
        {
            m_AppPresenter.OpenProduct(model);
        }

        #endregion
    }
}
