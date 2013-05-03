using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace ChunkManager
{
    public class ChunkSerializer
    {
        /// <summary>
        /// Reads a Chunk from disk.
        /// </summary>
        /// <param name="path">URL path for reading Chunk.</param>
        /// <returns></returns>
        public static Chunk ReadChunk(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Chunk));

            StreamReader stream = new StreamReader(path);

            Chunk c = (Chunk)serializer.Deserialize(stream);

            return c;
        }

        /// <summary>
        /// Writes a Chunk to disk.
        /// </summary>
        /// <param name="path">URL path for writing Chunk.</param>
        /// <param name="chunk">Chunk to write to.</param>
        public static void WriteChunk(string path, Chunk chunk)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(chunk.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);

            writer.Serialize(file, chunk);
            file.Close();
        }
    }
}
