using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using StoreManager.Presenters.Converters;

namespace StoreManager.UserControls
{
    /// <summary>
    /// Interaction logic for EditSupplierPassword.xaml
    /// </summary>
    public partial class EditSupplierPassword : UserControl
    {
        public EditSupplierPassword()
        {
            InitializeComponent();
        }

        public string PreviousPasswordValue
        {
            get
            {
                if (!string.IsNullOrEmpty(PreviousPassword.Password))
                {
                    return HashConverter.GetMD5Hash(PreviousPassword.Password);
                } return null;
            }
        }

        public string NewPasswordValue
        {
            get
            {
                if (NewPassword.Password == NewPassword2.Password)
                {
                    return HashConverter.GetMD5Hash(NewPassword.Password);
                } return null;
            }
        }

        private void Grid_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = e.OriginalSource as PasswordBox;
            if (passwordBox != null)
            {
                var BoxName = passwordBox.Name;
                if (BoxName == "NewPassword" || BoxName == "NewPassword2")
                {
                    var converter = new ImageSourceConverter();
                    if(NewPassword.Password == NewPassword2.Password)
                    {
                        MyImage.Source = (ImageSource)converter.ConvertFromString(@"Images\check.ico");
                    }
                    else
                    {
                        MyImage.Source = (ImageSource)converter.ConvertFromString(@"Images\uncheck.ico");
                    }
                }
            }
        }
    }
}
