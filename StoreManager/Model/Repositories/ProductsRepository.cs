using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreManager.Model.Repositories
{
    public sealed class ProductsRepository : Repository<Product>
    {
        #region Fields

        private const string SAVE_SP = "SAVE_PRODUCT";
        private const string DELETE_SP = "DELETE_PRODUCT";
        private const string SELECT_CMD = "SELECT {0} FROM [Products] {1};";

        #endregion

        #region ctr/dtr

        public ProductsRepository() : base(DbSettings.Default.ConnectionString) { }

        #endregion

        #region overrides

        public override void Save(Product model)
        {
            var parameters = GetParameters(model);
            ExecuteNonQuery(SAVE_SP, CommandType.StoredProcedure, parameters.ToArray());
        }

        public override void Delete(Product model)
        {
            var parameter = new SqlParameter("@ProductID", GetDbValue(model.ProductID));
            parameter.SqlDbType = SqlDbType.Int;
            ExecuteNonQuery(DELETE_SP, CommandType.StoredProcedure, parameter);
        }

        public override ObservableCollection<Product> Select(string Condition, params SqlParameter[] parameters)
        {
            var CommandText = string.Format(SELECT_CMD, "*", ("WHERE " + Condition));
            var list = ExecuteReader<Product>(CommandText, CommandType.Text, GetModel, parameters);
            return new ObservableCollection<Product>(list);
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        protected override Product GetModel(SqlDataReader dr)
        {
            var result = new Product(dr["ProductID"], dr["ProductName"], dr["CategoryID"],
                dr["ProductCount"], dr["ProductPrice"], dr["Description"]);
            return result;
        }

        protected override List<SqlParameter> GetParameters(Product model)
        {
            SqlParameter PProductID = new SqlParameter();
            PProductID.ParameterName = "@ProductID";
            PProductID.SqlDbType = SqlDbType.Int;
            PProductID.Value = GetDbValue(model.ProductID);

            SqlParameter PProductName = new SqlParameter();
            PProductName.ParameterName = "@ProductName";
            PProductName.SqlDbType = SqlDbType.NVarChar;
            PProductName.Value = GetDbValue(model.ProductName);

            SqlParameter PCategoryID = new SqlParameter();
            PCategoryID.ParameterName = "@CategoryID";
            PCategoryID.SqlDbType = SqlDbType.Int;
            PCategoryID.Value = GetDbValue(model.CategoryID);

            SqlParameter PProductCount = new SqlParameter();
            PProductCount.ParameterName = "@ProductCount";
            PProductCount.SqlDbType = SqlDbType.Int;
            PProductCount.Value = GetDbValue(model.ProductCount);

            SqlParameter PProductPrice = new SqlParameter();
            PProductPrice.ParameterName = "@ProductPrice";
            PProductPrice.SqlDbType = SqlDbType.BigInt;
            PProductPrice.Value = GetDbValue(model.ProductPrice);

            SqlParameter PDescription = new SqlParameter();
            PDescription.ParameterName = "@Description";
            PDescription.SqlDbType = SqlDbType.NVarChar;
            PDescription.Value = GetDbValue(model.Description);

            var parameters = new SqlParameter[]
            {
	            PProductID,
	            PProductName,
	            PCategoryID,
	            PProductCount,
	            PProductPrice,
	            PDescription
            };

            return new List<SqlParameter>(parameters);
        }

        #endregion

        #region methods

        public ObservableCollection<Product> GetAllInfo()
        {
            var CommandText = string.Format(SELECT_CMD, "*", "WHERE ProductCount > 0");
            var list = ExecuteReader<Product>(CommandText, CommandType.Text, GetModel, null);
            return new ObservableCollection<Product>(list);
        }

        #endregion
    }
}
