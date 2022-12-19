using System;
using System.Windows;
using StoreManager.Views;
using StoreManager.Model;
using StoreManager.Model.Repositories;

namespace StoreManager.Presenters
{
    public sealed class EditSupplierPresenter : Presenter<EditSupplierView>
    {
        #region fields

        private readonly ApplicationPresenter m_AppPresenter;
        private readonly SuppliersRepository m_Repository;
        private readonly Supplier m_Supplier;
        private Visibility m_PPassVisibility;

        #endregion

        #region ctr/dtr

        public EditSupplierPresenter(ApplicationPresenter appPresenter, EditSupplierView view, SuppliersRepository repository, Supplier supplier)
            : base(view,"Supplier.ContactName")
        {
            m_AppPresenter = appPresenter;
            m_Repository = repository;
            m_Supplier = supplier;

            if(!string.IsNullOrEmpty(m_Supplier.Password))
            {
                m_PPassVisibility = Visibility.Visible;
            }
            else
            {
                m_PPassVisibility = Visibility.Hidden;
            }
        }

        #endregion

        #region properties

        public Supplier Supplier { get { return m_Supplier; } }

        public Visibility PreviousPasswordVisibility
        {
            get { return m_PPassVisibility; }
        }

        #endregion

        #region methods

        public void Save(string PreviousPassword, string NewPassword)
        {
            if (this.SetPassword(PreviousPassword, NewPassword))
            {
                m_Repository.Save(Supplier);
                if (m_Repository.ExecutedGood)
                {
                    m_AppPresenter.StatusText = "فروشنده با موفقیت ذخیره شد";
                }
            }
        }

        public void Delete()
        {
            m_Repository.Delete(Supplier);
            if (m_Repository.ExecutedGood)
            {
                m_AppPresenter.RemoveTab(this);
                m_AppPresenter.StatusText = "فروشنده با موفقیت حذف شد";
            }
        }

        public void Close()
        {
            m_AppPresenter.RemoveTab(this);
        }

        //-------------------------------------------------------------------------------------

        private bool SetPassword(string PreviousPassword, string NewPassword)
        {
            try
            {
                if ((m_PPassVisibility == Visibility.Visible && string.IsNullOrEmpty(PreviousPassword))
                    || (m_PPassVisibility == Visibility.Visible && PreviousPassword != Supplier.Password)
                    || (m_PPassVisibility == Visibility.Hidden && string.IsNullOrEmpty(NewPassword)))
                {
                    throw new Exception();
                }
                Supplier.Password = NewPassword;
            }
            catch { ShowError("رمز عبور نادرست می باشد"); return false; }
            return true;
        }

        #endregion
    }
}
