using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repos
{
    public interface IArmorRepo
    {
        void Create(Armor armor);
        List<Armor> GetAllArmors();
    }
}