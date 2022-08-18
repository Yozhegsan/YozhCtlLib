using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YozhCtlLib
{
    public partial class RoundProgress : UserControl
    {
        float aAngle = 0f;
        float min = 0f;
        float max = 0f;
        Bitmap bmp = new Bitmap(1000, 1000);
        Graphics gfx;
        readonly Font fnt = new Font("Arial", 180, FontStyle.Bold);
        readonly Font fnt2 = new Font("Arial", 360, FontStyle.Bold);

        //############################################################

        public RoundProgress()
        {
            InitializeComponent();
        }

        private void RoundProgress_Load(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
            pic.BackColor = Color.Transparent;
            gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Random r = new Random();
            aAngle = (float)DateTime.Now.Second * 6f;
            ShowPB(0);
        }

        public void SetValue(float val)
        {
            if (val > max || val < min) return;
            float tmp = val / max * 100;
            tmp *= 3.6f;
            ShowPB(tmp);
        }

        public void SetMax(float max)
        {
            this.max = max;
        }

        private void ShowPB(float val)
        {
            gfx.Clear(Color.Transparent);
            SizeF f = gfx.MeasureString(Math.Round(val / 3.6).ToString(), fnt);
            float ll = (1000f - f.Width) / 2f;
            float tt = (1000f - f.Height) / 2f;
            SizeF f2 = gfx.MeasureString("%", fnt2);
            float ll2 = (1000f - f2.Width) / 2f;
            float tt2 = (1000f - f2.Height) / 2f;
            gfx.FillEllipse(new SolidBrush(Color.FromArgb(200, 230, 200)), 40, 40, 920, 920);
            gfx.DrawArc(new Pen(Color.Black, 30), 30f, 30f, 940f, 940f, 0f, 360f);
            gfx.DrawArc(new Pen(Color.Green, 150), 150f, 150f, 700f, 700f, aAngle, val);
            gfx.DrawString("%", fnt2, new SolidBrush(Color.FromArgb(170, 200, 170)), ll2 + 10f, tt2 + 30f);
            gfx.DrawString(Math.Round(val / 3.6).ToString(), fnt, Brushes.Black, ll + 5f, tt + 10f);
            //gfx.DrawLine(new Pen(Color.Black), 500, 0, 500, 1000); // Debug line v
            //gfx.DrawLine(new Pen(Color.Black), 0, 500, 1000, 500); // Debug line h
            pic.Image = bmp;
        }

        private void PbYozh_Resize(object sender, EventArgs e)
        {
            Height = Width;
            pic.Left = 0;
            pic.Top = 0;
            pic.Width = Width;
            pic.Height = Height;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            aAngle += 2.5f;
            if (aAngle >= 360f) aAngle = 0;
        }
    }
}
