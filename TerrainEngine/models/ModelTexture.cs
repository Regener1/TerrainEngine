using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.models
{
    public class ModelTexture
    {
        public uint Id { get; set; }


        public ModelTexture(uint id)
        {
            this.Id = id;

        }

        public ModelTexture()
        {
        }
    }
}
