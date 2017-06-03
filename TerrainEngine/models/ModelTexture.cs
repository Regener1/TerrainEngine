using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.models
{
    public class ModelTexture
    {
        public uint Id { get; set; }
        public Image Image { get; set; }

        public ModelTexture()
        {

        }

        public ModelTexture(uint id, Image image)
        {
            this.Id = id;
            this.Image = image;
        }
    }
}
