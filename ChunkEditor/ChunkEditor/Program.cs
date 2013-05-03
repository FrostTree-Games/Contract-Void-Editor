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

        private static int roomWidth = 16;
        private static int roomHeight = 16;
        public static int RoomWidth { get { return roomWidth; } }
        public static int RoomHeight { get { return roomHeight; } }

        public static int[,] room;
    }
}
