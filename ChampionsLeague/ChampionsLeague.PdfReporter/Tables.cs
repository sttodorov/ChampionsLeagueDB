#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   PDFsharp Team (mailto:PDFsharpSupport@pdfsharp.de)
//
// Copyright (c) 2001-2009 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
namespace ChampionsLeague.PdfReporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;

    using ChampionsLeague.Model;

    internal class Tables
    {
        public static void DefineTables(Document document, List<IGrouping<DateTime, Match>> matches)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Matches Overview", "Heading1");

            DemonstrateSimpleTable(document, matches);
        }

        public static void DemonstrateSimpleTable(Document document, List<IGrouping<DateTime, Match>> matchesGroupedByDate)
        {
            foreach (var groupedMatches in matchesGroupedByDate)
            {
                var totalAttendance = 0;
                //Adjust the date
                var dateToPrint = groupedMatches.Key.Date.ToString("MM-dd-yyyy");
                document.LastSection.AddParagraph(dateToPrint, "Heading2");

                Table table = new Table();
                table.TopPadding = 3;
                table.BottomPadding = 3;
                table.Borders.Width = 0.75;

                //Adjust the number of columns of the table
                Column column = table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;
                table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;
                table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;
                table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Center;
                table.AddColumn(Unit.FromCentimeter(3));
                column.Format.Alignment = ParagraphAlignment.Right;

                Row row = table.AddRow();
                row.Shading.Color = Colors.LightGray;
                Cell cell = row.Cells[0];
                cell.AddParagraph("Stadium");
                cell = row.Cells[1];
                cell.AddParagraph("Town");
                cell = row.Cells[2];
                cell.AddParagraph("Host Team");
                cell = row.Cells[3];
                cell.AddParagraph("Away Team");
                cell = row.Cells[4];
                cell.AddParagraph("Attendance");

                foreach (var match in groupedMatches)
                {
                    row = table.AddRow();
                    cell = row.Cells[0];
                    cell.AddParagraph(match.Stadium.Name);
                    cell = row.Cells[1];
                    cell.AddParagraph(match.Stadium.Town.TownName);
                    cell = row.Cells[2];
                    cell.AddParagraph(match.HostTeam.TeamName);
                    cell = row.Cells[3];
                    cell.AddParagraph(match.GuestTeam.TeamName);
                    cell = row.Cells[4];
                    cell.AddParagraph(match.Attendance.ToString());
                    totalAttendance += match.Attendance;
                }

                row = table.AddRow();
                cell = row.Cells[0];
                row.Cells[0].MergeRight = 4;
                cell.AddParagraph("Total attendance for the round: " + totalAttendance.ToString());

                table.SetEdge(0, 0, 5, groupedMatches.Count() + 1, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
                document.LastSection.Add(table);
            }
        }
    }
}
