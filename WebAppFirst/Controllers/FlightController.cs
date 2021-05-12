using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAppFirst.Models;

namespace WebAppFirst.Controllers
{
    public class FlightController : ApiController
    {
        private void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = C:\\SQLITE\\Flights.db; Version = 3;"))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Flight> real_flights = new List<Flight>();

        public IEnumerable<Flight> Get()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = C:\\SQLITE\\Flights.db; Version = 3;"))
            {
                conn.Open();

                using (SQLiteCommand select_query = new SQLiteCommand("SELECT * from Flights", conn))
                {
                    using (SQLiteDataReader reader = select_query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            real_flights.Add(
                                  new Flight
                                  {
                                      Id = (long)reader["ID"],
                                      DestCountry = reader["DestCountry"].ToString(),
                                      OriginCountry = reader["OriginCountry"].ToString(),
                                      Remaining = (long)reader["Remaining"]
                                  });
                        }
                    }
                }
            }

            return real_flights;
        }

        public Flight Get(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source = C:\\SQLITE\\Flights.db; Version = 3;"))
            {
                conn.Open();

                using (SQLiteCommand select_query = new SQLiteCommand($"SELECT * from Flights WHERE Flights.ID = {id}", conn))
                {
                    using (SQLiteDataReader reader = select_query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flight flight = new Flight
                                  {
                                      Id = (long)reader["ID"],
                                      DestCountry = reader["DestCountry"].ToString(),
                                      OriginCountry = reader["OriginCountry"].ToString(),
                                      Remaining = (long)reader["Remaining"]
                                  };
                            return flight;
                        }
                    }
                }
            }
            return null;
        }

        public void Post([FromBody] Flight flight)
        {
            ExecuteNonQuery($"INSERT INTO Flights VALUES ('{flight.OriginCountry}', '{flight.DestCountry}', {flight.Remaining})");
        }

        public void Put(int id, [FromBody] Flight flight)
        {
            ExecuteNonQuery($"UPDATE Flights SET " +
                $"DestCountry = '{flight.DestCountry}',  " +
                $"OriginCountry = '{flight.OriginCountry}', " +
                $"Remaining = {flight.Remaining} WHERE Id = {flight.Id}");
        }

        public void Delete(long id)
        {
            ExecuteNonQuery($"DELETE from Flights WHERE Flights.ID = {id}");
        }
    }
}
