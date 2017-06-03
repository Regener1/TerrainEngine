using GlmNet;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.renderEngine;

namespace TerrainEngine.models
{
    public class TerrainBrush
    {
        private Terrain _terrain;

        private float _radius = 1f; 
        private vec3 _position;

        public TerrainBrush(Terrain terrain)
        {
            _terrain = terrain;
        }

        public float Radius
        {
            get
            {
                return _radius;
            }
        }

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

        public void Update(vec3 position, float radius)
        {
            _position = position;
            _radius = radius;
        }


        public void ChangeTerrainHeight(float amount)
        {
            _terrain.ChangeTerrainHeight(_position.x, _position.z, _radius, amount);
        }

        public void ChangeTerrainBlendMap()
        {
            _terrain.ChangeBlendMap(_position.x, _position.z, _radius);
        }
    }
}
