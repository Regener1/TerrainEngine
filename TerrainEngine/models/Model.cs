using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.models
{
    public class Model
    {
        public uint Id { get; set; }
        public uint[] Indices { get; set; }
        public float[] Vertices { get; set; }
        public float[] TextureCoords { get; set; }

        public Model(uint id, float[] vertices, float[] textureCoords, uint[] indices)
        {
            this.Id = id;
            this.Indices = indices;
            this.Vertices = vertices;
            this.TextureCoords = textureCoords;
        }
    }
}
