using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace Mots_Meles
{
    internal class Excel
    {
        string path = "";
        Workbook wb;
        Application excel;       // ça sert à quoi?
        Worksheet ws;            
        private int sheet;       // autant de sheets que de joueur
        private int difficulty;
        public Excel(string path, Application excel, Workbook wb, Worksheet ws, int sheet, int difficulty)
        {
            this.path = path;
            this.excel = excel;
            wb = excel.Workbooks.Open(path);
            ws = excel.Worksheets[sheet];
            this.difficulty = difficulty;   
        }


        /// <summary>
        /// Get la valeur d'une cellule
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public string ReadCell(int i, int j)
        {
            i++; // car tableau excel commence à 1
            j++;
            if (ws.Cells[i, j] != null)
                return ws.Cells[i, j].Value2;
            else
                return "empty cell";
        }


        /// <summary>
        /// set la valeur d'une cellule
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="value"></param>
        public void WriteCell(int i, int j, string value)
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = value;
        }


        /// <summary>
        /// Sauvegarde le WorkBook
        /// </summary>
        public void Save()
        {
            wb.Save();
        }


        /// <summary>
        /// Sauvegarde Sous le WorkBook
        /// </summary>
        /// <param name="path"></param>
        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }


        /// <summary>
        /// instancie le WorkBook
        /// </summary>
        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet); 
        }


        /// <summary>
        /// Lis les valeurs de d'un seul WorkSheet 
        /// Pour de l'Affichage console si besoin
        /// </summary>
        /// <param name="first_i"></param>
        /// <param name="last_i"></param>
        /// <param name="first_j"></param>
        /// <param name="last_j"></param>
        /// <returns></returns>
        public string [,] ReadRange(int first_i, int last_i, int first_j, int last_j)
        {
            Range range = (Range)ws.Range[ws.Cells[first_i, first_j], ws.Cells[last_i, last_j]];  // instancie un range (tableau Excel)
            string [,] interm = range.Value;
            string [,] returnstring = new string[last_i - first_i + 1, last_j - first_j + 1];
            for(int i = 1; i<=last_i - first_i; i++)
            {
                for(int j = 1; j<=last_j - first_j; j++)
                {
                    returnstring[i - 1, j - 1] = interm[i, j].ToString();
                }
            }
            return returnstring;
        }


        /// <summary>
        /// Rempli un WorkSheet selon une matrice
        /// On veut selon la difficulté et non une matrice
        /// </summary>
        /// <param name="first_i"></param>
        /// <param name="last_i"></param>
        /// <param name="first_j"></param>
        /// <param name="last_j"></param>
        /// <param name="writestring"></param>
        public void WriteString(int first_i, int last_i, int first_j, int last_j, string[,] writestring)
        {
            Range range = ws.Range[ws.Cells[first_i, first_j], ws.Cells[last_i, last_j]];
            range.Value = writestring;
        }
    }
}
