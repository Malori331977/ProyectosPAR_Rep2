using ControlColegiadosBlazorServer.Pages.Certificaciones;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.JSInterop;
using ProyectoPAR.Models;
using ProyectoParLibs.Models.Configuracion;


namespace Blazor_PDF.PDF
{
    public class Report
    {

        private ReporteExt _reporteExt { get; set; }
        private Parametro _param { get; set; }
        private byte[] img { get; set; }
        public class ExportDocument
        {
            public MemoryStream memoryStream { get; set; }
            public Document pdf { get; set; }
            public PdfWriter? writer { get; set; }

            public ExportDocument()
            {
                memoryStream = new MemoryStream();
                pdf = new Document();
            }

        }
        public Report()
        {
        }

        private async Task<ExportDocument> CreateDocument(bool vRotate=true)
        {
            ExportDocument exportDocument = new ExportDocument();

            exportDocument.memoryStream = new MemoryStream();
            exportDocument.pdf = new Document();

            // Marge in centimeter, then I convert with .ToDpi()
            float margeLeft = 1.0f;
            float margeRight = 1.0f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;

            if (vRotate)
            {
                exportDocument.pdf.SetPageSize(PageSize.Letter.Rotate());
            }
            
            exportDocument.pdf.SetMargins(margeLeft, margeRight, margeTop, margeBottom);

            exportDocument.pdf.AddTitle("{PAR}+");
            exportDocument.pdf.AddAuthor("GSITCR");
            exportDocument.pdf.AddCreationDate();
            exportDocument.pdf.AddKeywords("PARGSITCR");
            exportDocument.pdf.AddSubject("ReporteVisita");

            exportDocument.writer = PdfWriter.GetInstance(exportDocument.pdf, exportDocument.memoryStream);

            //HEADER and FOOTER
            var fontStyle = FontFactory.GetFont("Verdana", 16, BaseColor.White);

            var labelFooter = new Chunk("Page", fontStyle);
            HeaderFooter footer = new HeaderFooter(new Phrase(labelFooter), true)
            {
                Border = Rectangle.NO_BORDER,
                Alignment = Element.ALIGN_RIGHT
            };
            exportDocument.pdf.Footer = footer;

            return exportDocument;
        }


        private async Task<byte[]> GenerarReporte(bool vRotate=true)
        {
            ExportDocument exportDocument = await CreateDocument(vRotate);

            exportDocument.pdf.Open();

            ReporteVisita.CreateReport(exportDocument.pdf, exportDocument.writer!, _reporteExt, img, _param);

            exportDocument.pdf.Close();

            return exportDocument.memoryStream.ToArray();
        }

   
        public async Task<string> GenerarReporteVisita(IJSRuntime js, bool preview, ReporteExt reporteExt, byte[] data, Parametro param)
        {
            string PdfPath = "";
            try
            {
                _reporteExt = reporteExt;
                _param = param;
                img = data;
                MemoryStream sourceStream = new MemoryStream(await GenerarReporte(false));
                PdfPath = Convert.ToBase64String(sourceStream.ToArray());

                if (preview)
                    PdfPath = string.Format($"data:application/pdf;base64,{PdfPath}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return PdfPath;

        }

        public async Task<byte[]> GenerarReporteXEmail(IJSRuntime js, bool preview, ReporteExt reporteExt, byte[] data, Parametro param)
        {
            try
            {
                _reporteExt = reporteExt;
                _param = param;
                img = data;
                MemoryStream sourceStream = new MemoryStream(await GenerarReporte(false));
                return sourceStream.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Array.Empty<byte>();

        }

    }

}

