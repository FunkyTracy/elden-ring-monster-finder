using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFormatLib
{
    public interface IMonsterAPI
    {
        ObservableCollection<Monster> GetMonsters();
    }
}
