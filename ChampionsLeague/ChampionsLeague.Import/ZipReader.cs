namespace ChampionsLeague.Import
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;

    using Ionic.Zip;

    using ChampionsLeague.Data;
    using ChampionsLeague.Model;

    public class ZipReader
    {
        private ChampionsLeagueData dbContext;
        private readonly string tempFolderPath;
        private readonly string importPath;

        public ZipReader(ChampionsLeagueData context, string importPath, string tempPath)
        {
            this.dbContext = context;
            this.tempFolderPath = tempPath;
            this.importPath = importPath;
        }

        public ICollection<Match> ReadFile(string fileName)
        {
            string path = Path.Combine(this.importPath, fileName);

            ICollection<Match> result = new List<Match>();

            if (!File.Exists(path))
            {
                Console.WriteLine("File {0} do not exist", fileName);
                return result;
            }

            using (ZipFile zip = ZipFile.Read(path))
            {
                Directory.CreateDirectory(this.tempFolderPath);

                foreach (ZipEntry entry in zip)
                {
                    if (!entry.IsDirectory)
                    {
                        string fullName = entry.FileName;
                        string filePath = Path.Combine(this.tempFolderPath, fullName);
                        if (!File.Exists(filePath))
                        {
                            var excelFile = zip[fullName];
                            excelFile.Extract(this.tempFolderPath);
                        }

                        var fileResult = this.ReadExcelFile(fullName, "B3:E50");

                        foreach (var match in fileResult)
                        {
                            result.Add(match);
                        }

                        File.Delete(filePath);
                    }
                }

                Directory.Delete(this.tempFolderPath, true);
            }

            return result;
        }

        public ICollection<Match> ReadExcelFile(string fileName, string range)
        {
            string filePath = Path.Combine(this.tempFolderPath, fileName);

            ICollection<Match> result = new List<Match>();

            // Connection String for Excel 97-2003 Format (.XLS)
            string connectionString = string.Format(@" Provider = Microsoft.Jet.OLEDB.4.0; Data Source= {0};  Extended Properties = 'Excel 8.0; HDR=Yes' ", filePath);

            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            using (dbConnection)
            {
                // get first sheet name
                var sheets = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();

                string sqlCommand = string.Format("SELECT * FROM [{0}{1}]", sheetName, range);
                OleDbCommand command = new OleDbCommand(sqlCommand, dbConnection);
                OleDbDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var firstColumn = reader["ProductID"];
                        if (firstColumn == DBNull.Value)
                        {
                            break;
                        }

                        int id = (int)(double)firstColumn;
                        int name = (int)(double)reader["Quantity"];
                        decimal score = (decimal)(double)reader["Sum"];

                        DateTime date = this.ParseDate(fileName);

                        // Console.WriteLine("{0} | {1} | {2}", id, name, score);

                        var currentMatch = new Match()
                        {
                            Date = date
                        };                        

                        result.Add(currentMatch);
                    }
                }
            }
            
            return result;
        }

        private DateTime ParseDate(string path)
        {
            string[] nameParts = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 2)
            {
                throw new ArgumentException("Invalid file names in the Zip file");
            }

            string entryName = nameParts[nameParts.Length - 1];
            string folder = nameParts[0];
            DateTime date = DateTime.Parse(folder);
            return date;
        }
    }
}

