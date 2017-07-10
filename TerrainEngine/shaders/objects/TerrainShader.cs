using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using TerrainEngine.entities;

namespace TerrainEngine.shaders.objects
{
    public class TerrainShader : BaseShader
    {
        private TerrainBrush _terrainBrush;
        private float _terrainSize;

        public TerrainBrush TerrainBrush
        {
            get
            {
                return _terrainBrush;
            }

            set
            {
                _terrainBrush = value;
            }
        }

        public float TerrainSize
        {
            get
            {
                return _terrainSize;
            }

            set
            {
                _terrainSize = value;
            }
        }

        public TerrainShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode, Light[] lights,
            TerrainBrush terrainBrush, float terrainSize) 
            : base(gl, vertexShaderCode, fragmentShaderCode, lights)
        {
            this._terrainBrush = terrainBrush;
            this._terrainSize = terrainSize;
        }

        public override void LoadAllVariables()
        {
            LoadTerrainSize();
            LoadBrush();
            LoadTerrainTextures();
        }

        private void LoadTerrainSize()
        {
            _shaderProgram.SetUniform1(_gl, "terrainSize", _terrainSize);
        }

        private void LoadBrush()
        {
            _shaderProgram.SetUniform1(_gl, "brushRadius", _terrainBrush.Radius);
            _shaderProgram.SetUniform3(_gl, "brushPosition", _terrainBrush.Position.x, _terrainBrush.Position.y, _terrainBrush.Position.z);
        }

        private void LoadTerrainTextures()
        {
            _shaderProgram.SetUniform1(_gl, "backgroundTexture", 0);
            _shaderProgram.SetUniform1(_gl, "rTexture", 1);
            _shaderProgram.SetUniform1(_gl, "gTexture", 2);
            _shaderProgram.SetUniform1(_gl, "bTexture", 3);
            _shaderProgram.SetUniform1(_gl, "blendMap", 4);
        }

        
    }
}
