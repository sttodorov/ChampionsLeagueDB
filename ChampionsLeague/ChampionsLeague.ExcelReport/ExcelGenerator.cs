namespace ChampionsLeague.ExcelReport
{
    using System.Linq;
    using System.Data.OleDb;
    public class ExcelGenerator
    {
        public SQLiteDbData SQLiteDb { get; private set; }
        public MySqlDbData MySqlDb { get; private set; }

        public ExcelGenerator(string pathToJson)
        {
            this.SQLiteDb = new SQLiteDbData();
            this.MySqlDb = new MySqlDbData(pathToJson);

        }

        public void GenerateReport()
        {
            var allplayers = this.MySqlDb.GetAllPlayers();
            
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\SalariesReport.xlsx; Extended Properties=""Excel 12.0 Xml;HDR=YES""");
            conn.Open();
            using (conn)
            {
                //Comment this two rows if you already have xlsx file.
                OleDbCommand createSheet = new OleDbCommand("CREATE TABLE Salaries(FullName nvarchar(50), InitialSalary int, Tax int, FinalSalary int)",conn);
                createSheet.ExecuteNonQuery();
                
                foreach (var team in this.MySqlDb.GetAllTeams())
	            {
                    OleDbCommand insertTeam = new OleDbCommand("INSERT INTO Salaries (Fullname) VALUES (@teamName)",conn);
                    insertTeam.Parameters.AddWithValue("@teamname", team.TeamName);
                    insertTeam.ExecuteNonQuery();

                    var playersInCurrentTeam = allplayers.Where(p => p.TeamId == team.TeamId);

                    foreach (var player in playersInCurrentTeam)
	                {
                        var findPlayer = this.SQLiteDb.GetAll().FirstOrDefault(p => p.FirstName == player.FirstName && p.LastName == player.LastName);
                        int playertax = 0;
                        if(findPlayer != null)
                        {
                            playertax = findPlayer.CardsCount * 50;
                        }
		                OleDbCommand insertInExcel = new OleDbCommand("INSERT INTO Salaries (FullName, InitialSalary, Tax, FinalSalary) VALUES (@name, @firstSalary, @tax, @finalSalary)", conn);
                        insertInExcel.Parameters.AddWithValue("@name", player.FirstName + " " + player.LastName);
                        insertInExcel.Parameters.AddWithValue("@firstSalary", player.Salary);
                        insertInExcel.Parameters.AddWithValue("@tax", playertax);
                        insertInExcel.Parameters.AddWithValue("@finalSalary", player.Salary - playertax);
                        insertInExcel.ExecuteNonQuery();
	                }
    	        }
            }

        }
    }
}
