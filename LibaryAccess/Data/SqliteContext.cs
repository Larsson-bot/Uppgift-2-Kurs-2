using LibaryAccess.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Streaming.Adaptive;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace LibaryAccess.Data
{

    public static class SqliteContext


    {
        private static string _dbpath { get; set; }
        public static async Task UseSQLite(string databaseName = "sqlite.db")
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            _dbpath = $"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName)}";
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "CREATE TABLE IF NOT EXISTS Customers (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Created DATETIME NOT NULL); CREATE TABLE IF NOT EXISTS Errands(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER, Catagory TEXT NOT NULL, Status TEXT NOT NULL, Description TEXT NOT NULL, Created DATETIME NOT NULL,FOREIGN KEY (CustomerId) REFERENCES Customers(Id)); CREATE TABLE IF NOT EXISTS Comments(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, ErrandId INTEGER,Description TEXT NOT NULL,Created DATETIME NOT NULL, FOREIGN KEY (ErrandId) REFERENCES Errands(Id));";
                var cmd = new SqliteCommand(query, db);
                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
        public static async Task<long> CreateCustomerAsync(Customer customer)
        {
            long id = 0;
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "INSERT INTO Customers VALUES(null,@Name,@Created);";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();
                db.Close();
            }
            return id;
        }
        public static async Task<long> CreateErrandAsync(Errand errand)
        {
            long id = 0;
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "INSERT INTO Errands VALUES(null,@CustomerId,@Catagory,@Status,@Description,@Created);";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@CustomerId", errand.CustomerId);
                cmd.Parameters.AddWithValue("@Catagory", errand.Catagory);
                cmd.Parameters.AddWithValue("@Status", "new");
                cmd.Parameters.AddWithValue("@Description", errand.Description);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();
                cmd.CommandText = "SELECT last_insert_rowid()";
                id = (long)await cmd.ExecuteScalarAsync();
                db.Close();
            }
            return id;
        }

        public static async Task CreateCommentAsync(Comment comment)
        {
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "INSERT INTO Comments VALUES(null,@ErrandId,@Description,@Created);";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@ErrandId", comment.ErrandId);
                cmd.Parameters.AddWithValue("@Description", comment.Description);
                cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }

        public static async Task<IEnumerable<string>> GetCustomerNames()
        {
            var customernames = new List<string>();

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();

                var query = "SELECT Name FROM Customers";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customernames.Add(result.GetString(0));
                    }
                }
                db.Close();
            }
            return customernames;
        }

        public static async Task<Customer> GetCustomerByIdAsync(long id)
        {
            var customer = new Customer();
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "SELECT * FROM Customers WHERE Id = @Id";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@Id", id);
                var result = await cmd.ExecuteReaderAsync();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        customer = new Customer(result.GetInt64(0), result.GetString(1), result.GetDateTime(2));
                    }
                }
                db.Close();
            }
            return customer;
        }
        public static async Task<long> GetCustomerIdByName(string name)
        {
            long customerid = 0;
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "Select Id FROM Customers WHERE Name =@Name";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@Name", name);
                customerid = (long)await cmd.ExecuteScalarAsync();
                db.Close();
            }
            return customerid;
        }

        public static async Task<ObservableCollection<Errand>> GetErrandsAsync()
        {
            var errands = new ObservableCollection<Errand>();
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "SELECT * FROM Errands";
                var cmd = new SqliteCommand(query, db);

                var result = await cmd.ExecuteReaderAsync();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        errands.Add(new Errand() { Id = result.GetInt64(0), CustomerId = result.GetInt64(1), Catagory = result.GetString(2), Status = result.GetString(3), Description = result.GetString(4), Created = result.GetDateTime(5), Customer = await GetCustomerByIdAsync(result.GetInt64(1)) });
                    }
                }
                db.Close();
            }
            return errands;
        }

        public static async Task<IEnumerable<Comment>> GetCommentsByErrandId(long errandid)
        {
            var comments = new List<Comment>();
            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "SELECT * FROM Comments WHERE ErrandId = @ErrandId";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@ErrandId", errandid);
                var result = await cmd.ExecuteReaderAsync();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        comments.Add(new Comment(result.GetInt64(0), result.GetInt64(1), result.GetString(2), result.GetDateTime(3)));
                    }
                }
                db.Close();
            }
            return comments;
        }

        public static async Task UpdateErrandStatus(string input, long id)
        {

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "UPDATE Errands SET Status = @Status WHERE Id = @Id ";
                var cmd = new SqliteCommand(query, db);
                cmd.Parameters.AddWithValue("@Status", input);
                cmd.Parameters.AddWithValue("@Id", id);
                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
    }
}
