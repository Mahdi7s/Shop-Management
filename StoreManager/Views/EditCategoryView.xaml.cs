using System.Windows;
using System.Drawing;
using StoreManager.Presenters;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for EditCategoryView.xaml
    /// </summary>
    public partial class EditCategoryView : UserControl
    {
        public EditCategoryView()
        {
            InitializeComponent();
        }

        private EditCategoryPresenter Presenter
        { 
            get { return DataContext as EditCategoryPresenter; }
        }

        private void DockPanel_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnChangeImage":
                        Presenter.ChangePicture();
                        break;
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
