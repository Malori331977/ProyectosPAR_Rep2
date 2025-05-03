using iTextSharp.text;
using iTextSharp.text.pdf;
using ProyectoPAR.Models;
using ProyectoPAR.Pages.Reportes;
using ProyectoParLibs.Models.Configuracion;
using Image = iTextSharp.text.Image;
using Table = iTextSharp.text.Table;

namespace ControlColegiadosBlazorServer.Pages.Certificaciones
{
    
    public class ReporteVisita:ReporteBase
    {

        public static void CreateReport(Document pdf, PdfWriter writer, ReporteExt reporteExt, byte[] logo, Parametro param)
        {
            PdfContentByte cb = writer.DirectContent;
            Paragraph p = new Paragraph();


            //impresion de imagen de compañia
            if (logo != null)
            {
                Image img = Image.GetInstance(logo);
                img.ScalePercent(20);
                img.SetAbsolutePosition(PageSize.Letter.Width - (PageSize.Letter.Width - 50),
                                        PageSize.Letter.Height - 45);
                pdf.Add(img);
            }

            p = new Paragraph();
            p.IndentationLeft = 140f;
            p.IndentationRight = 58f;

            p.Add(Text($"\n{param.NombreCompania}\n", 12f));
            p.Add(Text($"{param.DireccionCompania}\n", 10f));
            p.Add(Text($"{param.PaisCompania}\n", 10f));
            p.Add(Text($"{param.TelefonoCompania}\n", 10f));
            pdf.Add(p);


            p = new Paragraph();
            p.Alignment = Element.ALIGN_CENTER;


            p.Add(Text("\n"));
            p.Add(Text("Reporte de Visita\n",20f));
            p.Add(Text($"Número: {reporteExt.Id}\n",15f));
            p.Add(Text($"Fecha: {reporteExt.Fecha.ToString("dd-MM-yyyy")}\n\n",15f));
            pdf.Add(p);

            p = new Paragraph();
            p.Alignment = Element.ALIGN_LEFT;
            p.IndentationLeft = 58f;
            p.IndentationRight = 58f; 

            
            p.Add(Text($"Cliente: {reporteExt.NombreCliente}\n", 12f));

            p.Add(Text($"Categoría: {reporteExt.DescCategoriaReporte}\n", 12f));
            p.Add(Text($"Consultor(a): {reporteExt.NombreConsultor}\n", 12f));
            p.Add(Text($"Hora de Entrada: {reporteExt.HoraEntrada}\nHora de Salida: {reporteExt.HoraSalida}\n", 12f));
            p.Add(Text($"Tiempo Efectivo Facturable: {reporteExt.CantidadHorasFacturables} horas\n", 12f));
            p.Add(Text($"Tiempo Efectivo NO Facturable: {reporteExt.CantidadHorasNoFacturables} horas\n\n", 12f));


            p.Add(Text($"En la fecha arriba indicada, con la participación del encargado(a) siguiente:\n\n", 12f));
            pdf.Add(p);

            PdfPTable tableEncargado = new PdfPTable(3);
            float[] headerwidths = { 35, 30, 35 };

            tableEncargado.SetWidths(headerwidths);

            PdfPCell cell = new PdfPCell(new Phrase("Nombre", new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.White)));
            cell.BackgroundColor = new BaseColor(36, 24, 130);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableEncargado.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rol", new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.White)));
            cell.BackgroundColor = new BaseColor(36, 24, 130);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableEncargado.AddCell(cell);

            cell = new PdfPCell(new Phrase("Correo Electrónico", new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.White)));
            cell.BackgroundColor = new BaseColor(36, 24, 130);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableEncargado.AddCell(cell);


            cell = new PdfPCell(Text($"{reporteExt.ResponsableCliente}", 12f));
            cell.BackgroundColor = new BaseColor(255, 255, 255);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableEncargado.AddCell(cell);

            cell = new PdfPCell(Text($"Encargado(a)", 12f));
            cell.BackgroundColor = new BaseColor(255, 255, 255);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableEncargado.AddCell(cell);

            cell = new PdfPCell(Text($"{reporteExt.CorreoCliente}", 12f));
            cell.BackgroundColor = new BaseColor(255, 255, 255);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableEncargado.AddCell(cell);

            pdf.Add(tableEncargado);

            p = new Paragraph();
            p.Alignment = Element.ALIGN_LEFT;
            p.IndentationLeft = 58f;
            p.IndentationRight = 58f;
            p.Add(Text($"\nSe realizaron las siguientes actividades según lo solicitado:\n\n", 12f));
            pdf.Add(p);


            PdfPTable tableTareasRealizadas = new PdfPTable(1);

            cell = new PdfPCell(new Phrase("Tareas Realizadas", new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.White)));
            cell.BackgroundColor = new BaseColor(36, 24, 130);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableTareasRealizadas.AddCell(cell);
           

            cell = new PdfPCell(new Phrase(reporteExt.Descripcion, new Font(Font.HELVETICA, 12f, Font.NORMAL, BaseColor.Black)));
            cell.BackgroundColor = new BaseColor(224, 235, 255);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableTareasRealizadas.AddCell(cell);

            pdf.Add(tableTareasRealizadas);

            p = new Paragraph();
            p.Alignment = Element.ALIGN_LEFT;
            p.IndentationLeft = 58f;
            p.IndentationRight = 58f;
            p.Add(Text($"\nSe acordaron los siguientes puntos como tareas pendientes:\n\n", 12f));
            pdf.Add(p);


            PdfPTable tableTareasPendientes = new PdfPTable(1);

            cell = new PdfPCell(new Phrase("Tareas Pendientes", new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.White)));
            cell.BackgroundColor = new BaseColor(36, 24, 130);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableTareasPendientes.AddCell(cell);
            pdf.Add(tableTareasPendientes);

            tableTareasPendientes = new PdfPTable(1);
            cell = new PdfPCell(Text($"{reporteExt.TareasPendientes}", 12f));
            cell.BackgroundColor = new BaseColor(224, 235, 255);
            cell.BorderColor = new BaseColor(36, 24, 130);
            cell.PaddingBottom = 10f;
            cell.PaddingLeft = 20f;
            cell.PaddingTop = 4f;
            tableTareasPendientes.AddCell(cell);
            pdf.Add(tableTareasPendientes);

            p = new Paragraph();
            p.Alignment = Element.ALIGN_LEFT;
            p.IndentationLeft = 58f;
            p.IndentationRight = 58f;
            p.Add(Text($"\n\"Doy por aceptadas las labores realizadas y expuestas en este reporte de visita, asi mismo, doy fe de que el total de las horas laboradas son CORRECTAS\".", 12f));
            
            pdf.Add(p);

            cb.SaveState();
            cb.RestoreState();

        }

    }
}
