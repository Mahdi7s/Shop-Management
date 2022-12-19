using System.Windows.Controls;
using StoreManager.Presenters;
using System.Windows;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for EditSuppliersView.xaml
    /// </summary>
    public partial class EditSupplierView : UserControl
    {
        public EditSupplierView()
        {
            InitializeComponent();
        }

        private EditSupplierPresenter Presenter { get { return DataContext as EditSupplierPresenter; } }

        private void DockPanel_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnSave":
                        Presenter.Save(PasswordGeter.PreviousPasswordValue, PasswordGeter.NewPasswordValue);
                        break;
                    case "btnDelete":
                        Presenter.Delete();
                        break;
                    case "btnClose":
                        Presenter.Close();
                        break;
                }
            }
        }
    }
}
