﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Graphis
{
    public partial class Form1 : Form
    {
        private List<Line> lines = new List<Line>();
        private bool flag = false;
        private bool flagLine = false;
        private bool krutoiFlag = false;
        private bool delete = false;
        private bool h = false;
        Point startChoice;
        Line line = new Line();
        Graphics g;
        Bitmap buffer;
        Matrix m1 = new Matrix(2, 4);
        Matrix m2 = new Matrix(4, 4);
        Matrix t = new Matrix(4, 4);
        Matrix j = new Matrix(2, 4);
        int x;

        public void Paint(Line x)
        {
            if (!h)
            {
                g.DrawLine(new Pen(Color.Black, 1), 0, 228, 1038, 228);
                g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 218);
                g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 238);
                g.DrawLine(new Pen(Color.Black, 1), 519, 0, 519, 456);
                g.DrawLine(new Pen(Color.Black, 1), 519, 0, 509, 20);
                g.DrawLine(new Pen(Color.Black, 1), 519, 0, 529, 20);
                g.DrawLine(new Pen(Color.Black, 3), x.Start.X, x.Start.Y, x.End.X, x.End.Y);
                g.DrawLine(new Pen(Color.Red, 6), x.Start.X - 3, x.Start.Y, x.Start.X + 3, x.Start.Y);
                if (x.MidSelected)
                    g.DrawLine(new Pen(Color.Blue, 6), Math.Abs(x.End.X + x.Start.X) / 2 - 3, Math.Abs(x.End.Y + x.Start.Y) / 2, Math.Abs(x.End.X + x.Start.X) / 2 + 3, Math.Abs(x.End.Y + x.Start.Y) / 2);
                else
                    g.DrawLine(new Pen(Color.ForestGreen, 6), Math.Abs(x.End.X + x.Start.X) / 2 - 3, Math.Abs(x.End.Y + x.Start.Y) / 2, Math.Abs(x.End.X + x.Start.X) / 2 + 3, Math.Abs(x.End.Y + x.Start.Y) / 2);
                g.DrawLine(new Pen(Color.Orange, 6), x.End.X, x.End.Y - 3, x.End.X, x.End.Y + 3);
            }

            else
            {
                Line y = new Line();
                double d = -1000;
                y.Start.X = x.Start.X;
                y.Start.Y = x.Start.Y;
                y.End.X = x.End.X;
                y.End.Y = x.End.Y;
                y.StartZ = x.StartZ;
                y.EndZ = x.EndZ;
                y.EndOK = x.EndOK;
                y.StartOK = x.StartOK;

                j[0, 0] = y.Start.X - 519;
                j[0, 1] = -y.Start.Y + 228;
                j[0, 2] = y.StartZ;
                j[0, 3] = y.StartOK;
                j[1, 0] = y.End.X - 519;
                j[1, 1] = -y.End.Y + 228;
                j[1, 2] = y.EndZ;
                j[1, 3] = y.EndOK;

                t[0, 0] = 1;
                t[0, 1] = 0;
                t[0, 2] = 0;
                t[0, 3] = 0;
                t[1, 0] = 0;
                t[1, 1] = 1;
                t[1, 2] = 0;
                t[1, 3] = 0;
                t[2, 0] = 0;
                t[2, 1] = 0;
                t[2, 2] = 1;
                t[2, 3] = -1 / d;
                t[3, 0] = 0;
                t[3, 1] = 0;
                t[3, 2] = 0;
                t[3, 3] = 1;
                
                j = j * t;
                
                y.Start.X = Convert.ToInt32(Convert.ToDouble(j[0, 0]) / Convert.ToDouble(j[0, 3]) + 519);
                y.Start.Y = Convert.ToInt32(-Convert.ToDouble(j[0, 1]) / Convert.ToDouble(j[0, 3]) + 228);
                y.StartZ = Convert.ToInt32(Convert.ToDouble(j[0, 2]) / Convert.ToDouble(j[0, 3]));

                y.End.X = Convert.ToInt32(Convert.ToDouble(j[1, 0]) / Convert.ToDouble(j[1, 3]) + 519);
                y.End.Y = Convert.ToInt32(-Convert.ToDouble(j[1, 1]) / Convert.ToDouble(j[1, 3]) + 228);
                y.EndZ = Convert.ToInt32(Convert.ToDouble(j[1, 2]) / Convert.ToDouble(j[1, 3]));

                g.DrawLine(new Pen(Color.Black, 1), 0, 228, 1038, 228);
                g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 218);
                g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 238);
                g.DrawLine(new Pen(Color.Black, 1), 519, 0, 519, 456);
                g.DrawLine(new Pen(Color.Black, 1), 519, 0, 509, 20);
                g.DrawLine(new Pen(Color.Black, 1), 519, 0, 529, 20);
                g.DrawLine(new Pen(Color.Black, 3), y.Start.X, y.Start.Y, y.End.X, y.End.Y);
                g.DrawLine(new Pen(Color.Red, 6), y.Start.X - 3, y.Start.Y, y.Start.X + 3, y.Start.Y);
                if (x.MidSelected)
                    g.DrawLine(new Pen(Color.Blue, 6), Math.Abs(y.End.X + y.Start.X) / 2 - 3, Math.Abs(y.End.Y + y.Start.Y) / 2, Math.Abs(y.End.X + y.Start.X) / 2 + 3, Math.Abs(y.End.Y + y.Start.Y) / 2);
                else
                    g.DrawLine(new Pen(Color.ForestGreen, 6), Math.Abs(y.End.X + y.Start.X) / 2 - 3, Math.Abs(y.End.Y + y.Start.Y) / 2, Math.Abs(y.End.X + y.Start.X) / 2 + 3, Math.Abs(y.End.Y + y.Start.Y) / 2);
                g.DrawLine(new Pen(Color.Orange, 6), y.End.X, y.End.Y - 3, y.End.X, y.End.Y + 3);
            }
        }
        public Form1()
        {
            InitializeComponent();
            buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(buffer);
            x = 0;        
            krutoiFlag = false;
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            g.Clear(Color.White);
            if (flagLine)
            {
                line.StartZ = 0;
                line.EndZ = 0;
                line.StartOK = 1;
                line.EndOK = 1;
                lines.Add(line);
                flagLine = false;
            }
            foreach (var l in lines)
            {
                Paint(l);
                l.StartSelected = false;
                l.EndSelected = false;   
            }
            flag = false;
            x = 0;
            pictureBox1.Image = buffer; // отображаем буфер на pictureBox
        }
        protected override void OnPaint(PaintEventArgs e)
        {

        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            startChoice = e.Location;
            Point mouse = new Point(e.X, e.Y);
            foreach (var l in lines)
            {
                l.DistanceStart.X = -e.X + l.Start.X;
                l.DistanceStart.Y = -e.Y + l.Start.Y;
                l.DistanceEnd.X = -e.X + l.End.X;
                l.DistanceEnd.Y = -e.Y + l.End.Y;
                if (l.DistanceToStart(mouse) < 6) // проверяем, находится ли мышь рядом с началом линии
                {
                    l.StartSelected = true;
                    flag = true;
                    x += 1;

                }


                if (l.DistanceToEnd(mouse) < 6) // проверяем, находится ли мышь рядом с концом линии
                {
                    l.EndSelected = true;
                    flag = true;
                    x += 1;
                }


                if (l.DistanceToMid(mouse) < 20 && krutoiFlag)
                {                  
                    l.MidSelected = true;
                    flag = true;                    
                    x += 1;
                    if(x == 1)
                    {
                        textBox1.Text = l.Start.X.ToString();
                        textBox2.Text = l.Start.Y.ToString();
                        textBox3.Text = l.End.X.ToString();
                        textBox4.Text = l.End.Y.ToString();
                        textBox5.Text = l.StartZ.ToString();
                        textBox6.Text = l.EndZ.ToString();
                    }
                }
                if (l.DistanceToMid(mouse) < 20)
                {
                    x += 1;
                }

            }
            if (x == 0)
                flagLine = true;

            startChoice = new Point(e.X,e.Y);
            flag = true;            
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                g.Clear(Color.White); // очищаем буфер
                if (!flagLine)
                {
                    foreach (var l in lines)
                    {
                            if (l.StartSelected && !krutoiFlag) // если выделенo начало линии
                            {
                                l.Start = new Point(e.X, e.Y); // перемещаем начало линии
                            }
                            if (l.EndSelected && !krutoiFlag) // если выделен конец линии
                            {
                                l.End = new Point(e.X, e.Y); // перемещаем конец линии
                            }
                        if (l.MidSelected)
                        {
                            l.FromMidStart(new Point(e.X, e.Y));
                        }
                        if (l.Start == l.End)
                            {
                                lines.Remove(l);
                            }
                        Paint(l);
                    }

                }
                else
                {
                    if (!krutoiFlag)
                    {
                        line = new Line
                        {
                            Start = startChoice,
                            End = new Point(e.X, e.Y)
                        };
                        Paint(line);
                    }
                    foreach (var l in lines)
                    {
                        Paint(l);
                    }
                }
                pictureBox1.Image = buffer; // отображаем буфер на pictureBox
            }
            
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:                    
                    krutoiFlag = true;
                    break;


                case Keys.ShiftKey:
                    g.Clear(Color.White); // очищаем буфер
                    foreach (var l in lines)
                    {
                        l.MidSelected = false;
                        Paint(l);
                    }
                    g.DrawLine(new Pen(Color.Black, 1), 0, 228, 1038, 228);
                    g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 218);
                    g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 238);
                    g.DrawLine(new Pen(Color.Black, 1), 519, 0, 519, 456);
                    g.DrawLine(new Pen(Color.Black, 1), 519, 0, 509, 20);
                    g.DrawLine(new Pen(Color.Black, 1), 519, 0, 529, 20);
                    pictureBox1.Image = buffer; // отображаем буфер на pictureBox
                    break;


                case Keys.Delete:
                    List<int> mass = new List<int> ();                    
                    foreach (var l in lines)
                    {
                        if (l.MidSelected)
                        {
                            mass.Add(lines.IndexOf(l));
                            
                        }

                    }
                    mass.Sort();
                    for (int l = mass.Count-1; l >= 0; l--)
                    { 
                        lines.RemoveAt(mass[l]);
                    }
                    
                        g.Clear(Color.White); // очищаем буфер
                    foreach (var l in lines)
                    {
                        Paint(l);
                    }
                    g.DrawLine(new Pen(Color.Black, 1), 0, 228, 1038, 228);
                    g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 218);
                    g.DrawLine(new Pen(Color.Black, 1), 1038, 228, 1018, 238);
                    g.DrawLine(new Pen(Color.Black, 1), 519, 0, 519, 456);
                    g.DrawLine(new Pen(Color.Black, 1), 519, 0, 509, 20);
                    g.DrawLine(new Pen(Color.Black, 1), 519, 0, 529, 20);
                    pictureBox1.Image = buffer; // отображаем буфер на pictureBox
                    break;

                case Keys.Enter:
                    g.Clear(Color.White); // очищаем буфер
                    foreach (var l in lines)
                    {                        
                        if (l.MidSelected && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
                        {
                            l.Start.X = Convert.ToInt32(textBox1.Text);
                            l.Start.Y = Convert.ToInt32(textBox2.Text);
                            l.StartZ = Convert.ToInt32(textBox5.Text);
                            l.End.X = Convert.ToInt32(textBox3.Text);
                            l.End.Y = Convert.ToInt32(textBox4.Text);
                            l.EndZ = Convert.ToInt32(textBox6.Text);
                        }
                        Paint(l);
                    }

                    pictureBox1.Image = buffer; // отображаем буфер на pictureBox
                    break;

                
            }           
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:

                    krutoiFlag = false;
                    break;
            }

        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            m2[0, 0] = Convert.ToDouble(textBox8.Text);
            m2[0, 1] = Convert.ToDouble(textBox11.Text);
            m2[0, 2] = Convert.ToDouble(textBox15.Text);
            m2[0, 3] = Convert.ToDouble(textBox19.Text);
            m2[1, 0] = Convert.ToDouble(textBox7.Text);
            m2[1, 1] = Convert.ToDouble(textBox12.Text);
            m2[1, 2] = Convert.ToDouble(textBox16.Text);
            m2[1, 3] = Convert.ToDouble(textBox20.Text);
            m2[2, 0] = Convert.ToDouble(textBox9.Text);
            m2[2, 1] = Convert.ToDouble(textBox13.Text);
            m2[2, 2] = Convert.ToDouble(textBox17.Text);
            m2[2, 3] = Convert.ToDouble(textBox21.Text);
            m2[3, 0] = Convert.ToDouble(textBox10.Text);
            m2[3, 1] = Convert.ToDouble(textBox14.Text);
            m2[3, 2] = Convert.ToDouble(textBox18.Text);
            m2[3, 3] = Convert.ToDouble(textBox22.Text);
            foreach (var l in lines)
            {
                if (l.MidSelected) 
                {
                    m1[0, 0] = l.Start.X - 519;
                    m1[0, 1] = -l.Start.Y + 228;
                    m1[0, 2] = l.StartZ;
                    m1[0, 3] = l.StartOK;
                    m1[1, 0] = l.End.X - 519;
                    m1[1, 1] = -l.End.Y + 228;
                    m1[1, 2] = l.EndZ;
                    m1[1, 3] = l.EndOK;
                    m1 = m1 * m2;

                        l.Start.X = Convert.ToInt32(Convert.ToDouble(m1[0, 0]) / Convert.ToDouble(m1[0, 3]) + 519);
                        l.Start.Y = Convert.ToInt32(-Convert.ToDouble(m1[0, 1]) / Convert.ToDouble(m1[0, 3]) + 228);
                        l.StartZ = Convert.ToInt32(Convert.ToDouble(m1[0, 2]) / Convert.ToDouble(m1[0, 3]));
                    
                        l.End.X = Convert.ToInt32(Convert.ToDouble(m1[1, 0]) / Convert.ToDouble(m1[1, 3]) + 519);
                        l.End.Y = Convert.ToInt32(-Convert.ToDouble(m1[1, 1]) / Convert.ToDouble(m1[1, 3]) + 228);
                        l.EndZ = Convert.ToInt32(Convert.ToDouble(m1[1, 2]) / Convert.ToDouble(m1[1, 3]));
                    
                }
                Paint(l);
            }
            pictureBox1.Image = buffer; // отображаем буфер на pictureBox
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string json = JsonConvert.SerializeObject(lines);
            string f = Convert.ToString(textBox23.Text);
            string fileName = "C:\\Users\\Марк\\source\\repos\\Graphis\\" + f + ".json";
            File.WriteAllText(fileName, json);            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string f = Convert.ToString(textBox23.Text);
            string fileName = "C:\\Users\\Марк\\source\\repos\\Graphis\\"+ f + ".json";
            string json = File.ReadAllText(fileName);
            lines.Clear();
            lines = JsonConvert.DeserializeObject<List<Line>>(json);
            g.Clear(Color.White);
            foreach (var l in lines)
            Paint(l);
            pictureBox1.Image = buffer; //отображаем буфер на pictureBox
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (h) { h = false; } else { h = true; };
            g.Clear(Color.White);
            foreach (var l in lines)
                Paint(l);
            pictureBox1.Image = buffer; //отображаем буфер на pictureBox
        }
    }
}
