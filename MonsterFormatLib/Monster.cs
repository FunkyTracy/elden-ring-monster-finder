using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFormatLib
{
    public class Monster
    {
        private string name;
        private string location;
        private string drops;
        private string photoUrl;

        //monster info
        public string Name { get { return name; } set { this.name = value; } }
        public string Location { get { return location; } set { this.location = value; } }
        public string Drops { get { return drops; } set { this.drops = value; } }
        public string PhotoURL { get { return photoUrl; } set { this.photoUrl = value; } }
    }
}
