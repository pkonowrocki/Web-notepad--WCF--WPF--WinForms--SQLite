using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using z7WinForms.ServiceReference1;

namespace SieciowyNotatnik
{
    public partial class Form1 : Form
    {
        static IwcfServiceClient client = new IwcfServiceClient();
        string imie;
        bool saved = false;
        public Form1()
        {
            InitializeComponent();
            if (!client.DBis())
                MessageBox.Show("Brak dostępu do bazy");
            this.Text = "Notatnik";
            richTextBox1.TextChanged += (o, e) => { saved = false; };
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            imie = textBox1.Text; 
            if (!client.userExist(imie))
            {
                MessageBox.Show("Brak uzytkownika w bazie.\nUtworzono nowego uzytkownika.");
                client.addNewUser(imie);
                client.addNewText(imie);
            }
            richTextBox1.Text = client.firstText(imie);
            button2.Enabled = true;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            this.Text = "Notatnik   Brak ostatniego zapisu [Ctrl+S]";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Save();
        }
        void Save()
        {
            client.updateText(client.firstTextID(imie), richTextBox1.Text);
            this.Text = "Notatnik   Ostatni zapis " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            MessageBox.Show("Zapisano");
            saved = true;
        }
        void CloseAndSave()
        {
            if (!saved)
            {
                DialogResult dialogReuslt = MessageBox.Show("Czy chcesz zapisać plik?", "Zapisywanie?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogReuslt == DialogResult.Yes)
                {
                    Save();
                }
                else
                {
                    MessageBox.Show("Nie zapisano");
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(imie!=null)
                CloseAndSave();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imie != null)
                CloseAndSave();
        }
    }
}
