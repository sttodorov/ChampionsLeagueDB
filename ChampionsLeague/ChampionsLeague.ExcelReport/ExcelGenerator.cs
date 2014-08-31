namespace ChampionsLeague.ExcelReport
{
    using System.Linq;
    using ExcelLibrary.SpreadSheet;
    public class ExcelGenerator
    {
        public SQLiteDbData SQLiteDb { get; private set; }
        public MySqlDbData MySqlDb { get; private set; }

        public ExcelGenerator()
        {
            this.SQLiteDb = new SQLiteDbData();
            this.MySqlDb = new MySqlDbData();

        }

        public void GenerateReport()
        {
            var workbook = new Workbook();
            var worksheet = new Worksheet("Report");
            int i = 0;
            var allplayers = this.MySqlDb.GetAllPlayers();
            foreach (var team in this.MySqlDb.GetAllTeams())
            {
                worksheet.Cells[i, 0]= new Cell(team.TeamName);
                i++;
                foreach (var player in allplayers.Where(p => p.TeamId == team.TeamId))
                {
                    int playertax = this.SQLiteDb.GetAll().FirstOrDefault(p => p.FirstName == player.FirstName && p.LastName == player.LastName).CardsCount * 50;
                    worksheet.Cells[i, 0] = new Cell(player.FirstName + " " + player.LastName);
                    worksheet.Cells[i, 1] = new Cell(player.Salary);
                    worksheet.Cells[i, 2]= new Cell(playertax);
                    worksheet.Cells[i, 3]= new Cell(player.Salary - playertax);
                    i++;
                }
            }
            workbook.Worksheets.Add(worksheet);
            workbook.Save(@"..\..\SalariesReport.xls");
        }



    }
}
