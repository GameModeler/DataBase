using GMDataBase.Database;
using GMDataBase.Database.DbSettings;
using GMDataBase.Database.DbSettings.DbClasses;
using GMDataBase.Entities;
using GMDataBase.Interfaces;
using GMDataBase.Utils;
using GMLDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMDataBase
{
    class Program
    {

        public static async void InitTest()
        {
            // Database information
            MySqlDatabase settingdb2 = new MySqlDatabase();
            settingdb2.DatabaseName = "db2";
            settingdb2.Server = "localhost";
            settingdb2.UserId = "root";

            SqLiteDatabase sqlDbSettings = new SqLiteDatabase();
            sqlDbSettings.DatabaseName = "sqLiteDb";
            sqlDbSettings.DataSource = @"C:\Users\Anne\SQLDatabase\test.db.db";

            // MySqlDatabase dbset = DatabaseFactory.DatabaseSettings<MySqlDatabase>("dbSet");

            // 1. Création d'un manager par type de provider.
            // Va permettre de gérer plusieurs bases de données ayant le même provider
            //GmDbManager dbManager = new GmDbManager(ProviderType.MySQL);

            //SqliteContext<Car> sqlparking = new SqliteContext<Car>(sqlDbSettings);

            //2. Initialisation de la base avec l'entity + settings 

            //MySqlContext<Car> parking = null;

            List<Car> cars = new List<Car>();

            cars.Add(new Car { Manufacturer = "Nissan", Model = "370Z", Year = 2012 });
            cars.Add(new Car { Manufacturer = "Ford", Model = "Mustang", Year = 2013 });
            cars.Add(new Car { Manufacturer = "Chevrolet", Model = "Camaro", Year = 2012 });
            cars.Add(new Car { Manufacturer = "Dodge", Model = "Charger", Year = 2013 });

            //////////////////// DB CONTEXT (using) /////////////////////////

            //using (var sqlparking1 = new SqliteContext<Car>(sqlDbSettings))
            //{
            //    await sqlparking1.Insert(cars);
            //}

            //using (var parking1 = new MySqlContext<Car>(settingdb2))
            //{
            //    await parking1.Insert(cars);
            //}

            //////////////////// DB CONTEXT (task) /////////////////////////

            //MySqlContext<Car> parking = await Task.Run(() =>
            //{
            //    return new MySqlContext<Car>(settingdb2);
            //});

            //await parking.Insert(cars);

            //SqliteContext<Car> sqlParking = await Task.Run(() =>
            //{
            //    return new SqliteContext<Car>(sqlDbSettings);
            //});

            //await sqlParking.Insert(cars);

            //Task t = Task.Run(() =>
            //{
            //    parking = new MySqlContext<Car>(settingdb2);
            //});
            //t.Wait();

            //await Task.Factory.StartNew(() =>
            //{
            //    MySqlContext<Car> parking = new MySqlContext<Car>(settingdb2);
            //    await parking.Insert(cars);
            //});

            //////////////////// GLOBAL CONTEXT /////////////////////////

            GmDbManager dbManager = GmDbManager.Instance;

            GmDbContext<Car> dbContext = dbManager.ContextFactory<Car>();

            var sqlparking = dbContext
                                      .Context(settingdb2)
                                      .Context(sqlDbSettings);
           //                           .MySqlContext(settingdb2);

            await sqlparking.Insert(cars);

            //////////////////////////////////////////////////////////////

            //List<Cat> cats = new List<Cat>();

            //cats.Add(new Cat { Name = "Minou", Color = "Blanc", Year = 2012 });
            //cats.Add(new Cat { Name = "Felix", Color = "Noir", Year = 2013 });

            //MySqlContext<Cat> catHouse = new MySqlContext<Cat>(settingdb2);
            //await catHouse.Insert(cats);

            //SqliteContext<Cat> sqlCatHouse = new SqliteContext<Cat>(sqlDbSettings);
            //await sqlCatHouse.Insert(cats);
        }


        static void Main(string[] args)
        {
            InitTest();

        }
    }
}
