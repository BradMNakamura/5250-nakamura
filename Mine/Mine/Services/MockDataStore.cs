using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Long Sword", Description="Small increase in attack damage stat.", Value=5 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "B.F Sword", Description="Massive increase in attack damage stat.", Value=50 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Infinty Edge", Description="Increases critical strike damage.", Value = 20 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Blood Thirster", Description="Lifesteal from attacks.", Value = 25 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Edge of Night", Description="Gives a spell-immune shield.", Value = 10 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Essence Reaver", Description="Gives mana back on attack.", Value = 30 }
            };
        }

        public async Task<bool> AddItemAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}