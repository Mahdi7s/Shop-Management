using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace StoreManager.Model.Repositories
{
    public abstract class Connector : Notifier
    {
        #region fields

        private readonly string m_ConnectionString;
        public delegate R Action<R, P>(P parameter); // R = method 'return'

        #endregion

        #region ctr/dtr

        protected Connector(string ConnectionString) { m_ConnectionString = ConnectionString; }

        #endregion

        #region properties

        public bool ExecutedGood { get; private set; }

        #endregion

        #region protected methods

        protected virtual int ExecuteNonQuery(string CommandText, CommandType type, params SqlParameter[] parameters)
        {
            ExecutedGood = true;
            var result = new int();
            try
            {
                result = Execute<int>(CommandText, type, parameters, NonQuery);
            }
            catch (Exception ex) { ShowError(ex.Message); ExecutedGood = false; }
            return result;
        }

        protected virtual object ExecuteScalar(string CommandText, CommandType type, params SqlParameter[] parameters)
        {
            ExecutedGood = true;
            var result = new object();
            try
            {
                result = Execute<object>(CommandText, type, parameters, Scalar);
            }
            catch (Exception ex) { ShowError(ex.Message); ExecutedGood = false; }
            return result;
        }

        protected virtual IList<T> ExecuteReader<T>(string CommandText, CommandType type, Action<T, SqlDataReader> method, params SqlParameter[] parameters)
        {
            ExecutedGood = true;
            var result = new List<T>();
            using (var connection = new SqlConnection(m_ConnectionString))
            {
                using (var command = new SqlCommand(CommandText, connection))
                {
                    try
                    {
                        if (parameters != null && parameters.Length > 0)
                            command.Parameters.AddRange(parameters);
                        command.CommandType = type; connection.Open();
                        using (var dr = command.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            while (dr.Read())
                            {
                                result.Add(method(dr));
                            }
                        }
                    }
                    catch (Exception ex) { ShowError(ex.Message); ExecutedGood = false; }
                }
            }
            return result;
        }

        #endregion

        #region private methods

        private R Execute<R>(string CommandText, CommandType type, SqlParameter[] parameters, Action<R, SqlCommand> method)
        {
            using (var connection = new SqlConnection(m_ConnectionString))
            {
                using (var command = new SqlCommand(CommandText, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                        command.Parameters.AddRange(parameters);
                    command.CommandType = type; connection.Open();
                    return method(command);
                }
            }
        }

        private int NonQuery(SqlCommand command) { return command.ExecuteNonQuery(); }
        private object Scalar(SqlCommand command) { return command.ExecuteScalar(); }

        #endregion
    }
}
