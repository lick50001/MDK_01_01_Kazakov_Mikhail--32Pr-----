using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylRecordsApplication_2.Classes
{
    public class Supply
    {
        public int Id { get; set; }
        public int IdManufacrurer { get; set; }
        public int IdRecord { get; set; }
        public string DateDelivery { get; set; }
        public int Count { get; set; }

        public static IEnumerable<Supply> AllSupples()
        {
            List<Supply> supples = new List<Supply>();
            DataTable recordQuery = Classes.DBConnection.Connection("SELECT * FROM [dbo].[Supple]");
            foreach (DataRow row in recordQuery.Rows)
            {
                DateTime dt = new DateTime();
                DateTime.TryParse(row[3].ToString(), out dt);
                string CorrectDate = dt.Year + "-" + dt.Month + "-" + dt.Day;
                supples.Add(new Supply()
                {
                    Id = Convert.ToInt32(row[0]),
                    IdManufacrurer = Convert.ToInt32(row[1]),
                    IdRecord = Convert.ToInt32(row[2]),
                    DateDelivery = CorrectDate,
                    Count = Convert.ToInt32(row[4])
                });
            }
            return supples;
        }

        public void Save(bool Update = false)
        {
            if (Update == false)
            {
                Classes.DBConnection.Connection(
                    "INSERT INTO [dbo].[Supple]([IdManufacturer], [IdRecord], [DateDelivery], [Count]) " +
                    $"VALUES ({this.IdManufacrurer}, {this.IdRecord}, '{this.DateDelivery}', {this.Count});");

                this.Id = Supply.AllSupples().Where(
                    x => x.IdManufacrurer == this.IdManufacrurer &&
                    x.IdRecord == this.IdRecord &&
                    x.DateDelivery == this.DateDelivery &&
                    x.Count == this.Count).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection(
                    "UPDATE [dbo].[Supple] " +
                    "SET " +
                        $"[IdManufacturer] = {this.IdManufacrurer}, " +
                        $"[IdRecord] = {this.IdRecord}, " +
                        $"[DateDelivery] = '{this.DateDelivery}', " +
                        $"[Count] = {this.Count} " +
                    $"WHERE [Id] = {this.Id};");
            }
        }

        public void Delete()
        {
            Classes.DBConnection.Connection($"DELETE FROM [dbo].[Supple] WHERE [Id] = {this.Id};");
        }
    }
}