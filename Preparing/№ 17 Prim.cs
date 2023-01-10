using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class Prim
    {
        // o(Е * lg V)
        private List<Vertex> allVertexes = new List<Vertex>();
        private List<Vertex> passedVertexes = new List<Vertex>();

        private List<Edge> allEdges = new List<Edge>();
        private List<Edge> passedEdges = new List<Edge>();

        public Prim(Vertex first)
        {
            allVertexes = Repo.GetAllVertexes();
            allEdges = Repo.GetAllEdges();

            passedVertexes.Add(first);
        }

        public void Start()
        {
            while (passedVertexes.Count != allVertexes.Count)
            {
                List<Edge> touch = new List<Edge>();

                foreach (var elem in allEdges)
                {
                    List<Vertex> vertexes = Repo.GetVertexes(elem);
                    Vertex v1 = vertexes[0];
                    Vertex v2 = vertexes[1];

                    if ((passedVertexes.Contains(v1) && !passedVertexes.Contains(v2)) ||
                        (passedVertexes.Contains(v2) && !passedVertexes.Contains(v1)))
                    {
                        touch.Add(elem);
                    }
                }

                Edge min = touch.First();
                for (int i = 1; i < touch.Count; i++)
                {
                    if (touch[i].Value < min.Value) min = touch[i];
                }
                passedEdges.Add(min);

                List<Vertex> list = Repo.GetVertexes(min);
                if (passedVertexes.Contains(list[0]))
                {
                    passedVertexes.Add(list[1]);
                }
                else passedVertexes.Add(list[0]);
            }

            PrintVertexes(passedVertexes);
            PrintEdges(passedEdges);
        }

        private void PrintVertexes(List<Vertex> list)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (var elem in list)
            {
                sb.Append($"{elem.Name}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            Console.WriteLine(sb.ToString());
        }

        private void PrintEdges(List<Edge> list)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (var elem in list)
            {
                sb.Append($"{elem.Value}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            Console.WriteLine(sb.ToString());
        }
    }
}
