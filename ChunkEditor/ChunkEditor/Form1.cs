using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChunkEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            drawPane.Click += new System.EventHandler(this.drawPane_Click);

            brushSelection.Text = "Add Tile";
            Program.ProgramBrushState = Program.BrushState.AddTile;
        }

        private void drawPane_Click(object sender, EventArgs e)
        {
            int xPos = (int)(Program.RoomWidth*((this.PointToClient(Cursor.Position).X - 11) / ((float)drawPane.Bounds.Width)));
            int yPos = (int)(Program.RoomHeight*(this.PointToClient(Cursor.Position).Y - 38)/((float)drawPane.Bounds.Height));

            switch (Program.ProgramBrushState)
            {
                case Program.BrushState.AddTile:
                    Program.room[xPos % Program.RoomWidth, yPos % Program.RoomHeight] = 1;
                    break;
                case Program.BrushState.RemoveTile:
                    Program.room[xPos % Program.RoomWidth, yPos % Program.RoomHeight] = 0;
                    break;
                default:
                    break;
            }

            drawPane.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutChunkEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow about = new AboutWindow();
            DialogResult dialogResult = about.ShowDialog();

            about.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void drawPane_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float tileWidth = e.ClipRectangle.Width / (float)Program.RoomWidth;
            float tileHeight = e.ClipRectangle.Height / (float)Program.RoomHeight;
            Pen p = new Pen(System.Drawing.Color.Blue);
            Brush b = new SolidBrush(Color.CornflowerBlue);

            for (int i = 0; i < Program.room.GetLength(0); i++)
            {
                for (int j = 0; j < Program.room.GetLength(1); j++)
                {
                    Rectangle rect = new Rectangle((int)(i * tileWidth), (int)(j * tileHeight), (int)tileWidth, (int)tileHeight);
                    
                    if (Program.room[i, j] == 0)
                    {
                        g.DrawRectangle(p, rect);
                    }
                    else
                    {
                        g.FillRectangle(b, rect);
                    }
                }
            }

            b.Dispose();
            p.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (brushSelection.Text)
            {
                case "Add Tile":
                    Program.ProgramBrushState = Program.BrushState.AddTile;
                    break;
                case "Remove Tile":
                    Program.ProgramBrushState = Program.BrushState.RemoveTile;
                    break;
                default:
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
