using StoreManager.Presenters;
using System.Windows.Controls;
using System.Windows;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for EditCustomerView.xaml
    /// </summary>
    public partial class EditCustomerView : UserControl
    {
        public EditCustomerView()
        {
            InitializeComponent();
        }

        private EditCustomerPresenter Presenter { get { return DataContext as EditCustomerPresenter; } }

        private void DockPanel_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnSave":
                        Presenter.Save();
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
