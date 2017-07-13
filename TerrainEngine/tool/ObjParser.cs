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

            Console.WriteLine(objCode);

            List<float> vertices = new List<float>();
            List<uint> indices = new List<uint>();
            List<float> uv = new List<float>();
            List<float> normals = new List<float>();

            string[] lines = objCode.Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            string[] parseLine;
            string[] parseInds;

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
                        normals.Add(float.Parse(parseLine[1]));
                        normals.Add(float.Parse(parseLine[2]));
                        normals.Add(float.Parse(parseLine[3]));

                    }
                    else if (parseLine[0].Equals("vt")) // uv
                    {
                        uv.Add(float.Parse(parseLine[1]));
                        uv.Add(float.Parse(parseLine[2]));
                    }
                    else if (parseLine[0].Equals("f")) // 
                    {
                        for(int j = 1; j < parseLine.Length; j++)
                        {
                            parseInds = parseLine[j].Split('/');
                            indices.Add(uint.Parse(parseInds[0]));
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
