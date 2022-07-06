using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circles
{
    public partial class Form1 : Form
    {
        private TCanvas canvas = null;
        private List<Circle> circles = new List<Circle>();
        private Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
            if (canvas == null) canvas = new TCanvas(picDraw, 0, 100, 0, 100);
        }

        private void DrawCircles()
        {
            // 화면지우기
            canvas.ClearDrawing(Color.White);
            // 모든 원 그리기
            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].draw(canvas);
            }
            lblNCir.Text = "원의개수 : " + Convert.ToString(circles.Count);
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

            //움직임
            if (chkMove.Checked == false) return; 
            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].move(0, 100, 0, 100);
            }
            DrawCircles();

            //원안에 원이 들어가면 작은원 지우기

            if (chkDelIncluded.Checked)
            {
                List<Circle> cdel = new List<Circle>();
                for (int i = 0; i < circles.Count; i++)
                {
                    for (int j = 0; j < circles.Count; j++)
                    {
                        if (circles[i].isCINC(circles[j]))
                            cdel.Add(circles[i]);
                    }
                }
                for (int i = 0; i < cdel.Count; i++)
                    circles.Remove(cdel[i]);
            }


        }
          
        
        private void picDraw_MouseUp(object sender, MouseEventArgs e)
        {
            if (radAdd.Checked)
            {
                Circle cir = new Circle(canvas.xposD(e.X), canvas.yposD(e.Y));
                circles.Add(cir);
                DrawCircles();
            }
            else if (radDel.Checked)
            {
                List<Circle> cdel = new List<Circle>();
                for (int i = 0; i < circles.Count; i++)
                {
                    if (circles[i].isinside(canvas.xposD(e.X), canvas.yposD(e.Y))) cdel.Add(circles[i]);
                }

                for (int i = 0; i < cdel.Count; i++) circles.Remove(cdel[i]);
                DrawCircles();
            }
        }
        private void btnAdd100_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                double xc = rnd.Next(100);
                double yc = rnd.Next(100);
                Circle cir = new Circle(xc, yc);
                circles.Add(cir);
            }
            DrawCircles();
        }
        private void btnDelAll_Click(object sender, EventArgs e)
        {
            circles.Clear();
            DrawCircles();
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
