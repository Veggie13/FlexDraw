using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlexDraw;

namespace FlexDrawTest
{
    public partial class Form2 : Form
    {
        public GCViewport _view = new GCViewport();
        private Timer _timer = new Timer();

        public Form2()
        {
            InitializeComponent();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        void Form2_Paint(object sender, PaintEventArgs e)
        {
            _view.GC = e.Graphics;
            _view.EndDraw();
            _view.GC = null;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _view.View = new RectangleD(0, 5, 0, 5);
            _view.Window = DisplayRectangle;
            _view.IsYUp = false;

            this.Paint += new PaintEventHandler(Form2_Paint);

            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = 1000;
            _timer.Start();

            this.DoubleBuffered = true;
        }
    }
}
