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

        public void ReadFile(string fileName, string cellRange)
        {
            string path = Path.Combine(this.importPath, fileName);

            if (!File.Exists(path))
            {
                Console.WriteLine("File {0} do not exist", fileName);
            }

            using (ZipFile zip = ZipFile.Read(path))
            {
                if (Directory.Exists(this.tempFolderPath))
                {
                    Directory.Delete(this.tempFolderPath, true);
                }

                Directory.CreateDirectory(this.tempFolderPath);

                foreach (ZipEntry entry in zip)
                {
                    if (!entry.IsDirectory)
                    {
                        string fullName = entry.FileName;
                        string filePath = Path.Combine(this.tempFolderPath, fullName);

                        var excelFile = zip[fullName];
                        excelFile.Extract(this.tempFolderPath);
                        this.ReadExcelFile(fullName, cellRange);

                        File.Delete(filePath);
                    }
                }

                Directory.Delete(this.tempFolderPath, true);
            }

            dbContext.SaveChanges();
        }

        public void ReadExcelFile(string fileName, string cellRange)
        {
            string filePath = Path.Combine(this.tempFolderPath, fileName);
            string leagueName = ParseLeague(fileName);

            // Connection String for Excel 97-2003 Format (.XLS)
            string connectionString = string.Format(@" Provider = Microsoft.Jet.OLEDB.4.0; Data Source= {0};  Extended Properties = 'Excel 8.0; HDR=Yes' ", filePath);

            OleDbConnection dbConnection = new OleDbConnection(connectionString);
            dbConnection.Open();
            using (dbConnection)
            {
                // get first sheet name
                var sheets = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                string sheetName = sheets.Rows[0]["TABLE_NAME"].ToString();

                string sqlCommand = string.Format("SELECT * FROM [{0}{1}]", sheetName, cellRange);
                OleDbCommand command = new OleDbCommand(sqlCommand, dbConnection);
                OleDbDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var currentMatch = ReadExcelLine(reader, fileName);
                        if (currentMatch == null)
                        {
                            break;
                        }

                        dbContext.Matches.Add(currentMatch);
                    }
                }
            }
        }
 
        private Match ReadExcelLine(OleDbDataReader reader, string fileName)
        {
            var stadiumNameObj = reader["Stadium Name"];
            if (stadiumNameObj == DBNull.Value)
            {
                return null;
            }

            string stadiumName = ((string)stadiumNameObj).Trim();
            string hostTeamName = ((string)reader["Host team"]).Trim();
            string awayTeamName = ((string)reader["Away team"]).Trim();
            int attendance = (int)(double)reader["Attendance"];

            DateTime date = this.ParseDate(fileName);

            var stadiumId = dbContext.Stadiums
                                     .SearchFor(s => s.Name == stadiumName)
                                     .Select(s => s.StadiumId)
                                     .First();

            var hostTeamId = dbContext.Teams
                                      .SearchFor(t => t.TeamName == hostTeamName)
                                      .Select(t => t.TeamId)
                                      .First();

            var awayTeamId = dbContext.Teams
                                      .SearchFor(t => t.TeamName == awayTeamName)
                                      .Select(t => t.TeamId)
                                      .First();

            var currentMatch = new Match()
            {
                HostTeamId = hostTeamId,
                GuestTeamId = awayTeamId,
                StadiumId = stadiumId,
                Attendance = attendance,
                Date = date
            };

            return currentMatch;
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

        private string ParseLeague(string filename)
        {
            string[] nameParts = filename.Split(new string[] { "Matches", "/" }, StringSplitOptions.RemoveEmptyEntries);

            if (nameParts.Length != 3)
            {
                throw new ArgumentException(string.Format("Invalid file name \"{0}\"", filename));
            }

            string leagueName = nameParts[1].Replace("-", " ").Trim();
            return leagueName;
        }
    }
}

