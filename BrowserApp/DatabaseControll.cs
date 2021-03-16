using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;

namespace BrowserApp
{
    public class DatabaseControll
    {

        private static string PATH = "./bookmarks.txt";
        public static void addBookmark(string url)
        {
            SQLiteConnection sqLiteConnection = getConnection();
            sqLiteConnection.Open();

            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = string.Format("INSERT INTO Bookmarks(url) VALUES ('{0}')", url);
            sqLiteCommand.ExecuteNonQuery();
            
            sqLiteConnection.Close();
        }

        public static List<string> getBookmarks()
        {
            List<string> bookmarks = new List<string>();
            SQLiteConnection sqLiteConnection = getConnection();
            sqLiteConnection.Open();

            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter("SELECT url FROM bookmarks;", sqLiteConnection);
            DataSet dataSet = new DataSet();
            sqLiteDataAdapter.Fill(dataSet);

            
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string bookmark = row.ItemArray[0].ToString();
                bookmarks.Add(bookmark);

            }
            
            return bookmarks;
        }


        private static SQLiteConnection getConnection()
        {
            SQLiteConnection sqlConnection = new SQLiteConnection("Data Source=../../bookmarks.db;New=False;Compress=True");
            

            return sqlConnection;
        }
        
        
    }
}