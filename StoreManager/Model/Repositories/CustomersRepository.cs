using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreManager.Model.Repositories
{
    public sealed class CustomersRepository : Repository<Customer>
    {
        #region Fields

        private const string SAVE_SP = "SAVE_CUSTOMER";
        private const string DELETE_SP = "DELETE_CUSTOMER";
        private const string SELECT_CMD = "SELECT {0} FROM [Customers] {1};";

        #endregion

        #region ctr/dtr

        public CustomersRepository() : base(DbSettings.Default.ConnectionString) { }

        #endregion

        #region overrides

        public override void Save(Customer model)
        {
            var parameters = GetParameters(model);
            ExecuteNonQuery(SAVE_SP, CommandType.StoredProcedure, parameters.ToArray());
        }

        public override void Delete(Customer model)
        {
            var parameter = new SqlParameter("@CustomerID", model.CustomerID);
            parameter.SqlDbType = SqlDbType.Int;
            ExecuteNonQuery(DELETE_SP, CommandType.StoredProcedure, parameter);
        }

        public override ObservableCollection<Customer> Select(string Condition, params SqlParameter[] parameters)
        {
            var CommandText = string.Format(SELECT_CMD, "*", ("WHERE " + Condition));
            var list = ExecuteReader<Customer>(CommandText, CommandType.Text, GetModel, parameters);
            return new ObservableCollection<Customer>(list);
        }

        //--------------------------------------------------------------------------------------------------------------------

        protected override Customer GetModel(SqlDataReader dr)
        {
            var result = new Customer(dr["CustomerID"], dr["CompanyName"], dr["ContactName"], 
                dr["Address"], dr["City"], dr["Region"], dr["PostalCode"],
                dr["Country"], dr["Phone"], dr["Fax"], dr["Description"]);
            return result;
        }

        protected override List<SqlParameter> GetParameters(Customer model)
        {
            SqlParameter PCustomerID = new SqlParameter();
            PCustomerID.ParameterName = "@CustomerID";
            PCustomerID.SqlDbType = SqlDbType.Int;
            PCustomerID.Value = GetDbValue(model.CustomerID);

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

            SqlParameter PDescription = new SqlParameter();
            PDescription.ParameterName = "@Description";
            PDescription.SqlDbType = SqlDbType.NVarChar;
            PDescription.Value = GetDbValue(model.Description);

            var parameters = new SqlParameter[]
            {
	            PCustomerID,
	            PCompanyName,
	            PContactName,
	            PAddress,
	            PCity,
	            PRegion,
	            PPostalCode,
	            PCountry,
	            PPhone,
	            PFax,
	            PDescription
            };

            return new List<SqlParameter>(parameters);
        }

        #endregion

        #region methods

        public ObservableCollection<Customer> GetAllInfo()
        {
            var CommandText = string.Format(SELECT_CMD, "CustomerID, ContactName", "");
            var list = ExecuteReader<Customer>(CommandText, CommandType.Text, GetCustomer, null);
            return new ObservableCollection<Customer>(list);
        }

        private Customer GetCustomer(SqlDataReader dr)
        {
            return new Customer(dr["CustomerID"], null, dr["ContactName"], null, null, null, null, null, null, null, null);
        }

        #endregion
    }
}
