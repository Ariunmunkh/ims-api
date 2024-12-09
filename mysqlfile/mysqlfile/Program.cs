
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ignore = new List<string>() {
                "columns_priv",
"component",
"db",
"default_roles",
"engine_cost",
"func",
"general_log",
"global_grants",
"gtid_executed",
"help_category",
"help_keyword",
"help_relation",
"help_topic",
"innodb_index_stats",
"innodb_table_stats",
"ndb_binlog_index",
"password_history",
            "plugin",
"procs_priv",
"proxies_priv",
"replication_asynchronous_connection_failover",
"replication_asynchronous_connection_failover_managed",
"replication_group_configuration_version",
"replication_group_member_actions",
"role_edges",
"server_cost",
"servers",
"slave_master_info",
"slave_relay_log_info",
"slave_worker_info",
"slow_log",
"tables_priv",
"time_zone",
"time_zone_leap_second",
"time_zone_name",
"time_zone_transition",
"time_zone_transition_type",
"user",


            };
            using (DataTable dt = GetData("select * from information_schema.tables where table_schema = 'mysql'"))
            {

                List<string> collist = new List<string>();
                List<string> vallist = new List<string>();
                List<string> vallist2 = new List<string>();
                foreach (DataRow dr in dt.Rows)
                {
                    if (ignore.Contains(dr["TABLE_NAME"].ToString()))
                        continue;

                    string sql;
                    using (DataTable cdt = GetData($"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'mysql' AND TABLE_NAME ='{dr["TABLE_NAME"]}'"))
                    {
                        string pri = string.Empty;
                        collist.Clear();
                        foreach (DataRow row in cdt.Select("", "ORDINAL_POSITION ASC"))
                        {
                            if (row["COLUMN_KEY"].ToString().Equals("PRI"))
                                pri = row["COLUMN_NAME"].ToString();

                            collist.Add($"{row["COLUMN_NAME"]} {row["COLUMN_TYPE"]} {(row["COLUMN_DEFAULT"] != DBNull.Value ? "default " + row["COLUMN_DEFAULT"] : string.Empty)}" + (row["IS_NULLABLE"].ToString().Equals("NO") ? " NOT NULL" : string.Empty) + (row["EXTRA"].ToString().Equals("auto_increment") ? " AUTO_INCREMENT" : string.Empty));
                        }

                        sql = $"create table {dr["TABLE_NAME"]}({string.Join(",", collist)} " + (string.IsNullOrEmpty(pri) ? ")" : $", PRIMARY KEY ({pri}))");

                    }

                    if (ExecuteNonQuery(sql))
                    {
                        WriteLog(dr["TABLE_NAME"].ToString(), sql);

                        Console.WriteLine(dr["TABLE_NAME"]);
                        collist.Clear();
                        vallist.Clear();
                        int counter = 0;
                        using (DataTable cdt = GetData($"SELECT * FROM {dr["TABLE_NAME"]} "))
                        {
                            foreach (DataColumn dc in cdt.Columns)
                            {
                                collist.Add(dc.Caption);
                            }
                            foreach (DataRow row in cdt.Rows)
                            {
                                counter++;
                                vallist2.Clear();
                                foreach (DataColumn dc in cdt.Columns)
                                {
                                    if (row[dc.Caption] == DBNull.Value)
                                        vallist2.Add("null");
                                    else if (dc.DataType == typeof(string))
                                        vallist2.Add($"'{row[dc.Caption].ToString().Replace("'", ".")}'");
                                    else if (dc.DataType == typeof(DateTime))
                                        vallist2.Add($"'{Convert.ToDateTime(row[dc.Caption]).ToString("yyyy-MM-dd HH:mm:ss")}'");
                                    else
                                        vallist2.Add(row[dc.Caption].ToString());
                                }
                                vallist.Add($"({string.Join(",", vallist2)})");

                                if (counter == 1000)
                                {
                                    sql = $"insert into {dr["TABLE_NAME"]}({string.Join(",", collist)}) values {string.Join(",", vallist)}";
                                    ExecuteNonQuery(sql);
                                    WriteLog(dr["TABLE_NAME"].ToString(), sql);
                                    vallist.Clear();
                                    counter = 0;
                                }
                            }
                            if (vallist.Count > 0)
                            {
                                sql = $"insert into {dr["TABLE_NAME"]}({string.Join(",", collist)}) values {string.Join(",", vallist)}";
                                ExecuteNonQuery(sql);
                                WriteLog(dr["TABLE_NAME"].ToString(), sql);
                            }
                            Console.WriteLine($"{dr["TABLE_NAME"]} ok.");
                        }
                    }

                }
                Console.WriteLine("");
                Console.WriteLine("ok. successful");
            }
            Console.ReadKey();
        }

        static void WriteLog(string name, string log)
        {
            string filename = Path.Combine(Directory.GetCurrentDirectory(), "backup", DateTime.Now.ToString("yyyy-MM-dd"), string.Format("{0}.sql", name));
            if (!Directory.Exists(Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
            File.AppendAllLines(filename, new string[] { log });
        }

        static DataTable GetData(string sql)
        {
            try
            {
                string connectionString = "Server=167.172.94.246;" +
                "Port=3333;" +
                "Database=mysql;" +
                "Uid=root;" +
                "Pwd=rqzs1jwpe1rqmk1jndo;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sql;

                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        static bool ExecuteNonQuery(string sql)
        {
            try
            {
                string connectionString = "Server=185.170.198.36;" +
                "Port=3333;" +
                "Database=mysql;" +
                "Uid=root;" +
                "Pwd=rqzs1jwpe1rqmk1jndo;";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }


    public class DBConnection
    {
        private DBConnection()
        {
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}
