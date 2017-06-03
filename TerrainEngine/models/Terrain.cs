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
using TerrainEngine.tool;

namespace TerrainEngine.models
{
    public class Terrain
    {
        private int _id;

        private OpenGL _gl;
        private Loader _loader;

        private Model _model;
        private TerrainTexturePack _terrainTextures;
        private ModelShader _shader;
        private Light _light;

        private Image _blendMap;
        private Image _backgroundTexture;
        private Image _rTexture;
        private Image _gTexture;
        private Image _bTexture;
        private string _vertShaderCode;
        private string _fragShaderCode;

        private float _terrainSize = 64f;

        private float[] _vertices;
        private float[] _textureCoords;
        private uint[] _indices;
        private float[] _normals;

        private float _posX;
        private float _posZ;

        private vec3[,] _verticesMap;
        private vec3[,] _normalsMap;

        public Terrain(OpenGL gl, Loader loader, Light light,
                        Image blendMap, Image backgroundTexture, Image rTexture, Image gTexture, Image bTexture,
                        string vertShaderCode, string fragShaderCode,
                        int id, float posX, float posZ) 
        {
            this._gl = gl;
            this._loader = loader;
            this._light = light;
            this._blendMap = blendMap;
            this._backgroundTexture = backgroundTexture;
            this._rTexture = rTexture;
            this._gTexture = gTexture;
            this._bTexture = bTexture;
            this._vertShaderCode = vertShaderCode;
            this._fragShaderCode = fragShaderCode;
            this._id = id;
            this._posX = posX * _terrainSize;
            this._posZ = posZ * _terrainSize;
        }

        public float[] Vertices
        {
            get
            {
                return _vertices;
            }

        }

        public float[] TextureCoords
        {
            get
            {
                return _textureCoords;
            }
        }

        public uint[] Indices
        {
            get
            {
                return _indices;
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

        public float PosX
        {
            get
            {
                return _posX;
            }
        }

        public float PosZ
        {
            get
            {
                return _posZ;
            }
        }

        public float TerrainSize
        {
            get
            {
                return _terrainSize;
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

        public TerrainTexturePack TerrainTextures
        {
            get
            {
                return _terrainTextures;
            }

            set
            {
                _terrainTextures = value;
            }
        }

        public void CreateTerrain(int n)
        {
            int count = (n + 1) * (n + 1);
            _vertices = new float[count * 3];
            _verticesMap = new vec3[n + 1, n + 1];
            _normalsMap = new vec3[n + 1, n + 1];
            _normals = new float[count * 3];
            _textureCoords = new float[count * 2];
            _indices = new uint[6 * n * n];
            int vertexPointer = 0;
            float x = 0;
            float z = 0;
            float y = 0;
            for (int i = 0; i < (n + 1); i++)
            {
                for (int j = 0; j < (n + 1); j++)
                {
                    x = _posX + (float)j / (float)n * _terrainSize;
                    y = 0;
                    z = _posZ + (float)i / (float)n * _terrainSize;
                    _vertices[vertexPointer * 3] = x;
                    _vertices[vertexPointer * 3 + 1] = y;
                    _vertices[vertexPointer * 3 + 2] = z;
                    _verticesMap[i, j] = new vec3(x, y, z);
                    _normalsMap[i, j] = new vec3(0, 1, 0);
                    _normals[vertexPointer * 3] = 0;
                    _normals[vertexPointer * 3 + 1] = 1;
                    _normals[vertexPointer * 3 + 2] = 0;
                    _textureCoords[vertexPointer * 2] = (float)j / (float)n;
                    _textureCoords[vertexPointer * 2 + 1] = (float)i / (float)n;
                    vertexPointer++;
                }
            }
            int pointer = 0;
            for (int gz = 0; gz < n; gz++)
            {
                for (int gx = 0; gx < n; gx++)
                {
                    uint topLeft = (uint)((gz * (n + 1)) + gx);
                    uint topRight = topLeft + 1;
                    uint bottomLeft = (uint)(((gz + 1) * (n + 1)) + gx);
                    uint bottomRight = bottomLeft + 1;
                    _indices[pointer++] = topLeft;
                    _indices[pointer++] = bottomLeft;
                    _indices[pointer++] = topRight;
                    _indices[pointer++] = topRight;
                    _indices[pointer++] = bottomLeft;
                    _indices[pointer++] = bottomRight;
                }
            }

            _model = new Model();
            _loader.LoadEntityToVao(_gl, _model, _vertices, _textureCoords, _indices, _normals);
            _terrainTextures = new TerrainTexturePack();
            _loader.LoadTerrainTexturePack(_gl, _terrainTextures, _blendMap, _backgroundTexture, 
                                        _rTexture, _gTexture, _bTexture);
            _shader = _loader.LoadTerrainShader(_gl, _vertShaderCode, _fragShaderCode);
        }

        private vec3 GetNormal(vec3 a, vec3 b, vec3 c)
        {
            vec3 vec1 = b - a;
            vec3 vec2 = c - a;
            vec3 normal = MultVector(vec1, vec2) * -1;
            return glm.normalize(normal);
        }

        private vec3 MultVector(vec3 a, vec3 b)
        {
            return new vec3(a.y * b.z - a.z * b.y,
                            a.z * b.x - a.x * b.z,
                            a.x * b.y - a.y * b.x);
        }

        private vec3 GetAverageVector(vec3 v1, vec3 v2)
        {
            return glm.normalize(v1 + v2);
        }

        public float GetHeightOfTerrain(float worldX, float worldZ)
        {
            float terrainX = worldX - this._posX;
            float terrainZ = worldZ - this._posZ;
            float gridSquareSize = _terrainSize / ((float)_verticesMap.GetLength(0) - 1);
            int gridX = (int)Math.Floor(terrainX / gridSquareSize);
            int gridZ = (int)Math.Floor(terrainZ / gridSquareSize);


            if (gridX >= _verticesMap.GetLength(0) - 1 || gridZ >= _verticesMap.GetLength(0) - 1 || gridX < 0 || gridZ < 0)
            {
                return -1;
            }

            float xCoord = (terrainX % gridSquareSize) / gridSquareSize;
            float zCoord = (terrainZ % gridSquareSize) / gridSquareSize;
            float answer;

            if (xCoord <= (1 - zCoord))
            {
                answer = MatrixMath.barryCentric(new vec3(0, _verticesMap[gridX, gridZ].y, 0), new vec3(1,
                                _verticesMap[gridX + 1, gridZ].y, 0), new vec3(0,
                                _verticesMap[gridX, gridZ + 1].y, 1), new vec2(xCoord, zCoord));
            }
            else
            {
                answer = MatrixMath.barryCentric(new vec3(1, _verticesMap[gridX + 1, gridZ].y, 0), new vec3(1,
                                _verticesMap[gridX + 1, gridZ + 1].y, 1), new vec3(0,
                                _verticesMap[gridX, gridZ + 1].y, 1), new vec2(xCoord, zCoord));
            }

            return answer;
        }

        public void ChangeTerrainHeight(float worldX, float worldZ, float radius, float amount)
        {

            float terrainX = worldX - this._posX;
            float terrainZ = worldZ - this._posZ;
            float gridSquareSize = _terrainSize / ((float)_verticesMap.GetLength(0) - 1);

            int beginGridX = (int)Math.Floor((terrainX - radius) / gridSquareSize);
            int endGridX = (int)Math.Floor((terrainX + radius) / gridSquareSize);

            int beginGridZ = (int)Math.Floor((terrainZ - radius) / gridSquareSize);
            int endGridZ = (int)Math.Floor((terrainZ + radius) / gridSquareSize);

            if (beginGridX < 0 && beginGridZ < 0)
            {
                return;
            }

            if (beginGridX < 0 || endGridX >= _verticesMap.GetLength(1) - 1 ||
                beginGridZ < 0 || endGridZ >= _verticesMap.GetLength(0) - 1)
            {
                return;
            }

            int curIndex;
            for (int i = beginGridZ; i <= endGridZ + 1; i++)
            {
                for (int j = beginGridX; j <= endGridX + 1; j++)
                {
                    if (MatrixMath.InCircle(terrainX, terrainZ, _verticesMap[i, j].x, _verticesMap[i, j].z, radius))
                    {
                        curIndex = (_verticesMap.GetLength(0) * i + j) * 3;
                        _verticesMap[i, j].y += amount;
                        _vertices[curIndex + 1] = _verticesMap[i, j].y;

                        _normalsMap[i, j] = GetAverageVector(_normalsMap[i, j],
                            GetNormal(_verticesMap[i, j], _verticesMap[i, j + 1], _verticesMap[i + 1, j]));

                        _normals[curIndex] = _normalsMap[i, j].x;
                        _normals[curIndex + 1] = _normalsMap[i, j].y;
                        _normals[curIndex + 2] = _normalsMap[i, j].z;

                        _normals[curIndex + 3] = _normalsMap[i, j].x;
                        _normals[curIndex + 4] = _normalsMap[i, j].y;
                        _normals[curIndex + 5] = _normalsMap[i, j].z;

                        _normals[(_verticesMap.GetLength(0) * (i + 1) + j) * 3] = _normalsMap[i, j].x;
                        _normals[(_verticesMap.GetLength(0) * (i + 1) + j) * 3 + 1] = _normalsMap[i, j].y;
                        _normals[(_verticesMap.GetLength(0) * (i + 1) + j) * 3 + 2] = _normalsMap[i, j].z;
                    }
                }

            }

            ReloadTerrain();
        }

        public void ChangeBlendMap(float worldX, float worldZ, float radius)
        {
            float terrainX = worldX - this._posX;
            float terrainZ = worldZ - this._posZ;

            float texturePosX = (_terrainTextures.BlendMap.Image.Height / _terrainSize) * terrainX;
            float texturePosZ = (_terrainTextures.BlendMap.Image.Width / _terrainSize) * terrainZ;

            radius = (_terrainTextures.BlendMap.Image.Height / _terrainSize) * radius;
            using (Graphics g = Graphics.FromImage(_terrainTextures.BlendMap.Image))
            {
                g.FillEllipse(Brushes.Green, new RectangleF(texturePosX - radius, texturePosZ - radius, radius * 2, radius * 2));
            }
            

            ReloadBlendMap(_terrainTextures.BlendMap.Image);
        }

        public void ReloadTerrain()
        {
            _loader.ReloadTerrain(_gl, this);
        }

        public void ReloadBlendMap(Image blendMap)
        {
            _loader.ReloadTerrainTexture(_gl, _terrainTextures.BlendMap, blendMap);
        }
    }
}
