using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AudioUnit;
using MauiToDo.Models;
using SQLite;

namespace MauiToDo.Data
{
    public class DataBase
    {
        private readonly SQLiteAsyncConnection? _connection;

        public DataBase()
        {
            var dataDir = FileSystem.AppDataDirectory;
            var dataBasePath = Path.Combine(dataDir, "MauiToDo.db");

            string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

            if (string.IsNullOrEmpty(_dbEncryptionKey))
            {
                Guid g = new Guid();
                _dbEncryptionKey = g.ToString();
                SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
            }

            var dbOptions = new SQLiteConnectionString(
                dataBasePath, true, key: _dbEncryptionKey);
            _connection = new SQLiteAsyncConnection(dbOptions);

            // underscore means discarding the return value
            // introduced in c#7.0 onwards
            _ = Initialise();


        }

    

        private async Task Initialise()
        {
            await _connection.CreateTableAsync<ToDoItem>();
        }

        public async Task<List<ToDoItem>> GetTodos()
        {
            return await _connection.Table<ToDoItem>().ToListAsync();
        }

        public async Task<ToDoItem> GetToDo(int id)
        {
            var query = _connection.Table<ToDoItem>().
                Where(t => t.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> AddToDo(ToDoItem item)
        {
            return await _connection.InsertAsync(item);
        }

        public async Task<int> DeleteToDo(ToDoItem item)
        {
            return await _connection.DeleteAsync(item);

        }

        public async Task<int> UpdateTodo(ToDoItem item)
        {
            return await _connection.UpdateAsync(item);
        }


    }
}
