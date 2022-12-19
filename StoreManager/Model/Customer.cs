
namespace StoreManager.Model
{
    public sealed class Customer : Notifier
    {
        private int m_CustomerID;
        private string m_CompanyName;
        private string m_ContactName;
        private string m_Address;
        private string m_City;
        private string m_Region;
        private string m_PostalCode;
        private string m_Country;
        private string m_Phone;
        private string m_Fax;
        private string m_Description;
        //________________________________________________________________________________________________________________________________________________________________________________________________________
        public Customer(object customerID, object companyName, object contactName, object address, object city, object region, object postalCode, object country, object phone, object fax, object description)
        {
            this.CustomerID = (int)customerID;
            this.CompanyName = companyName as string;
            this.ContactName = contactName as string;
            this.Address = address as string;
            this.City = city as string;
            this.Region = region as string;
            this.PostalCode = postalCode as string;
            this.Country = country as string;
            this.Phone = phone as string;
            this.Fax = fax as string;
            this.Description = description as string;
        }

        public Customer() { CustomerID = 0; }
        //________________________________________________________________________________________________________________________________________________________________________________
        public int CustomerID
        {
            get { return m_CustomerID; }
            set { m_CustomerID = value; OnPropertyChanged("CustomerID"); }
        }

        public string CompanyName
        {
            get { return m_CompanyName; }
            set { m_CompanyName = value; OnPropertyChanged("CompanyName"); }
        }

        public string ContactName
        {
            get { return m_ContactName; }
            set { m_ContactName = value; OnPropertyChanged("ContactName"); }
        }

        public string Address
        {
            get { return m_Address; }
            set { m_Address = value; OnPropertyChanged("Address"); }
        }

        public string City
        {
            get { return m_City; }
            set { m_City = value; OnPropertyChanged("City"); }
        }

        public string Region
        {
            get { return m_Region; }
            set { m_Region = value; OnPropertyChanged("Region"); }
        }

        public string PostalCode
        {
            get { return m_PostalCode; }
            set { m_PostalCode = value; OnPropertyChanged("PostalCode"); }
        }

        public string Country
        {
            get { return m_Country; }
            set { m_Country = value; OnPropertyChanged("Country"); }
        }

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; OnPropertyChanged("Phone"); }
        }

        public string Fax
        {
            get { return m_Fax; }
            set { m_Fax = value; OnPropertyChanged("Fax"); }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; OnPropertyChanged("Description"); }
        }

    }
}
