using Game;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LiTL
{
   
    class Program
    {
        
        public static Player currentPlayer = new Player();
        public static bool mainloop = true;

        static void Main(string[] args)
        {
            string saveDirectory = @"C:\Users\Julian\OneDrive\Desktop\saves\";

            if (!Directory.Exists(saveDirectory))
            {
                try
                {
                    Directory.CreateDirectory(saveDirectory);
                }
                catch (Exception ex)
                {
                    return;
                }
            }

            currentPlayer = Load(out bool newP);

            if (newP)
            {
                Encounters.FirstEncounter();

            }

            while (mainloop)
            {
                Encounters.RandomEncounter();
            }
        }


        

        public static Player NewStart(int i)
        {
            Console.Clear();
            Player p = new Player();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t██   ▄▄▄▄   ▄▄▄  ▄▄▄▄▄        ███  ██  █          ██    ███   ▄▄▄  █  █ ▄▄▄   ███ ██  █ ▄▄▄▄▄ █  █");
            Console.WriteLine("\t██   █  █   █      █           █   █ █ █          ██   █▄▄▄█  █▄▄▀  ██  █▄▄▀   █  █ █ █   █   ████");
            Console.WriteLine("\t▀▀▀  ▀▀▀▀ ▀▀▀      █          ███  █  ██          ▀▀▀  █   █  █▄▄▀  █   █  ▀▄ ███ █  ██   █   █  █");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n\nEnter Player Name: ");
            p.name = Console.ReadLine();
            p.id = i;

           
            Console.Clear();
            Console.WriteLine("You arouse from a deep slumber,\nWith no recollection of where and who you are...");
            if (p.name == "")
            {
                Console.WriteLine("You chose to remain nameless.");
            }
            else
            {
                Console.WriteLine("\nYou give yourself a name: " + p.name);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("You grope around in the Darkness until you find a door handle.");
                Console.WriteLine("You Pry open the Door, You see creature with great animosity Glaring at you\n");
            }
            return p;
        }

        public static void Quit()
        {
            Save();
            Environment.Exit(0);
            

        }
        // SERIALIZED BY XML >>PLAYER.CS  SAVE THE PLAYER'S STATS

        public static void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Player));
            string path = @"C:\Users\Julian\OneDrive\Desktop\saves\" + currentPlayer.id.ToString() + ".level";
            using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(file, currentPlayer);
            }
        }
        
        public static Player Load(out
            bool newP)
        {
            newP = false;
            Console.Clear();
           
            string[] paths = Directory.GetFiles(@"C:\Users\Julian\OneDrive\Desktop\saves");

            List<Player> players = new List<Player>();
            int Idcount = 0;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Player));

            foreach (string p in paths)
            {
                try
                {
                    using (FileStream file = File.Open(p, FileMode.Open))
                    {
                        Player player = (Player)xmlSerializer.Deserialize(file);
                        players.Add(player);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading player from file " + p + ": " + ex.Message);
                }
            }
            Idcount = players.Count;


            while (true)
            {

                Console.Clear();


                if (players.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\t██   ▄▄▄▄   ▄▄▄  ▄▄▄▄▄        ███  ██  █          ██    ███   ▄▄▄  █  █ ▄▄▄   ███ ██  █ ▄▄▄▄▄ █  █");
                    Console.WriteLine("\t██   █  █   █      █           █   █ █ █          ██   █▄▄▄█  █▄▄▀  ██  █▄▄▀   █  █ █ █   █   ████");
                    Console.WriteLine("\t▀▀▀  ▀▀▀▀ ▀▀▀      █          ███  █  ██          ▀▀▀  █   █  █▄▄▀  █   █  ▀▄ ███ █  ██   █   █  █");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\n \t\t\t\t\tType 'create' to create a new Player\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\t██   ▄▄▄▄   ▄▄▄  ▄▄▄▄▄        ███  ██  █          ██    ███   ▄▄▄  █  █ ▄▄▄   ███ ██  █ ▄▄▄▄▄ █  █");
                    Console.WriteLine("\t██   █  █   █      █           █   █ █ █          ██   █▄▄▄█  █▄▄▀  ██  █▄▄▀   █  █ █ █   █   ████");
                    Console.WriteLine("\t▀▀▀  ▀▀▀▀ ▀▀▀      █          ███  █  ██          ▀▀▀  █   █  █▄▄▀  █   █  ▀▄ ███ █  ██   █   █  █");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nChoose your Player: ");
                    foreach (Player p in players)
                    {
                        Console.Write("\n \t\t\t\t\t");
                        Console.WriteLine("▌▌" + p.id + ":" + p.name + "▌▌" + "Current Coins =" + p.coins + "▌▌" + "Weapon Strength=" + p.weaponValue+ "▌▌");
                       
                    }
                    Console.WriteLine("\nType 'delete:id' to delete a save file.");
                }


                string[] data = Console.ReadLine().Split(':');

                try
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player player in players)
                            {
                                if (player.id == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("There's no player with that ID~ press any key to continue");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Your ID needs to be a number~ press any key to continue");
                            Console.ReadKey();
                        }
                    }
                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(Idcount);
                        newP = true;
                        return newPlayer;
                    }


                    //DELETE MECHANIC  based on the provided player ID

                    else if (data[0].StartsWith("delete"))
                    {
                        int idToDelete = int.Parse(data[1]);
                        DeleteSave(idToDelete);
                        players = LoadPlayers();
                    }
                    else
                    {
                        foreach (Player player in players)
                        {
                            if (player.name == data[0])
                            {
                                return player;
                            }
                        }
                        Console.WriteLine("There's no player with that name~ press any key to continue");
                        Console.ReadKey();
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Your ID needs to be a number~ press any key to continue");
                    Console.ReadKey();
                }
            }
        }

        private static void DeleteSave(int id)
        {
            string path = @"C:\Users\Julian\OneDrive\Desktop\saves\" + id.ToString() + ".level";
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("Save file deleted successfully.");
            }
            else
            {
                Console.WriteLine("No save file found with that ID.");
            }
            Console.ReadKey();
        }

        // UPDATEE 
        private static List<Player> LoadPlayers()
        {
            List<Player> players = new List<Player>();
            string[] paths = Directory.GetFiles(@"C:\Users\Julian\OneDrive\Desktop\saves\");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Player));

            foreach (string p in paths)
            {
                using (FileStream file = File.Open(p, FileMode.Open))
                {
                    Player player = (Player)xmlSerializer.Deserialize(file);
                    players.Add(player);
                }

            }
            return players;
        }

      
    }
}

/*
 * 
Player Management: ---allows for the creation, loading, and saving of player profiles. via save laod mechanic

File Handling: -- store player data in XML format, ensuring persistence across game sessions.

User Interaction: --Mainly in Encounters.cs

Encounters: The game features random currently 3 types encounters basic/special/boss (not including the combat tutorial)*/

