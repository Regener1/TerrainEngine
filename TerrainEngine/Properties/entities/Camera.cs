using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrainEngine.entities
{
    public class Camera
    {
        private vec3 _direction = new vec3(0, 0, 0);
        private vec3 _position = new vec3(0, 0, 0);
        private float _pitch = 45f;
        private float _yaw = -45f;
        private float _distance = 1f;
        private float _projectionDistance;
        private float _angelAround = 45f;

        private float _step = 0.2f;


        public vec3 Direction
        {
            get { return _direction; }
        }

        public vec3 Position
        {
            get { return _position; }
        }

        public float Pitch
        {
            get { return _pitch; }
        }

        public float Yaw
        {
            get { return _yaw; }
            set { _yaw = value; }
        }

        private void CalculateDirection()
        {
            float x = _step * glm.sin(glm.radians(180 - _yaw));
            float z = _step * glm.cos(glm.radians(180 - _yaw));
            _direction.x += x;
            _direction.z += z;
        }

        public Camera() 
        {
            CalculateNewLocation();
        }

        public void MoveForward()
        {
            CalculateDirection();
            CalculateNewLocation();
        }
        public void MoveBack()
        {

            CalculateNewLocation();
        }
        public void MoveRight()
        {

            CalculateNewLocation();
        }
        public void MoveLeft()
        {

            CalculateNewLocation();
        }
        public void MoveUp()
        {
            _direction.y -= 0.05f;
            CalculateNewLocation();
        }
        public void MoveDown()
        {
            _direction.y += 0.05f;
            CalculateNewLocation();
        }

        public void IncreasePitch(int deltha)
        {
            _pitch += deltha;
            CalculateNewLocation();
        }

        public void DecreasePitch(int deltha)
        {
            _pitch -= deltha;
            CalculateNewLocation();
        }

        public void IncreaseAroundPoint(int deltha)
        {
            _angelAround += deltha;
            CalculateNewLocation();
            _yaw = 180 + (_angelAround + 90);
        }

        public void DecreaseAroundPoint(int deltha)
        {
            _angelAround -= deltha;
            CalculateNewLocation();
            _yaw = 180 + (_angelAround + 90);
        }

        public void ChangePosition(int delthaX, int delthaZ)
        {
            _direction.x += delthaX * 0.0005f;
            _direction.z += delthaZ * 0.0005f;
            CalculateNewLocation();
        }

        public void IncreaceDistance()
        {
            _distance += 0.5f;
            CalculateNewLocation();
        }

        public void DecreaceDistance()
        {
            _distance -= 0.5f;
            CalculateNewLocation();
        }


        public void CalculateNewLocation()
        {
            _projectionDistance = _distance * glm.cos(glm.radians(_pitch));
            _position.y = _direction.y + _distance * glm.sin(glm.radians(_pitch));

            _position.x = _direction.x + _projectionDistance * glm.cos(glm.radians(_angelAround));
            _position.z = _direction.z + _projectionDistance * glm.sin(glm.radians(_angelAround));
        }

        public string GetInfo()
        {
            return "pos X " + Position.x.ToString() + Environment.NewLine +
                "pos Y " + Position.y.ToString() + Environment.NewLine +
                "pos Z " + Position.z.ToString() + Environment.NewLine +
                "dir X " + Direction.z.ToString() + Environment.NewLine +
                "dir Y " + Direction.z.ToString() + Environment.NewLine +
                "dir Z " + Direction.z.ToString() + Environment.NewLine +
                "around " + _angelAround.ToString() + Environment.NewLine +
                "yaw " + Yaw.ToString() + Environment.NewLine +
                "pitch " + Pitch.ToString() + Environment.NewLine + 
                "R " + _distance.ToString();
        }
    }
}
