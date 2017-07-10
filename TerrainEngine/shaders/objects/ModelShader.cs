using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using TerrainEngine.entities;

namespace TerrainEngine.shaders.objects
{
    public class ModelShader : BaseShader
    {
        public ModelShader(OpenGL gl, string vertexShaderCode, string fragmentShaderCode, Light[] lights) : 
            base(gl, vertexShaderCode, fragmentShaderCode, lights)
        {
        }

        public override void LoadAllVariables()
        {
            
        }
    }
}
