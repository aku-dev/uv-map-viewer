using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

using Assimp;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private List<Vector3D>    UVCoords = new List<Vector3D>();    // Координаты
        private HashSet<Vector3D> UVVertex = new HashSet<Vector3D>(); // Оптимизированный массив точек

        private const int zoomFactor         = 8;
        private int defaultTextureSize       = 512;
        private int[] startDragPositionMouse = new int[2] { 0, 0 };   // Координаты мышки
        private bool isDrawing               = false;                 // Идет отрисовка
        private bool startDrag               = false;


        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        public Form1()
        {
            InitializeComponent();

            // Вокруг картинки ставим панель чтобы перехватывать событие прокрутки мыши
            panelOutPictureBox.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);

            openFileDialog1.Filter = "Filmbox files(*.fbx)|*.fbx|OBJ Geometry(*.obj)|*.obj|All files(*.*)|*.*";

            // Ставим стандартные размеры карты
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Width    = defaultTextureSize;
            pictureBox1.Height   = defaultTextureSize;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // Ставим двойную буфферизацию
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            this.UpdateStyles();
            DrawImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // Получаем выбранный файл
            string filename = openFileDialog1.FileName;
            textBox1.AppendText(String.Format("Load: {0} {1}", filename, Environment.NewLine));

            // Включим логи для импортера
            LogStream logstream = new LogStream(delegate (String msg, String userData)
            {
                textBox1.AppendText(msg + Environment.NewLine);
            });
            logstream.Attach();


            // Импортиуем
            UVCoords = new List<Vector3D>();    // Координаты
            UVVertex = new HashSet<Vector3D>(); // Точки
            AssimpContext importer = new AssimpContext();
            Scene scene = importer.ImportFile(filename, PostProcessSteps.ValidateDataStructure); // (PostProcessSteps.FindInstances | PostProcessSteps.ValidateDataStructure)

            if (scene.Meshes.Count > 0)
            {
                Mesh mesh = scene.Meshes[0];

                if (mesh.TextureCoordinateChannelCount > 0)
                {
                    UVCoords = mesh.TextureCoordinateChannels[0];
                    
                    
                    foreach (Vector3D uv in UVCoords)
                    {
                        UVVertex.Add(uv);
                    }
                }
            }

            DrawImage();
            importer.Dispose();
            Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                ZoomIn(e.X, e.Y);
            }
            else
            {
                ZoomOut(e.X, e.Y);
            }
        }

        private void ZoomIn(int x, int y)
        {
            if (pictureBox1.Width < 16384)
            {
                int inc = pictureBox1.Width / zoomFactor;

                pictureBox1.Width  = pictureBox1.Width  + inc;
                pictureBox1.Height = pictureBox1.Height + inc;
                defaultTextureSize = defaultTextureSize + inc;
            }


            pictureBox1_UpdateLocation(x, y);
            textBoxZoom_UpdateValue();
        }

        private void ZoomOut(int x, int y)
        {
            if (pictureBox1.Width > 128)
            {
                int inc = pictureBox1.Width / zoomFactor;

                pictureBox1.Width = pictureBox1.Width   - inc;
                pictureBox1.Height = pictureBox1.Height - inc;
                defaultTextureSize = defaultTextureSize - inc;
            }

            pictureBox1_UpdateLocation(x, y);
            textBoxZoom_UpdateValue();
        }

        private void pictureBox1_UpdateLocation(int x, int y)
        {
            // При приближении учитываем положение курсора мыши
            if (pictureBox1.Width > panelOutPictureBox.Width || pictureBox1.Height > panelOutPictureBox.Height)
            {
                // ToDo: Привязать координаты мыши
                pictureBox1.Location = new Point(panelOutPictureBox.Width / 2 - pictureBox1.Width / 2,
                    panelOutPictureBox.Height / 2 - pictureBox1.Height / 2);
            }
            else {
                pictureBox1.Location = new Point(panelOutPictureBox.Width / 2 - pictureBox1.Width / 2,
                    panelOutPictureBox.Height / 2 - pictureBox1.Height / 2);
            }

            DrawImage();
        }

        private void textBoxZoom_UpdateValue()
        {
            textBoxZoom.Text = Convert.ToString(Math.Floor((float)pictureBox1.Width / 512 * 100)) + "%";
        }

        private void DrawImage()
        {
            sw.Start();
            if (!isDrawing)
            {
                isDrawing = true;
                int textureSize = pictureBox1.Width;

                Bitmap frameImage = new Bitmap(textureSize, textureSize);
                using (Graphics gr = Graphics.FromImage(frameImage))
                {
                    gr.SmoothingMode = SmoothingMode.None;               // Качество в минимум для ускорения

                    // Обрезаем область рисования                
                    int clipX = (textureSize > panelOutPictureBox.Width) ? -pictureBox1.Location.X : 0;
                    int clipY = (textureSize > panelOutPictureBox.Height) ? -pictureBox1.Location.Y : 0;
                    int clipW = (textureSize > panelOutPictureBox.Width) ? panelOutPictureBox.Width : textureSize;
                    int clipH = (textureSize > panelOutPictureBox.Height) ? panelOutPictureBox.Height : textureSize;

                    // ToDo: Убрать отрисовку полигонов которые не входят в область видимости

                    Rectangle clip = new Rectangle(clipX, clipY, clipW, clipH);
                    gr.SetClip(clip);


                    // Сетка
                    using (Pen axis = new Pen(Color.FromArgb(127, 127, 127)))
                    {
                        int widthLines = textureSize / 32;
                        int heightLines = textureSize / 32;
                        for (int i = 0; i < textureSize - widthLines; i += widthLines)
                        {
                            // Вертикальные
                            gr.DrawLine(axis, new Point(i + widthLines, 0), new Point(i + widthLines, textureSize));

                            // Горизонтальные
                            gr.DrawLine(axis, new Point(0, i + heightLines), new Point(textureSize, i + heightLines));
                        }
                    }

                    // Развертка
                    if (UVCoords != null && UVCoords.Count > 0)
                    {
                        // Полигоны
                        using (Brush trisFill = new SolidBrush(Color.FromArgb(128, 150, 140, 220)))
                        using (Pen trisLine = new Pen(Color.FromArgb(67, 255, 163)))
                            for (int i = 0; i < UVCoords.Count; i++)
                            {
                                if (i % 3 == 2)
                                {
                                    PointF pa = new PointF(textureSize * UVCoords[i].X, textureSize * (1 - UVCoords[i].Y));
                                    PointF pb = new PointF(textureSize * UVCoords[i - 1].X, textureSize * (1 - UVCoords[i - 1].Y));
                                    PointF pc = new PointF(textureSize * UVCoords[i - 2].X, textureSize * (1 - UVCoords[i - 2].Y));
                                    PointF[] polyPoints = { pa, pb, pc };
                                    GraphicsPath path = new GraphicsPath();
                                    path.AddPolygon(polyPoints);
                                    gr.FillPolygon(trisFill, polyPoints);
                                    if (checkBoxShowEdge.Checked)
                                    {
                                        gr.DrawPath(trisLine, path);
                                    }
                                }
                            }

                        // Вершины
                        if (checkBoxShowVertex.Checked)
                        {
                            using (Brush trisFill = new SolidBrush(Color.FromArgb(255, 133, 0)))
                                foreach (Vector3D uv in UVVertex)
                                {
                                    Point pa = new Point((int)(textureSize * uv.X), (int)(textureSize * (1 - uv.Y)));
                                    Rectangle rect = new Rectangle(pa.X - 1, pa.Y - 1, 3, 3);
                                    gr.FillRectangle(trisFill, rect);
                                }
                        }
                    }

                    pictureBox1.Image = (Image)frameImage;
                    pictureBox1.Invalidate();
                } // Dispose Graphics gr
                isDrawing = false;
            }

            sw.Stop();            
            textBox1.AppendText(String.Format("Draw: {0} sec.{1}", (sw.ElapsedMilliseconds / 100.0), Environment.NewLine));
            sw.Reset();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void checkBoxShowVertex_CheckedChanged(object sender, EventArgs e)
        {
            DrawImage();
            pictureBox1.Invalidate();
        }

        private void checkBoxShowEdge_CheckedChanged(object sender, EventArgs e)
        {
            DrawImage();
            pictureBox1.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Перетаскиваем область
            if (e.Button == MouseButtons.Middle)
            {
                startDrag  = true;
                startDragPositionMouse[0] = e.X;
                startDragPositionMouse[1] = e.Y;
            }    
        }        

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Перетаскиваем область
            if (e.Button == MouseButtons.Middle)
            {
                DrawImage();
                startDrag = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startDrag == true)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X - (startDragPositionMouse[0] - e.X),
                    pictureBox1.Location.Y - (startDragPositionMouse[1] - e.Y));
            }
        }
    }
}