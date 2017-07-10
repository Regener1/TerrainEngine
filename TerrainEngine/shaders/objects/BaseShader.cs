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

namespace TerrainEngine.shaders.objects
{
    public abstract class BaseShader
    {
        public abstract void LoadAllVariables();

        protected OpenGL _gl;
        protected string _vertexShaderCode = "";
        protected string _fragmentShaderCode = "";
        protected Dictionary<uint, string> _attributeLocation = new Dictionary<uint, string>();
        protected ShaderProgram _shaderProgram = new ShaderProgram();

        protected int _locationTransformationMatrix;
        protected int _locationProjectionMatrix;
        protected int _locationViewMatrix;

        private Light[] _lights;

        public BaseShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode, Light[] lights)
        {
            this._vertexShaderCode = vertexShaderCode;
            this._fragmentShaderCode = fragmentShaderCode;
            this._gl = gl;
            this._lights = lights;
            CreateShaders();
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

        public void Start()
        {
            _shaderProgram.Bind(_gl);
        }

        public void Stop()
        {
            _shaderProgram.Unbind(_gl);
        }

        public void Delete()
        {
            _shaderProgram.Delete(_gl);
        }

        public void LoadTransformationMatrix(mat4 matrix)
        {
            _shaderProgram.SetUniformMatrix4(_gl, "transformationMatrix", matrix.to_array());
        }

        public void LoadViewMatrix(mat4 matrix)
        {
            _shaderProgram.SetUniformMatrix4(_gl, "viewMatrix", matrix.to_array());
        }

        public void LoadProjectionMatrix(mat4 matrix)
        {
            _shaderProgram.SetUniformMatrix4(_gl, "projectionMatrix", matrix.to_array());
        }

        
        private void CreateShaders()
        {
            AddAttributeLocation(0, "position");
            AddAttributeLocation(1, "textureCoordinates");
            AddAttributeLocation(2, "normal");

            try
            {
                _shaderProgram.Create(_gl, _vertexShaderCode, _fragmentShaderCode,
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

        public void LoadLight()
        {
            _shaderProgram.SetUniform3(_gl, "lightPosition", _lights[0].Position.x, _lights[0].Position.y, _lights[0].Position.z);
            _shaderProgram.SetUniform3(_gl, "lightColor", _lights[0].Color.x, _lights[0].Color.y, _lights[0].Color.z);
        }

        private void AddAttributeLocation(uint key, string value)
        {
            _attributeLocation.Add(key, value);
        }
    }


}
