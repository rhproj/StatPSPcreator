using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSPcreator00
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = true; groupBox2.Visible = false;
            comboMonths.SelectedItem = "1 месяц";
            this.ActiveControl = tbTitle;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true; groupBox2.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true; groupBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + tbTitle.Text;

            int qLines = int.Parse(tbEnd.Text) - int.Parse(tbStart.Text) + 1;

            int[] Aay = new int[qLines*3];

            for (int i = 0; i < Aay.Length; i += 3)
            {
                Aay[i] = int.Parse(tbStart.Text) + (i/3);
                Aay[i + 1] = 0;
                Aay[i + 2] = 0;
            }

            if (!rbRows.Checked && !rbColumns.Checked || tbStart.Text.Length == 0 || tbEnd.Text.Length == 0 || tbPSPstart.Text.Length == 0 || tbPSPend.Text.Length == 0 || tbFm1.Text.Length == 0 || tbFm2.Text.Length == 0 || tbSect1.Text.Length == 0 || tbSect2.Text.Length == 0) //&& 
            {
                MessageBox.Show("Остались незаполненные поля");  //
            }
            else if (rbRows.Checked)
            {
                Directory.CreateDirectory(dir);

                for (int j = int.Parse(tbPSPstart.Text); j <= int.Parse(tbPSPend.Text); j++)
                {
                    using (StreamWriter sW = new StreamWriter(dir + $@"\Строка {tbStart.Text} по {tbEnd.Text} ({j}).PSP"))
                    {
                        if (comboMonths.Text != "12 месяцев")
                        {
                            sW.WriteLine($"1: {tbFm1.Text} {tbSect1.Text} -0-{comboMonths.Text.Substring(0, 1)}");
                        }
                        else
                        {
                            sW.WriteLine($"1: {tbFm1.Text} {tbSect1.Text} -1-0");
                        }
                        sW.WriteLine($"2: {tbFm2.Text} {tbSect2.Text}");
                        sW.WriteLine();
                        sW.WriteLine("dat");

                        for (int i = 0; i < Aay.Length; i += 3)
                        {
                            sW.WriteLine($@"{i + 1}= 1\{Aay[i]}\{j}");
                            sW.WriteLine($@"{i + 2}= 2\{Aay[i]}\{j}");
                            sW.WriteLine($@"{i + 3}= g{i + 2} - g{i + 1}");
                        }

                        sW.WriteLine();
                        sW.Write("shif_pfo");
                    }
                    using (StreamWriter sW = new StreamWriter(dir + $@"\{tbTitle.Text}.LINK", append: true, encoding: Encoding.UTF8)) //adds to the end of the line
                    {
                        sW.WriteLine($@"Строка {tbStart.Text} по {tbEnd.Text} ({j}).PSP");
                        sW.WriteLine();
                    }
                }
                MessageBox.Show("Готово!");
            }
            else
            {
                Directory.CreateDirectory(dir);

                for (int j = int.Parse(tbPSPstart.Text); j <= int.Parse(tbPSPend.Text); j++)
                {
                    using (StreamWriter sW = new StreamWriter(dir + $@"\Столбец {tbStart.Text} по {tbEnd.Text} ({j}).PSP"))
                    {

                        if (comboMonths.Text != "12 месяцев")
                        {
                            sW.WriteLine($"1: {tbFm1.Text} {tbSect1.Text} -0-{comboMonths.Text.Substring(0, 1)}");
                        }
                        else
                        {
                            sW.WriteLine($"1: {tbFm1.Text} {tbSect1.Text} -1-0");
                        }
                        sW.WriteLine($"2: {tbFm2.Text} {tbSect2.Text}");
                        sW.WriteLine();
                        sW.WriteLine("dat");

                        for (int i = 0; i < Aay.Length; i += 3)
                        {
                            sW.WriteLine($@"{i + 1}= 1\{j}\{Aay[i]}");
                            sW.WriteLine($@"{i + 2}= 2\{j}\{Aay[i]}");
                            sW.WriteLine($@"{i + 3}= g{i + 2} - g{i + 1}");
                        }

                        sW.WriteLine();
                        sW.Write("shif_pfo");
                    }
                    using (StreamWriter sW = new StreamWriter(dir + $@"\{tbTitle.Text}.LINK", append: true, encoding: Encoding.UTF8)) //adds to the end of the line
                    {
                        sW.WriteLine($@"Столбец {tbStart.Text} по {tbEnd.Text} ({j}).PSP");
                        sW.WriteLine();
                    }
                }
                MessageBox.Show("Готово!");
            }
        }

        private void tbFm1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbFm1_TextChanged(object sender, EventArgs e)
        {
            tbFm2.Text = tbFm1.Text;
        }
    }
}