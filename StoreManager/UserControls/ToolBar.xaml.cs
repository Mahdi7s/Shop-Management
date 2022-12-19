
namespace StoreManager.UserControls
{
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    public partial class ToolBar : System.Windows.Controls.UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        public void SetUserState(bool IsManagerEntered)
        {
            if (!IsManagerEntered)
            {
                BackupToolBar.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
