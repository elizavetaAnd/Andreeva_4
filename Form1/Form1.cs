using Andreeva_4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Form1
{
    public partial class Form1 : Form
    {
        private float xMin = -10;
        private float xMax = 10;
        private float yMin = -10;
        private float yMax = 10;

        private float dx, dy;
        private float x0, y0;

        private float arrowSize = 7;

        float aX = -4, aY = -4; // первая точка
        float bX = 8, bY = 4; //вторая точка

        float rwidth = 8;
        float rheight = 4;
        float rstartX = 0;
        float rstartY = 0;
        public Form1()
        {
            InitializeComponent();
        }



        private void buttonWindow_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(tbWx.Text, out rstartX)) MessageBox.Show("Некорректное значение x0");
            if (!float.TryParse(tbWy.Text, out rstartY)) MessageBox.Show("Некорректное значение y0");
            if (!float.TryParse(tbWidth.Text, out rwidth)) MessageBox.Show("Некорректное значение width");
            if (!float.TryParse(tbHeight.Text, out rheight)) MessageBox.Show("Некорректное значение height");

            panel1.Refresh();
        }

        private void tb_wx(object sender, EventArgs e)
        {

        }

        private void tb_wy(object sender, EventArgs e)
        {

        }

        private void tb(object sender, EventArgs e)
        {

        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(tbLx0.Text, out aX)) MessageBox.Show("Некорректное значение x0");
            if (!float.TryParse(tbLy0.Text, out aY)) MessageBox.Show("Некорректное значение y0");
            if (!float.TryParse(tbLx1.Text, out bX)) MessageBox.Show("Некорректное значение x1");
            if (!float.TryParse(tbLy1.Text, out bY)) MessageBox.Show("Некорректное значение y1");

            panel1.Refresh();
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(textBoxXmin.Text, out xMin)) MessageBox.Show("Некорректное значение X min");
            if (!float.TryParse(textBoxXmax.Text, out xMax)) MessageBox.Show("Некорректное значение X max");
            if (!float.TryParse(textBoxYmin.Text, out yMin)) MessageBox.Show("Некорректное значение Y min");
            if (!float.TryParse(textBoxYmax.Text, out yMax)) MessageBox.Show("Некорректное значение Y max");

            panel1.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            ClipClass clip = new ClipClass(rstartX, rwidth, rstartY, rheight, aX, aY, bX, bY);
            clip.clip();
            aX = clip.x0;
            aY = clip.y0;
            bX = clip.x1;
            bY = clip.y1;
            panel1.Refresh();

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            dx = panel1.Width / (xMax - xMin);
            dy = panel1.Height / (yMax - yMin);
            x0 = -dx * xMin;
            y0 = dy * yMax;

            // сетка вертикальная
            for (float x = xMin; x <= xMax; x++)
            {
                e.Graphics.DrawLine(Pens.LightGray, x0 + x * dx, 0, x0 + x * dx, panel1.Height);
            }
            // сетка горизонтальная
            for (float y = yMin; y <= yMax; y++)
            {
                e.Graphics.DrawLine(Pens.LightGray, 0, y0 - y * dy, panel1.Width, y0 - y * dy);
            }

            // ось абсцисс
            e.Graphics.DrawLine(Pens.Black, 0, y0, panel1.Width, y0);
            // ось ординат
            e.Graphics.DrawLine(Pens.Black, x0, 0, x0, panel1.Height);

            // стрелки
            e.Graphics.FillPolygon(Brushes.Black, new PointF[] { new PointF(panel1.Width, y0), new PointF(panel1.Width - arrowSize, y0 - arrowSize), new PointF(panel1.Width - arrowSize, y0 + arrowSize) });
            e.Graphics.FillPolygon(Brushes.Black, new PointF[] { new PointF(x0, 0), new PointF(x0 - arrowSize, arrowSize), new PointF(x0 + arrowSize, arrowSize) });

            // подписи
            Font f = new Font("Arial", 12);
            e.Graphics.DrawString("Y", f, Brushes.Black, x0 + 10, 0);
            e.Graphics.DrawString("X", f, Brushes.Black, panel1.Width - 25, y0 + 10);

            //прямоугольник
            Pen pen = new Pen(Color.Red);
          
            
            e.Graphics.DrawRectangle(pen, x0 + rstartX * dx,  (y0 - rstartY*dy) - rheight * dy, rwidth* dx, rheight * dy);

            //линия         
            float xStartLine = x0 - ((aX) * (-dx));
            float yStartLine = y0 + ((aY) * (-dy));

            e.Graphics.DrawLine(Pens.Green, xStartLine, yStartLine, x0 +bX*dx, y0 - bY* dy);
        }
    }
      
}
