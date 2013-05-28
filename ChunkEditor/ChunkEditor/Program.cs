using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ChunkManager;

namespace ChunkEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            room = new int[roomWidth, roomHeight];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public enum BrushState
        {
            InvalidState = -1,
            AddTile = 1,
            RemoveTile = 2,
            FillTool = 3,
        }

        private static int roomWidth = 24;
        private static int roomHeight = 24;
        public static int RoomWidth { get { return roomWidth; } }
        public static int RoomHeight { get { return roomHeight; } }

        public static int[,] room;

        private static BrushState programBrushState = BrushState.AddTile;
        public static BrushState ProgramBrushState { get { return programBrushState; } set { programBrushState = value; } }
    }

    public class EditorChunkAttribute
    {
        public string attributeName;
        public List<string> attributeData;

        public EditorChunkAttribute(string attributeName)
        {
            this.attributeName = attributeName;
            attributeData = new List<string>();
        }
    }
}
