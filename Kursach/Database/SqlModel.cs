using Kursach.Objects.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Kursach.Database
{
    public class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();

            return sqlModel;
        }

        public Cash SelectCash()
        {
            var mySqlDB = MySqlDatabase.GetDataBase();
            string sql = "select * from Cash";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new(sql, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                        Cash.InitializeCash(dr.GetGuid("id"), dr.GetInt32("Balance"), new());
                }
            }
            return Cash.Instance;
        }

        public List<Detail> SelectDetails()
        {
            var mySqlDB = MySqlDatabase.GetDataBase();
            var result = new List<Detail>();
            string sql = "select * from Details";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(sql, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(new()
                        {
                            ID = dr.GetGuid("id"),
                            Type = dr.GetString("Type"),
                            Model = dr.GetString("Model"),
                            Amount = dr.GetInt32("Amount"),
                            Price = dr.GetInt32("Price")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return result;
        }

        internal List<CashRecord> SelectCashRecords()
        {
            var mySqlDB = MySqlDatabase.GetDataBase();
            var result = new List<CashRecord>();
            string sql = "select * from CashRecords order by Date";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(sql, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(new(dr.GetInt32("Sum"), dr.GetString("Comment"))
                        {
                            ID = dr.GetGuid("id"),
                            DateTime = dr.GetDateTime("Date")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return result;
        }

        public void Insert<T>(T value)
        {
            GetMetaData(value, out string table, out List<(string, object)> values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDatabase.GetDataBase();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }
        public void Update<T>(T value) where T : Model
        {
            GetMetaData(value, out string table, out List<(string, object)> values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDatabase.GetDataBase();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }
        public void Delete<T>(T value) where T : Model
        {
            var type = value.GetType();
            string table = GetTableName(type);
            var db = MySqlDatabase.GetDataBase();
            string query = $"delete from `{table}` where id = '{value.ID}'";
            db.ExecuteNonQuery(query);
        }
        public static int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDatabase.GetDataBase().GetRowsCount(table);
        }
        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Name;
        }
        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<(string, object)> values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }
        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<(string, object)> values, Guid id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = '{id}'");
            return (stringBuilder.ToString(), parameters.ToArray());
        }
        private static List<MySqlParameter> InitParameters(List<(string, object)> values, StringBuilder stringBuilder)
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Item2));
                return $"{s.Item1} = @p{count++}";
            });
            stringBuilder.Append(string.Join(',', rows));
            return parameters;
        }
        private static void GetMetaData<T>(T value, out string table, out List<(string, object)> values)
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Name;
            values = new List<(string, object)>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Name!;
                    values.Add(new(column, prop.GetValue(value)!));
                }
            }
        }
    }
}