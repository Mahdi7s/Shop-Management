using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreManager.Model.Repositories
{
    public sealed class SoldProductsRepository : Repository<SoldProduct>
    {
        #region Fields

        private const string SAVE_SP = "SAVE_SOLDPRODUCT";
        private const string DELETE_SP = "DELETE_SOLDPRODUCT";
        private const string SELECT_CMD = "SELECT {0} FROM [SoldProducts] {1};";

        #endregion

        #region ctr/dtr

        public SoldProductsRepository() : base(DbSettings.Default.ConnectionString) { }

        #endregion

        #region overrides

        public override void Save(SoldProduct model)
        {
            var parameters = GetParameters(model);
            var SldProductsCountParam = new SqlParameter("RetValue", SqlDbType.Int);
            SldProductsCountParam.Direction = ParameterDirection.ReturnValue;
            parameters.Add(SldProductsCountParam);
            ExecuteNonQuery(SAVE_SP, CommandType.StoredProcedure, parameters.ToArray());
            model.SoldProductsCount = (int)SldProductsCountParam.Value;
        }

        public override void Delete(SoldProduct model)
        {
            var parameter = new SqlParameter("@ID", model.ID);
            ExecuteNonQuery(DELETE_SP, CommandType.StoredProcedure, parameter);
        }

        public override ObservableCollection<SoldProduct> Select(string Condition, params SqlParameter[] parameters)
        {
            var CommandText = string.Format(SELECT_CMD, "*", ("WHERE " + Condition));
            var list = ExecuteReader<SoldProduct>(CommandText, CommandType.Text, GetModel, parameters);
            return new ObservableCollection<SoldProduct>(list);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------
        protected override SoldProduct GetModel(SqlDataReader dr)
        {
            var result = new SoldProduct(dr["ID"], dr["ProductID"], dr["SupplierID"], 
                dr["SoldProductsCount"], dr["Description"], dr["CustomerID"]);
            return result;
        }

        protected override List<SqlParameter> GetParameters(SoldProduct model)
        {
            SqlParameter PID = new SqlParameter();
            PID.ParameterName = "@ID";
            PID.SqlDbType = SqlDbType.Int;
            PID.Value = GetDbValue(model.ID);

            SqlParameter PProductID = new SqlParameter();
            PProductID.ParameterName = "@ProductID";
            PProductID.SqlDbType = SqlDbType.Int;
            PProductID.Value = GetDbValue(model.ProductID);

            SqlParameter PSupplierID = new SqlParameter();
            PSupplierID.ParameterName = "@SupplierID";
            PSupplierID.SqlDbType = SqlDbType.Int;
            PSupplierID.Value = GetDbValue(model.SupplierID);

            SqlParameter PSoldProductsCount = new SqlParameter();
            PSoldProductsCount.ParameterName = "@SoldProductsCount";
            PSoldProductsCount.SqlDbType = SqlDbType.Int;
            PSoldProductsCount.Value = GetDbValue(model.SoldProductsCount);

            SqlParameter PCustomerID = new SqlParameter();
            PCustomerID.ParameterName = "@CustomerID";
            PCustomerID.SqlDbType = SqlDbType.Int;
            PCustomerID.Value = GetDbValue(model.CustomerID);

            SqlParameter PDescription = new SqlParameter();
            PDescription.ParameterName = "@Description";
            PDescription.SqlDbType = SqlDbType.NVarChar;
            PDescription.Value = GetDbValue(model.Description);

            var parameters = new SqlParameter[]
            {
                PID,
	            PProductID,
	            PSupplierID,
	            PSoldProductsCount,
	            PDescription,
	            PCustomerID
            };

            return new List<SqlParameter>(parameters);
        }

        #endregion
    }
}
