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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DrawSurface _surface = new DrawSurface();
        private GCViewport _view = new GCViewport();
        private Timer _timer = new Timer();
        private MouseTracker _mst = new MouseTracker();
        private Form2 frm = new Form2();
        
        private void Form1_Load(object sender, EventArgs e)
        {
            _view.View = new RectangleD(-2 * Math.PI, 2 * Math.PI, -2 * Math.PI, 2 * Math.PI);
            _view.Window = DisplayRectangle;
            _view.IsYUp = true;
            _surface.Viewports.Add(_view);
            _surface.Viewports.Add(frm._view);

            _surface.Items.Add(new Background());
            _surface.Items.Add(new SinWave());
            _surface.Items.Add(_mst);

            this.Paint += new PaintEventHandler(Form1_Paint);

            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            _mst.Modified += new DrawableModifiedEvent(_mst_Modified);

            //_timer.Tick += new EventHandler(_timer_Tick);
            //_timer.Interval = 250;
            //_timer.Start();

            this.DoubleBuffered = true;

            frm.Show();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm.Close();
        }

        void _mst_Modified(IDrawable sender)
        {
            Invalidate();
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            _mst.Origin = _view.Transform(e.Location);
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            //Invalidate();
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            _view.GC = e.Graphics;
            _surface.Draw();
            _view.GC = null;
        }
    }
}
