using System;
using System.Data.SQLite;
using WeatherApp.Entities;

namespace WeatherApp.Database
{
    public class Db
    {
        SQLiteConnection sqlite_conn;
        public Db()
        {
            sqlite_conn = CreateConnection();
            CreateTables();
        }

        private SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return sqlite_conn;
        }

        private void CreateTables()
        {
            SQLiteCommand sqlite_cmd;
            string[] createsqls = new string[] {
                "CREATE TABLE City (name VARCHAR(20), country VARCHAR(20), population INT, timezone INT, sunrise INT, sunset INT)",
                "CREATE TABLE Coord (lat INT, lon INT)",
                "CREATE TABLE WeatherForecast (cod VARCHAR(20), message INT, cnt INT)",
                "CREATE TABLE WeatherInfo (dt INT, dt_text VARCHAR(20))",
                "CREATE TABLE WeatherInfoMain (temp REAL, feels_like REAL, temp_min REAL, temp_max REAL, pressure INT, sea_level INT, grnd_level INT, humidity INT, temp_kf REAL)"
            };
            
            sqlite_cmd = sqlite_conn.CreateCommand();
            foreach (var sql in createsqls)
            {
                try
                {
                    sqlite_cmd.CommandText = sql;
                    sqlite_cmd.ExecuteNonQuery();    
                }
                catch (System.Exception)
                {
                }
            }
        }

        public void InsertData(WeatherForecast weatherForecast)
        {
            var city = weatherForecast.city;
            var cityCoord = weatherForecast.city.coord;

            SQLiteCommand sqlite_cmd;
            List<string> createsqls = new List<string> {
                $"INSERT INTO Coord (lat, lon) VALUES('{cityCoord.lat}', '{cityCoord.lon}'); ",
                $"INSERT INTO City (name, population, timezone, sunrise, sunset) VALUES('{city.name} ', '{city.population}', '{city.timezone}', '{city.sunrise}', '{city.sunset}'); ",
                $"INSERT INTO WeatherForecast (cod, message, cnt) VALUES('{weatherForecast.cod}', '{weatherForecast.message}', '{weatherForecast.cnt}'); ",
            };
            foreach (var weatherInfo in weatherForecast.list)
            {
                createsqls.Add($"INSERT INTO WeatherInfo (dt, dt_text) VALUES('{weatherInfo.dt}', '{weatherInfo.dt_txt}'); ");
                var weatherInfoMain = weatherInfo.main;
                createsqls.Add($"INSERT INTO WeatherInfoMain (temp, feels_like, temp_min, temp_max, pressure, sea_level, grnd_level, humidity, temp_kf) VALUES('{weatherInfoMain.temp}', '{weatherInfoMain.feels_like}', '{weatherInfoMain.temp_min}', '{weatherInfoMain.temp_max}', '{weatherInfoMain.pressure}', '{weatherInfoMain.sea_level}', '{weatherInfoMain.grnd_level}', '{weatherInfoMain.humidity}', '{weatherInfoMain.temp_kf}'); ");
            }

            sqlite_cmd = sqlite_conn.CreateCommand();
            foreach (var sql in createsqls)
            {
                sqlite_cmd.CommandText = sql;
                sqlite_cmd.ExecuteNonQuery();
            }
        }
    }
}
