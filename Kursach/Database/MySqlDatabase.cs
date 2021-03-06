using MySql.Data.MySqlClient;
using System;

namespace Kursach.Database;

public class MySqlDatabase
{
    private MySqlDatabase() { }
    static MySqlDatabase db;
    public static MySqlDatabase GetDataBase()
    {
        if (db == null)
            db = new MySqlDatabase();

        return db;
    }

    internal MySqlConnection sqlConnection = null;

    internal void InitConnection()
    {
        InitConnection(Properties.Settings.Default.server, Properties.Settings.Default.user,
                       Properties.Settings.Default.database, Properties.Settings.Default.password);
    }

    internal void InitConnection(string server, string user, string db, string password)
    {
        MySqlConnectionStringBuilder builder = new();
        builder.UserID = user;
        builder.Password = password;
        builder.Database = db;
        builder.Server = server;
        builder.CharacterSet = "utf8";
        builder.ConnectionTimeout = 5;

        sqlConnection = new MySqlConnection(builder.GetConnectionString(true));
    }

    internal bool OpenConnection()
    {
        try
        {
            if (sqlConnection == null)
                InitConnection();

            sqlConnection.Open();

            return true;
        }
        catch (Exception e)
        {
            System.Windows.MessageBox.Show(e.Message);
        }
        return false;
    }

    internal void CloseConnection()
    {
        try
        {
            sqlConnection.Close(); 
        }
        catch (Exception e)
        {
            System.Windows.MessageBox.Show(e.Message);
        }
    }

    internal void ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
    {
        if (OpenConnection())
        {
            using (MySqlCommand mc = new MySqlCommand(query, sqlConnection))
            {
                if (parameters != null)
                    mc.Parameters.AddRange(parameters);
                mc.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
    internal int GetRowsCount(string table)
    {
        string column = "Rows";
        return GetTableInfo(table, column);
    }

    private int GetTableInfo(string table, string column)
    {
        int result = 0;

        if (OpenConnection())
        {
            using (MySqlCommand mc = new MySqlCommand($"SHOW TABLE STATUS WHERE `Name` = '{table}'", sqlConnection))
            using (MySqlDataReader dr = mc.ExecuteReader())
            {
                dr.Read();
                result = dr.GetInt32(column);
            }
            CloseConnection();
        }
        return result;
    }
}