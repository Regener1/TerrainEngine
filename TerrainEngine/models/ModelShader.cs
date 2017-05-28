using GlmNet;
using SharpGL;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.tool;

namespace TerrainEngine.models
{
    public class ModelShader
    {
        private string _vertexShaderPath = "";
        private string _fragmentShaderPath = "";
        private Dictionary<uint, string> _attributeLocation = new Dictionary<uint, string>();
        private ShaderProgram _shaderProgram = new ShaderProgram();

        private int _locationTransformationMatrix;
        private int _locationProjectionMatrix;
        private int _locationViewMatrix;

        public ModelShader(OpenGL gl, string vertexShaderPath, string fragmentShaderPath)
        {
            this._vertexShaderPath = vertexShaderPath;
            this._fragmentShaderPath = fragmentShaderPath;
            CreateShaders(gl);
        }

        public string VertexShaderPath
        {
            get { return _vertexShaderPath; }
            set { _vertexShaderPath = value; }
        }

        public string FragmentShaderPath
        {
            get { return _fragmentShaderPath; }
            set { _fragmentShaderPath = value; }
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

        public void LoadBrushRadius(OpenGL gl, float radius)
        {
            _shaderProgram.SetUniform1(gl, "brushRadius", radius);
        }

        public void LoadTerrainSize(OpenGL gl, float size)
        {
            _shaderProgram.SetUniform1(gl, "terrainSize", size);
        }

        public void LoadBrushPosition(OpenGL gl, float x, float y, float z)
        {
            _shaderProgram.SetUniform3(gl, "brushPosition", x, y, z);
        }

        private void CreateShaders(OpenGL gl)
        {
            AddAttributeLocation(0, "position");
            AddAttributeLocation(1, "textureCoordinates");

            string vertexShaderCode = "";
            string fragmentShaderCode = "";

            try
            {
                vertexShaderCode = ManifestResourceLoader.LoadShaderCode(_vertexShaderPath);
                fragmentShaderCode = ManifestResourceLoader.LoadShaderCode(_fragmentShaderPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                _shaderProgram.Create(gl, vertexShaderCode, fragmentShaderCode,
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
        private void GetAllUniformLocations(OpenGL gl)
        {
            _locationTransformationMatrix = _shaderProgram.
                GetUniformLocation(gl, "transformationMatrix");
            _locationProjectionMatrix = _shaderProgram.
                GetUniformLocation(gl, "projectionMatrix");
            _locationViewMatrix = _shaderProgram.
                GetUniformLocation(gl, "viewMatrix");
        }

        private int GetUniformLocation(OpenGL gl, string uniformName)
        {
            return gl.GetUniformLocation(_shaderProgram.ShaderProgramObject, uniformName);
        }

        private void AddAttributeLocation(uint key, string value)
        {
            _attributeLocation.Add(key, value);
        }
    }


}
