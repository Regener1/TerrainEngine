using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.models
{
    public class Object3D
    {
        private Model _model;
        private ModelTexture _texture;
        private ModelShader _shader;
        private int _id;
        private vec3 _position;
        private float _rotX, _rotY, _rotZ;
        private float _scale;

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

        public Object3D(Model model, ModelTexture texture, ModelShader shader, int id, vec3 position, float rotX, float rotY, float rotZ, float scale)
        {
            this._model = model;
            this._texture = texture;
            this._shader = shader;
            this._id = id;
            this._model = model;
            this._texture = texture;
            this._shader = shader;
            this._position = position;
            this._rotX = rotX;
            this._rotY = rotY;
            this._rotZ = rotZ;
            this._scale = scale;
        }

        public string GetInfo()
        {
            return "x " + _position.x +
                "y " + _position.y +
                "z " + _position.z; 
        }
    }
}
