using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChunkManager
{
    /// <summary>
    /// Serializable class for reading/writing chunk data to disk.
    /// </summary>
    public class Chunk
    {
        public class ChunkAttribute
        {
            /// <summary>
            /// Attribute identifier.
            /// </summary>
            public string AttributeName;

            /// <summary>
            /// Array of strings for each attribute, if necessary.
            /// </summary>
            public string[] Metadata;
        }

        /// <summary>
        /// Identifier.
        /// </summary>
        public string Name;

        /// <summary>
        /// 1D array of tile data.
        /// </summary>
        public int[] tilemap;

        /// <summary>
        /// Array of attributes per chunk.
        /// </summary>
        public ChunkAttribute[] Attributes;
    }
}
