using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

namespace HILYCode.view
{
    public partial class index : Form
    {
        private int count = -1;
        private ArrayList images = new ArrayList();
        public Bitmap[] bitmap = new Bitmap[8];
        private int _value=1;
        private Color _cirleColor = Color.Red;
        private float _circleSize = 0.8f;
        public index()
        {
            InitializeComponent();
        }

        public Color CircleColor
        {
            get { return _cirleColor; }
            set { _cirleColor = value;
                Invalidate(); }
        }
        public float CircleSize
        {
            get { return _circleSize; }
            set
            {
                if (value<=0.0F)
                {
                    _circleSize = 0.05F;
                }
                else
                {
                    _circleSize = value > 4.0F ? 4.0F : value;
                    Invalidate();
                }
            }

        }
        public Bitmap DrawCircle(int j)
        {
            const float angle = 320.0F / 8;
            Bitmap map = new Bitmap(260, 260);
            Graphics g = Graphics.FromImage(map);
            g.TranslateTransform(Width/2.0F,Height/2.0F);
            g.RotateTransform(angle*_value);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
             g.SmoothingMode=   SmoothingMode.AntiAlias;
           // int[] a = new int[8] { 25, 50, 75, 100, 125, 150, 175, 200 };
            int[] a = new int[8] { 15, 30, 45, 60, 75, 90, 105, 120 };
            for (int i = 1; i <= 8; i++)
            {
                int alpha = a[(i + j - 1) % 8];
                Color drawColor = Color.FromArgb(alpha,_cirleColor);
                using (SolidBrush brush = new SolidBrush(drawColor))
                {
                    float sizeRate = 2.5F / _circleSize;
                    float size = Width / (4 * sizeRate);
                    float diff = (Width / 6.0F) - size;
                    //float x = (Width / 80.0F) + diff;
                    //float y = (Height / 80.0F) + diff;
                    float x = (Width / 60.0F) + diff;
                    float y = (Height / 60.0F) + diff;
                    g.FillEllipse(brush,x,y,size,size);
                    g.RotateTransform(angle);
                }
            }
            return map;
        }
        public void Draw()
        {
            for (int j = 0; j < 8; j++)
            {
                bitmap[7 - j] = DrawCircle(j);
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            SetNewSize();
            base.OnSizeChanged(e);
        }
        private void SetNewSize()
        {
            int size = Math.Max(Width,Height);
            Size = new Size(size,size);
        }
        public void set()
        {
            for (int i = 0; i < 8; i++)
            {
                Draw();
                //Bitmap map = new Bitmap((bitmap[i]),new Size(120,110));
                Bitmap map = new Bitmap((bitmap[i]), new Size(100, 90));
                images.Add(map);
            }
            pictureBox2.Image = (Image)images[0];
            pictureBox2.Size = pictureBox2.Image.Size;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void index_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            base.Dispose();
        }
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            set();
            count = (count + 1) % 8;
            pictureBox2.Image = (Image)images[count];
            time++;
            if (time==5)
            {
                this.Hide();
                timer1.Enabled = false;
                Form1 form = new Form1();
                form.Show();
            }
            
        }
    }
}
