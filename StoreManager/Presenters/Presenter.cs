
namespace StoreManager.Presenters
{
    public abstract class Presenter<T> : Notifier
    {
        private readonly string m_TabHeaderPath;
        private readonly T m_View;
        //______________________________________________________________________________
        protected Presenter(T view) { m_View = view; }
        protected Presenter(T view, string tabHeaderPath)
        {
            m_View = view; m_TabHeaderPath = tabHeaderPath;
        }
        //______________________________________________________________________________
        public string TabHeaderPath { get { return m_TabHeaderPath; } }
        public T View { get { return m_View; } }
    }
}
