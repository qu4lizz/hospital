using System.Collections.Generic;
using System.Linq;
using Hospital.DataObjects;
using Hospital.DataObjects;

namespace Hospital.Services
{
    public class ItemsService
    {
        private readonly hospitalContext _context;

        public ItemsService()
        {
            _context = new hospitalContext();
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items
                .OrderBy(item => item.Id)
                .ToList();
        }

        public Item GetById(int itemId)
        {
            return _context.Items.Find(itemId);
        }

        public void Add(Item newItem)
        {
            _context.Items.Add(newItem);
            _context.SaveChanges();
        }

        public void Update(Item updatedItem)
        {
            var existingItem = _context.Items.Find(updatedItem.Id);

            if (existingItem != null)
            {
                existingItem.ItemName = updatedItem.ItemName;

                _context.SaveChanges();
            }
        }

        public void Delete(int itemId)
        {
            var itemToDelete = _context.Items.Find(itemId);

            if (itemToDelete != null)
            {
                _context.Items.Remove(itemToDelete);
                _context.SaveChanges();
            }
        }
    }
}
