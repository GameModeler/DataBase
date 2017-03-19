using DataBase.Criteria;
using DataBase.Database;
using DataBase.Database.DbContexts;
using DataBase.Database.DbSettings;
using DataBase.Database.DbSettings.DbClasses;
using DataBase.Entities;
using System.Collections.Generic;

namespace DataBase
{
    class Program
    {

        public static async void InitTest()
        {
            DbManager dbManager = DbManager.Instance;
            var database = dbManager.Databases;

            var settingdb2 = DatabaseFactory
                                .MySqlDb
                                .Set
                                .DatabaseName("db2")
                                .Server("localhost")
                                .UserId("root")
                                .ToMySqlDatabase;

            // Get the database name
            string dbName = settingdb2.DatabaseName;

            // Database information
            //MySqlDatabase settingdb2 = new MySqlDatabase();
            //settingdb2.DatabaseName = "db2";
            //settingdb2.Server = "localhost";
            //settingdb2.UserId = "root";

            string connectionstring = settingdb2.ToConnectionString();

            SqLiteDatabase sqlDbSettings = new SqLiteDatabase();
                
            sqlDbSettings.Set.DatabaseName("sqLiteDb")
                             .DataSource(@"C:\Users\Anne\SQLDatabase\test.db.db");

            //sqlDbSettings.DatabaseName = "sqLiteDb";
            //sqlDbSettings.DataSource = @"C:\Users\Anne\SQLDatabase\test.db.db";

            // Fluent API
            //SqLiteDatabase sqlDbSet = new SqLiteDatabase();
            
            //sqlDbSet.Set
            //        .DatabaseName("sql")
            //        .DataSource(@"C: \Users\Anne\SQLDatabase\test.db.db");

            //MySqlDatabase dbset = DatabaseFactory.DatabaseSettings<MySqlDatabase>("dbSet");

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

            MySqlContext<Car> parking = new MySqlContext<Car>(settingdb2);
            await parking.InsertAsync(cars);

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

            var mydb = dbManager.GetDatabase("db2");

            GlobalContext<Car> dbContext = dbManager.ContextFactory<Car>();

            var sqlparking = dbContext.Context(settingdb2)
                                      .Context(sqlDbSettings);

            sqlparking.Insert(cars);

            //await sqlparking.InsertAsync(cars);

            // delete car from cars
            // await sqlparking.Delete(cars[0]);

            Car myCar = sqlparking.Get(0)[settingdb2];

            //////////////////////////////////////////////////////////////

            /////////////////////// CUSTOM QUERIES ///////////////////////

            Criteria.Criteria myQuery = new Criteria.Criteria();

            Criterion criterion = new Criterion(DbVerb.AND, "Manufacturer", DbOperator.LIKE, "Niss%");

            myQuery.AddDbLink("Car", DbLinks.FROM);
            myQuery.AddCriterion(criterion);

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
