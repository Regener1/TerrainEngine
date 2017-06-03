using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.entities;
using TerrainEngine.models;
using TerrainEngine.tool;

namespace TerrainEngine.renderEngine
{
    public class Renderer
    {
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
            _projectionMatrix = MatrixMath.CreateProjectionMatrix(_fov, width, height,
                                                                    _near, _far);
        }

        public void SetViewProperties(float fov, float near, float far, int width, int height)
        {
            this._fov = fov;
            this._near = near;
            this._far = far;
        }

        public void Prepare(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1, 1, 1, 1);
        }

        public void Render(OpenGL gl, Terrain terrain, List<Object3D> entities, TerrainBrush brush, Camera camera)
        {
            SetProjectionMatrix(gl, terrain.Shader);
            SetViewMatrix(gl, terrain.Shader, camera);

            RenderTerrain(gl, terrain, brush);

            for (int i = 0; i < entities.Count; i++)
            {
                SetProjectionMatrix(gl, entities[i].Shader);
                SetViewMatrix(gl, entities[i].Shader, camera);

                RenderEntity(gl, entities[i]);
            }
        }

        private void RenderEntity(OpenGL gl, Object3D entity)
        {
            entity.Shader.Start(gl);

            gl.BindVertexArray(entity.Model.VAOId);
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);

            mat4 transformationMatrix = MatrixMath.CreateTransformationMatrix(entity.Position,
                entity.RotX, entity.RotY, entity.RotZ, entity.Scale);
            entity.Shader.LoadTransformationMatrix(gl, transformationMatrix);

            gl.ActiveTexture(OpenGL.GL_TEXTURE0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, entity.Texture.Id);
            gl.DrawElements(OpenGL.GL_TRIANGLES, entity.Model.Indices.Length, entity.Model.Indices);
            gl.DisableVertexAttribArray(0);
            gl.DisableVertexAttribArray(1);
            gl.BindVertexArray(0);

            entity.Shader.Stop(gl);
        }

        private void RenderTerrain(OpenGL gl, Terrain entity, TerrainBrush brush)
        {
            entity.Shader.Start(gl);

            gl.BindVertexArray(entity.Model.VAOId);
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);
            gl.EnableVertexAttribArray(2);

            (entity.Shader as TerrainShader).LoadTerrainSize(gl, entity.TerrainSize);
            (entity.Shader as TerrainShader).LoadBrush(gl, brush);
            (entity.Shader as TerrainShader).LoadTerrainTextures(gl);
            entity.Shader.LoadLight(gl, entity.Light);

            mat4 transformationMatrix = MatrixMath.CreateTransformationMatrix(new vec3(0, 0, 0), 0, 0, 0, 1);
            entity.Shader.LoadTransformationMatrix(gl, transformationMatrix);

            BindTerrainTexturePack(gl, entity.TerrainTextures);
            gl.DrawElements(OpenGL.GL_TRIANGLES, entity.Model.Indices.Length, entity.Model.Indices);
            gl.DisableVertexAttribArray(0);
            gl.DisableVertexAttribArray(1);
            gl.DisableVertexAttribArray(2);
            gl.BindVertexArray(0);

            entity.Shader.Stop(gl);
        }

        private void BindTerrainTexturePack(OpenGL gl, TerrainTexturePack terrainTexturePack)
        {
            gl.ActiveTexture(OpenGL.GL_TEXTURE0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack.BackgroundTexture.Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE1);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack.RTexture.Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE2);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack.GTexture.Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE3);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack.BTexture.Id);
            gl.ActiveTexture(OpenGL.GL_TEXTURE4);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, terrainTexturePack.BlendMap.Id);
        }

        public void SetProjectionMatrix(OpenGL gl, ModelShader shader)
        {

            shader.Start(gl);
            shader.LoadProjectionMatrix(gl, _projectionMatrix);
            shader.Stop(gl);
        }

        public void SetViewMatrix(OpenGL gl, ModelShader shader, Camera camera)
        {
            shader.Start(gl);
            shader.LoadViewMatrix(gl, MatrixMath.CreateViewMatrix(camera));
            shader.Stop(gl);
        }


    }
}
