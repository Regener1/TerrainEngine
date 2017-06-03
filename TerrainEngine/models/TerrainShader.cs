using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace TerrainEngine.models
{
    public class TerrainShader : ModelShader
    {
        public TerrainShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode) 
            : base(gl, vertexShaderCode, fragmentShaderCode)
        {

        }

        public void LoadTerrainSize(OpenGL gl, float size)
        {
            _shaderProgram.SetUniform1(gl, "terrainSize", size);
        }

        public void LoadBrush(OpenGL gl, TerrainBrush brush)
        {
            _shaderProgram.SetUniform1(gl, "brushRadius", brush.Radius);
            _shaderProgram.SetUniform3(gl, "brushPosition", brush.Position.x, brush.Position.y, brush.Position.z);
        }

        public void LoadTerrainTextures(OpenGL gl)
        {
            _shaderProgram.SetUniform1(gl, "backgroundTexture", 0);
            _shaderProgram.SetUniform1(gl, "rTexture", 1);
            _shaderProgram.SetUniform1(gl, "gTexture", 2);
            _shaderProgram.SetUniform1(gl, "bTexture", 3);
            _shaderProgram.SetUniform1(gl, "blendMap", 4);
        }
    }
}
