using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace Mots_Meles
{
    internal class Excel
    {
        string path = "";
        Workbook wb;
        Application excel;
        Worksheet ws;
        private int sheet;

        public Excel(string path, Application excel, Workbook wb, Worksheet ws, int sheet)
        {
            this.path = path;
            this.excel = excel;
            wb = excel.Workbooks.Open(path);
            ws = excel.Worksheets[sheet];
        }
        public string ReadCell(int i, int j)
        {
            i++; // car tableau excel commence à 1
            j++;
            if (ws.Cells[i, j] != null)
                return ws.Cells[i, j].Value2;
            else
                return "empty cell";
        }
        public void WriteCelle(int i, int j, string value)
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = value;
        }
        public void Save()
        {
            wb.Save();
        }
        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }
        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet); 
        }
        public string [,] WriteReange(int first_i, int last_i, int first_j, int last_j, string[,] writestring)
        {
            Ranges range = (Ranges)ws.Range[ws.Cells[first_i, last_i], ws.Cells[first_j, last_j]];  // instancie un range (tableau Excel)
            string[,] holder = range.Value2;
            string[,] returnstring = new string[last_i - first_i, last_j - first_j];
            for(int i = 1; i<=last_i - first_i; i++)
            {
                for(int j = 1; j<=last_j - first_j; j++)
                {
                    returnstring[i - 1, j - 1] = holder[i, j];
                }
            }
            return returnstring;
        }
    }
}
