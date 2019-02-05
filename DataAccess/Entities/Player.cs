using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int PlayerPower { get; set; }
        public int PlayerAcctualHealth { get; set; }
        public int PlayerMaxHealth { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = ("varchar"))]
        [Index(IsUnique = true)]
        public string Flie { get; set; }
        public int WeaponId { get; set; }
        public virtual Weapon Weapon { get; set; }
        public int ArmorId { get; set; }
        public virtual Armor Armor { get; set; }
        public int ItemId { get; set; }
    }
}
