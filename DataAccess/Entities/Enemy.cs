using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Enemy
    {
        public int Id { get; set; }
        [Column(TypeName = ("varchar"))]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public int Power { get; set; }
        public int Health { get; set; }
        public int AcctuallHealth { get; set; }
    }
}
