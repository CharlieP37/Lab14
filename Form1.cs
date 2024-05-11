using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualizationService.ServiceReference1;

namespace VisualizationService
{
    public partial class Form1 : Form
    {
        ServiceReference1.Player[] players;
        GameVisualizationServiceSoapClient cliente = new ServiceReference1.GameVisualizationServiceSoapClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            players = cliente.listajugadores();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();

            players = cliente.listajugadores();

            dataGridView1.DataSource = players;
            dataGridView1.Columns["skin"].Visible = false;
            if (dataGridView1.Columns.Contains("status") == false) 
            {
                dataGridView1.Columns.Add("status", "status");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Value = "Online";
                }
            }

            comboBox1.Items.Clear();

            foreach (var p in players) 
            {
                comboBox1.Items.Add(p.username.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void reportbtn_Click(object sender, EventArgs e)
        {
            var p = comboBox1.SelectedIndex;
            var p1 = comboBox1.SelectedItem;
            RefreshBtn_Click(null, new EventArgs());
            var t = players.ToList();
            //t.Remove(players.ElementAt(p-1));
            players = t.ToArray();
            comboBox1.Items.Remove(p1);
        }
    }
}
