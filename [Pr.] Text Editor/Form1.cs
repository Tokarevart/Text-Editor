using System;
using System.Windows.Forms;

namespace _Pr.__Text_Editor
{
    public partial class Form1 : Form
    {
        private int startInd, len, i = 0;
        private Timer tmr = new Timer();
        private string richTextBuf = "", textBoxBuf = "";

        public Form1()
        {
            InitializeComponent();
            panel1.Height = 1;
            panel2.Height = 1;
            tmr.Interval = 1;
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
            richTextBuf = richTextBox1.Text.ToLower();
            textBoxBuf = textBox1.Text.ToLower();
        }

        private void выделитьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Разработчик: Токарев А.А.");
        }

        private void WrapBtn_Click(object sender, EventArgs e)
        {
            if (WrapBtn.Checked == true)
            {
                WrapBtn.Checked = false;
                richTextBox1.WordWrap = false;
            }
            else
            {
                WrapBtn.Checked = true;
                richTextBox1.WordWrap = true;
            }
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = richTextBox1.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK) richTextBox1.Font = fontDialog1.Font;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBuf = richTextBox1.Text.ToLower();
            textBoxBuf = textBox1.Text.ToLower();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                richTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);
            }
            richTextBuf = richTextBox1.Text.ToLower();
            textBoxBuf = textBox1.Text.ToLower();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName.Length > 0)
            {
                richTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBuf = richTextBox1.Text.ToLower();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBoxBuf = textBox1.Text.ToLower();
        }

        private void заменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBuf = richTextBox1.Text.ToLower();
            textBoxBuf = textBox2.Text.ToLower();
            tmr = new Timer();
            tmr.Interval = 1;
            tmr.Tick += new EventHandler((o, ev) =>
            {
                panel2.Height += 1;
                if (panel2.Height == 40) tmr.Stop();
            });
            if (panel2.Height == 1) tmr.Start();
        }

        private void CloseBtn2_Click(object sender, EventArgs e)
        {
            tmr = new Timer();
            tmr.Interval = 1;
            tmr.Tick += new EventHandler((o, ev) =>
            {
                panel2.Height -= 1;
                if (panel2.Height == 1) tmr.Stop();
            });
            if (panel2.Height == 40) tmr.Start();
            richTextBox1.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBoxBuf = textBox2.Text.ToLower();
        }

        private void ReplaceBtn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text && richTextBox1.Text == richTextBox1.Text.Replace(textBox2.Text, textBox3.Text)) MessageBox.Show("Не удалось найти подстроку.");
            richTextBox1.Text = richTextBox1.Text.Replace(textBox2.Text, textBox3.Text);
        }

        private void LeftText_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void CenterText_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void RightText_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            tmr = new Timer();
            tmr.Interval = 1;
            tmr.Tick += new EventHandler((o, ev) =>
            {
                panel1.Height -= 1;
                if (panel1.Height == 1) tmr.Stop();
            });
            if (panel1.Height == 40) tmr.Start();
            richTextBox1.Focus();
        }

        private void FindBtn_Click(object sender, EventArgs e)
        {
            richTextBuf = richTextBox1.Text.ToLower();
            textBoxBuf = textBox1.Text.ToLower();
            try
            {
                if (DownRb.Checked)
                {
                    if (i >= richTextBox1.Text.Length + 1 - textBox1.Text.Length) i = 0;
                    if (i <= -1) i = 0;
                    if (checkBox1.Checked) startInd = richTextBox1.Text.IndexOf(textBox1.Text, i);
                    else startInd = richTextBuf.IndexOf(textBoxBuf, i);
                }
                if (UpRb.Checked)
                {
                    if (i > richTextBox1.Text.Length) i = richTextBox1.Text.Length;
                    if (i <= -2 + textBox1.Text.Length) i = richTextBox1.Text.Length;
                    if (checkBox1.Checked) startInd = richTextBox1.Text.LastIndexOf(textBox1.Text, i);
                    else startInd = richTextBuf.LastIndexOf(textBoxBuf, i);
                }
                i = startInd;
                len = textBox1.Text.Length;
                richTextBox1.Focus();
                richTextBox1.Select(startInd, len);
                if (DownRb.Checked) i++;
                if (UpRb.Checked) i = i - 2 + textBox1.Text.Length;
            }
            catch
            {
                try
                {
                    if (DownRb.Checked)
                    {
                        if (i >= richTextBox1.Text.Length + 1 - textBox1.Text.Length) i = 0;
                        if (i <= -1) i = 0;
                        if (checkBox1.Checked) startInd = richTextBox1.Text.IndexOf(textBox1.Text, i);
                        else startInd = richTextBuf.IndexOf(textBoxBuf, i);
                    }
                    if (UpRb.Checked)
                    {
                        if (i > richTextBox1.Text.Length) i = richTextBox1.Text.Length;
                        if (i <= -2 + textBox1.Text.Length) i = richTextBox1.Text.Length;
                        if (checkBox1.Checked) startInd = richTextBox1.Text.LastIndexOf(textBox1.Text, i);
                        else startInd = richTextBuf.LastIndexOf(textBoxBuf, i);
                    }
                    i = startInd;
                    len = textBox1.Text.Length;
                    richTextBox1.Focus();
                    richTextBox1.Select(startInd, len);
                    if (DownRb.Checked) i++;
                    if (UpRb.Checked) i = i - 2 + textBox1.Text.Length;
                }
                catch { MessageBox.Show("Не удалось найти подстроку."); }
            };
        }

        private void найтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmr = new Timer();
            tmr.Interval = 1;
            tmr.Tick += new EventHandler((o, ev) =>
            {
                panel1.Height += 1;
                if (panel1.Height == 40) tmr.Stop();
            });
            if (panel1.Height == 1) tmr.Start();
        }
    }
}
