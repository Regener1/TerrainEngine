using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrainEngine.entities;
using TerrainEngine.models;

namespace TerrainEngine.tool
{
    public class MousePicker
    {
        private const int RECURSION_COUNT = 200;
        private const float RAY_RANGE = 600;
        private vec3 _curRay;
        private vec3 _curTerrainPoint;

        private mat4 _projectionMatrix;
        private Camera _camera;
        private Terrain _terrain;

        public MousePicker(Terrain terrain, Camera camera, mat4 projection)
        {
            this._terrain = terrain;
            this._camera = camera;
            this._projectionMatrix = projection;
        }

        public vec3 CurRay { get { return _curRay; } }

        public vec3 CurTerrainPoint { get { return _curTerrainPoint; } }

        public void Update(float mouseX, float mouseY, float width, float height)
        {
            _curRay = CalculateMouceRay(mouseX, mouseY, width, height);
            if (IntersectionInRange(0, RAY_RANGE, _curRay))
            {
                _curTerrainPoint = BinarySearch(0, 0, RAY_RANGE, _curRay);
                //_curTerrainPoint = new vec3(_curTerrainPoint.x, _curTerrainPoint.y, _curTerrainPoint.z);
            }
            else
            {
                _curTerrainPoint = new vec3();
            }
        }

        private vec3 CalculateMouceRay(float mouseX, float mouseY, float width, float height)
        {
            vec2 normalizedCoords = GetNormalizedDeviceCoords(mouseX, mouseY, width, height);
            vec4 clipCoords = new vec4(normalizedCoords.x, normalizedCoords.y, -1f, -1f);
            vec4 eyeCoords = ToEyeCoords(clipCoords);
            vec3 worldRay = ToWorldCoords(eyeCoords);
            return worldRay;
        }

        private vec3 ToWorldCoords(vec4 eyeCoords)
        {
            mat4 invertedView = glm.inverse(MatrixMath.CreateViewMatrix(_camera));
            vec4 rayWorld = invertedView * eyeCoords;
            vec3 mouseRay = new vec3(rayWorld.x, rayWorld.y, rayWorld.z);
            mouseRay = glm.normalize(mouseRay);
            return mouseRay;
        }

        private vec4 ToEyeCoords(vec4 clipCoords)
        {
            mat4 invertedProjection = glm.inverse(_projectionMatrix);
            vec4 eyeCoords = invertedProjection * clipCoords;
            return new vec4(eyeCoords.x, eyeCoords.y, -1f, 0);
        }

        private vec2 GetNormalizedDeviceCoords(float mouseX, float mouseY, float width, float height)
        {
            float x = (2f * mouseX) / width - 1;
            float y = 1 - (2f * mouseY) / height;
            return new vec2(x, y);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////

        

        private vec3 BinarySearch(int count, float start, float finish, vec3 ray)
        {
            float half = start + ((finish - start) / 2f);
            if (count >= RECURSION_COUNT)
            {
                vec3 endPoint = GetPointOnRay(ray, half);
                endPoint = GetTerrainHeight(endPoint.x, endPoint.z);
                if (_terrain != null)
                {
                    return endPoint;
                }
                else
                {
                    return new vec3();
                }
            }
            if (IntersectionInRange(start, half, ray))
            {
                return BinarySearch(count + 1, start, half, ray);
            }
            else
            {
                return BinarySearch(count + 1, half, finish, ray);
            }
        }

        private vec3 GetPointOnRay(vec3 ray, float distance)
        {
            vec3 camPos = _camera.Position;
            vec3 start = new vec3(camPos.x, camPos.y, camPos.z);
            vec3 scaledRay = new vec3(ray.x * distance, ray.y * distance, ray.z * distance);
            return start + scaledRay;
        }

        private bool IntersectionInRange(float start, float finish, vec3 ray)
        {
            vec3 startPoint = GetPointOnRay(ray, start);
            vec3 endPoint = GetPointOnRay(ray, finish);
            if (!IsUnderGround(startPoint) && IsUnderGround(endPoint))
            {
                return true;
            }
            else
            {
                return false;
                
            }
        }

        private bool IsUnderGround(vec3 testPoint)
        {
            float height = 0;
            if (_terrain != null)
            {
                height = _terrain.GetHeightOfTerrain(testPoint.x, testPoint.z);
            }
            if (testPoint.y < height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private vec3 GetTerrainHeight(float x, float z)
        {
            float height = _terrain.GetHeightOfTerrain(x, z);
            return new vec3(x, height, z);
        }
    }
}
