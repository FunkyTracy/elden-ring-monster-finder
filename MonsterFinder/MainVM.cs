using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BossLib;
using MonsterFormatLib;
using RegularMonsterLib;

namespace MonsterFinder
{
    public class MainVM : INotifyPropertyChanged
    {
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyProperty([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private List<String> mainLocations = new List<string> { "Anywhere", "Limgrave", "Caelid", "Liurnia", "Altus Plateau", "Mountaintops of the Giants", 
                                                                   "Crumbling Farum Azula", "Siofra River", "Ainsel River", "Deeproot Depths"};

        private ObservableCollection<Monster> allMonsters;
        private Monster displayMonster;

        private string location;

        private string drops;

        //list to store the returned monsters in
        public ObservableCollection<Monster> AllMonsters
        {
            get { return allMonsters; }
            set { this.allMonsters = value; }
        }
        //drop down menu items for locations to choose from
        public List<String> MainLocations { get { return mainLocations; } }
        //Monster to be displayed within the UI
        public Monster DisplayMonster 
        { 
            get { return displayMonster; } 
            set { this.displayMonster = value; NotifyProperty(); }
        }
        //Stores the selected location from the UI
        public string Location
        {
            get { return location; }
            set { location = value; NotifyProperty(); }
        }
        //Stores the drop input from the UI
        public string Drops
        {
            get { return drops; }
            set { drops = value; NotifyProperty(); }
        }

        public Random rand;

        //Commands to bind the to UI
        public ICommand FindMonsterByLocationButton { get; set; }
        public ICommand FindMonsterByDrop { get; set; }
        public ICommand DoubleClickImage { get; set; }

        public MainVM()
        {
            rand = new Random();

            AllMonsters = new ObservableCollection<Monster>();
            AllMonsters = getAllMonsters();

            DisplayMonster = new Monster();

            FindMonsterByLocationButton = new RelayCommand(FindMonsterByLocationClick);
            FindMonsterByDrop = new RelayCommand(FindMonsterByDropClick);
            DoubleClickImage = new RelayCommand(OpenImage);
        }

        //function to get all monster in bosses and regular monsters from API. 
        //Calls functions in the separate monster libraries. 
        public ObservableCollection<Monster> getAllMonsters()
        {
            //TODO: Reflection? Go through assembly to find all the monster plugins?

            ObservableCollection<Monster> monCollection = new ObservableCollection<Monster>();

            RegularMonsterAPI regMonApi = new RegularMonsterAPI();

            BossAPI bossApi = new BossAPI();

            foreach (Monster mon in regMonApi.GetMonsters()) 
            {
                monCollection.Add(mon);
            }

            foreach(Monster mon in bossApi.GetMonsters()) 
            { 
                monCollection.Add(mon);
            }

            return monCollection;
        }

        //function to update the displayMonster if they click the search by location button
        public void FindMonsterByLocationClick()
        {
            DisplayMonster = GetMonsterAtLocation(Location);
        }

        //function to update the displayMonster if they click the search by drops button
        public void FindMonsterByDropClick()
        {
            DisplayMonster = GetMonsterByDrops();
        }

        //Function to search for monsters based on the location that was input from the drop down menu
        //returns the monster found
        public Monster GetMonsterAtLocation(string loc)
        {
            ObservableCollection<Monster> foundMonsters = new ObservableCollection<Monster>();

            if (loc != null) //if there was a location selected
            {
                if (loc == "Anywhere")   //if they select anywhere, use the random monster finder for anywhere
                {
                    Monster anywhereMonster = new Monster();
                    anywhereMonster = GetMonsterAnywhere();
                    return anywhereMonster;
                }
                else //if they selected a location on the map
                {
                    foreach (Monster mon in allMonsters)
                    {
                        if (mon.Location != null)
                        {
                            if (mon.Location.Contains(loc))
                            {
                                foundMonsters.Add(mon);
                            }
                        }
                    }
                } 
            }

            int idx = rand.Next(0, foundMonsters.Count);

            if (foundMonsters.Count > 0)
                return foundMonsters[idx];
            else  //if there were no monsters found
                return new Monster { Drops = "", Location = "", Name = "Sorry! No Monsters Found in this Area.\n Try Somewhere Else!", PhotoURL = "" };
        }

        //Function grabs a random monster from all of the monsters found
        //returns the random monster found
        public Monster GetMonsterAnywhere()
        {
            int idx = rand.Next(0, allMonsters.Count);

            return allMonsters[idx];
        }

        //Function to search through the monsters grabbed from the API and look for ones based on the drop input in the text box
        //returns the monster found
        public Monster GetMonsterByDrops()
        {
            if(Drops != null)   //if there was a drop entered
            {
                ObservableCollection<Monster> foundMonsters = new ObservableCollection<Monster>();

                foreach (Monster mon in allMonsters)
                {
                    if (mon.Drops.Contains(Drops))
                    {
                        foundMonsters.Add(mon);
                    }
                }

                if(foundMonsters.Count > 0) //if monsters with that drop were found
                {
                    int idx = rand.Next(0, foundMonsters.Count);
                    return foundMonsters[idx];
                }
                else //if no monsters with the drop were found
                {
                    return new Monster { Drops = "", Location = "", Name = "Sorry! No monsters found by the drop you entered.\nTry Something else or check your spelling!\n(Search is case sensitive)", PhotoURL = "" };
                }
            }
            else //If there was no drop entered
            {
                return new Monster { Drops = "", Location = "", Name = "Sorry! No monsters found by the drop you entered.\nTry Something else or check your spelling!\n(Search is case sensitive)", PhotoURL = "" };
            }
        }

        //Function opens a new window when the image is double clicked to show the monster image as a larger version
        public void OpenImage()
        {
            Monster mon = displayMonster;

            if (mon != null)
            {
                FullImage photoDetail = new FullImage()
                {
                    Url = mon.PhotoURL,
                };

                photoDetail.Show();
            }
        }
    }
}
