using SharpGL;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.models;
using System.Drawing;
using System.Drawing.Imaging;
using GlmNet;

namespace TerrainEngine.renderEngine
{
    public class Loader
    {
        private List<uint> _vaos = new List<uint>();
        private List<uint> _vbos = new List<uint>();
        private List<uint> _textures = new List<uint>();

        public void ReloadTerrain(OpenGL gl, Terrain entity)
        {
            DeleteEntityModel(gl, entity.Model.VAOId, entity.Model.IndicesId, entity.Model.VerticesId,
                        entity.Model.TextureCoordsId, entity.Model.NormalsId);

            LoadEntityToVao(gl, entity.Model, entity.Model.Vertices, entity.Model.TextureCoords, 
                                                entity.Model.Indices, entity.Model.Normals);
        }

        public ModelShader LoadEntityShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode)
        {
            return new ModelShader(gl, vertexShaderCode, fragmentShaderCode);
        }

        public TerrainShader LoadTerrainShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode)
        {
            return new TerrainShader(gl, vertexShaderCode, fragmentShaderCode);
        }

        public void LoadEntityToVao(OpenGL gl, Model model, float[] vertices, float[] textureCoords, uint[] indices,
                                            float[] normals)
        {
            uint vaoId = BindVao(gl);
            uint indicesId = SetIndexBuffer(gl, indices);
            uint verticesId = SetVbo(gl, 0, 3, vertices);
            uint textureCoordsId = SetVbo(gl, 1, 2, textureCoords);
            uint normalsId = SetVbo(gl, 2, 3, normals);
            UnbindVao(gl);

            model.VAOId = vaoId;
            model.IndicesId = indicesId;
            model.VerticesId = verticesId;
            model.TextureCoordsId = textureCoordsId;
            model.NormalsId = normalsId;

            model.Indices = indices;
            model.Vertices = vertices;
            model.TextureCoords = textureCoords;
            model.Normals = normals;
        }


        public void DeleteEntityModel(OpenGL gl, uint vaoId, uint indicesId, uint verticesId, 
                                uint textureCoordsId, uint normalsId)
        {
            DeleteVbo(gl, indicesId);
            DeleteVbo(gl, verticesId);
            DeleteVbo(gl, textureCoordsId);
            DeleteVbo(gl, normalsId);
            DeleteVao(gl, vaoId);
        }

        public void DeleteEntity(OpenGL gl, uint vaoId, uint indicesId, uint verticesId,
                                uint textureCoordsId, uint normalsId, uint textureId)
        {
            DeleteVbo(gl, indicesId);
            DeleteVbo(gl, verticesId);
            DeleteVbo(gl, textureCoordsId);
            DeleteVbo(gl, normalsId);
            DeleteVao(gl, vaoId);
            DeleteTexture(gl, textureId);
        }
        
        public void ReloadTerrainTexture(OpenGL gl, ModelTexture modelTexture, Image textureImg)
        {
            DeleteTexture(gl, modelTexture.Id);
            LoadTexture(gl, modelTexture, textureImg);
        }

        private uint BindVao(OpenGL gl)
        {
            uint[] ids = new uint[1];
            gl.GenVertexArrays(1, ids);
            uint vaoId = ids[0];
            gl.BindVertexArray(vaoId);
            return vaoId;
        }

        private void UnbindVao(OpenGL gl)
        {
            gl.BindVertexArray(0);
        }

        private void DeleteVbo(OpenGL gl, uint id)
        {
            gl.DeleteBuffers(1, new uint[] { id });
        }

        private void DeleteVao(OpenGL gl, uint id)
        {
            gl.DeleteVertexArrays(1, new uint[] { id });
        }

        private void DeleteTexture(OpenGL gl, uint id)
        {
            gl.DeleteTextures(1, new uint[] { id });
        }

        private uint SetIndexBuffer(OpenGL gl, uint[] indices)
        {
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            uint indexId = ids[0];
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexId);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ToUShort(indices), OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);

            return indexId;
        }

        private uint SetVbo(OpenGL gl, uint attributeIndex, int stride, float[] data)
        {
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            uint vboId = ids[0];
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboId);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, data, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);

            return vboId;
        }

        public void LoadTexture(OpenGL gl, ModelTexture modelTexture, Image textureImg)
        {
            uint[] ids = new uint[1];
            gl.GenTextures(1, ids);
            uint textureId = ids[0];
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, textureId);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);

            Bitmap bitmap = new Bitmap(textureImg);

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA,
                bitmap.Width, bitmap.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE,
                bmpData.Scan0);
            bitmap.UnlockBits(bmpData);

            bitmap.Dispose();

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            modelTexture.Id = textureId;
            modelTexture.Image = textureImg;

        }

        public void LoadTerrainTexturePack(OpenGL gl, TerrainTexturePack terrainTexturePack, Image blendMap,
                Image backgroundTexture, Image rTexture, Image gTexture, Image bTexture)
        {
            
            LoadTexture(gl, terrainTexturePack.BackgroundTexture, backgroundTexture);
            LoadTexture(gl, terrainTexturePack.RTexture, rTexture);
            LoadTexture(gl, terrainTexturePack.GTexture, gTexture);
            LoadTexture(gl, terrainTexturePack.BTexture, bTexture);
            LoadTexture(gl, terrainTexturePack.BlendMap, blendMap);
        }

        private ushort[] ToUShort(uint[] arr)
        {
            ushort[] res = new ushort[arr.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Convert.ToUInt16(arr[i]);
            }
            return res;
        }
    }
}
