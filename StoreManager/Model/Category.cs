
namespace StoreManager.Model
{
    public sealed class Category : Notifier
    {
        private int m_CategoryID;
        private string m_CategoryName;
        private byte[] m_Picture;
        private string m_Description;
        //_______________________________________________________________________________________________
        public Category(object categoryID, object categoryName, object picture, object description)
        {
            this.CategoryID = (int)categoryID;
            this.CategoryName = categoryName as string;
            this.Picture = picture as byte[];
            this.Description = description as string;
        }

        public Category() { m_CategoryID = 0; }
        //________________________________________________________________________________________________
        public int CategoryID
        {
            get { return m_CategoryID; }
            set { m_CategoryID = value; OnPropertyChanged("CategoryID"); }
        }

        public string CategoryName
        {
            get { return m_CategoryName; }
            set { m_CategoryName = value; OnPropertyChanged("CategoryName"); }
        }

        public byte[] Picture
        {
            get { return m_Picture; }
            set { m_Picture = value; OnPropertyChanged("Picture"); }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; OnPropertyChanged("Description"); }
        }
    }
}
