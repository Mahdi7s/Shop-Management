using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreManager.Model.Repositories
{
    public sealed class SuppliersRepository : Repository<Supplier>
    {
        #region Fields

        private const string SAVE_SP = "SAVE_SUPPLIER";
        private const string DELETE_SP = "DELETE_SUPPLIER";
        private const string SELECT_CMD = "SELECT {0} FROM [Suppliers] {1};";

        #endregion

        #region ctr/dtr

        public SuppliersRepository() : base(DbSettings.Default.ConnectionString) { }

        #endregion

        #region overrides

        public override void Save(Supplier model)
        {
            var parameters = GetParameters(model);
            ExecuteNonQuery(SAVE_SP, CommandType.StoredProcedure, parameters.ToArray());
        }

        public override void Delete(Supplier model)
        {
            var parameter = new SqlParameter("@SupplierID", model.SupplierID);
            parameter.SqlDbType = SqlDbType.Int;
            ExecuteNonQuery(DELETE_SP, CommandType.StoredProcedure, parameter);
        }

        public override ObservableCollection<Supplier> Select(string Condition, params SqlParameter[] parameters)
        {
            var CommandText = string.Format(SELECT_CMD, "*", ("WHERE " + Condition));
            var list = ExecuteReader<Supplier>(CommandText, CommandType.Text, GetModel, parameters);
            return new ObservableCollection<Supplier>(list);
        }

        //-------------------------------------------------------------------------------------------------------------------------------

        protected override Supplier GetModel(SqlDataReader dr)
        {
            var result = new Supplier(dr["SupplierID"], dr["Password"], dr["CompanyName"], dr["ContactName"]
                , dr["Address"], dr["City"], dr["Region"], dr["PostalCode"], dr["Country"],
                dr["Phone"], dr["Fax"], dr["State"], dr["Description"]);
            return result;
        }

        protected override List<SqlParameter> GetParameters(Supplier model)
        {
            SqlParameter PSupplierID = new SqlParameter();
            PSupplierID.ParameterName = "@SupplierID";
            PSupplierID.SqlDbType = SqlDbType.Int;
            PSupplierID.Value = GetDbValue(model.SupplierID);

            SqlParameter PPassword = new SqlParameter();
            PPassword.ParameterName = "@Password";
            PPassword.SqlDbType = SqlDbType.NVarChar;
            PPassword.Value = GetDbValue(model.Password);

            SqlParameter PCompanyName = new SqlParameter();
            PCompanyName.ParameterName = "@CompanyName";
            PCompanyName.SqlDbType = SqlDbType.NVarChar;
            PCompanyName.Value = GetDbValue(model.CompanyName);

            SqlParameter PContactName = new SqlParameter();
            PContactName.ParameterName = "@ContactName";
            PContactName.SqlDbType = SqlDbType.NVarChar;
            PContactName.Value = GetDbValue(model.ContactName);

            SqlParameter PAddress = new SqlParameter();
            PAddress.ParameterName = "@Address";
            PAddress.SqlDbType = SqlDbType.NVarChar;
            PAddress.Value = GetDbValue(model.Address);

            SqlParameter PCity = new SqlParameter();
            PCity.ParameterName = "@City";
            PCity.SqlDbType = SqlDbType.NVarChar;
            PCity.Value = GetDbValue(model.City);

            SqlParameter PRegion = new SqlParameter();
            PRegion.ParameterName = "@Region";
            PRegion.SqlDbType = SqlDbType.NVarChar;
            PRegion.Value = GetDbValue(model.Region);

            SqlParameter PPostalCode = new SqlParameter();
            PPostalCode.ParameterName = "@PostalCode";
            PPostalCode.SqlDbType = SqlDbType.NVarChar;
            PPostalCode.Value = GetDbValue(model.PostalCode);

            SqlParameter PCountry = new SqlParameter();
            PCountry.ParameterName = "@Country";
            PCountry.SqlDbType = SqlDbType.NVarChar;
            PCountry.Value = GetDbValue(model.Country);

            SqlParameter PPhone = new SqlParameter();
            PPhone.ParameterName = "@Phone";
            PPhone.SqlDbType = SqlDbType.NVarChar;
            PPhone.Value = GetDbValue(model.Phone);

            SqlParameter PFax = new SqlParameter();
            PFax.ParameterName = "@Fax";
            PFax.SqlDbType = SqlDbType.NVarChar;
            PFax.Value = GetDbValue(model.Fax);

            SqlParameter PState = new SqlParameter();
            PState.ParameterName = "@State";
            PState.SqlDbType = SqlDbType.NVarChar;
            PState.Value = GetDbValue(model.State);

            SqlParameter PDescription = new SqlParameter();
            PDescription.ParameterName = "@Description";
            PDescription.SqlDbType = SqlDbType.NVarChar;
            PDescription.Value = GetDbValue(model.Description);

            var parameters = new SqlParameter[]
            {
	            PSupplierID,
	            PPassword,
	            PCompanyName,
	            PContactName,
	            PAddress,
	            PCity,
	            PRegion,
	            PPostalCode,
	            PCountry,
	            PPhone,
	            PFax,
	            PState,
	            PDescription
            };

            return new List<SqlParameter>(parameters);
        }

        #endregion

        #region methods

        public string GetPassword(int SupplierID)
        {
            var CommandText = string.Format(SELECT_CMD, "Password", "WHERE SupplierID = @SupplierID");
            var param = new SqlParameter("@SupplierID", SupplierID);
            param.SqlDbType = SqlDbType.Int;
            return ExecuteScalar(CommandText, CommandType.Text, param) as string;
        }

        public ObservableCollection<Supplier> GetAllInfo()
        {
            var CommandText = string.Format(SELECT_CMD, "SupplierID, ContactName", "");
            var list = ExecuteReader<Supplier>(CommandText, CommandType.Text, GetSupplier, null);
            return new ObservableCollection<Supplier>(list);
        }

        private Supplier GetSupplier(SqlDataReader dr)
        {
            return new Supplier(dr["SupplierID"], null, null, dr["ContactName"], null, null, null, null, null, null, null, null, null);
        }

        #endregion
    }
}
