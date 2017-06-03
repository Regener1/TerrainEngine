using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.models
{
    public class Model
    {
        public uint VAOId { get; set; }
        public uint VerticesId { get; set; }
        public uint NormalsId { get; set; }
        public uint TextureCoordsId { get; set; }
        public uint IndicesId { get; set; }
        public uint[] Indices { get; set; }
        public float[] Vertices { get; set; }
        public float[] TextureCoords { get; set; }
        public float[] Normals { get; set; }

        public Model(uint vaoId, uint verticesId, uint textureCoordsId, uint normalsId, uint indicesId,
                    float[] vertices, float[] textureCoords, uint[] indices, float[] normals)
        {
            this.VAOId = vaoId;
            this.VerticesId = verticesId;
            this.TextureCoordsId = textureCoordsId;
            this.NormalsId = normalsId;
            this.IndicesId = indicesId;

            this.Indices = indices;
            this.Vertices = vertices;
            this.TextureCoords = textureCoords;
            this.Normals = normals;
        }

        public Model()
        {
            
        }
    }
}
