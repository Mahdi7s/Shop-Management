using System.Windows.Controls;
using System.Windows;

namespace StoreManager.UserControls
{
    /// <summary>
    /// Interaction logic for EditTables.xaml
    /// </summary>
    public partial class EditingTables : UserControl
    {
        public EditingTables()
        {
            InitializeComponent();
        }

        public void SetState(bool ManagerEntered)
        {
            if (!ManagerEntered)
            {
                grpCategories.Visibility = grpCustomers.Visibility = Visibility.Hidden;
                grpProducts.Visibility = grpSuppliers.Visibility = Visibility.Hidden;
            }
        }
    }
}
