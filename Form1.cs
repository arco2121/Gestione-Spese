using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestiFInanza
{
    public partial class GestiFinanza : Form
    {
        public GestiFinanza()
        {
            InitializeComponent();
        }

        private void GestiFinanza_Load(object sender, EventArgs e)
        {
            try
            {
                string[] gh = File.ReadAllLines("Config.GestiFinanza");
                gh = gh.Distinct().ToArray();
                for (int i = 0; i<gh.Length;i++)
                {
                    comboBox1.Items.Add(gh[i]);
                }
            }
            catch { }
            try { view.DataSource = StampaInTab(); }
            catch 
            {
                string all = "Nome,Categoria,Importo,Data\n";
                File.WriteAllText("Dati.csv", all);
                view.DataSource = StampaInTab();
            }
        }

        private void Inserisci_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text;
            string cat = comboBox1.Text;
            DateTime data = dateTimePicker1.Value;
            float imp = (float) numericUpDown1.Value;
            string dta = nome + "," + cat + "," + imp + "," + data + "\n";
            string j = "";
            try
            {
                j = File.ReadAllText("Config.GestiFinanza");
                j += cat + "\n";
                File.WriteAllText("Config.GestiFinanza", j);
            }
            catch
            {
                j += cat + "\n";
                File.WriteAllText("Config.GestiFinanza", j);
            }
            string h = "";
            try { h = File.ReadAllText("Dati.csv"); }
            catch { h = ""; }
            if (h == "")
            {
                string all = "Nome,Categoria,Importo,Data\n" + dta;
                File.WriteAllText("Dati.csv", all);
                view.DataSource = StampaInTab();
            }
            else
            {
                h += dta;
                File.WriteAllText("Dati.csv", h);
                view.DataSource = StampaInTab();
            }
        }

        private DataTable StampaInTab()
        {
            string[] all = File.ReadAllLines("Dati.csv");
            DataTable tab = new DataTable();
            for (int i = 0; i<all.Length; i++)
            {
                string[] s = all[i].Split(',');
                if(i == 0)
                { 
                    tab.Columns.Add(s[0], typeof(string));
                    tab.Columns.Add(s[1], typeof(string));
                    tab.Columns.Add(s[2], typeof(float));
                    tab.Columns.Add(s[3], typeof(DateTime));
                }
                else
                {
                    tab.Rows.Add(s[0], s[1], s[2], s[3]);
                }
            }
            try
            {
                string[] gh = File.ReadAllLines("Config.GestiFinanza");
                gh = gh.Distinct().ToArray();
                for (int i = 0; i < gh.Length; i++)
                {
                    comboBox1.Items.Add(gh[i]);
                }
            }
            catch { };
            return tab;
        }

        private void dettaglio_Click(object sender, EventArgs e)
        {
            Form2 Finestra = new Form2();
            Finestra.ShowDialog();
        }
    }
}
