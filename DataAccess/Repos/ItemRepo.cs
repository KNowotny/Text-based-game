using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class ItemRepo : IItemRepo
    {
        private readonly IItemRepo _itemRepo;

        private Homework04DbContext _dbContext;
        public ItemRepo(Homework04DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Item item)
        {
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
        }

        public List<Item> GetAllItems()
        {
            return _dbContext.Items.ToList();
        }
    }
}
