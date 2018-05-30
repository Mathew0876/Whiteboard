using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WhiteBoard1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // creates the window and the drawing surface
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        // drawing surface
        // pen used for drawing
        Graphics g;
        Pen p = new Pen(Color.Black, 2);
        private bool mouse_down = false;

        Bitmap bmp = new Bitmap(1089, 803);
        //C:\Users\Mathew\OneDrive\Pictures


        // starting coordinates for the line
        int x_cor;
        int y_cor;

        // when the mouse is down
        // save inital coordinates
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_down = true;
            x_cor = e.X;
            y_cor = e.Y;

        }

        // when mouse is released, exit drawing mode
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_down = false;
        }

        // when the mouse moves
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // if in drawing mode
            // draw a line from the previous point to the current point
            // update the inital point to the current point
            if (mouse_down)
            {
                g.DrawLine(p, new Point(x_cor, y_cor), new Point(e.X, e.Y));

                x_cor = e.X;
                y_cor = e.Y;
            }

        }

        // clears the panel
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
        }

        // closes the application
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // changes the colour of the pen
        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                p = new Pen(c.Color, p.Width);
            }
        }

        // creates an eraser by setting the pen colour to white
        private void eraserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p = new Pen(Color.White, p.Width);
        }

        // changes the width of the pen or eraser
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // catches error of invalied input null or less than 1
            try
            {
                p = new Pen(p.Color, float.Parse(textBox1.Text));
            }
            catch { }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /**using (Bitmap graphicSurface = new Bitmap(panel1.Width, panel1.Height))
            {
                using (StreamWriter bitmapWriter = new StreamWriter("TEST.JPEG"))
                {
                    panel1.DrawToBitmap(graphicSurface, new Rectangle(0, 0, panel1.Width, panel1.Height));
                    graphicSurface.Save(bitmapWriter.BaseStream, ImageFormat.Jpeg);
                }
            }*/

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Select output file:";
            dialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
                //panel1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                bmp.Save(dialog.FileName);// System.Drawing.Imaging.ImageFormat.Jpeg);
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Configure the message box to be displayed
            string messageBoxText = "Created May 2018" +
                "\nVersion 1.0\nReleased May 30 2018\nWritten by Mathew Smith";
            string caption = "White Board";
            MessageBoxButtons button = MessageBoxButtons.OKCancel;

            // Display message box
            MessageBox.Show(messageBoxText, caption, button);
        }
    }
}
