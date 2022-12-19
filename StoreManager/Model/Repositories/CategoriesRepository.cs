using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreManager.Model.Repositories
{
    public sealed class CategoriesRepository : Repository<Category>
    {
        #region Fields

        private const string SAVE_SP = "SAVE_CATEGORY";
        private const string DELETE_SP = "DELETE_CATEGORY";
        private const string SELECT_CMD = "SELECT {0} FROM [Categories] {1};";

        #endregion

        #region ctr/dtr

        public CategoriesRepository() : base(DbSettings.Default.ConnectionString) { }

        #endregion

        #region overrides

        public override void Save(Category model)
        {
            var parameters = GetParameters(model);
            ExecuteNonQuery(SAVE_SP, CommandType.StoredProcedure, parameters.ToArray());
        }

        public override void Delete(Category model)
        {
            var parameter = new SqlParameter("@CategoryID", model.CategoryID);
            parameter.SqlDbType = SqlDbType.Int;
            ExecuteNonQuery(DELETE_SP, CommandType.StoredProcedure, parameter);
        }

        public override ObservableCollection<Category> Select(string Condition, params SqlParameter[] parameters)
        {
            var CommandText = string.Format(SELECT_CMD, "*", ("WHERE " + Condition));
            var list = ExecuteReader<Category>(CommandText, CommandType.Text, GetModel, parameters);
            return new ObservableCollection<Category>(list);
        }

        //----------------------------------------------------------------------------------------------------------------------------

        protected override Category GetModel(SqlDataReader dr)
        {
            var result = new Category(dr["CategoryID"], dr["CategoryName"],
                dr["Picture"], dr["Description"]);
            return result;
        }

        protected override List<SqlParameter> GetParameters(Category model)
        {
            SqlParameter PCategoryID = new SqlParameter();
            PCategoryID.ParameterName = "@CategoryID";
            PCategoryID.SqlDbType = SqlDbType.Int;
            PCategoryID.Value = GetDbValue(model.CategoryID);

            SqlParameter PCategoryName = new SqlParameter();
            PCategoryName.ParameterName = "@CategoryName";
            PCategoryName.SqlDbType = SqlDbType.NVarChar;
            PCategoryName.Value = GetDbValue(model.CategoryName);

            SqlParameter PPicture = new SqlParameter();
            PPicture.ParameterName = "@Picture";
            PPicture.SqlDbType = SqlDbType.VarBinary;
            PPicture.Value = GetDbValue(model.Picture);

            SqlParameter PDescription = new SqlParameter();
            PDescription.ParameterName = "@Description";
            PDescription.SqlDbType = SqlDbType.NVarChar;
            PDescription.Value = GetDbValue(model.Description);

            var parameters = new SqlParameter[]
            {
	            PCategoryID,
	            PCategoryName,
	            PPicture,
	            PDescription
            };

            return new List<SqlParameter>(parameters);
        }

        #endregion

        #region methods

        public ObservableCollection<Category> GetAllInfo()
        {
            var CommandText = string.Format(SELECT_CMD, "CategoryID, CategoryName", "");
            var list = ExecuteReader<Category>(CommandText, CommandType.Text, GetCategory, null);
            return new ObservableCollection<Category>(list);
        }

        private Category GetCategory(SqlDataReader dr)
        {
            return new Category(dr["CategoryID"], dr["CategoryName"], null, null);
        }

        #endregion
    }
}
