using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTracking
{
    public class ExportToExcel
    {
        internal static void ExcelExport(DataGridView dataGridView1)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // Populating column names
            for (int ht = 1; ht < dataGridView1.Columns.Count + 1; ht++)
            {
                worksheet.Cells[1, ht] = dataGridView1.Columns[ht - 1].HeaderText;
            }
            // Populating data cells
            for (int row = 0; row < dataGridView1.Rows.Count; row++) // was: row < dataGridView1.Rows.Count - 1
            {
                for (int col = 0; col < dataGridView1.Columns.Count; col++)
                {
                    if (dataGridView1.Rows[row].Cells[col].Value != null)
                        worksheet.Cells[row + 2, col + 1] = dataGridView1.Rows[row].Cells[col].Value.ToString();
                    else
                        worksheet.Cells[row + 2, col + 1] = "'";
                }
            }
            // Saving file and closing workbook
            //workbook.SaveAs("C:\\LEARNING\\Training Sessions\\C# Course 01\\C#01 Lessons\\_Files\\TableData\\CurrentEmplyees.xlsx");
            //workbook.Close();
        }
    }
}
