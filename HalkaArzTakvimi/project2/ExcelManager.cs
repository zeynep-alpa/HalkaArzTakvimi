using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;
using Proje1;

namespace project2
{
    public class ExcelManager
    {
        public static void SaveToExcel(List<HisseModel> halkaArz, List<HisseModel> taslakArz)
        {
            using (var workbook = new XLWorkbook())
            {
                if (halkaArz.Count > 0)
                {
                    var worksheetHalkaArz = workbook.Worksheets.Add("Halka Arz");
                    worksheetHalkaArz.Cell(1, 1).Value = "Hisse Kodu";
                    worksheetHalkaArz.Cell(1, 2).Value = "Başlık";
                    worksheetHalkaArz.Cell(1, 3).Value = "Arz Tarihi";

                    for (int i = 0; i < halkaArz.Count; i++)
                    {
                        worksheetHalkaArz.Cell(i + 2, 1).Value = halkaArz[i].HisseKodu;
                        worksheetHalkaArz.Cell(i + 2, 2).Value = halkaArz[i].Aciklamasi;
                        worksheetHalkaArz.Cell(i + 2, 3).Value = halkaArz[i].ArzTarihi;
                    }
                }

                if (taslakArz.Count > 0)
                {
                    var worksheetTaslakArz = workbook.Worksheets.Add("Taslak Arz");
                    worksheetTaslakArz.Cell(1, 1).Value = "Hisse Kodu";
                    worksheetTaslakArz.Cell(1, 2).Value = "Başlık";

                    for (int i = 0; i < taslakArz.Count; i++)
                    {
                        worksheetTaslakArz.Cell(i + 2, 1).Value = taslakArz[i].HisseKodu;
                        worksheetTaslakArz.Cell(i + 2, 2).Value = taslakArz[i].Aciklamasi;
                    }
                }

                string excelFileName = "veriler.xlsx";
                workbook.SaveAs(excelFileName);
            }
        }
    }
}



