using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace LConnection.Model
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MCommand : IDisposable
    {
        private bool _disposed = false;
        /// <summary>
        /// 
        /// </summary>
        public MySqlCommand MyCommand { get; private set; } = new MySqlCommand();

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_con"></param>
        public MCommand(MySqlConnection? _con)
        {
            ResetCommand();
            MyCommand.Connection = _con;
        }
        /// <summary>
        /// 
        /// </summary>
        ~MCommand()
        {
            Dispose(false);
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (MyCommand != null)
            {
                MyCommand.Dispose();
            }

            GC.SuppressFinalize(this);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="type"></param>
        public void CommandText(string commandText, CommandType type = CommandType.Text)
        {
            MyCommand.CommandText = commandText;
            MyCommand.CommandType = type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public void AddParam(string name, DbType type)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Direction = ParameterDirection.Input;
            MyCommand.Parameters.Add(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="direction"></param>
        public void AddParam(string name, DbType type, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Direction = direction;
            MyCommand.Parameters.Add(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        public void AddParam(string name, DbType type, object? value, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;
            param.Direction = direction;
            MyCommand.Parameters.Add(param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        public void AddParam(string name, DbType type, int size, object value, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Size = size;
            param.Value = value;
            param.Direction = direction;
            MyCommand.Parameters.Add(param);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearParam()
        {
            MyCommand.Parameters.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetParamValue(string name)
        {
            return MyCommand.Parameters[name].Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetParamValue(string name, object value)
        {
            MyCommand.Parameters[name].Value = value;
        }

        #endregion

        #region Private methods
        private void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Dispose();
            }

            _disposed = true;
        }

        private void ResetCommand()
        {
            MyCommand.CommandType = CommandType.Text;
            MyCommand.CommandText = "";
            MyCommand.Parameters.Clear();
        }
        #endregion
    }
}
