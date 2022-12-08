using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace LConnection.Model
{
    public sealed class MCommand : IDisposable
    {
        private bool _disposed = false;

        public MySqlCommand MyCommand { get; private set; } = new MySqlCommand();

        #region Constructor
        public MCommand(MySqlConnection _con)
        {
            ResetCommand();
            MyCommand.Connection = _con;
        }

        ~MCommand()
        {
            Dispose(false);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            if (MyCommand != null)
            {
                MyCommand.Dispose();
                MyCommand = null;
            }

            GC.SuppressFinalize(this);
        }
        #endregion

        #region Public methods
        public void CommandText(string commandText, CommandType type = CommandType.Text)
        {
            MyCommand.CommandText = commandText;
            MyCommand.CommandType = type;
        }

        public void AddParam(string name, DbType type)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Direction = ParameterDirection.Input;
            MyCommand.Parameters.Add(param);
        }

        public void AddParam(string name, DbType type, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Direction = direction;
            MyCommand.Parameters.Add(param);
        }

        public void AddParam(string name, DbType type, object value, ParameterDirection direction)
        {
            MySqlParameter param = new MySqlParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;
            param.Direction = direction;
            MyCommand.Parameters.Add(param);
        }

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

        public void ClearParam()
        {
            MyCommand.Parameters.Clear();
        }

        public object GetParamValue(string name)
        {
            return MyCommand.Parameters[name].Value;
        }

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
