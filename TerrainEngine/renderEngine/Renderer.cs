using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.entities;
using TerrainEngine.shaders.objects;
using TerrainEngine.tool;

namespace TerrainEngine.renderEngine
{

    public class Renderer
    {
        private uint _renderType = OpenGL.GL_TRIANGLES;

        private float _fov = 45;
        private float _near = 0.1f;
        private float _far = 1000;

        private mat4 _projectionMatrix;

        public mat4 ProjectionMatrix
        {
            get
            {
                return _projectionMatrix;
            }
        }

        public Renderer(float fov, float near, float far, int width, int height)
        {
            SetViewProperties(fov, near, far, width, height);
        }

        public void SetViewProperties(float fov, float near, float far, int width, int height)
        {
            this._fov = fov;
            this._near = near;
            this._far = far;
            this._projectionMatrix = TerrainEngineMath.CreateProjectionMatrix(_fov, width, height,
                                                                    _near, _far);
        }

        public void Prepare(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1, 1, 1, 1);
        }

        public void Render(OpenGL gl, Terrain terrain, List<Model> models, TerrainBrush brush, Camera camera)
        {
            SetProjectionMatrix(gl, terrain.Shader);
            SetViewMatrix(gl, terrain.Shader, camera);

            RenderTerrain(gl, terrain, brush);

            for (int i = 0; i < models.Count; i++)
            {
                SetProjectionMatrix(gl, models[i].Shader);
                SetViewMatrix(gl, models[i].Shader, camera);

                RenderModel(gl, models[i]);
            }
        }

        private void RenderModel(OpenGL gl, Model model)
        {
            model.StartShader();

            gl.BindVertexArray(model.Id);
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);

            mat4 transformationMatrix = TerrainEngineMath.CreateTransformationMatrix(model.Position, model.Rotate, model.Scale);
            model.SetTransformationMatrix(transformationMatrix);

            gl.ActiveTexture(OpenGL.GL_TEXTURE0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, model.Texture.Id);
            gl.DrawElements(_renderType, model.Indices.Length, model.Indices);
            gl.DisableVertexAttribArray(0);
            gl.DisableVertexAttribArray(1);
            gl.BindVertexArray(0);

            model.StopShader();
        }

        private void RenderTerrain(OpenGL gl, Terrain terrain, TerrainBrush brush)
        {
            terrain.StartShader();

            gl.BindVertexArray(terrain.Id);
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);
            gl.EnableVertexAttribArray(2);

            terrain.LoadShaderVariables();

            mat4 transformationMatrix = TerrainEngineMath.CreateTransformationMatrix(new vec3(0, 0, 0), new vec3(0, 0, 0), 1);
            terrain.SetTransformationMatrix(transformationMatrix);

            BindTerrainTexturePack(gl, terrain.Textures);
            gl.DrawElements(_renderType, terrain.Indices.Length, terrain.Indices);
            gl.DisableVertexAttribArray(0);
            gl.DisableVertexAttribArray(1);
            gl.DisableVertexAttribArray(2);
            gl.BindVertexArray(0);

            terrain.StopShader();
        }

        private void BindTerrainTexturePack(OpenGL gl, ModelTexture[] terrainTexturePack)
        {
            gl.ActiveTexture(OpenGL.GL_TEXTURE0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack[0].Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE1);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack[1].Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE2);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack[2].Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE3);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack[3].Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE4);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack[4].Id);
        }

        public void SetProjectionMatrix(OpenGL gl, BaseShader shader)
        {
            shader.Start();
            shader.LoadProjectionMatrix(_projectionMatrix);
            shader.Stop();
        }

        public void SetViewMatrix(OpenGL gl, BaseShader shader, Camera camera)
        {
            shader.Start();
            shader.LoadViewMatrix(TerrainEngineMath.CreateViewMatrix(camera));
            shader.Stop();
        }


    }
}
