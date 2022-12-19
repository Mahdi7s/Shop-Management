using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreManager.Model.Repositories
{
    public abstract class Repository<T> : Connector // T = Model object
    {
        #region ctr/dtr

        protected Repository(string ConnectionString) : base(ConnectionString) { }

        #endregion

        #region abstract methods

        public abstract void Save(T model); // Insert Or Update
        public abstract void Delete(T model);
        public abstract ObservableCollection<T> Select(string Condition, params SqlParameter[] parameters);

        protected abstract T GetModel(SqlDataReader dr);
        protected abstract List<SqlParameter> GetParameters(T model);

        #endregion

        #region converters

        protected virtual object GetDbValue(object value)
        {
            if (value is string && (!string.IsNullOrEmpty(value as string))) return value;
            if (value != null && value != DBNull.Value) return value;
            return DBNull.Value;
        }

        protected virtual object GetDbValue<P>(Nullable<P> value) where P : struct
        {
            if (value.HasValue) return value.Value;
            return DBNull.Value;
        }

        #endregion
    }
}
