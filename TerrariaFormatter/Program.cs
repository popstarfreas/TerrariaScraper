using System;
using Terraria;

namespace TerrariaFormatter
{
    class Program
    {
        public static Database database;
        static void Main(string[] args)
        {
            // Necessary for item 269
            var player = new Terraria.Player();
            player.shirtColor = new Color(255, 255, 255);
            Terraria.Main.player[Terraria.Main.myPlayer] = player;

            // Necessary for projectile 221
            Terraria.Main.rand = new Random();

            database = new Database();
            insertProjectiles();
            string line = Console.ReadLine();
        }

        static void insertProjectiles()
        {
            try
            {
                Projectile proj = new Projectile();
                for (int i = 1; i < Terraria.Main.maxProjectileTypes; i++)
                {
                    proj.SetDefaults(i);
                    if ((proj.ranged || proj.magic || proj.melee) && proj.friendly)
                    {
                        database.insertProjectile(proj);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            Console.WriteLine("Finished");
        }

        static void insertWeapons()
        {
            try
            {
                Item item = new Item();
                for (int i = 1; i < Terraria.Main.maxItemTypes; i++)
                {
                    item.SetDefaults(i);
                    if (item.damage > 0 && (item.ranged || item.magic || item.melee) && !item.notAmmo)
                    {
                        Console.WriteLine(item.name + ": " + item.damage);
                        Console.WriteLine(item.ammo);
                        //database.AddItem(item);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
