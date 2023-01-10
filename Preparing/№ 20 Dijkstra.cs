using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    public class Dijkstra
    {
        private List<string> marks = new List<string>();

        private List<Vertex> allVertexes = new List<Vertex>();
        private List<Edge> allEdges = new List<Edge>();

        private List<Vertex> gotMinWay = new List<Vertex>();
        private List<Vertex> buffer = new List<Vertex>();
        private Vertex buffered;

        private Vertex source { get; set; }
        private Vertex dest { get; set; }

        public Dijkstra(Vertex v1, Vertex v2)
        {
            source = v1;
            dest = v2;

            allVertexes = Repo.GetAllVertexes();
            SetMarks();
        }

        public void SetMarks()
        {
            for (int i = 0; i < allVertexes.Count; i++)
            {
                if (allVertexes[i] == source) marks.Add("0");
                else marks.Add("∞");
            }
        }

        public void Start()
        {
            while (true)
            {
                if (buffer.Count == 0)
                {
                    Vertex min = GetMin();
                    gotMinWay.Add(min);

                    if (min == dest) break;

                    buffered = min;

                    List<(Edge, Vertex)> connections = Repo.GetConnections(min);
                    foreach ((Edge edge, Vertex vertex) in connections)
                    {
                        if (!gotMinWay.Contains(vertex))
                        {
                            buffer.Add(vertex);
                        }
                    }
                }
                else
                {
                    Vertex src = buffered;
                    Vertex dst = buffer[0];
                    buffer.Remove(dst);

                    string srcMark = marks[allVertexes.IndexOf(src)];
                    string dstMark = marks[allVertexes.IndexOf(dst)];

                    int newMark = int.Parse(srcMark) + Repo.GetConnection(src, dst).Value;

                    if (dstMark == "∞") dstMark = newMark.ToString();
                    else
                    {
                        if (int.Parse(dstMark) > newMark) dstMark = newMark.ToString();
                    }
                    marks[allVertexes.IndexOf(dst)] = dstMark;
                }
            }

            Console.WriteLine(marks[allVertexes.IndexOf(dest)]);
        }

        private Vertex GetMin()
        {
            Vertex minVertex = GetFirstNotInFoundMinWay();
            string min = marks[allVertexes.IndexOf(minVertex)];

            for (int i = 0; i < marks.Count; i++)
            {
                string cur = marks[i];
                if (cur != "∞" && !gotMinWay.Contains(allVertexes[marks.IndexOf(cur)]))
                {
                    int curNum = int.Parse(cur);
                    if (min == "∞") min = cur;
                    else
                    {
                        if (int.Parse(min) > curNum)
                        {
                            min = cur;
                        }
                    }
                }
            }
            return allVertexes[marks.IndexOf(min)];
        }

        private Vertex GetFirstNotInFoundMinWay()
        {
            for (int i = 0; i < allVertexes.Count; i++)
            {
                if (!gotMinWay.Contains(allVertexes[i]))
                {
                    return allVertexes[i];
                }
            }
            return null;
        }
    }
}
