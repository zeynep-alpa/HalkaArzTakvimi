using System;
using System.Drawing;
using System.Windows.Forms;

public class DataGridViewFormatter
{
    public void FormatCell(DataGridViewCellFormattingEventArgs e, DataGridView dgv)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex == dgv.Columns["sonTarih"].Index)
        {
            var cellValue = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (cellValue != null && cellValue != DBNull.Value)
            {
                DateTime sonTarih = (DateTime)cellValue;
                DateTime bugun = DateTime.Today;
                TimeSpan fark = sonTarih - bugun;

                if (fark.TotalDays <= 0) // Teslim tarihi geçti
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
                else if (fark.TotalDays <= 3) // Teslim tarihine 3 günden az kaldı
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else // Teslim tarihine 3 günden fazla kaldı
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }
    }
}

