using System;
using StoreManager.Views;
using StoreManager.Model;
using StoreManager.Model.Repositories;

namespace StoreManager.Presenters
{
    public sealed class EditCustomerPresenter : Presenter<EditCustomerView>
    {
        #region fields

        private readonly ApplicationPresenter m_AppPresenter;
        private readonly CustomersRepository m_Repository;
        private readonly Customer m_Customer;

        #endregion

        #region ctr/dtr

        public EditCustomerPresenter(ApplicationPresenter appPresenter, EditCustomerView view, CustomersRepository repository, Customer customer)
            : base(view, "Customer.ContactName")
        {
            m_AppPresenter = appPresenter;
            m_Repository = repository;
            m_Customer = customer;
        }

        #endregion

        #region properties

        public Customer Customer { get { return m_Customer; } }

        #endregion

        #region methods

        public void Save()
        {
            m_Repository.Save(Customer);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.StatusText = "خریدار با موفقیت ذخیره شد";
            }
        }

        public void Delete()
        {
            m_Repository.Delete(Customer);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.RemoveTab(this);
                m_AppPresenter.StatusText = "خریدار با موفقیت حذف شد";
            }
        }

        public void Close()
        {
            m_AppPresenter.RemoveTab(this);
        }

        #endregion
    }
}
