using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;

namespace VinylRecordsApplication_2.Classes
{
    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Format { get; set; }
        public int Size { get; set; }
        public int IdManufacturer { get; set; }
        public float Price { get; set; }
        public int IdState { get; set; }
        public string Description { get; set; }

        public static IEnumerable<Record> AllRecords()
        {
            List<Record> records = new List<Record>();
            DataTable recordQuery = Classes.DBConnection.Connection("SELECT * FROM [dbo].[Record]");
            foreach (DataRow row in recordQuery.Rows)
            {
                records.Add(new Record()
                {
                    Id = Convert.ToInt32(row[0]),
                    Name = Convert.ToString(row[1]),
                    Year = Convert.ToInt32(row[2]),
                    Format = Convert.ToInt32(row[3]),
                    Size = Convert.ToInt32(row[4]),
                    IdManufacturer = Convert.ToInt32(row[5]),
                    Price = float.Parse(row[6].ToString()),
                    IdState = Convert.ToInt32(row[7]),
                    Description = Convert.ToString(row[8]),
                });
            }
                return records;
        }

        public void Save(bool Update = false)
        {
            string CorrectPrice = this.Price.ToString().Replace(",", ".");
            if (Update == false)
            {
                Classes.DBConnection.Connection($"INSERT INTO [dbo].[Record] ([Name], [Year], [Format], [Size], [IdManufacturer], [Price], [IdState], [Description]) VALUES(N'{this.Name}', {this.Year}, {this.Format}, {this.Size}, {this.IdManufacturer}, {CorrectPrice}, {this.IdState}, N'{this.Description}')");
                
                this.Id = Record.AllRecords().Where(x => x.Name == this.Name && x.Year == this.Year && x.Format == this.Format && x.Size == this.Size && x.IdManufacturer == this.IdManufacturer && x.IdState == this.IdState && x.Description == this.Description).First().Id;
            }
            else
            {
                Classes.DBConnection.Connection($"UPDATE [dbo].[Record] SET [Name] = N'{this.Name}', [Year] = {this.Year}, [Format] = {this.Format}, [Size] = {this.Size}, [IdManufacturer] = {this.IdManufacturer}, [Price] = {CorrectPrice}, [IdState] = {this.IdState}, [Description] = N'{this.Description}' WHERE [Id] = {this.Id}");
            }
        }

        public void Delete()
        {
            Classes.DBConnection.Connection($"DELETE FROM [dbo].[Record] WHERE [Id] = {this.Id}");
        }

        public static void Export(string path, IEnumerable<Record> records)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Виниловые пластинки";

            string[] headers = { "ID", "Наименование", "Год", "Формат", "Размер", "Цена", "Описание" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1] = headers[i];
                worksheet.Cells[1, i + 1].Font.Bold = true;
            }

            int rowIndex = 2;
            foreach (var item in records)
            {
                worksheet.Cells[rowIndex, 1] = item.Id;
                worksheet.Cells[rowIndex, 2] = item.Name;
                worksheet.Cells[rowIndex, 3] = item.Year;
                worksheet.Cells[rowIndex, 4] = item.Format == 0 ? "Моно" : "Стерео";

                string sizeText = "";
                switch (item.Size)
                {
                    case 0: sizeText = "7 дюймов"; break;
                    case 1: sizeText = "10 дюймов"; break;
                    case 2: sizeText = "12 дюймов"; break;
                    default: sizeText = "Иной"; break;
                }
                worksheet.Cells[rowIndex, 5] = sizeText;
                worksheet.Cells[rowIndex, 6] = item.Price;
                worksheet.Cells[rowIndex, 7] = item.Description;

                rowIndex++;
            }

            worksheet.Columns.AutoFit();

            try
            {
                workbook.SaveAs(path);
                System.Windows.MessageBox.Show("Данные успешно экспортированы!", "Успех");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка");
            }
            finally
            {
                workbook.Close();
                excelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
        }
    }
}
