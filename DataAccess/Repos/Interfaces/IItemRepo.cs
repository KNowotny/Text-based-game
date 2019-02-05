using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repos
{
    public interface IItemRepo
    {
        void Create(Item item);
        List<Item> GetAllItems();
    }
}