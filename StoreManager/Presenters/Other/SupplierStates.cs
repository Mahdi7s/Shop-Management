

namespace StoreManager.Presenters.Other
{
    public static class SupplierStates
    {
        private readonly static string[] m_States;
        //-------------------------------------------
        static SupplierStates()
        {
            m_States = new string[] { "رئیس", "فروشنده" };
        }

        public static string[] GetStates() { return m_States; }
    }
}
