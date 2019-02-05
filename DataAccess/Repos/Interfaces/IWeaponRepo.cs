using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repos
{
    public interface IWeaponRepo
    {
        void Create(Weapon weapon);
        List<Weapon> GetAllWeapons();
    }
}