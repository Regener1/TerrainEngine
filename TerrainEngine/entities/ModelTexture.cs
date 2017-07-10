using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.entities
{
    public class ModelTexture
    {
        private uint _id;
        private Image _image;

        public ModelTexture(uint id, Image image)
        {
            this._id = id;
            this._image = image;
        }

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

        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
            }
        }
    }
}
