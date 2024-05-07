using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestiFInanza
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = StampaInTa2b();
        }

        private DataTable StampaInTa2b()
        {
            string[] all = File.ReadAllLines("Dati.csv");
            DataTable tab = new DataTable();
            string lines = "";
            for (int i = 1; i < all.Length; i++)
            {
                string[] s = all[i].Split(',');
                if (!lines.Contains(s[1]))
                {
                    lines += s[1] + ",";
                }
            }
            string[] linesea = lines.Split(',').Distinct().ToArray();
            string[] linese = new string[linesea.Length-1];
            Array.Copy(linesea, linese, linese.Length);
            float[] tot = new float[linese.Length];
            for (int i = 0; i < all.Length; i++)
            {
                string[] s = all[i].Split(',');
                for(int j = 0; j<linese.Length;j++)
                {
                    if (s[1] == linese[j])
                    {
                        tot[j] += Convert.ToSingle(s[2]);
                    }
                }
            }
            tab.Columns.Add("Categoria", typeof(string));
            tab.Columns.Add("Importo Totale in Euro", typeof(float));
            for (int i = 0; i<linese.Length;i++)
            {
                tab.Rows.Add(linese[i], tot[i]);
            }
            return tab;
        }
    }
}
