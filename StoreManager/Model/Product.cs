namespace StoreManager.Model
{
    public sealed class Product : Notifier
    {
        private int m_ProductID;
        private string m_ProductName;
        private int? m_CategoryID;
        private int m_ProductCount;
        private long m_ProductPrice;
        private string m_Description;
        //_________________________________________________________________________________________________________________
        public Product(object productID, object productName, object categoryID, object productCount, object productPrice, object description)
        {
            this.ProductID = (int)productID;
            this.ProductName = productName as string;
            this.CategoryID = categoryID as int?;
            this.ProductCount = (int)productCount;
            this.ProductPrice = (long)productPrice;
            this.Description = description as string;
        }

        public Product() { ProductID = 0; }
        //__________________________________________________________________________________________________________________
        public int ProductID
        {
            get { return m_ProductID; }
            set { m_ProductID = value; OnPropertyChanged("ProductID"); }
        }

        public string ProductName
        {
            get { return m_ProductName; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    this["ProductName"] = "Product name is required";
                    return;
                }
                else this.RemoveError("ProductName");

                m_ProductName = value;
                OnPropertyChanged("ProductName");
            }
        }

        public int? CategoryID
        {
            get { return m_CategoryID; }
            set { m_CategoryID = value; OnPropertyChanged("CategoryID"); }
        }

        public int ProductCount
        {
            get { return m_ProductCount; }
            set { m_ProductCount = value; OnPropertyChanged("ProductCount"); }
        }

        public long ProductPrice
        {
            get { return m_ProductPrice; }
            set { m_ProductPrice = value; OnPropertyChanged("ProductPrice"); }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; OnPropertyChanged("Description"); }
        }
    }
}
