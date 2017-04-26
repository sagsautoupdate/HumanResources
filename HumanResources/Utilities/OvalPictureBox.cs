using System;
using System.Drawing;
using System.Windows.Forms;

internal class OvalPictureBox : PictureBox
{
    public OvalPictureBox()
    {
        BackColor = Color.DarkGray;
        DoubleBuffered = true;
    }

    protected override void OnResize(EventArgs e)
    {
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.White, ButtonBorderStyle.Outset);
    }
}