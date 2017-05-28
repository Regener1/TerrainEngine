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

        private int _width = 1280;
        private int _height = 720;

        private mat4 _projectionMatrix;

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

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
            _projectionMatrix = MatrixMath.CreateProjectionMatrix(_fov, _width, _height,
                                                                    _near, _far);
        }

        public void SetViewProperties(float fov, float near, float far, int width, int height)
        {
            this._fov = fov;
            this._near = near;
            this._far = far;
            this._width = width;
            this._height = height;
        }

        public void Prepare(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1, 1, 1, 1);
        }

        public void Render(OpenGL gl, Terrain terrain, List<Object3D> entities, TerrainBrush brush, Camera camera)
        {
            SetProjectionMatrix(gl, terrain.ModelShader);
            SetViewMatrix(gl, terrain.ModelShader, camera);

            RenderTerrain(gl, terrain);

            for (int i = 0; i < entities.Count; i++)
            {
                SetProjectionMatrix(gl, entities[i].Shader);
                SetViewMatrix(gl, entities[i].Shader, camera);

                RenderEntity(gl, entities[i]);
            }

            if (brush != null)
            {
                SetProjectionMatrix(gl, brush.Object.Shader);
                SetViewMatrix(gl, brush.Object.Shader, camera);

                RenderEntity(gl, brush.Object);
            }
        }

        private void RenderEntity(OpenGL gl, Object3D entity)
        {
            entity.Shader.Start(gl);

            gl.BindVertexArray(entity.Model.Id);
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

        private void RenderLines(OpenGL gl, Object3D entity)
        {
            entity.Shader.Start(gl);

            gl.BindVertexArray(entity.Model.Id);
            gl.EnableVertexAttribArray(0);

            mat4 transformationMatrix = MatrixMath.CreateTransformationMatrix(entity.Position,
                entity.RotX, entity.RotY, entity.RotZ, entity.Scale);
            entity.Shader.LoadTransformationMatrix(gl, transformationMatrix);

            gl.DrawElements(OpenGL.GL_LINES, entity.Model.Indices.Length, entity.Model.Indices);
            gl.DisableVertexAttribArray(0);
            gl.BindVertexArray(0);

            entity.Shader.Stop(gl);
        }

        private void RenderTerrain(OpenGL gl, Terrain entity)
        {
            entity.ModelShader.Start(gl);

            gl.BindVertexArray(entity.Model.Id);
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);

            mat4 transformationMatrix = MatrixMath.CreateTransformationMatrix(new vec3(0, 0, 0), 0, 0, 0, 1);
            entity.ModelShader.LoadTransformationMatrix(gl, transformationMatrix);

            gl.ActiveTexture(OpenGL.GL_TEXTURE0);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, entity.ModelTexture.Id);
            gl.DrawElements(OpenGL.GL_TRIANGLES, entity.Model.Indices.Length, entity.Model.Indices);
            gl.DisableVertexAttribArray(0);
            gl.DisableVertexAttribArray(1);
            gl.BindVertexArray(0);

            entity.ModelShader.Stop(gl);
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
