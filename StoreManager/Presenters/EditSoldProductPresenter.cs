using StoreManager.Views;
using StoreManager.Model;
using System.Collections.ObjectModel;
using StoreManager.Model.Repositories;

namespace StoreManager.Presenters
{
    public sealed class EditSoldProductPresenter : Presenter<EditSoldProductView>
    {
        #region fields

        private readonly ApplicationPresenter m_AppPresenter;
        private readonly SoldProductsRepository m_Repository;
        private readonly SoldProduct m_SoldProduct;

        private readonly ObservableCollection<Product> m_Products;
        private readonly ObservableCollection<Supplier> m_Suppliers;
        private readonly ObservableCollection<Customer> m_Customers;

        #endregion

        #region ctr/dtr

        public EditSoldProductPresenter(ApplicationPresenter appPresenter, EditSoldProductView view,
            SoldProductsRepository repository, SoldProduct soldProduct, ObservableCollection<Product> products,
            ObservableCollection<Supplier> suppliers, ObservableCollection<Customer> customers)
            : base(view, "TabHeader")
        {
            m_AppPresenter = appPresenter;
            m_Repository = repository;
            m_SoldProduct = soldProduct;
            m_Products = products;
            m_Suppliers = suppliers;
            m_Customers = customers;
        }

        #endregion

        #region properties

        public string TabHeader { get { return "ثبت و ویرایش خرید"; } }

        public SoldProduct SoldProduct { get { return m_SoldProduct; } }

        public ObservableCollection<Product> Products { get { return m_Products; } }
        public ObservableCollection<Supplier> Suppliers { get { return m_Suppliers; } }
        public ObservableCollection<Customer> Customers { get { return m_Customers; } }

        #endregion

        #region methods

        public void Save()
        {
            m_Repository.Save(SoldProduct);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.StatusText = "خرید با موفقیت ثبت شد";
            }
        }

        public void Delete()
        {
            m_Repository.Delete(SoldProduct);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.RemoveTab(this);
                m_AppPresenter.StatusText = "حذف با موفقیت انجام شد";
            }
        }

        public void Close()
        {
            m_AppPresenter.RemoveTab(this);
        }

        #endregion
    }
}
