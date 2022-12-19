using System;
using System.Windows;
using StoreManager.Presenters;

namespace StoreManager.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
            DataContext = new AboutDlgPresenter(this);
        }

        private AboutDlgPresenter Presenter { get { return DataContext as AboutDlgPresenter; } }

        private void Button_Click(object sender, RoutedEventArgs e) { Presenter.OK(); }

        private void MailToMe_Click(object sender, RoutedEventArgs e) { Presenter.MailToMe(); }
    }
}
