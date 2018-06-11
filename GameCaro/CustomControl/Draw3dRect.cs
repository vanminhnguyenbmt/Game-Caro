using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hsCustomControl
{
    public partial class Draw3dRect : UserControl
    {
        Color BLUE_CELL_TL = Color.FromArgb(255, 240, 240, 246);
        Color BLUE_CELL_BR = Color.FromArgb(255, 170, 170, 182);
        Color BLUE_CELL_GT = Color.FromArgb(255, 51, 51, 51);
        public Draw3dRect()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Draw3dRectangle(g, new Rectangle(0, 0, 240, 95), BLUE_CELL_TL, BLUE_CELL_BR);
        }

        void Draw3dRectangle(Graphics g, Rectangle r, Color clrTopLeft, Color clrBottomRight)
        {
            int x = r.X;
            int y = r.Y;
            int width = r.Width;
            int height = r.Height;

            g.FillRectangle(new SolidBrush(BLUE_CELL_GT), r);
            g.FillRectangle(new SolidBrush(clrBottomRight), x, y, 2, height);
            g.FillRectangle(new SolidBrush(clrBottomRight), x + 1, y, width - 1, 2);
            g.FillRectangle(new SolidBrush(clrTopLeft), x + 1, y + height - 1, width, 2);
            g.FillRectangle(new SolidBrush(clrTopLeft), x + width - 1, y, 2, height);
        }
    }
}
