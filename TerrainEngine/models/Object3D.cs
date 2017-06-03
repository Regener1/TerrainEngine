using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.entities;
using TerrainEngine.renderEngine;

namespace TerrainEngine.models
{
    public class Object3D
    {
        protected OpenGL _gl;
        protected Loader _loader;

        protected Model _model;
        protected ModelTexture _texture;
        protected ModelShader _shader;
        protected Light _light;

        protected Image _textureImg;
        protected string _vertShaderCode;
        protected string _fragShaderCode;

        protected int _id;
        protected vec3 _position;
        protected float _rotX, _rotY, _rotZ;
        protected float _scale;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

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

        public float RotX
        {
            get
            {
                return _rotX;
            }

            set
            {
                _rotX = value;
            }
        }

        public float RotY
        {
            get
            {
                return _rotY;
            }

            set
            {
                _rotY = value;
            }
        }

        public float RotZ
        {
            get
            {
                return _rotZ;
            }

            set
            {
                _rotZ = value;
            }
        }

        public float Scale
        {
            get
            {
                return _scale;
            }

            set
            {
                _scale = value;
            }
        }

        public Model Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
            }
        }

        public ModelTexture Texture
        {
            get
            {
                return _texture;
            }

            set
            {
                _texture = value;
            }
        }

        public ModelShader Shader
        {
            get
            {
                return _shader;
            }

            set
            {
                _shader = value;
            }
        }

        public Light Light
        {
            get
            {
                return _light;
            }

            set
            {
                _light = value;
            }
        }

        public Object3D(OpenGL gl, Loader loader, Light light,
                        Image textureImg, string vertShaderCode, string fragShaderCode,
                        int id, vec3 position, 
                        float rotX, float rotY, float rotZ, float scale)
        {
            this._gl = gl;
            this._loader = loader;
            this._light = light;
            this._textureImg = textureImg;
            this._vertShaderCode = vertShaderCode;
            this._fragShaderCode = fragShaderCode;
            this._id = id;
            this._position = position;
            this._rotX = rotX;
            this._rotY = rotY;
            this._rotZ = rotZ;
            this._scale = scale;
        }

    }
}
