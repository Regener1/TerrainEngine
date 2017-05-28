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
    public class TerrainBrush
    {
        private OpenGL _gl;
        private Loader _loader;
        private Terrain _terrain;

        private Object3D _object;
        private float _radius = 1f; //scale

        private Image _textureImg;
        private string _vertShaderPath;
        private string _fragShaderPath;

        private float[] vertices =
            {
                -0.5f, 0, -0.5f,
                -0.5f, 0, 0.5f,
                0.5f, 0, 0.5f,
                0.5f, 0, -0.5f
            };

        private uint[] indices =
        {
                0,1,3,
                3,1,2
            };

        private float[] textureCoords =
        {
                0,0,
                0,1,
                1,1,
                1,0
            };

        public Object3D Object
        {
            get
            {
                return _object;
            }

            set
            {
                _object = value;
            }
        }

        public float Radius
        {
            get
            {
                return _radius;
            }
        }

        public TerrainBrush(OpenGL gl, Terrain terrain, Loader loader, Image texture, string vertexShader, string fragmentShader)
        {
            this._gl = gl;
            this._terrain = terrain;
            this._loader = loader;
            this._textureImg = texture;
            this._vertShaderPath = vertexShader;
            this._fragShaderPath = fragmentShader;

            Model model = _loader.LoadEntityToVao(_gl, vertices, textureCoords, indices);
            ModelTexture modelTexture = _loader.LoadTexture(_gl, texture);
            ModelShader shader = new ModelShader(_gl, vertexShader, fragmentShader);
            _object = _loader.CreateObject3D(model, modelTexture, shader, 1, new vec3(0, 0, 0), 0, 0, 0, _radius);
        }


        public void Update(vec3 position, float radius)
        {
            _object.Position = position;
            _radius = radius;
            _object.Scale = _radius;

            ReloadEntity();
        }

        private void ReloadEntity()
        {
            _loader.ReloadEntityVao(_gl, _object.Model);
        }

        public void ChangeTerrainHeight(float amount)
        {
            _terrain.ChangeTerrainHeight(_object.Position.x, _object.Position.z, _radius, amount);
        }
    }
}
