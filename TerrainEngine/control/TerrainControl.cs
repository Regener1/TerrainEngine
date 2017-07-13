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

namespace TerrainEngine.control
{
    class TerrainData
    {
        private float[] vertices;
        private uint[] indices;
        private float[] uv;
        private float[] normals;

        private vec3[,] verticesMap;
        private vec3[,] normalsMap;

        public float[] Vertices
        {
            get
            {
                return vertices;
            }

            set
            {
                vertices = value;
            }
        }

        public uint[] Indices
        {
            get
            {
                return indices;
            }

            set
            {
                indices = value;
            }
        }

        public float[] Uv
        {
            get
            {
                return uv;
            }

            set
            {
                uv = value;
            }
        }

        public float[] Normals
        {
            get
            {
                return normals;
            }

            set
            {
                normals = value;
            }
        }

        public vec3[,] VerticesMap
        {
            get
            {
                return verticesMap;
            }

            set
            {
                verticesMap = value;
            }
        }

        public vec3[,] NormalsMap
        {
            get
            {
                return normalsMap;
            }

            set
            {
                normalsMap = value;
            }
        }
    }

    public class TerrainControl
    {
        private Loader _loader;
        private OpenGL _gl;

        public Loader Loader
        {
            get
            {
                return _loader;
            }

            set
            {
                _loader = value;
            }
        }

        public TerrainControl(OpenGL gl,Loader loader)
        {
            this._loader = loader;
            this._gl = gl;
        }

        public Terrain CreateTerrain(OpenGL gl, int n, float terrainSize, float posX, float posZ, Image[] textures, 

            Light[] lights, string vertexShader, string fragmentShader, TerrainBrush brush)
        {
            TerrainData terrainData = CreateData(n, terrainSize, posX, posZ);

            Terrain terrain = _loader.LoadTerrainToVao(gl, terrainData.Vertices, terrainData.Indices,
                terrainData.Uv, terrainData.Normals, posX, posZ, terrainSize, textures,
                lights, vertexShader, fragmentShader, brush);


            return terrain;
        }

        private TerrainData CreateData(int n, float terrainSize, float posX, float posZ)
        {
            TerrainData terrainData = new TerrainData();
            int count = (n + 1) * (n + 1);
            terrainData.Vertices = new float[count * 3];
            terrainData.VerticesMap = new vec3[n + 1, n + 1];
            terrainData.NormalsMap = new vec3[n + 1, n + 1];
            terrainData.Normals = new float[count * 3];
            terrainData.Uv = new float[count * 2];
            terrainData.Indices = new uint[6 * n * n];
            int vertexPointer = 0;
            float x = 0;
            float z = 0;
            float y = 0;
            for (int i = 0; i < (n + 1); i++)
            {
                for (int j = 0; j < (n + 1); j++)
                {
                    x = posX + (float)j / (float)n * terrainSize;
                    y = 0;
                    z = posZ + (float)i / (float)n * terrainSize;
                    terrainData.Vertices[vertexPointer * 3] = x;
                    terrainData.Vertices[vertexPointer * 3 + 1] = y;
                    terrainData.Vertices[vertexPointer * 3 + 2] = z;
                    terrainData.VerticesMap[i, j] = new vec3(x, y, z);
                    terrainData.NormalsMap[i, j] = new vec3(0, 1, 0);
                    terrainData.Normals[vertexPointer * 3] = 0;
                    terrainData.Normals[vertexPointer * 3 + 1] = 1;
                    terrainData.Normals[vertexPointer * 3 + 2] = 0;
                    terrainData.Uv[vertexPointer * 2] = (float)j / (float)n;
                    terrainData.Uv[vertexPointer * 2 + 1] = (float)i / (float)n;
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
                    terrainData.Indices[pointer++] = topLeft;
                    terrainData.Indices[pointer++] = bottomLeft;
                    terrainData.Indices[pointer++] = topRight;
                    terrainData.Indices[pointer++] = topRight;
                    terrainData.Indices[pointer++] = bottomLeft;
                    terrainData.Indices[pointer++] = bottomRight;
                }
            }

            return terrainData;
        }

        public float GetHeightOfTerrain(Terrain terrain, float worldX, float worldZ)
        {
            float terrainX = worldX - terrain.PosX;
            float terrainZ = worldZ - terrain.PosZ;
            float gridSquareSize = terrain.TerrainSize / ((float)terrain.VerticesMap.GetLength(0) - 1);
            int gridX = (int)Math.Floor(terrainX / gridSquareSize);
            int gridZ = (int)Math.Floor(terrainZ / gridSquareSize);


            if (gridX >= terrain.VerticesMap.GetLength(0) - 1 || gridZ >= terrain.VerticesMap.GetLength(0) - 1 || gridX < 0 || gridZ < 0)
            {
                return -1;
            }

            float xCoord = (terrainX % gridSquareSize) / gridSquareSize;
            float zCoord = (terrainZ % gridSquareSize) / gridSquareSize;
            float answer;

            if (xCoord <= (1 - zCoord))
            {
                answer = TerrainEngineMath.BarryCentric(new vec3(0, terrain.VerticesMap[gridX, gridZ].y, 0), new vec3(1,
                                terrain.VerticesMap[gridX + 1, gridZ].y, 0), new vec3(0,
                                terrain.VerticesMap[gridX, gridZ + 1].y, 1), new vec2(xCoord, zCoord));
            }
            else
            {
                answer = TerrainEngineMath.BarryCentric(new vec3(1, terrain.VerticesMap[gridX + 1, gridZ].y, 0), new vec3(1,
                                terrain.VerticesMap[gridX + 1, gridZ + 1].y, 1), new vec3(0,
                                terrain.VerticesMap[gridX, gridZ + 1].y, 1), new vec2(xCoord, zCoord));
            }

            return answer;
        }

        public void ChangeTerrainHeight(Terrain terrain, /*float worldX, float worldZ, float radius,*/ float amount)
        {
            float worldX = terrain.GetBrushPosition().x;
            float worldZ = terrain.GetBrushPosition().z;
            float radius = terrain.GetBrushRadius();

            float terrainX = worldX - terrain.PosX;
            float terrainZ = worldZ - terrain.PosZ;
            float gridSquareSize = terrain.TerrainSize / ((float)terrain.VerticesMap.GetLength(0) - 1);

            int beginGridX = (int)Math.Floor((terrainX - radius) / gridSquareSize);
            int endGridX = (int)Math.Floor((terrainX + radius) / gridSquareSize);

            int beginGridZ = (int)Math.Floor((terrainZ - radius) / gridSquareSize);
            int endGridZ = (int)Math.Floor((terrainZ + radius) / gridSquareSize);

            if (beginGridX < 0 && beginGridZ < 0)
            {
                return;
            }

            if (beginGridX < 0 || endGridX >= terrain.VerticesMap.GetLength(1) - 1 ||
                beginGridZ < 0 || endGridZ >= terrain.VerticesMap.GetLength(0) - 1)
            {
                return;
            }

            int curIndex;
            for (int i = beginGridZ; i <= endGridZ + 1; i++)
            {
                for (int j = beginGridX; j <= endGridX + 1; j++)
                {
                    if (TerrainEngineMath.InCircle(terrainX, terrainZ, terrain.VerticesMap[i, j].x, terrain.VerticesMap[i, j].z, radius))
                    {
                        curIndex = (terrain.VerticesMap.GetLength(0) * i + j) * 3;
                        terrain.VerticesMap[i, j].y += amount;
                        terrain.Vertices[curIndex + 1] = terrain.VerticesMap[i, j].y;

                        terrain.NormalsMap[i, j] = TerrainEngineMath.GetAverageVector(terrain.NormalsMap[i, j],
                            TerrainEngineMath.GetNormal(terrain.VerticesMap[i, j], terrain.VerticesMap[i, j + 1], terrain.VerticesMap[i + 1, j]));

                        terrain.Normals[curIndex] = terrain.NormalsMap[i, j].x;
                        terrain.Normals[curIndex + 1] = terrain.NormalsMap[i, j].y;
                        terrain.Normals[curIndex + 2] = terrain.NormalsMap[i, j].z;

                        terrain.Normals[curIndex + 3] = terrain.NormalsMap[i, j].x;
                        terrain.Normals[curIndex + 4] = terrain.NormalsMap[i, j].y;
                        terrain.Normals[curIndex + 5] = terrain.NormalsMap[i, j].z;

                        terrain.Normals[(terrain.VerticesMap.GetLength(0) * (i + 1) + j) * 3] = terrain.NormalsMap[i, j].x;
                        terrain.Normals[(terrain.VerticesMap.GetLength(0) * (i + 1) + j) * 3 + 1] = terrain.NormalsMap[i, j].y;
                        terrain.Normals[(terrain.VerticesMap.GetLength(0) * (i + 1) + j) * 3 + 2] = terrain.NormalsMap[i, j].z;
                    }
                }

            }

            _loader.UpdateTerrain(_gl, terrain);
        }
    }
}
