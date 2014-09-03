namespace ChampionsLeague.PdfReporter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using MigraDoc.DocumentObjectModel;
    using MigraDoc.Rendering;

    using ChampionsLeague.Model;

    public class PdfReporter
    {
        public PdfReporter()
        {
        }

        public void CreateTableReport(List<IGrouping<DateTime, Match>> matches)
        {
            // Create a MigraDoc document
            Document document = Documents.CreateDocument(matches);

            //string ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();

            // Save the document...
            string filename = @"..\..\" +  Guid.NewGuid().ToString("N").ToUpper() + ".pdf";

            renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}
