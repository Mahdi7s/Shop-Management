using System;
using System.Data;
using StoreManager.Model;
using System.Data.SqlClient;
using StoreManager.Views.Dialogs;
using StoreManager.Model.Repositories;
using StoreManager.Presenters.Converters;

namespace StoreManager.Presenters
{
    public sealed class StartDlgPresenter : Presenter<StartDialog>
    {
        #region Fields

        private int m_CurrentID = 0;
        private int m_FalsePasswordEntered = 0;
        private string m_CurrentPassword = string.Empty;
        private readonly SuppliersRepository m_Repository;

        #endregion

        #region ctr/dtr

        public StartDlgPresenter(StartDialog view, SuppliersRepository Repository) : base(view)
        {
            m_Repository = Repository;
        }

        #endregion

        #region properties

        public Supplier EnteredSupplier { get; private set; }

        #endregion

        #region public methods

        public void EditCnStr()
        {
            var EditCnStrDlg = new EditConnectionStringDialog();
            EditCnStrDlg.ShowDialog();
        }

        public void Enter(int SupplierID, string Password)
        {
            var CorrectPass = this.GetPassword(SupplierID);
            if ((!string.IsNullOrEmpty(CorrectPass)) && (CorrectPass.Equals(HashConverter.GetMD5Hash(Password))))
            {
                EnteredSupplier = this.GetSupplier(SupplierID);
                View.DialogResult = true;
            }
            else
            {
                ++m_FalsePasswordEntered;
            }
            if (m_FalsePasswordEntered == 3) View.DialogResult = false;
        }

        public void Close() { View.DialogResult = false; }

        #endregion

        #region private methods

        private Supplier GetSupplier(int SupplierID)
        {
            var param = new SqlParameter("@SupplierID",SupplierID);
            param.SqlDbType = SqlDbType.Int;
            return m_Repository.Select("SupplierID = @SupplierID", param)[0];
        }

        private string GetPassword(int SupplierID)
        {
            if (m_CurrentID != SupplierID)
            {
                m_CurrentPassword = m_Repository.GetPassword(SupplierID);
                m_CurrentID = SupplierID;
            }
            return m_CurrentPassword;
        }

        #endregion
    }
}
