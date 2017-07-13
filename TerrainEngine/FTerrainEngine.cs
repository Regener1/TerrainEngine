using GlmNet;
using SharpGL;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TerrainEngine.entities;
using TerrainEngine.renderEngine;
using TerrainEngine.tool;
using TerrainEngine.control;

namespace TerrainEngine
{
    public partial class FTerrainEngine : Form
    {
        private bool _isMouseDown;
        private Point _oldPosition;

        private OpenGL _gl;
        private Loader _loader;
        private Light _baseLight;
        private Renderer _renderer;
        private Camera _camera;
        private TerrainBrush _brush;
        private MousePicker _picker;
        private List<Model> _objects3D = new List<Model>();
        private Terrain _terrain;
        private TerrainControl _terrainControl;
        private float _brushRadius = 1.0f;

        public FTerrainEngine()
        {
            InitializeComponent();
        }

        private void ShowCamPosition()
        {
            stLblCamPosX.Text = _camera.Position.x.ToString();
            stLblCamPosY.Text = _camera.Position.y.ToString();
            stLblCamPosZ.Text = _camera.Position.z.ToString();
        }

        private void UpdateBrush(MouseEventArgs e)
        {
            _picker.Update((float)e.X, (float)e.Y, glControl.Width, glControl.Height);
            vec3 position = _picker.CurTerrainPoint;
            position.y += 0.1f;
            _brush.Update(position, _brushRadius);
        }

        private void glControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            _renderer.Prepare(_gl);
            _renderer.Render(_gl, _terrain, _objects3D, _brush, _camera);

            tbCameraInfo.Text = _camera.GetInfo();
        }

        private void glControl_OpenGLInitialized(object sender, EventArgs e)
        {
            _gl = glControl.OpenGL;
            _loader = new Loader();
            _renderer = new Renderer(45f, 0.1f, 1000f, glControl.Width, glControl.Height);
            _camera = new Camera();
            _baseLight = new Light(new vec3(0, 50, 0), new vec3(1, 1, 1));

            Image[] textures =
                {   Resources.Textures.ground,
                    Resources.Textures.dark_stones,
                    Resources.Textures.grass,
                    Resources.Textures.stone_wall,
                    Resources.Textures.blendMap
                };

            _brush = new TerrainBrush();

            _terrainControl = new TerrainControl(_gl, _loader);
            _terrain = _terrainControl.CreateTerrain(_gl, 200, 64, 0, 0, textures, new Light[] { _baseLight },
                Resources.Shaders.terrainVertexShader, Resources.Shaders.terrainFragmentShader, _brush);


            //_terrain = new Terrain(_gl, _loader, _baseLight, Resources.Textures.blendMap,
            //                            Resources.Textures.ground, Resources.Textures.dark_stones, 
            //                            Resources.Textures.grass, Resources.Textures.stone_wall,
            //                            Resources.Shaders.terrainVertexShader,
            //                            Resources.Shaders.terrainFragmentShader,
            //                            1, 0, 0);
            //_terrain.CreateTerrain(200);


            _picker = new MousePicker(_terrain, _terrainControl, _camera, _renderer.ProjectionMatrix);
            

            AddObject3D();
        }

        private void AddObject3D()
        {
            float[] vertices =
            {
                -0.5f, 0, -0.5f,
                -0.5f, 0, 0.5f,
                0.5f, 0, 0.5f,
                0.5f, 0, -0.5f
            };

            uint[] indices =
            {
                0,1,3,
                3,1,2
            };

            float[] textureCoords =
            {
                0,0,
                0,1,
                1,1,
                1,0
            };

            //Model model = _loader.LoadEntityToVao(_gl, vertices, textureCoords, indices);
            //ModelTexture texture = _loader.LoadTexture(_gl, LoadTextureImage("brush.png"));
            //ModelShader shader = new ModelShader(_gl, "v.vert", "f.frag");
            //Object3D obj = _loader.CreateObject3D(model, texture, shader, 5, new vec3(0,0,0), 0, 0, 0, 1);
            //_objects3D.Add(obj);

            //Model modelLine = _loader.LoadLineToVao(_gl, _terrain.Vertices, _terrain.Indices);
            //ModelShader modelShaderLine = new ModelShader(_gl, Resouce.SHADERS_PATH + Resouce.VERTEX_SHADER_LINE,
            //                            Resouce.SHADERS_PATH + Resouce.FRAGMENT_SHADER_LINE);
            //_renderer.SetProjectionMatrix(_gl, modelShaderLine);
            //_newLineObject3D = _loader.CreateObject3D(modelLine, new ModelTexture(), modelShaderLine,
            //                                _idObjectCounter, new vec3(0, 0, 0), 0, 0, 0, 1);
        }

        private Image LoadTextureImage(string path)
        {
            try
            {
                Image image = Image.FromFile(path);
                return image;
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void glControl_Resized(object sender, EventArgs e)
        {
            _renderer.SetViewProperties(45f, 0.1f, 1000f, glControl.Width, glControl.Height);
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isMouseDown = true;
                _oldPosition = e.Location;
            }
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateBrush(e);
            if (e.Button == MouseButtons.Right)
            {
                if (!_isMouseDown)
                {
                    return;
                }

                int dx = _oldPosition.X - e.Location.X;
                int dy = _oldPosition.Y - e.Location.Y;

                if (Math.Abs(dy) > Math.Abs(dx))
                {
                    if (dy < 0)
                    {
                        _camera.IncreasePitch(1);
                    }
                    else
                    {
                        _camera.DecreasePitch(1);
                    }
                    _oldPosition = e.Location;
                }
                if (Math.Abs(dy) < Math.Abs(dx))
                {
                    if (dx < 0)
                    {
                        _camera.IncreaseAroundPoint(1);
                    }
                    else
                    {
                        _camera.DecreaseAroundPoint(1);
                    }
                    _oldPosition = e.Location;
                }

                ShowCamPosition();
            }

            if (e.Button == MouseButtons.Left)
            {
                //_brush.ChangeTerrainBlendMap();

                _terrainControl.ChangeTerrainHeight(_terrain, 0.05f);
                //_brush.ChangeTerrainHeight(0.05f);

                //for (int i = 0; i < _picker.CurTerrainPoint.to_array().Length; i++)
                //{
                //    Console.Write(_picker.CurTerrainPoint.to_array()[i] + " ");
                //}
                //Console.WriteLine();
            }


        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _isMouseDown = false;
            }

        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                _camera.MoveForward();
            }

            if (e.KeyCode == Keys.S)
            {
                _camera.MoveBack();
            }

            if (e.KeyCode == Keys.A)
            {
                _camera.MoveLeft();
            }

            if (e.KeyCode == Keys.D)
            {
                _camera.MoveRight();
            }

            ///
            if(e.KeyCode == Keys.J)
            {
                _camera.IncreaseAroundPoint(1);
            }
            if (e.KeyCode == Keys.L)
            {
                _camera.DecreaseAroundPoint(1);
            }
            if (e.KeyCode == Keys.I)
            {
                _camera.IncreasePitch(1);
            }
            if (e.KeyCode == Keys.K)
            {
                _camera.DecreasePitch(1);
            }
            ///

            ShowCamPosition();
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                _camera.DecreaceDistance();
            }
            else
            {
                _camera.IncreaceDistance();
            }

        }



        private void trackBarBrushSize_Scroll(object sender, EventArgs e)
        {
            _brushRadius = trackBarBrushSize.Value / 10f;
        }
    }
}
