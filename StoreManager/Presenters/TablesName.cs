namespace StoreManager.Presenters
{
    public static class TablesName
    {
        private static TableName[] m_TablesName;
        //_____________________________________________________________________________________________
        static TablesName()
        {
            var TablesName = new System.Collections.Generic.List<TableName>();
            TablesName.Add(new TableName("Products", "کالاهای موجود","ProductID"));
            TablesName.Add(new TableName("Suppliers", "فروشندگان","SupplierID"));
            TablesName.Add(new TableName("Customers", "خریداران","CustomerID"));
            TablesName.Add(new TableName("SoldProducts", "کالاهای فروخته شده",""));
            m_TablesName = TablesName.ToArray();
        }

        public static TableName[] GetTablesName() { return m_TablesName; }
    }
    //_____________________________________________________________________________________________
    public sealed class TableName
    {
        public TableName(string EnName, string PeName, string SearchField)
        {
            EnglishName = EnName;
            PersianName = PeName;
            DefaultSearchField = SearchField;
        }

        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public string DefaultSearchField { get; set; }
    }
}
