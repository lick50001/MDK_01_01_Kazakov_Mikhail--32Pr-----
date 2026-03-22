using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylRecordsApplication_2.Classes
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryCode { get; set; }
        public string Phone {  get; set; }
        public string Mail {  get; set; }

        public static IEnumerable<Manufacturer> AllManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            DataTable recordQuery = Classes.DBConnection.Connection("SELECT * FROM [dbo].[Manufacturer]");
            foreach (DataRow row in recordQuery.Rows)
                manufacturers.Add(new Manufacturer()
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = Convert.ToString(row[1]),
                    CountryCode = Convert.ToInt32(row[2]),
                    Phone = Convert.ToString(row[3]),
                    Mail = Convert.ToString(row[4]),
                });
            return manufacturers;
        }

        public void Save(bool Update = false)
        {
            if (Update == false)
            {
                Classes.DBConnection.Connection($"INSERT INTO [dbo].[Manufacturer]([Name], [CountryCode], [Phone], [Mail]) VALUES (N'{this.Name}', {this.CountryCode}, N'{this.Phone}', N'{this.Mail}')");

                this.Id = Manufacturer.AllManufacturers().Where(x => x.Name == this.Name && x.CountryCode == this.CountryCode && this.Phone == this.Phone && this.Mail == this.Mail).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection($"UPDATE [dbo].[Manufacturer] SET [Name] = N'{this.Name}', [CountryCode] = {this.CountryCode}, [Phone] = N'{this.Phone}', [Mail] = N'{this.Mail}' WHERE [Id] = {this.Id}");
            }
        }

        public void Delete() => Classes.DBConnection.Connection($"DELETE FROM [dbo].[Manufacturer] WHERE [Id] = {this.Id};");
    }
}
