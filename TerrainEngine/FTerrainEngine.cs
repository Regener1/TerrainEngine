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
using TerrainEngine.models;
using TerrainEngine.renderEngine;
using TerrainEngine.tool;
using TerrainEngine.settings;

namespace TerrainEngine
{
    public partial class FTerrainEngine : Form
    {
        private bool _isMouseDown;
        private Point _oldPosition;

        private OpenGL _gl;
        private Loader _loader;
        private Renderer _renderer;
        private Camera _camera;
        private TerrainBrush _brush;
        private MousePicker _picker;
        private List<Object3D> _objects3D = new List<Object3D>();
        private Terrain _terrain;
        private float _brushRadius = 1.0f;
        private int _idObjectCounter = 0;

        public FTerrainEngine()
        {
            InitializeComponent();
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
            

            _terrain = new Terrain(_gl, _loader, 0, 0, LoadTextureImage("rock.bmp"),
                                        Resouce.SHADERS_PATH + Resouce.VERTEX_SHADER_TERRAIN,
                                        Resouce.SHADERS_PATH + Resouce.FRAGMENT_SHADER_TERRAIN);
            _terrain.CreateTerrain(64);


            _picker = new MousePicker(_terrain, _camera, _renderer.ProjectionMatrix);
            _brush = new TerrainBrush(_gl, _terrain, _loader, LoadTextureImage("brush.png"), "terrainBrush.vert", "terrainBrush.frag");

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
            catch(IOException e)
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
            }


            

            if (e.Button == MouseButtons.Left)
            {
                
                _brush.ChangeTerrainHeight(0.05f);

                for (int i = 0; i < _picker.CurTerrainPoint.to_array().Length; i++)
                {
                    Console.Write(_picker.CurTerrainPoint.to_array()[i] + " ");
                }
                Console.WriteLine();
            }
                

        }

        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                _isMouseDown = false;
            }

        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
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
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                _camera.DecreaceDistance();
            }
            else {
                _camera.IncreaceDistance();
            }
            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void UpdateBrush(MouseEventArgs e)
        {
            _picker.Update((float)e.X, (float)e.Y, glControl.Width, glControl.Height);
            vec3 position = _picker.CurTerrainPoint;
            position.y += 0.1f;
            _brush.Update(position, _brushRadius);
        }

        private void trackBarBrushSize_Scroll(object sender, EventArgs e)
        {
            _brushRadius = trackBarBrushSize.Value / 10f;
        }
    }
}
