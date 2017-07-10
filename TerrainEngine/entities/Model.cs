using GlmNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.shaders.objects;

namespace TerrainEngine.entities
{
    public class Model
    {
        // id
        private uint _id;
        private uint _verticesId;
        private uint _indicesId;
        private uint _uvId;
        private uint _normalsId;

        // data
        private float[] _vertices;
        private uint[] _indices;
        private float[] _uv;
        private float[] _normals;

        private vec3 _position;
        private vec3 _rotate;
        private float _scale;

        private ModelTexture _texture;
        private Light[] _lights;

        private BaseShader _shader;

        public uint Id
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

        public uint VerticesId
        {
            get
            {
                return _verticesId;
            }

            set
            {
                _verticesId = value;
            }
        }

        public uint IndicesId
        {
            get
            {
                return _indicesId;
            }

            set
            {
                _indicesId = value;
            }
        }

        public uint UvId
        {
            get
            {
                return _uvId;
            }

            set
            {
                _uvId = value;
            }
        }

        public uint NormalsId
        {
            get
            {
                return _normalsId;
            }

            set
            {
                _normalsId = value;
            }
        }

        public float[] Vertices
        {
            get
            {
                return _vertices;
            }

            set
            {
                _vertices = value;
            }
        }

        public uint[] Indices
        {
            get
            {
                return _indices;
            }

            set
            {
                _indices = value;
            }
        }

        public float[] Uv
        {
            get
            {
                return _uv;
            }

            set
            {
                _uv = value;
            }
        }

        public float[] Normals
        {
            get
            {
                return _normals;
            }

            set
            {
                _normals = value;
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

        public vec3 Rotate
        {
            get
            {
                return _rotate;
            }

            set
            {
                _rotate = value;
            }
        }

        public BaseShader Shader
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

        public Model(uint _id, uint _verticesId, uint _indicesId, uint _uvId, uint _normalsId,
            float[] _vertices, uint[] _indices, float[] _uv, float[] _normals, 
            vec3 _position, vec3 _rotate, float _scale, ModelTexture _texture, BaseShader _shader)
        {
            this._id = _id;
            this._verticesId = _verticesId;
            this._indicesId = _indicesId;
            this._uvId = _uvId;
            this._normalsId = _normalsId;
            this._vertices = _vertices;
            this._indices = _indices;
            this._uv = _uv;
            this._normals = _normals;
            this._position = _position;
            this._rotate = _rotate;
            this._scale = _scale;
            this._texture = _texture;
            this._shader = _shader;
        }

        public uint GetTextureId()
        {
            return _texture.Id;
        }

        public void StartShader()
        {
            _shader.Start();
        }

        public void StopShader()
        {
            _shader.Stop();
        }

        public void LoadShaderVariables()
        {
            _shader.LoadAllVariables();
        }

        public void SetTransformationMatrix(mat4 matrix)
        {
            _shader.LoadTransformationMatrix(matrix);
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
