using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OfficeOpenXml;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Client.Shared
{
    public partial class ListaRazor
    {
        public int numeroRegistrosPagina { get; set; } = 10;

        public int indiceVal { get; set; }

        public void Paginar(int indicePag)
        {
            indiceVal = indicePag;
        }

        public int numeroPaginas()
        {

            int numeroBotao;
            int numeroRegistro = lista.Length;
            if (numeroRegistro % numeroRegistrosPagina == 0)
            {
                numeroBotao = (numeroRegistro / numeroRegistrosPagina);
            }
            else
            {
                numeroBotao = (numeroRegistro / numeroRegistrosPagina) + 1;
            }
            return numeroBotao;
        }

        public string excel = "";
        public string word = "";

        public string valorEliminar { get; set; }

        public string valor { get; set; }


        [Parameter]
        public string rotaAdicionar { get; set; } = "";



        [Parameter]
        public string rotaEditar { get; set; } = "";

        [Parameter]
        public string[] cabecalho { get; set; }

        [Parameter]
        public object[] lista { get; set; }

        [Parameter]
        public string type { get; set; } = "none";

        [Parameter]
        public string label { get; set; } = "";

        [Parameter]
        public bool button { get; set; } = true;

        [Parameter]
        public EventCallback<string> eventBusca { get; set; }


        public void exportarExcel()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ExcelPackage ep = new ExcelPackage())
                {
                    ep.Workbook.Worksheets.Add("Hoja");

                    ExcelWorksheet ew = ep.Workbook.Worksheets[0];
                    for (int i = 0; i < cabecalho.Length; i++)
                    {
                        ew.Column(i + 1).Width = 50;
                        ew.Cells[1, i + 1].Value = cabecalho[i];
                    }
                    int fila = 2;
                    int col = 1;
                    foreach (object item in lista)
                    {
                        col = 1;
                        foreach (string propiedad in nomesPropriedadesCabecalho)
                        {
                            ew.Cells[fila, col].Value = item.GetType().GetProperty(propiedad).GetValue(item).ToString();
                            col++;
                        }
                        fila++;

                    }

                    ep.SaveAs(ms);
                    byte[] buffer = ms.ToArray();
                    string base64 = Convert.ToBase64String(buffer);
                    excel = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + base64;
                }
            }

        }


        private void exportarWord()
        {

            using (MemoryStream ms = new MemoryStream())
            {

                WordDocument document = new WordDocument();

                WSection section = document.AddSection() as WSection;

                section.PageSetup.Margins.All = 72;

                IWParagraph paragraph = section.AddParagraph();

                paragraph.ApplyStyle("Normal");

                paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;

                WTextRange textRange = paragraph.AppendText(tituloReporte) as WTextRange;
                textRange.CharacterFormat.FontSize = 20f;
                textRange.CharacterFormat.FontName = "Calibri";
                textRange.CharacterFormat.TextColor = Syncfusion.Drawing.Color.Blue;

                IWTable table = section.AddTable();
                int numeroCabeceras = cabecalho.Length;
                int numeroFilas = lista.Length;
                table.ResetCells(numeroFilas + 1, numeroCabeceras);

                for (int i = 0; i < numeroCabeceras; i++)
                {
                    table[0, i].AddParagraph().AppendText(cabecalho[i]);
                }

                int fila = 1;
                int col = 0;
                foreach (object item in lista)
                {
                    col = 0;
                    foreach (string prop in nomesPropriedadesCabecalho)
                    {
                        table[fila, col].AddParagraph().AppendText(
                            item.GetType().GetProperty(prop).GetValue(item).ToString()
                            );
                        col++;
                    }
                    fila++;
                }


                document.Save(ms, FormatType.Docx);

                byte[] buffer = ms.ToArray();
                string base64 = Convert.ToBase64String(buffer);

                word = "data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64," + base64;

            }

        }

        [Parameter]
        public EventCallback<string> eventEditar { get; set; }

        [Parameter]
        public EventCallback<string> eventEliminar { get; set; }

        public void Editar(string data)
        {
            navigation.NavigateTo(rotaEditar + "/" + data);
            eventEditar.InvokeAsync(data);
        }
        public void Eliminar(string data)
        {
            valorEliminar = data;

        }

        public void EliminarDados(string data)
        {
            eventEliminar.InvokeAsync(data);
        }

        public void Busca()
        {
            eventBusca.InvokeAsync(valor);
        }


        [Parameter]
        public string tituloReporte { get; set; }
        public string pdf { get; set; }

        public void exportarPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                using (var pdfDoc = new PdfDocument(writer))
                {
                    Document doc = new Document(pdfDoc);
                    Paragraph p1 = new Paragraph(tituloReporte);

                    p1.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    p1.SetFontSize(20);
                    doc.Add(p1);

                    Table table = new Table(cabecalho.Length);

                    Cell celda;
                    for (int i = 0; i < cabecalho.Length; i++)
                    {
                        celda = new Cell();
                        celda.Add(new Paragraph(cabecalho[i]));
                        table.AddHeaderCell(celda);
                    }


                    foreach (object item in lista)
                    {

                        foreach (string propiedad in nomesPropriedadesCabecalho)
                        {
                            celda = new Cell();
                            celda.Add(
                                new Paragraph(item.GetType().GetProperty(propiedad).GetValue(item).ToString()));
                            table.AddCell(celda);
                        }

                    }


                    doc.Add(table);

                    doc.Close();
                    writer.Close();

                    byte[] buffer = ms.ToArray();
                    string base64 = Convert.ToBase64String(buffer);
                    pdf = "data:application/pdf;base64," + base64;

                    jsruntime.InvokeVoidAsync("descargarPDF", pdf);
                }
            }
        }

        [Parameter]
        public object[] combobox { get; set; } = null;
        [Parameter]
        public string displayMember { get; set; } = null;
        [Parameter]
        public string valueMember { get; set; } = null;

        [Parameter]

        public bool permitirAdicionar { get; set; } = false;
        [Parameter]
        public bool permitirEditar { get; set; } = false;
        [Parameter]
        public bool permitirEliminar { get; set; } = false;
        [Parameter]
        public bool permitirReporteExcel { get; set; } = false;

        [Parameter]
        public bool permitirExportarPDF { get; set; } = false;

        [Parameter]
        public bool permitirExportarWord { get; set; } = false;



        [Parameter]
        public string[] nomesPropriedadesCabecalho { get; set; }

    }
}
