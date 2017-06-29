using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddMatrix
{
    public partial class Result : Form
    {
        public Result(int [,] result, int columns, int rows)
        {
            
            InitializeComponent();
            for (int i = 0; i < columns; i++)
                dataGrid.Columns.Add("", "");
            for (int i = 0; i < rows; i++)
                dataGrid.Rows.Add();
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    dataGrid[j, i].Value = result[i, j];
                }
            }

        }
    }
}
