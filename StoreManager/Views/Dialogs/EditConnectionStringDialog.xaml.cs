using System;
using System.Windows;
using StoreManager.Presenters;

namespace StoreManager.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for EditConnectionStringDialog.xaml
    /// </summary>
    public partial class EditConnectionStringDialog : Window
    {
        public EditConnectionStringDialog()
        {
            InitializeComponent();
            DataContext = new EditConnectionStringDlgPresenter(this);
        }

        private EditConnectionStringDlgPresenter Presenter { get { return DataContext as EditConnectionStringDlgPresenter; } }

        private void Button_Click(object sender, RoutedEventArgs e) { Presenter.OK(); }
    }
}
