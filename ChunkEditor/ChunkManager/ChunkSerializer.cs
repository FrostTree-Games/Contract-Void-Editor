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
        /// Reads a Chunk from disk.
        /// </summary>
        /// <param name="path">Stream for reading Chunk.</param>
        /// <returns></returns>
        public static Chunk ReadChunk(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Chunk));

            Chunk c = (Chunk)serializer.Deserialize(stream);

            return c;
        }

        /// <summary>
        /// Writes a Chunk to disk using a specified Path.
        /// </summary>
        /// <param name="path">URL path for writing Chunk.</param>
        /// <param name="chunk">Chunk to write to disk.</param>
        public static void WriteChunk(string path, Chunk chunk)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(chunk.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);

            writer.Serialize(file, chunk);
            file.Close();
        }

        /// <summary>
        /// Writes a Chunk to disk using a specified Stream.
        /// </summary>
        /// <param name="stream">Stream for writing Chunk.</param>
        /// <param name="chunk">Chunk to write to disk.</param>
        public static void WriteChunk(Stream stream, Chunk chunk)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(chunk.GetType());
            System.IO.StreamWriter file = new System.IO.StreamWriter(stream);

            writer.Serialize(file, chunk);
            file.Close();
        }
    }
}
