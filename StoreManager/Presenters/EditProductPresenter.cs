using StoreManager.Views;
using StoreManager.Model;
using System.Collections.ObjectModel;
using StoreManager.Model.Repositories;

namespace StoreManager.Presenters
{
    public sealed class EditProductPresenter : Presenter<EditProductView>
    {
        #region fields

        private readonly Product m_Product;
        private readonly ProductsRepository m_Repository;
        private readonly ApplicationPresenter m_AppPresenter;

        private readonly ObservableCollection<Category> m_Categories;
       
        #endregion

        #region ctr/dtr

        public EditProductPresenter(ApplicationPresenter AppPresenter, EditProductView view,
            ProductsRepository repository, Product product,ObservableCollection<Category> categories)
            : base(view, "Product.ProductName")
        {
            m_AppPresenter = AppPresenter;
            m_Repository = repository;
            m_Product = product;
            m_Categories = categories;
        }

        #endregion

        #region properties

        public Product Product { get { return m_Product; } }

        public ObservableCollection<Category> Categories
        {
            get { return m_Categories; }
        }

        #endregion

        #region methods

        public void Close()
        {
            m_AppPresenter.RemoveTab(this);
        }

        public void Save()
        {
            m_Repository.Save(Product);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.StatusText = "کالای مورد نظر ذخیره شد";
            }
        }

        public void Delete()
        {
            m_Repository.Delete(Product);
            if (m_Repository.ExecutedGood)
            {
                this.Close();
                m_AppPresenter.StatusText = "کالای مورد نظر حذف شد";
            }
        }

        #endregion
    }
}
