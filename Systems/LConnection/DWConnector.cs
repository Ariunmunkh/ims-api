using Connection.Model;
using LConnection.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BaseLibrary.LConnection
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DWConnector
    {
        #region Attributes

        private MySqlTransaction? MySqlTransaction = null;
        private string ConnectionString = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public MySqlConnection? MySqlConnection { get; set; } = null;
        /// <summary>
        /// 
        /// </summary>
        public MClientHeaderInfo _requestHeaderInfo { get; set; } = new MClientHeaderInfo();
        private string? _errMsg { get; set; }
        private string? _errTrace { get; set; }
        private int _errNo { get; set; }
        private int _affectedrows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Transacted
        {
            get { return (MySqlTransaction != null); }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public DWConnector()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionString"></param>
        public DWConnector(string ConnectionString)
        {
            this.InitializePool(ConnectionString);
        }

        private void InitializePool(string _connectionString)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException(nameof(_connectionString));
            }

            ConnectionString = _connectionString;

            using (MySqlConnection poolInitializer = new MySqlConnection(ConnectionString))
            {
                try
                {
                    poolInitializer.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("Өгөгдлийн сантай холбогдоход алдаа гарлаа: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ReloadConnectionString(string _connectionString)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ArgumentNullException(nameof(_connectionString));
            }

            ConnectionString = _connectionString;
            this.InitializePool(ConnectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MySqlConnection Initialize()
        {
            MySqlConnection = new MySqlConnection(ConnectionString);
            return MySqlConnection;
        }
        /// <summary>
        /// 
        /// </summary>
        public void OpenMySqlConnection()
        {
            if (MySqlConnection != null && MySqlConnection.State != ConnectionState.Open)
            {
                MySqlConnection.Open();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void CloseMySqlConnection()
        {
            if (MySqlConnection != null && MySqlConnection.State == ConnectionState.Open)
            {
                MySqlConnection.Close();
            }
        }

        #region Query commands
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MCommand PopCommand()
        {
            return new MCommand(MySqlConnection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="nonQuery"></param>
        /// <returns></returns>
        public MResult Execute(ref MCommand cmd, bool nonQuery)
        {
            MResult result = new MResult();
            DataTable dataTable;

            ResetFields();

            try
            {
                if (nonQuery)
                {
                    _affectedrows = cmd.MyCommand.ExecuteNonQuery();
                }
                else
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd.MyCommand))
                    {
                        adapter.SelectCommand = cmd.MyCommand;

                        dataTable = new DataTable
                        {
                            Locale = CultureInfo.InvariantCulture
                        };

                        _affectedrows = adapter.Fill(dataTable);
                    }
                    if (_errNo == 0)
                    {
                        result.retdata = dataTable;
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.InnerException == null)
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.Message;
                    _errTrace = ex.StackTrace;
                }
                else
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.InnerException.Message;
                    _errTrace = ex.InnerException.StackTrace;
                }
                result.retdata = cmd.MyCommand.CommandText;
            }
            catch (Exception ex)
            {
                _errNo = -1;
                _errMsg = ex.Message;
                _errTrace = ex.StackTrace;
                result.retdata = ex;
            }

            result.rettype = _errNo;
            result.retmsg = _errMsg;
            result.affectedrows = _affectedrows;


            return result;
        }


        #endregion

        #region Transaction methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool BeginTransaction()
        {
            bool result = false;

            try
            {
                if (MySqlTransaction == null)
                {
                    MySqlTransaction = MySqlConnection?.BeginTransaction();
                    result = true;
                }
            }
            catch (MySqlException ex)
            {
                if (ex.InnerException == null)
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.Message;
                    _errTrace = ex.StackTrace;
                }
                else
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.InnerException.Message;
                    _errTrace = ex.InnerException.StackTrace;
                }
            }
            catch (Exception ex)
            {
                _errNo = -1;
                _errMsg = ex.Message;
                _errTrace = ex.StackTrace;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RollbackTransaction()
        {
            bool result = false;

            try
            {
                if (MySqlTransaction != null)
                {
                    MySqlTransaction.Rollback();
                    MySqlTransaction.Dispose();
                }

                result = true;
            }
            catch (MySqlException ex)
            {
                if (ex.InnerException == null)
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.Message;
                    _errTrace = ex.StackTrace;
                }
                else
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.InnerException.Message;
                    _errTrace = ex.InnerException.StackTrace;
                }
            }
            catch (Exception ex)
            {
                _errNo = -1;
                _errMsg = ex.Message;
                _errTrace = ex.StackTrace;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CommitTransaction()
        {
            bool result = false;

            try
            {
                if (MySqlTransaction != null)
                {
                    MySqlTransaction.Commit();
                    MySqlTransaction.Dispose();
                }

                result = true;
            }
            catch (MySqlException ex)
            {
                if (ex.InnerException == null)
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.Message;
                    _errTrace = ex.StackTrace;
                }
                else
                {
                    _errNo = ex.ErrorCode;
                    _errMsg = ex.InnerException.Message;
                    _errTrace = ex.InnerException.StackTrace;
                }
            }
            catch (Exception ex)
            {
                _errNo = -1;
                _errMsg = ex.Message;
                _errTrace = ex.StackTrace;
            }

            return result;
        }
        #endregion

        private void ResetFields()
        {
            _errNo = 0;
            _errMsg = "";
            _errTrace = "";
            _affectedrows = 0;
        }

    }
}