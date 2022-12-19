
namespace StoreManager.Model
{
    public sealed class SoldProduct : Notifier
    {
        private int m_ID;
        private int m_ProductID;
        private int m_SupplierID;
        private int? m_CustomerID;
        private int m_SoldProductsCount;
        private string m_Description;
        //_______________________________________________________________________________________________________________________
        public SoldProduct(object id, object productID, object supplierID, object customerID, object soldProductsCount, object description)
        {
            m_ID = (int)id;
            this.ProductID = (int)productID;
            this.SupplierID = (int)supplierID;
            this.CustomerID = customerID as int?;
            this.SoldProductsCount = (int)soldProductsCount;
            this.Description = description as string;
        }

        public SoldProduct() { m_ProductID = 0; }
        //_______________________________________________________________________________________________________________________
        public int ID { get { return m_ID; } }

        public int ProductID
        {
            get { return m_ProductID; }
            set { m_ProductID = value; OnPropertyChanged("ProductID"); }
        }

        public int SupplierID
        {
            get { return m_SupplierID; }
            set { m_SupplierID = value; OnPropertyChanged("SupplierID"); }
        }

        public int? CustomerID
        {
            get { return m_CustomerID; }
            set { m_CustomerID = value; OnPropertyChanged("CustomerID"); }
        }

        public int SoldProductsCount
        {
            get { return m_SoldProductsCount; }
            set { m_SoldProductsCount = value; OnPropertyChanged("SoldProductsCount"); }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; OnPropertyChanged("Description"); }
        }
    }
}
