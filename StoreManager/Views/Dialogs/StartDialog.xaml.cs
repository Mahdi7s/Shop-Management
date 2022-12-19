using System;
using System.Windows;
using StoreManager.Model;
using System.Windows.Controls;
using StoreManager.Presenters;
using StoreManager.Model.Repositories;

namespace StoreManager.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for StartDialog.xaml
    /// </summary>
    public partial class StartDialog : Window
    {
        public StartDialog(SuppliersRepository repository)
        {
            InitializeComponent();
            DataContext = new StartDlgPresenter(this, repository);
        }

        private StartDlgPresenter Presenter { get { return DataContext as StartDlgPresenter; } }

        public Supplier EnteredSupplier { get { return Presenter.EnteredSupplier; } }

        private void DockPanel_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnEditCnStr": Presenter.EditCnStr(); break;
                    case "btnOK":
                        if ((!string.IsNullOrEmpty(txtID.Text)) && (!string.IsNullOrEmpty(txtPassword.Password)))
                        {
                            int Id;
                            if(!int.TryParse(txtID.Text,out Id)) Id = 0;
                            Presenter.Enter(Id, txtPassword.Password);
                        }
                        break;
                    case "btnCancel": Presenter.Close(); break;
                }
            }
        }
    }
}
