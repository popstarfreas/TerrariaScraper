using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Mono.Data.Sqlite;
using MySql.Data.MySqlClient;
using TShockAPI;
using TShockAPI.DB;
using Terraria;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace TerrariaScraper
{
    public class Database
    {
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> projCollection;
        private IMongoCollection<BsonDocument> weaponCollection;

        public Database()
        {
            var host = "localhost";
            int port = 27017;
            var dbName = "pvpcontroller";

            client = new MongoClient($"mongodb://{host}:{port}");
            db = client.GetDatabase(dbName);
            projCollection = db.GetCollection<BsonDocument>("projectiles");
            weaponCollection = db.GetCollection<BsonDocument>("weapons");
        }

        public void insertProjectile(Projectile projectile)
        {
            var document = new BsonDocument
            {
                { "NetID", projectile.type },
                { "Name", projectile.name },
                { "DamageRatio", 1f },
                { "VelocityRatio", 1f },
                { "MaxDamage", -1},
                { "MinDamage", -1},
                { "Banned", false }
            };

            projCollection.InsertOne(document);
        }
        // "NetID":3349,"Name":"Exotic Scimitar","BaseDamage":20,"BaseVelocity":0,"CurrentDamage":200
        // "CurrentVelocity":0,"BaseUseTime":18,"CurrentUseTime":18,"WeaponType":"melee","Banned":false,"MaxDamage":9,"MinDamage":-1}

        public void insertWeapon(Item weapon)
        {
            string weaponType = "Ranged";
            if (weapon.magic)
            {
                weaponType = "Magic";
            }
            else if(weapon.melee)
            {
                weaponType = "Melee";
            }

            var document = new BsonDocument
            {
                { "NetID", weapon.type },
                { "Name", weapon.name },
                { "BaseDamage", weapon.damage },
                { "BaseVelocity", weapon.velocity.ToString() },
                { "CurrentDamage", weapon.damage },
                { "CurrentVelocity", weapon.velocity.ToString() },
                { "BaseUseTime", weapon.useTime },
                { "CurrentUseTime", weapon.useTime },
                { "WeaponType", weaponType },
                { "MaxDamage", -1},
                { "MinDamage", -1},
                { "Banned", false }
            };

            weaponCollection.InsertOne(document);
        }
    }
}