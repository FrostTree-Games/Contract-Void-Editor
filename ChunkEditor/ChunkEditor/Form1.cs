using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ChunkManager;

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

        private void floodFill(int[,] room, int x, int y)
        {
            if (room == null)
            {
                return;
            }

            if (room[x, y] != 0)
            {
                return;
            }

            room[x, y] = 1;

            if (x > 0) { floodFill(room, x - 1, y); }
            if (y > 0) { floodFill(room, x, y - 1); }
            if (x < Program.RoomWidth - 1) { floodFill(room, x + 1, y); }
            if (y < Program.RoomHeight - 1) { floodFill(room, x, y + 1); }
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
                case Program.BrushState.FillTool:
                    floodFill(Program.room, xPos % Program.RoomWidth, yPos % Program.RoomHeight);
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
            Stream st = null;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "FrostTree Chunk Format|*.ftc";
            open.Title = "Load a Chunk";
            open.RestoreDirectory = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((st = open.OpenFile()) != null)
                    {
                        using (st)
                        {
                            Chunk c = ChunkSerializer.ReadChunk(st);

                            clearGUI();

                            chunkNameBox.Text = c.Name;

                            foreach (Chunk.ChunkAttribute attr in c.Attributes)
                            {
                                listView1.Items.Add(attr.AttributeName);
                            }

                            listView1.Invalidate();

                            for (int i = 0; i < Program.room.GetLength(0); i++)
                            {
                                for (int j = 0; j < Program.room.GetLength(0); j++)
                                {
                                    Program.room[i, j] = c.tilemap[(j * Program.RoomHeight) + i];
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read Chunk from disk. Original error: \n" + ex.Message);
                }
            }
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
                case "Flood Fill":
                    Program.ProgramBrushState = Program.BrushState.FillTool;
                    break;
                default:
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void clearMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Program.room.GetLength(0); i++)
            {
                for (int j = 0; j < Program.room.GetLength(1); j++)
                {
                    Program.room[i, j] = 0;
                }
            }

            drawPane.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chunkNameBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a name for the Chunk", "No Chunk Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "FrostTree Chunk Format|*.ftc";
            save.Title = "Save a Chunk";
            save.ShowDialog();

            if (save.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)save.OpenFile();

                switch (save.FilterIndex)
                {
                    case 1:

                        Chunk c = new Chunk();
                        c.Name = chunkNameBox.Text;
                        c.tilemap = new int[Program.RoomHeight * Program.RoomWidth];

                        for (int i = 0; i < Program.room.GetLength(0); i++)
                        {
                            for (int j = 0; j < Program.room.GetLength(0); j++)
                            {
                                c.tilemap[(j * Program.RoomHeight) + i] = Program.room[i, j];
                            }
                        }

                        List<Chunk.ChunkAttribute> attribList = new List<Chunk.ChunkAttribute>();
                        foreach (ListViewItem lvi in listView1.Items)
                        {
                            Chunk.ChunkAttribute ca = new Chunk.ChunkAttribute();
                            ca.AttributeName = lvi.Text;
                            attribList.Add(ca);
                        }
                        c.Attributes = attribList.ToArray();

                        ChunkSerializer.WriteChunk(fs, c);

                        break;
                }

                fs.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                attributeNameField.Text = listView1.SelectedItems[0].Text;
            }
            catch (Exception)
            {
                //whatever
            }
        }

        private void addAttributeButton_Click(object sender, EventArgs e)
        {
            if (attributeNameField.Text == null)
            {
                return;
            }
            else if (attributeNameField.Text.Length == 0)
            {
                return;
            }

            foreach (ListViewItem lvi in listView1.Items)
            {
                if (lvi.Text == attributeNameField.Text)
                {
                    return;
                }
            }

            listView1.Items.Add(attributeNameField.Text);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Text == attributeNameField.Text)
                {
                    listView1.Items.RemoveAt(i);
                    break;
                }
            }
        }

        private void removeAttributesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void attributeNameField_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearGUI()
        {
            listView1.Items.Clear();
            attributeNameField.Clear();

            for (int i = 0; i < Program.room.GetLength(0); i++)
            {
                for (int j = 0; j < Program.room.GetLength(1); j++)
                {
                    Program.room[i, j] = 0;
                }
            }

            drawPane.Invalidate();

            chunkNameBox.Clear();
        }

        private void totalClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            attributeNameField.Clear();

            for (int i = 0; i < Program.room.GetLength(0); i++)
            {
                for (int j = 0; j < Program.room.GetLength(1); j++)
                {
                    Program.room[i, j] = 0;
                }
            }

            drawPane.Invalidate();

            chunkNameBox.Clear();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
