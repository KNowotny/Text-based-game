using DataAccess.Repos;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IArmorRepo>().To<ArmorRepo>();
            Bind<IEnemyRepo>().To<EnemyRepo>();
            Bind<IItemRepo>().To<ItemRepo>();
            Bind<IPlayerRepo>().To<PlayerRepo>();
            Bind<IWeaponRepo>().To<WeaponRepo>();
        }
    }
}
