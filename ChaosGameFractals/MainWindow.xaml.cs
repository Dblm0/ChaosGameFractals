using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SharpGL.WPF;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
namespace ChaosGameFractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum Restriction
    {
        NoRestriction,
        UniqueNeighbours,
        Not2PlacesAway,
    }
    public partial class MainWindow : Window
    {
        OpenGLControl GLControl;

        public MainWindow()
        {
            InitializeComponent();

            GLControl = new OpenGLControl();
            GLControl.FrameRate = 60;

            MainGrid.Children.Add(GLControl);

            GLControl.OpenGLInitialized += GL_INIT;
            GLControl.Resized += GLControl_Resized;
            GLControl.OpenGLDraw += ChaosGame;

            FractalsCB.ItemsSource = Enumerable.Range(3, 10);
            FractalsCB.SelectedItem = 3;
        }

        void GLControl_Resized(object sender, OpenGLEventArgs args)
        {
            OpenGL GL = args.OpenGL;
            GL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_COLOR_BUFFER_BIT);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho2D(
                left: -15.0f,
                right: 15.0f,
                bottom: -15.0f,
                top: 15.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        void GL_INIT(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL GL = args.OpenGL;

            GL.ClearColor(0f, 0f, 0f, 0f);
            GL.ShadeModel(ShadeModel.Smooth);
            GL.Enable(OpenGL.GL_BLEND);
            GL.BlendFunc(BlendingSourceFactor.SourceAlpha, BlendingDestinationFactor.OneMinusSourceAlpha);
            GL.Enable(OpenGL.GL_POINT_SMOOTH);
            GL.Color(0.3f, 1f, 0.3f, 0.1f);
            GL.PointSize(2f);
            GL.LineWidth(3f);
            Point = new Vertex(r.Next(), r.Next(), r.Next());
        }
        Vertex[] Figure;
        Restriction Restr = Restriction.NoRestriction;
       static Random r = new Random();
        Vertex Point;
        int lastvert = 0;
        int VertN;
        void ChaosGame(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL GL = GLControl.OpenGL;
            GL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT);
            GL.LoadIdentity();           
            int iterations = 5000;
            GL.Begin(BeginMode.Points);
            for (int i = 0; i < iterations; i++)
            {
                VertN = GetVertNumber(Figure.Length, lastvert, Restr);
                lastvert = VertN;
                Point = (Point + Figure[VertN]) * (float)Fraction.Value;
                GL.Vertex(Point);
            }
            GL.End();
        }
        int GetVertNumber(int N, int Last, Restriction R)
        {
           
            int v = r.Next(0,N);
            switch (R)
            {
                case Restriction.NoRestriction:
                    return v;

                case Restriction.UniqueNeighbours:
                    while (Last == v)
                        v = r.Next(0, N);
                    return v;

                case Restriction.Not2PlacesAway:
                    while (Math.Abs(Last-v) == 2)
                        v = r.Next(0, N);
                    return v;

                default: return v;
            }

        }
        private void Fraction_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OpenGL GL = GLControl.OpenGL;
            GL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_COLOR_BUFFER_BIT);
            GL.LoadIdentity();
        }

        private void FractalsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenGL GL = GLControl.OpenGL;
            GL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_COLOR_BUFFER_BIT);
            GL.LoadIdentity();


            Figure = Figures.GetFigure(
                VertNum: (int)e.AddedItems[0],
                Phi: phirad
                );
        }


        float phirad = 0;
        private void PhiAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            OpenGL GL = GLControl.OpenGL;
            GL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_COLOR_BUFFER_BIT);
            GL.LoadIdentity();

            phirad = (float)(Math.PI / 180 * e.NewValue);

            Figure = Figures.GetFigure(
                VertNum: (int)FractalsCB.SelectedItem,
                Phi: phirad
                );
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == true)
            {
                Restr = Restriction.UniqueNeighbours;
            }
            else
            {
                Restr = Restriction.NoRestriction;
            }
            OpenGL GL = GLControl.OpenGL;
            GL.Clear(OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_COLOR_BUFFER_BIT);
            GL.LoadIdentity();
        }
    }
}
