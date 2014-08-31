namespace ChampionsLeague.ExcelReport
{
    using System.IO;
    using System.Web.Script.Serialization;

    using MySql.Data.MySqlClient;
    using ChampionsLeague.Model;

    public class MySqlDbData
    {
        private MySqlConnection MySqlDb;
        private JavaScriptSerializer jsnoSerializer;
        private string pathToFile;

        public MySqlDbData()
        {
            this.MySqlDb = new MySqlConnection(@"Server=localhost;Port=3306;Database=championsleague;IntegratedSecurity=yes;Uid=auth_windows;");
            this.jsnoSerializer = new JavaScriptSerializer();
            this.pathToFile = @"..\..\JsonReports";
        }

        public void ReadJsonReports()
        {
            var teamReports = Directory.GetFiles(this.pathToFile);
            int id = 1;
            foreach (var report in teamReports)
            {
                var reader = new StreamReader(this.pathToFile + @"\" + id + ".json");
                //Chose type of deserialization
                //var team = this.jsnoSerializer.Deserialize<string>(reader.ReadToEnd());

                //Save  to MySqlDbData
            }
        }




    }
}
