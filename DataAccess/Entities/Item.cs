using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int ItemPowerBuff { get; set; }
        public int ItemArmorBuff { get; set; }
    }
}
