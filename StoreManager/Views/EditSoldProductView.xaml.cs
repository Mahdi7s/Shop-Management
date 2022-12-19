using System.Windows.Controls;
using StoreManager.Presenters;
using System.Windows;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for EditSoldProductView.xaml
    /// </summary>
    public partial class EditSoldProductView : UserControl
    {
        public EditSoldProductView()
        {
            InitializeComponent();
        }

        private EditSoldProductPresenter Presenter { get { return DataContext as EditSoldProductPresenter; } }

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
