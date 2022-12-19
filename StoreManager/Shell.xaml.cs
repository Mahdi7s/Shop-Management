using System.Windows;
using StoreManager.Model;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;
using StoreManager.Presenters;
using System.Windows.Controls;

namespace StoreManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Shell : Window
    {
        #region ctr/dtr

        public Shell()
        {
            InitializeComponent();
            this.DataContext = new ApplicationPresenter(this);
        }

        #endregion

        #region methods & properties

        public void AddTab<T>(Presenter<T> presenter)
        {
            TabItem NewTab = this.RemoveTab(presenter);

            if (NewTab == null)
            {
                NewTab = new TabItem();
                NewTab.DataContext = presenter;
                NewTab.Content = presenter.View;
                var HeaderBinding = new Binding(presenter.TabHeaderPath);
                BindingOperations.SetBinding(NewTab, TabItem.HeaderProperty, HeaderBinding);
            }

            Tabs.Items.Insert(0, NewTab); NewTab.Focus();
        }

        public bool IsManagerEntered
        {
            set
            {
                var visibility = Visibility.Hidden;
                if (value) visibility = Visibility.Visible;
                //------------------------------------------
                MySimpleSearch.Visibility = visibility;
                MyToolBar.SetUserState(value);
                MyEditingTables.SetState(value);
            }
        }

        public void CloseTab<T>(Presenter<T> presenter)
        {
            this.RemoveTab(presenter);
        }

        private TabItem RemoveTab<T>(Presenter<T> presenter)
        {
            TabItem result = null;
            foreach (TabItem item in Tabs.Items)
            {
                if (item.DataContext.Equals(presenter))
                {
                    Tabs.Items.Remove(item);
                    result = item; break;
                }
            }
            return result;
        }

        private ApplicationPresenter Presenter
        {
            get { return (ApplicationPresenter)DataContext; }
        }

        #endregion
        //________________________________________________________________----------------_______________________________________________________
        private void SimpleSearch_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null && button.Name == "btnSearch")
            {
                var TableName = MySimpleSearch.TableForSearch.EnglishName;
                var Condition = MySimpleSearch.SearchText;
                switch (TableName)
                {
                    case "Products":
                        Presenter.SearchProduct(Condition);
                        break;
                }
            }
        }

        private void ToolBar_ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnBackup": 
                        Presenter.BackupDatabase();
                        break;
                    case "btnRestore":
                        Presenter.RestoreDatabase();
                        break;
                    case "btnPrint":
                        Presenter.Print(Tabs.SelectedContent as UserControl);
                        break;
                    case"btnExportBitmap":
                        Presenter.ExportBitmap(Tabs.SelectedContent as UserControl);
                        break;
                }
            }
        }

        private void ToolBar_ComboBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            var comboBox = e.OriginalSource as ComboBox;
            if (comboBox != null)
            {
                switch (comboBox.Name)
                {
                    case "cmbFonts":
                        Presenter.SetFont(comboBox.SelectedItem as FontFamily);
                        break;
                }
            }
        }

        private void EditingTables_ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnAddProduct":
                        Presenter.OpenProduct(new Product());
                        break;
                    case "btnAddSuppliers":
                        Presenter.OpenSupplier(new Supplier());
                        break;
                    case "btnAddCustomer":
                        Presenter.OpenCustomer(new Customer());
                        break;
                    case "btnAddSold":
                        Presenter.OpenSoldProduct(new SoldProduct());
                        break;
                    case"btnAddCategory":
                        Presenter.OpenCategory(new Category());
                        break;
                }
            }
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) { Presenter.Close(); }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e) { Presenter.Help(); }

        private void Window_Loaded(object sender, RoutedEventArgs e) {  }

    }
}
