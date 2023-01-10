using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    // O(V + E)
    public class DepthSearch
    {
        private List<Vertex> passedVertexes = new List<Vertex>();
        private List<Edge> passedEdges = new List<Edge>();

        private Stack<Vertex> stack = new Stack<Vertex>();

        public DepthSearch(Vertex first)
        {
            stack.Push(first);
        }

        public void Start()
        {
            while (stack.Count != 0)
            {
                Vertex cur = stack.Pop();
                if (!passedVertexes.Contains(cur))
                {
                    passedVertexes.Add(cur);
                    for (int i = passedVertexes.Count - 1; i >= 0; i--)
                    {
                        if (Repo.IsAnyConnection(passedVertexes[i], cur))
                        {
                            passedEdges.Add(Repo.GetConnection(passedVertexes[i], cur));
                            break;
                        }
                    }

                    List<(Edge, Vertex)> connections = Repo.GetConnections(cur);
                    foreach((Edge edge, Vertex vertex) in connections)
                    {
                        if (!passedVertexes.Contains(vertex))
                        {
                            stack.Push(vertex);
                        }
                    }
                }
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
            if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
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
            if (sb.Length > 2) sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            Console.WriteLine(sb.ToString());
        }
    }
}
