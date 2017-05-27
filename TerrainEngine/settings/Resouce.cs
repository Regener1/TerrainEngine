using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.settings
{
    public static class Resouce
    {
        public static string SHADERS_PATH = @"res\shaders";
        public static string TEXTURES_PATH = @"res\textures";

        public static string FRAGMENT_SHADER_TERRAIN = @"\terrainFragmentShader.frag";
        public static string VERTEX_SHADER_TERRAIN = @"\terrainVertexShader.vert";

        public static string FRAGMENT_SHADER_LINE = @"\lineFragmentShader.frag";
        public static string VERTEX_SHADER_LINE = @"\lineVertexShader.vert";
    }
}
