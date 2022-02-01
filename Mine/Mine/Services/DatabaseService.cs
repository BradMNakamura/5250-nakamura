using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using Mine.Models;


namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> LazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => LazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        /// <summary>
        /// Adds a ItemModel to the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            //Check if item exists.
            if (item == null)
            {
                return false;
            }

            var result = await Database.InsertAsync(item);
            //Check if item was inserted into the database.
            if(result == 0)
            {
                return false;
            }

            //Item was created.
            return true;
        }

        /// <summary>
        /// Updates an item in the database. If the item is not found in the database
        /// or if the item is null, it will return false. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel item)
        {
            //Check if the item has any information in it.
            if (item == null)
            {
                return false;
            }
            var result = await Database.UpdateAsync(item);
            //Check if item has been updated.
            if (result == 0)
            {
                return false;
            }

            //Item has been updated.
            return true;
        }
        /// <summary>
        /// Deletes a item from the Database. If the item is not found, return false.
        /// If item is found, it will delete it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {
            //Check if the item exists.
            var data = await ReadAsync(id);
            if (data == null)
            {
                return false;
            }
            //Delete item from database.
            var result = await Database.DeleteAsync(data);
            if(result == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if(id == null)
            {
                return null;
            }

            //Call the Database to read the ID
            //Using Linq syntax. find the first record that has the ID that matches
            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));
            return result;
        }

        /// <summary>
        /// Return all items from database as a list.
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            //Get all items from database as a async list.
            var result = await Database.Table<ItemModel>().ToListAsync(); 
            return result;
        }
    }
}
