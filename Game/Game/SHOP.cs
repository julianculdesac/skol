using LiTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Game
{
    public class SHOP
    {



        public static void LoadShop(Player p)
        {
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            int potionP;
            int amorP;
            int weaponp;
            int diffP;

            while (true)
            {
                potionP = 20;
                amorP = 20 * (p.armorValue+1);
                weaponp = 10 * p.weaponValue;
                diffP = 30 + 100 * p.mods;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("XXXXXX 𐌔𐋅Ꝋ𐌐 XXXXXX\n");
                Console.WriteLine("==================");
                Console.WriteLine("|W|eapon:        " + weaponp);
                Console.WriteLine("|A|rmor:         " + amorP);
                Console.WriteLine("|P|otions:      " + potionP);
                Console.WriteLine("|D|iffMod:      " + diffP);
                Console.WriteLine("==================");
                Console.WriteLine("|E|xit\n\n");
                Console.WriteLine("|Q|uit Game");

                Console.WriteLine("IIII MY STATS IIII\n");
                Console.WriteLine("Current Health:   " + p.health);
                Console.WriteLine("Coins:            " + p.coins);
                Console.WriteLine("==================");
                Console.WriteLine("|Weapon Strengh:  " + p.weaponValue);
                Console.WriteLine("|Armor Durab:  " + p.armorValue);
                Console.WriteLine("Potions:          " + p.potion);
                Console.WriteLine("Difficulty Mods:  " + p.mods);
                Console.WriteLine("==================");


                string input = Console.ReadLine().ToLower();

                if (input == "p" || input == "potion")
                {
                    
                    TryBuy("potion", potionP, p);
                }
                else if (input == "w" || input == "weapon")
                {
                    
                    TryBuy("weapon", weaponp, p);
                }
                else if (input == "a" || input == "armor")
                {
                    
                    TryBuy("armor", amorP, p);
                }
                else if (input == "d" || input == "diff mod")
                {
                    
                    TryBuy("diff", diffP, p);
                }

                else if (input == "q" || input == "quit")
                {
                    Program.Quit();
                }
                else if (input == "e" || input == "exit")
                {
                    
                    break;
                }

            }
        }
        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins >= cost)
            {
                if (item == "potion")
                    p.potion++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "armor")
                    p.armorValue++;
                else if (item == "dif")
                    p.mods++;

                p.coins -= cost;
            }
            else
            {
                Console.WriteLine("Not Enough Gold!!");
            }
        }
    }
}
//TBA