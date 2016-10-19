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

namespace TerrariaFormatter
{
    public class Database
    {
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<BsonDocument> collection;

        public Database()
        {
            var host = "localhost";
            int port = 27017;
            var dbName = "pvpcontroller";

            client = new MongoClient($"mongodb://{host}:{port}");
            db = client.GetDatabase(dbName);
            collection = db.GetCollection<BsonDocument>("projectiles");
        }

        public void insertProjectile(Projectile projectile)
        {
            var document = new BsonDocument
            {
                { "NetID", projectile.type },
                { "Name", projectile.name },
                { "DamageRatio", 1f },
                { "VelocityRatio", 1f },
                { "Banned", false }
            };

            collection.InsertOne(document);
        }
    }
}