using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.entities;
using TerrainEngine.shaders;
using TerrainEngine.tool;

namespace TerrainEngine.models
{
    public class ModelShader
    {
        /*
         * СДЕЛАТЬ НАСЛЕДОВАНИЕ     ШЕЙДЕР ОБЪЕКТА/ ШЕЙДЕР ТЕРРИТОРИИ/ ШЕЙДЕР ЧЕГОНИБУДЬ ЕЩЁ 
         */
        protected string _vertexShaderCode = "";
        protected string _fragmentShaderCode = "";
        protected Dictionary<uint, string> _attributeLocation = new Dictionary<uint, string>();
        protected ShaderProgram _shaderProgram = new ShaderProgram();

        protected int _locationTransformationMatrix;
        protected int _locationProjectionMatrix;
        protected int _locationViewMatrix;

        public ModelShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode)
        {
            this._vertexShaderCode = vertexShaderCode;
            this._fragmentShaderCode = fragmentShaderCode;
            CreateShaders(gl);
        }

        public string VertexShaderCode
        {
            get { return _vertexShaderCode; }
            set { _vertexShaderCode = value; }
        }

        public string FragmentShaderCode
        {
            get { return _fragmentShaderCode; }
            set { _fragmentShaderCode = value; }
        }

        public void Start(OpenGL gl)
        {
            _shaderProgram.Bind(gl);
        }

        public void Stop(OpenGL gl)
        {
            _shaderProgram.Unbind(gl);
        }

        public void Delete(OpenGL gl)
        {
            _shaderProgram.Delete(gl);
        }

        public void LoadTransformationMatrix(OpenGL gl, mat4 matrix)
        {
            _shaderProgram.SetUniformMatrix4(gl, "transformationMatrix", matrix.to_array());
        }

        public void LoadViewMatrix(OpenGL gl, mat4 matrix)
        {
            _shaderProgram.SetUniformMatrix4(gl, "viewMatrix", matrix.to_array());
        }

        public void LoadProjectionMatrix(OpenGL gl, mat4 matrix)
        {
            _shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", matrix.to_array());
        }

        public void LoadLight(OpenGL gl, Light light)
        {
            _shaderProgram.SetUniform3(gl, "lightPosition", light.Position.x, light.Position.y, light.Position.z);
            _shaderProgram.SetUniform3(gl, "lightColor", light.Color.x, light.Color.y, light.Color.z);
        }

        private void CreateShaders(OpenGL gl)
        {
            AddAttributeLocation(0, "position");
            AddAttributeLocation(1, "textureCoordinates");
            AddAttributeLocation(2, "normal");

            try
            {
                _shaderProgram.Create(gl, _vertexShaderCode, _fragmentShaderCode,
                    _attributeLocation);
            }
            catch (Exception e)
            {
                Console.WriteLine("SHADER ERROR");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("SHADER ERROR");
            }
        }

        //not used
        //private void GetAllUniformLocations(OpenGL gl)
        //{
        //    _locationTransformationMatrix = _shaderProgram.
        //        GetUniformLocation(gl, "transformationMatrix");
        //    _locationProjectionMatrix = _shaderProgram.
        //        GetUniformLocation(gl, "projectionMatrix");
        //    _locationViewMatrix = _shaderProgram.
        //        GetUniformLocation(gl, "viewMatrix");
        //}

        //private int GetUniformLocation(OpenGL gl, string uniformName)
        //{
        //    return gl.GetUniformLocation(_shaderProgram.ShaderProgramObject, uniformName);
        //}

        private void AddAttributeLocation(uint key, string value)
        {
            _attributeLocation.Add(key, value);
        }
    }


}
