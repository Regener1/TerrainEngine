using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainEngine.entities
{
    public class Light
    {
        private vec3 _position;
        private vec3 _color;

        public vec3 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public vec3 Color
        {
            get
            {
                return _color;
            }

            set
            {
                _color = value;
            }
        }

        public Light(vec3 position, vec3 color)
        {
            this._position = position;
            this._color = color;
        }


    }
}
