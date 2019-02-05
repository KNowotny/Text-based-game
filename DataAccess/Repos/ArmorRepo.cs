using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class ArmorRepo : IArmorRepo
    {
        private readonly IArmorRepo _armorRepo;
        private Homework04DbContext _dbContext;
        public ArmorRepo(Homework04DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Armor armor)
        {
            _dbContext.Armors.Add(armor);
            _dbContext.SaveChanges();
        }

        public List<Armor> GetAllArmors()
        {
            return _dbContext.Armors.ToList();
        }

    }
}
