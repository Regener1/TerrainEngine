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

        public Model LoadEntityToVao(OpenGL gl, float[] vertices, float[] textureCoords, uint[] indices)
        {
            uint vaoId = BindVao(gl);
            SetIndexBuffer(gl, indices);
            SetVbo(gl, 0, 3, vertices);
            SetVbo(gl, 1, 2, textureCoords);
            UnbindVao(gl);
            return new Model(vaoId, vertices, textureCoords, indices);
        }

        public Model LoadLineToVao(OpenGL gl, float[] vertices, uint[] indices)
        {
            uint vaoId = BindVao(gl);
            SetIndexBuffer(gl, indices);
            SetVbo(gl, 0, 3, vertices);
            UnbindVao(gl);
            return new Model(vaoId, vertices, new float[1], indices);
        }

        public void ReloadEntityVao(OpenGL gl, Model model)
        {
            gl.BindVertexArray(model.Id);
            SetIndexBuffer(gl, model.Indices);
            SetVbo(gl, 0, 3, model.Vertices);
            SetVbo(gl, 1, 2, model.TextureCoords);
            gl.BindVertexArray(0);
        }

        public void ReloadLineVao(OpenGL gl, Model model)
        {
            gl.BindVertexArray(model.Id);
            SetIndexBuffer(gl, model.Indices);
            SetVbo(gl, 0, 3, model.Vertices);
            gl.BindVertexArray(0);
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

        private void SetIndexBuffer(OpenGL gl, uint[] indices)
        {
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            uint indexId = ids[0];
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexId);
            gl.BufferData(OpenGL.GL_ELEMENT_ARRAY_BUFFER, ToUShort(indices), OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, 0);
        }

        private void SetVbo(OpenGL gl, uint attributeIndex, int stride, float[] data)
        {
            uint[] ids = new uint[1];
            gl.GenBuffers(1, ids);
            uint vboId = ids[0];
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, vboId);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, data, OpenGL.GL_STATIC_DRAW);
            gl.VertexAttribPointer(attributeIndex, stride, OpenGL.GL_FLOAT, false, 0, IntPtr.Zero);
            gl.EnableVertexAttribArray(attributeIndex);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
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


            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            return new ModelTexture(textureId);

        }

        public Object3D CreateObject3D(Model model, ModelTexture texture, ModelShader shader, int id, vec3 position, float rotX, float rotY, float rotZ, float scale)
        {
            return new Object3D(model, texture, shader, id, position, rotX, rotY, rotZ, scale);
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
