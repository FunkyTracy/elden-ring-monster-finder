using MonsterFormatLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BossLib
{
    public class BossAPI : IMonsterAPI
    {
        //Gets data from the API and returns the data in the form of BossData classes
        public BossData GetData() 
        {
            BossData results = null;

            //call API for bosses
            const string url = "https://eldenring.fanapis.com/api/bosses?limit=100";
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString(url);

                results = JsonConvert.DeserializeObject<BossData>(json);
            }

            return results;
        }

        //Gets data from the BossData classes and turns it into the Monster Class format
        public ObservableCollection<Monster> GetMonsters()
        {
            ObservableCollection<Monster> bosses = new ObservableCollection<Monster>();

            //get data from internet
            BossData results = GetData();

            //add each monster from the json to the list
            foreach (Datum id in results.data)
            {
                Monster monster = new Monster();

                monster.Name = id.name + " (Boss)";

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

                bosses.Add(monster);
            }

            //return list of monsters
            return bosses;
        }
    }
}
