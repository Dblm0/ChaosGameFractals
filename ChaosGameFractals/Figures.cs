using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL.SceneGraph;
namespace ChaosGameFractals
{
    class Figures
    {     
        static double PI = Math.PI;

        public static Vertex[] Triangle = new Vertex[]
        {
            new Vertex(-10,-10,0),
            new Vertex(10,-10,0),
            new Vertex(0,10,0)
        };

        public static Vertex[] Square = new Vertex[] 
        { 
            new Vertex(-10,-10,0),
            new Vertex(10,-10,0),
            new Vertex(-10,10,0),
            new Vertex(10,10,0),
        };
   
        public static Vertex[] Pentagon = new Vertex[] 
        { 
            new Vertex(10 * (float)Math.Cos(0),10 * (float)Math.Sin(0),0),
            new Vertex(10 * (float)Math.Cos(2*PI/5),10 * (float)Math.Sin(2*PI/5),0),
            new Vertex(10 * (float)Math.Cos(4*PI/5),10 * (float)Math.Sin(4*PI/5),0),
            new Vertex(10 * (float)Math.Cos(6*PI/5),10 * (float)Math.Sin(6*PI/5),0),
            new Vertex(10 * (float)Math.Cos(8*PI/5),10 * (float)Math.Sin(8*PI/5),0)
        };

        public static Vertex[] Hexagon = new Vertex[] 
        { 
            new Vertex(10 * (float)Math.Cos(0),10 * (float)Math.Sin(0),0),
            new Vertex(10 * (float)Math.Cos(2*PI/6),10 * (float)Math.Sin(2*PI/6),0),
            new Vertex(10 * (float)Math.Cos(4*PI/6),10 * (float)Math.Sin(4*PI/6),0),
            new Vertex(10 * (float)Math.Cos(6*PI/6),10 * (float)Math.Sin(6*PI/6),0),
            new Vertex(10 * (float)Math.Cos(8*PI/6),10 * (float)Math.Sin(8*PI/6),0),
            new Vertex(10 * (float)Math.Cos(10*PI/6),10 * (float)Math.Sin(10*PI/6),0),
        };


        public static Vertex[] GetFigure(int VertNum, float R = 10, float Phi = 0)
        {
            if (VertNum < 3)
                VertNum = 3;
            Vertex[] V = new Vertex[VertNum];
            Double d = PI/VertNum;
            for (int i = 0; i < VertNum; i++)
            {
                V[i] = new Vertex(
                    x: R * (float)Math.Cos(2*i*d + Phi),
                    y: R * (float)Math.Sin(2*i*d + Phi),
                    z: 0
                    );
            }
            return V;
        }
    }
}
