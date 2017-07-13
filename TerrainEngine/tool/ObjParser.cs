using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TerrainEngine.renderEngine;

namespace TerrainEngine.tool
{
    public class ObjParser
    {
        private Loader _loader;

        public ObjParser(Loader loader)
        {
            this._loader = loader;
        }

        public void Parse(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return;
            }

            string objCode;

            try
            {
                objCode = File.ReadAllText(filepath);
            }
            catch (IOException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }

            objCode = objCode.Replace('.', ',');

            Console.WriteLine(objCode);

            List<float> vertices = new List<float>();
            List<uint> indices = new List<uint>();
            float[] uv = null;
            float[] normals = null;

            List<float> uvBase = new List<float>();
            List<float> normalsBase = new List<float>();

            string[] lines = objCode.Split('\n');

            string[] parseLine;
            string[] parseInds;
            uint position = 0;

            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    parseLine = lines[i].Split(' ');

                    if (parseLine[0].Equals("v")) // vertices
                    {
                        vertices.Add(float.Parse(parseLine[1]));
                        vertices.Add(float.Parse(parseLine[2]));
                        vertices.Add(float.Parse(parseLine[3]));
                    }
                    else if (parseLine[0].Equals("vn")) // normals
                    {
                        normalsBase.Add(float.Parse(parseLine[1]));
                        normalsBase.Add(float.Parse(parseLine[2]));
                        normalsBase.Add(float.Parse(parseLine[3]));

                    }
                    else if (parseLine[0].Equals("vt")) // uv
                    {
                        uvBase.Add(float.Parse(parseLine[1]));
                        uvBase.Add(float.Parse(parseLine[2]));
                    }
                    else if (parseLine[0].Equals("f")) // v/vt/n
                    {
                        if (uv == null && normals == null)
                        {
                            uv = new float[(vertices.Count / 3) * 2]; // /3*2
                            normals = new float[vertices.Count];
                        }

                        for(int j = 1; j < parseLine.Length; j++)
                        {
                            parseInds = parseLine[j].Split('/');

                            position = uint.Parse(parseInds[0]);
                            indices.Add(position);

                            normals[(position - 1) * 3] = normalsBase[(int.Parse(parseInds[2]) - 1) * 3];
                            normals[(position - 1) * 3 + 1] = normalsBase[(int.Parse(parseInds[2]) - 1) * 3 + 1];
                            normals[(position - 1) * 3 + 2] = normalsBase[(int.Parse(parseInds[2]) - 1) * 3 + 2];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Swap(List<float> list, int first, int second)
        {
            float tmp = list[first];
            list[first] = list[second];
            list[second] = tmp; 
        }
    }
}
