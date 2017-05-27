using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.renderEngine;

namespace TerrainEngine.models
{
    public class Brush
    {
        private OpenGL _gl;
        private Loader _loader;

        private Object3D _object;
        private float _radius = 1f; //scale
        private vec3 _position;

        private Image _textureImg;
        private string _vertShaderPath;
        private string _fragShaderPath;

        public vec3 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public Brush(OpenGL gl, Loader loader, vec3 position, Image texture, string vertexShader, string fragmentShader)
        {
            this._gl = gl;
            this._loader = loader;
            this._position = position;
            this._textureImg = texture;
            this._vertShaderPath = vertexShader;
            this._fragShaderPath = fragmentShader;


        }


        public void UpdatePosition(vec3 center)
        {

        }

    }
}
