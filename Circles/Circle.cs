using System;
using System.Drawing;

namespace Circles
{
    class Circle
    {
        private static Random rnd = new Random();
        private double xcen, ycen, radius;
        private int red, green, blue;
        private double xmov, ymov;

        // 생성자 - 주어진 위치에 랜덤 크기, 랜덤 색의 원 생성
        public Circle(double xcen, double ycen)
        {
            this.xcen = xcen;
            this.ycen = ycen;
            this.radius = rnd.Next(10) + 2;
            this.red = rnd.Next(200);
            this.green = rnd.Next(200);
            this.blue = rnd.Next(200);  // 너무밝지않게
            while (true) { this.xmov = rnd.Next(-3, 3); if (xmov != 0) break; }
            while (true) { this.ymov = rnd.Next(-3, 3); if (ymov != 0) break; }
        }

        // 움직임
        public void move(double xmin, double xmax, double ymin, double ymax)
        {
            if (xcen <= xmin + radius) xmov = Math.Abs(xmov);
            if (xcen >= xmax - radius) xmov = -Math.Abs(xmov);
            if (ycen <= ymin + radius) ymov = Math.Abs(ymov);
            if (ycen >= ymax - radius) ymov = -Math.Abs(ymov);
            xcen += xmov;
            ycen += ymov;
        }

        // 그리기
        public void draw(TCanvas canvas)
        {
            canvas.DrawEllipse(Color.FromArgb(this.red, this.green, this.blue),
            this.xcen, this.ycen, this.radius * 2, this.radius * 2);
        }

        // 주어진 점이 내부인지 판단
        public bool isinside(double xp, double yp)
        {
            double dx = xp - this.xcen;
            double dy = yp - this.ycen;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            return (dist <= this.radius) ? true : false;
        }
        // 원이 원 내부인지 판단
        /*public bool isCINC(double xp, double yp,double r)
        {
            // 반지름차이 > 중심거리 일 경우 작은원이 큰 원 안에 들어감.
            double dr = Math.Abs(r-this.radius); // 반지름차이
            double dx = Math.Abs(xp - this.xcen); 
            double dy = Math.Abs(yp - this.ycen);
            double dist = Math.Sqrt(dx * dx + dy * dy);// 중심거리
            return (dr > dist) ? true : false; //반지름차이 > 중심거리인가?
        }*/
        public bool isCINC(Circle cir)
        {
            // 반지름차이 > 중심거리 일 경우 작은원이 큰 원 안에 들어감.
            double dr = cir.radius - this.radius; // 반지름차이
            double dx = Math.Abs(cir.xcen - this.xcen);
            double dy = Math.Abs(cir.ycen - this.ycen);
            double dist = Math.Sqrt(dx * dx + dy * dy);// 중심거리
            return (dr > dist) ? true : false; //반지름차이 > 중심거리인가?
        }
        
    }
}
