using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.models
{
    public class TerrainTexturePack
    {
        private ModelTexture _backgroundTexture = new ModelTexture();
        private ModelTexture _rTexture = new ModelTexture();
        private ModelTexture _gTexture = new ModelTexture();
        private ModelTexture _bTexture = new ModelTexture();
        private ModelTexture _blendMap = new ModelTexture();

        public ModelTexture BackgroundTexture
        {
            get
            {
                return _backgroundTexture;
            }

            set
            {
                _backgroundTexture = value;
            }
        }

        public ModelTexture RTexture
        {
            get
            {
                return _rTexture;
            }

            set
            {
                _rTexture = value;
            }
        }

        public ModelTexture GTexture
        {
            get
            {
                return _gTexture;
            }

            set
            {
                _gTexture = value;
            }
        }

        public ModelTexture BTexture
        {
            get
            {
                return _bTexture;
            }

            set
            {
                _bTexture = value;
            }
        }

        public ModelTexture BlendMap
        {
            get
            {
                return _blendMap;
            }

            set
            {
                _blendMap = value;
            }
        }

        public TerrainTexturePack()
        {

        }


    }
}
