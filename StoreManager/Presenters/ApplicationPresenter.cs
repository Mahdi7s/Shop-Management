using System;
using System.IO;
using System.Xml;
using System.Windows;
using Microsoft.Win32;
using System.Threading;
using StoreManager.Model;
using StoreManager.Views;
using MVW.Data.SqlClient;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Windows.Markup;
using StoreManager.Presenters;
using System.Windows.Controls;
using StoreManager.Views.Dialogs;
using System.Windows.Media.Imaging;
using StoreManager.Model.Repositories;

namespace StoreManager.Presenters
{
    public sealed class ApplicationPresenter : Presenter<Shell>
    {
        #region fields

        private readonly Database m_DatabaseManager = new Database();

        private readonly ProductsRepository m_ProductsRepository = new ProductsRepository();
        private readonly SuppliersRepository m_SuppliersRepository = new SuppliersRepository();
        private readonly CustomersRepository m_CustomersRepository = new CustomersRepository();
        private readonly CategoriesRepository m_CategoriesRepository = new CategoriesRepository();
        private readonly SoldProductsRepository m_SoldProductsRepository = new SoldProductsRepository();

        private string m_StatusText = string.Empty;
        private bool m_CanStart = true;

        #endregion

        #region ctr/dtr

        public ApplicationPresenter(Shell view) : base(view)
        {
            this.AttachDatabase();
            this.SetLanguage();
            this.Start();
            if (!m_CanStart) this.Close();
        }

        #endregion

        #region properties

        public bool CanStart { get { return m_CanStart; } }

        public string StatusText
        {
            get { return m_StatusText; }
            set { m_StatusText = value; OnPropertyChanged("StatusText"); }
        }

        #endregion

        #region public methods

        public void AddTab<T>(Presenter<T> presenter) { View.AddTab(presenter); }

        public void RemoveTab<T>(Presenter<T> presenter) { View.CloseTab(presenter); }

        public void BackupDatabase()
        {
            var SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "Backup Files (*.bak)|*.bak";
            SaveDlg.Title = "گرفتن پشتیبانی";
            SaveDlg.RestoreDirectory = true;
            if (SaveDlg.ShowDialog() == true)
            {
                var DestPath = SaveDlg.FileName;
                if (!string.IsNullOrEmpty(DestPath))
                {
                    m_DatabaseManager.CreateBackUp(DestPath);
                    StatusText = "گرفتن پشتیبانی با موفقیت انجام شد";
                }
            }
        }

        public void RestoreDatabase()
        {
            var OpenDlg = new OpenFileDialog();
            OpenDlg.Filter = "Backup Files (*.bak)|*.bak";
            OpenDlg.Title = "انتخاب فایل پشتیبانی";
            OpenDlg.RestoreDirectory = true;
            if (OpenDlg.ShowDialog() == true)
            {
                var SrcPath = OpenDlg.FileName;
                if (!string.IsNullOrEmpty(SrcPath))
                {
                    m_DatabaseManager.Restore(SrcPath);
                    StatusText = "بازیابی اطلاعات با موفقیت انجام شد";
                }
            }
        }

        public void SetFont(FontFamily font)
        {
            if (font != null) View.FontFamily = font;
        }

        public void OpenProduct(Product model)
        {
            this.AddTab(new EditProductPresenter(this, new EditProductView(), m_ProductsRepository, model, m_CategoriesRepository.GetAllInfo()));
        }

        public void OpenCategory(Category model)
        {
            this.AddTab(new EditCategoryPresenter(this, new EditCategoryView(), m_CategoriesRepository, model));
        }

        public void OpenSoldProduct(SoldProduct model)
        {
            this.AddTab(new EditSoldProductPresenter(this, new EditSoldProductView(), m_SoldProductsRepository, model,
                m_ProductsRepository.GetAllInfo(), m_SuppliersRepository.GetAllInfo(), m_CustomersRepository.GetAllInfo()));
        }

        public void OpenSupplier(Supplier model)
        {
            this.AddTab(new EditSupplierPresenter(this, new EditSupplierView(), m_SuppliersRepository, model));
        }

        public void OpenCustomer(Customer model)
        {
            this.AddTab(new EditCustomerPresenter(this, new EditCustomerView(), m_CustomersRepository, model));
        }

        public void SearchProduct(string Condition)
        {
            if (!string.IsNullOrEmpty(Condition))
            {
                this.AddTab(new ProductsListPresenter(this, new ProductsListView(), m_ProductsRepository.Select(Condition, null)));
            }
        } 

        public void Print(UserControl view)
        {
            if (view != null)
            {
                var PrintVisual = view.FindName("PrintVisual") as Control;

                var PrintDlg = new PrintDialog();
                PrintDlg.PrintVisual(PrintVisual, "چاپ");
            }
        }

        public void ExportBitmap(UserControl view)
        {
            var BitmapDest = this.GetBitmapDest();
            if ((!string.IsNullOrEmpty(BitmapDest)) && (view != null))
            {
                var visual = view.FindName("PrintVisual") as Control;

                RenderTargetBitmap bmp = new RenderTargetBitmap(400, 600, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(visual);

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (var stream = File.Create(BitmapDest))
                {
                    encoder.Save(stream);
                }
            }
        }

        public void Help()
        {
            var AboutDlg = new AboutDialog();
            AboutDlg.ShowDialog();
        }

        public void Close()
        {
            Environment.Exit(Environment.ExitCode);
        }

        #endregion

        #region private methods

        private void Start()
        {
            var StartDlg = new StartDialog(m_SuppliersRepository);
            if (StartDlg.ShowDialog() == true)
            {
                if (StartDlg.EnteredSupplier != null)
                {
                    this.SetUserState(StartDlg.EnteredSupplier.State);
                    return;
                }
            }
            m_CanStart = false;
        }

        private void AttachDatabase()
        {
            try
            {
                var SCSB = new SqlConnectionStringBuilder(DbSettings.Default.ConnectionString);
                m_DatabaseManager.ServerName = SCSB.DataSource;
                m_DatabaseManager.DatabaseName = SCSB.InitialCatalog;
                if (!m_DatabaseManager.HasDatabaseAttached())
                {
                    var MdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\StoreDatabase.mdf");
                    var LdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\StoreDatabase_log.ldf");
                    m_DatabaseManager.Attach(MdfPath, LdfPath);
                    if ((!SCSB.IntegratedSecurity) && (!string.IsNullOrEmpty(SCSB.UserID)) && (!string.IsNullOrEmpty(SCSB.Password)))
                    {
                        m_DatabaseManager.CreateLogin(SCSB.UserID, SCSB.Password);
                    }
                }
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void SetLanguage()
        {
            var dic = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "fa-IR":
                    dic.Source = new Uri(@"Resources/Languages/Fa.xaml", UriKind.Relative);
                    break;
                default:
                    dic.Source = new Uri(@"Resources/Languages/En.xaml", UriKind.Relative);
                    break;
            }
            App.Current.Resources.MergedDictionaries.Add(dic);
        }

        private string GetBitmapDest()
        {
            var SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "PNG Image (*.png)|*.png";
            SaveDlg.RestoreDirectory = true;
            if (SaveDlg.ShowDialog() == true)
            {
                return SaveDlg.FileName;
            }
            return null;
        }

        private void SetUserState(string state)
        {
            switch (state)
            {
                case "رئیس":
                    View.IsManagerEntered = true;
                    break;
                case "فروشنده":
                    View.IsManagerEntered = false;
                    break;
                default: m_CanStart = false; break;
            }
        }

        #endregion
    }
}
