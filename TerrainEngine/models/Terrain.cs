﻿using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.renderEngine;
using TerrainEngine.settings;
using TerrainEngine.tool;

namespace TerrainEngine.models
{
    public class Terrain
    {
        private const float SIZE = 40f;

        private OpenGL _gl;
        private Loader _loader;

        private Model _model;
        private ModelTexture _modelTexture;
        private ModelShader _modelShader;

        private Image _textureImg;
        private string _vertShaderPath;
        private string _fragShaderPath;

        private float[] _vertices;
        private float[] _textureCoords;
        private uint[] _indices;
        private float[] _normals;

        private float _posX;
        private float _posZ;

        private float[,] _heightMap;

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

        public Model Model
        {
            get
            {
                return _model;
            }
        }

        public ModelTexture ModelTexture
        {
            get
            {
                return _modelTexture;
            }

        }

        public ModelShader ModelShader
        {
            get
            {
                return _modelShader;
            }

        }

        public Terrain(OpenGL gl, Loader loader, float posX, float posZ, Image texture, string vertexShader, string fragmentShader)
        {
            this._gl = gl;
            this._loader = loader;
            this._posX = posX * SIZE;
            this._posZ = posZ * SIZE;
            this._textureImg = texture;
            this._vertShaderPath = vertexShader;
            this._fragShaderPath = fragmentShader;


        }

        public void CreateTerrain(int n)
        {
            int count = (n + 1) * (n + 1);
            _vertices = new float[count * 3];
            _heightMap = new float[n + 1, n + 1];
            _normals = new float[count * 3];
            _textureCoords = new float[count * 2];
            _indices = new uint[6 * n * n];
            int vertexPointer = 0;
            for (int i = 0; i < (n + 1); i++)
            {
                for (int j = 0; j < (n + 1); j++)
                {
                    _vertices[vertexPointer * 3] = _posX + (float)j / (float)n * SIZE;
                    _vertices[vertexPointer * 3 + 1] = 0;
                    _vertices[vertexPointer * 3 + 2] = _posZ + (float)i / (float)n * SIZE;
                    _heightMap[i, j] = 0;
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

            _model = _loader.LoadEntityToVao(_gl, _vertices, _textureCoords, _indices);
            _modelTexture = _loader.LoadTexture(_gl, _textureImg);
            _modelShader = new ModelShader(_gl, _vertShaderPath, _fragShaderPath);

        }

        public void Update()
        {
            _loader.ReloadEntityVao(_gl, _model);
        }

        public float GetHeightOfTerrain(float worldX, float worldZ)
        {
            float terrainX = worldX - this._posX;
            float terrainZ = worldZ - this._posZ;
            float gridSquareSize = SIZE / ((float)_heightMap.GetLength(0) - 1);
            int gridX = (int)Math.Floor(terrainX / gridSquareSize);
            int gridZ = (int)Math.Floor(terrainZ / gridSquareSize);

            if (gridX >= _heightMap.GetLength(0) - 1 || gridZ >= _heightMap.GetLength(0) - 1 || gridX < 0 || gridZ < 0)
            {
                return -1;
            }

            float xCoord = (terrainX % gridSquareSize) / gridSquareSize;
            float zCoord = (terrainZ % gridSquareSize) / gridSquareSize;
            float answer;

            if (xCoord <= (1 - zCoord))
            {
                answer = MatrixMath.barryCentric(new vec3(0, _heightMap[gridX, gridZ], 0), new vec3(1,
                                _heightMap[gridX + 1, gridZ], 0), new vec3(0,
                                _heightMap[gridX, gridZ + 1], 1), new vec2(xCoord, zCoord));
            }
            else
            {
                answer = MatrixMath.barryCentric(new vec3(1, _heightMap[gridX + 1, gridZ], 0), new vec3(1,
                                _heightMap[gridX + 1, gridZ + 1], 1), new vec3(0,
                                _heightMap[gridX, gridZ + 1], 1), new vec2(xCoord, zCoord));
            }

            return answer;
        }
    }
}