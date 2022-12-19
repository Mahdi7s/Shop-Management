using System;
using System.Windows;
using StoreManager.Presenters;

namespace StoreManager.Views
{
    /// <summary>
    /// Interaction logic for EditProductView.xaml
    /// </summary>
    public partial class EditProductView : System.Windows.Controls.UserControl
    {
        public EditProductView()
        {
            InitializeComponent();
        }

        private EditProductPresenter Presenter { get { return DataContext as EditProductPresenter; } }

        private void btnClose_Click(object sender, RoutedEventArgs e) { Presenter.Close(); }

        private void btnDelete_Click(object sender, RoutedEventArgs e) { Presenter.Delete(); }

        private void btnSave_Click(object sender, RoutedEventArgs e) { Presenter.Save(); }
    }
}
