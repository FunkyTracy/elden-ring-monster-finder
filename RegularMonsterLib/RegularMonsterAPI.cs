using MonsterFormatLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RegularMonsterLib
{
    public class RegularMonsterAPI : IMonsterAPI
    {
        //gets data from the API and returns it as RegularMonsterData classes
        public RegularMonsterData GetData()
        {
            RegularMonsterData results = null;

            //call API for monsters
            const string url = "https://eldenring.fanapis.com/api/creatures?limit=100";
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(url);

                results = JsonConvert.DeserializeObject<RegularMonsterData>(json);
            }

            return results;
        }

        //takes the data from RegularMonsterData and formats it in the Monster class format
        public ObservableCollection<Monster> GetMonsters()
        {
            ObservableCollection<Monster> regMonsters = new ObservableCollection<Monster>();

            //get data from internet
            RegularMonsterData results = GetData();

            //add each monster from the json to the list
            foreach(Datum id in results.data)
            {
                Monster monster = new Monster();

                monster.Name = id.name;
                if (id.location != null)
                {
                    monster.Location = id.location;
                }
                else
                {
                    monster.Location = "No specific location";
                }

                if (id.drops != null)
                {
                    foreach (String drop in id.drops)
                    {
                        monster.Drops += drop + "\n";
                    }
                }
                else
                {
                    monster.Drops = "No drops";
                }

                monster.PhotoURL = id.image;

                regMonsters.Add(monster);
            }

            //return list of monsters
            return regMonsters;
        }
    }
}
