using SharpGL;
using SharpGL.VertexBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.entities;
using System.Drawing;
using System.Drawing.Imaging;
using GlmNet;
using TerrainEngine.tool;
using TerrainEngine.shaders.objects;

namespace TerrainEngine.renderEngine
{
    public class Loader
    {
        private List<uint> _vaos = new List<uint>();
        private List<uint> _vbos = new List<uint>();
        private List<uint> _textures = new List<uint>();

        private uint BindVao(OpenGL gl)
        {
            uint[] ids = new uint[1];
            gl.GenVertexArrays(1, ids);
            uint vaoId = ids[0];
            EnableVao(gl, vaoId);
            return vaoId;
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

        private uint SetIndexBuffer(OpenGL gl, uint[] indices)
        {
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            uint indexId = ids[0];
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexId);

            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, TerrainEngineMath.ToUShort(indices), OpenGL.GL_STATIC_DRAW);

            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);

            return indexId;
        }

        public ModelTexture LoadTexture(OpenGL gl, Image textureImg)
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


            return new ModelTexture(textureId, textureImg);
        }

        private void EnableVao(OpenGL gl, uint id)
        {
            gl.BindVertexArray(id);
        }

        private void UpdateVbo(OpenGL gl, uint id, uint attributeIndex, int stride, float[] data)
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, id);

            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, data, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        private void UpdateTexture(OpenGL gl, ModelTexture modelTexture)
        {
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, modelTexture.Id);

            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);

            Bitmap bitmap = new Bitmap(modelTexture.Image);

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, (int)OpenGL.GL_RGBA,
                bitmap.Width, bitmap.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE,
                bmpData.Scan0);
            bitmap.UnlockBits(bmpData);
            bitmap.Dispose();

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
        }

        private void UpdateIndexBuffer(OpenGL gl, uint id, uint[] indices)
        {
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, id);

            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, TerrainEngineMath.ToUShort(indices), OpenGL.GL_STATIC_DRAW);

            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }


        private void DisableVao(OpenGL gl)
        {
            gl.BindVertexArray(0);
        }

        private void DeleteVao(OpenGL gl, uint id)
        {
            gl.DeleteVertexArrays(1, new uint[] { id });
        }

        private void DeleteVbo(OpenGL gl, uint id)
        {
            gl.DeleteBuffers(1, new uint[] { id });
        }

        private void DeleteTexture(OpenGL gl, uint id)
        {
            gl.DeleteTextures(1, new uint[] { id });
        }



        //public void ReloadTerrain(OpenGL gl, Terrain entity)
        //{
        //    DeleteEntityModel(gl, entity.Model.VAOId, entity.Model.IndicesId, entity.Model.VerticesId,
        //                entity.Model.TextureCoordsId, entity.Model.NormalsId);

        //    LoadEntityToVao(gl, entity.Model, entity.Model.Vertices, entity.Model.TextureCoords,
        //                                        entity.Model.Indices, entity.Model.Normals);
        //}

        private ModelShader LoadModelShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode, Light[] lights)
        {
            return new ModelShader(gl, vertexShaderCode, fragmentShaderCode, lights);
        }

        private TerrainShader LoadTerrainShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode, Light[] lights,
            TerrainBrush terrainBrush, float terrainSize)
        {
            return new TerrainShader(gl, vertexShaderCode, fragmentShaderCode, lights, terrainBrush, terrainSize);
        }

        public Model LoadModelToVao(OpenGL gl, float[] vertices, uint[] indices, float[] uv, float[] normals,
                                    Image textureImg, Light[] lights, string vertexShader, string fragmentShader)
        {
            uint vaoId = BindVao(gl);
            uint indicesId = SetIndexBuffer(gl, indices);
            uint verticesId = SetVbo(gl, 0, 3, vertices);
            uint uvId = SetVbo(gl, 1, 2, uv);
            uint normalsId = SetVbo(gl, 2, 3, normals);
            DisableVao(gl);

            ModelTexture modelTexture = LoadTexture(gl, textureImg);

            BaseShader shader = LoadModelShader(gl, vertexShader, fragmentShader, lights);

            return new Model(vaoId, verticesId, indicesId, uvId, normalsId,
                vertices, indices, uv, normals,
                new vec3(0, 0, 0), new vec3(0, 0, 0), 1f,
                modelTexture, shader);
        }

        public Terrain LoadTerrainToVao(OpenGL gl, float[] vertices, uint[] indices, float[] uv, float[] normals,
                                    float posX, float posZ, float terrainSize, Image[] textures, Light[] lights,
                                    string vertexShader, string fragmentShader, TerrainBrush brush)
        {
            uint vaoId = BindVao(gl);
            uint indicesId = SetIndexBuffer(gl, indices);
            uint verticesId = SetVbo(gl, 0, 3, vertices);
            uint uvId = SetVbo(gl, 1, 2, uv);
            uint normalsId = SetVbo(gl, 2, 3, normals);
            DisableVao(gl);

            ModelTexture[] mTextures = new ModelTexture[textures.Length];
            for (int i = 0; i < mTextures.Length; i++)
            {
                mTextures[i] = LoadTexture(gl, textures[i]);
            }

            BaseShader shader = LoadTerrainShader(gl, vertexShader, fragmentShader, lights, brush, terrainSize );

            return new Terrain(vaoId, verticesId, indicesId, uvId, normalsId,
                vertices, indices, uv, normals, terrainSize,
                1f, posX, posZ, mTextures, shader, brush);
        }

        public void UpdateModel(OpenGL gl, Model model)
        {

            EnableVao(gl, model.Id);

            UpdateIndexBuffer(gl, model.IndicesId, model.Indices);

            UpdateVbo(gl, model.VerticesId, 0, 3, model.Vertices);
            UpdateVbo(gl, model.UvId, 1, 2, model.Uv);
            UpdateVbo(gl, model.NormalsId, 2, 3, model.Normals);

            DisableVao(gl);

            UpdateTexture(gl, model.Texture);
        }

        public void UpdateTerrain(OpenGL gl, Terrain terrain)
        {

            EnableVao(gl, terrain.Id);

            UpdateIndexBuffer(gl, terrain.IndicesId, terrain.Indices);

            UpdateVbo(gl, terrain.VerticesId, 0, 3, terrain.Vertices);
            UpdateVbo(gl, terrain.UvId, 1, 2, terrain.Uv);
            UpdateVbo(gl, terrain.NormalsId, 2, 3, terrain.Normals);

            DisableVao(gl);
        }

        public void FreeModel(OpenGL gl, Model model)
        {
            DeleteVbo(gl, model.IndicesId);
            DeleteVbo(gl, model.VerticesId);
            DeleteVbo(gl, model.UvId);
            DeleteVbo(gl, model.NormalsId);

            DeleteTexture(gl, model.GetTextureId());

            DeleteVao(gl, model.Id);
        }

        public void FreeTerrain(OpenGL gl, Terrain terrain)
        {
            DeleteVbo(gl, terrain.IndicesId);
            DeleteVbo(gl, terrain.VerticesId);
            DeleteVbo(gl, terrain.UvId);
            DeleteVbo(gl, terrain.NormalsId);

            for (int i = 0; i < terrain.GetTexturesCount(); i++)
            {
                DeleteTexture(gl, terrain.GetTextureId(i));
            }

            DeleteVao(gl, terrain.Id);
        }

        //public void LoadTerrainTexturePack(OpenGL gl, TerrainTexturePack terrainTexturePack, Image blendMap,
        //        Image backgroundTexture, Image rTexture, Image gTexture, Image bTexture)
        //{

        //    LoadTexture(gl, terrainTexturePack.BackgroundTexture, backgroundTexture);
        //    LoadTexture(gl, terrainTexturePack.RTexture, rTexture);
        //    LoadTexture(gl, terrainTexturePack.GTexture, gTexture);
        //    LoadTexture(gl, terrainTexturePack.BTexture, bTexture);
        //    LoadTexture(gl, terrainTexturePack.BlendMap, blendMap);
        //}


    }
}
