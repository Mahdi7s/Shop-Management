using System;
using StoreManager.Views.Dialogs;

namespace StoreManager.Presenters
{
    public sealed class EditConnectionStringDlgPresenter : Presenter<EditConnectionStringDialog>
    {
        #region ctr/dtr

        public EditConnectionStringDlgPresenter(EditConnectionStringDialog view) : base(view) { }

        #endregion

        #region methods & properties

        public string ConnectionString 
        {
            get { return DbSettings.Default.ConnectionString; }
            set { DbSettings.Default.ConnectionString = value; }
        }

        public void OK()
        {
            DbSettings.Default.Save();
            View.DialogResult = true;
        }

        #endregion
    }
}
