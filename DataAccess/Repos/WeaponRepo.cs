using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class WeaponRepo : IWeaponRepo
    {
        private readonly IWeaponRepo _weaponRepo;

        private Homework04DbContext _dbContext;
        public WeaponRepo(Homework04DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Weapon weapon)
        {
            _dbContext.Weapons.Add(weapon);
            _dbContext.SaveChanges();
        }

        public List<Weapon> GetAllWeapons()
        {
            return _dbContext.Weapons.ToList();
        }
    }
}
