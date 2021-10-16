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

namespace LogCounter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || timeBox.Text == "")
            {
                button1.Enabled = true;
                return;
            }

            string time = timeBox.Text;

            string[] path = new string[] { textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text };

            int[][] result = new int[4][];

            for(int i = 0; i < path.Length; i++)
            {
                result[i] = calc(path[i], time);
            }
            textBox5.Text = $"総数:{result[0][0]}\r\nタブレット:{result[0][1]}\r\nOLT:{result[0][2]}";
            textBox6.Text = $"総数:{result[1][0]}\r\nタブレット:{result[1][1]}\r\nOLT:{result[1][2]}";
            textBox7.Text = $"総数:{result[2][0]}\r\nタブレット:{result[2][1]}\r\nOLT:{result[2][2]}";
            textBox8.Text = $"総数:{result[3][0]}\r\nタブレット:{result[3][1]}\r\nOLT:{result[3][2]}";

            int sumsousuu = result[0][0] + result[1][0] + result[2][0] + result[3][0];
            int sumiPad = result[0][1] + result[1][1] + result[2][1] + result[3][1];
            int sumOLT = result[0][2] + result[1][2] + result[2][2] + result[3][2];

            textBox9.Text = $"{(int.Parse(time) + 9) % 24}時までの集計です\r\nタブレット:{sumiPad}\r\nPC:{sumsousuu-sumiPad}\r\nOLT:{sumOLT}";

            button1.Enabled = true;

        }

        private static int[] calc(string path,string time)
        {

            string[] lines = File.ReadAllLines(@path, Encoding.UTF8);
            if (time.Length == 1)
            {
                time = "0" + time;
            }

            var basic = lines.Skip(4).Where(item => !(item.Split(' ')[1].Split(':')[0] == time)); //ここの書き方重要

            var array = basic.Where(item => item.Split(' ')[5] == "/home/member/astrac/sers/mumt/6.0/sers/Main.aspx");

            int sousuu = array.Count();

            var array2 = array.Where(item => item.Split(' ')[11].Contains("iPad"));

            int iPad = array2.Count();

            var array3 = basic.Where(item => item.Split(' ')[5] == "/home/member/astrac/sers/mumo/4.0/sers/Main.aspx")
                .Where(item => item.Split(' ')[14] == "200");

            int OLT = array3.Count();

            int[] rlist = new int[] { sousuu, iPad, OLT };

            return rlist;//$"総数:{sousuu}\r\niPad:{iPad}\r\nOLT:{OLT}";
        }

        private void textBox1Enter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox1Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textBox1.Text = files[0];
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textBox2.Text = files[0];
        }

        private void textBox3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textBox3.Text = files[0];
        }

        private void textBox4_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textBox4.Text = files[0];
        }

        private void timeBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
