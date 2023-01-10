using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preparing
{
    // О (Е * lg V)
    // e рёбер
    public class Kruskal
    {
        private List<Vertex> allVertexes = new List<Vertex>();
        private List<Vertex> passedVertexes = new List<Vertex>();

        private List<Edge> allEdges = new List<Edge>();
        private List<Edge> passedEdges = new List<Edge>();

        public Kruskal()
        {
            List<Vertex> list1 = Repo.GetAllVertexes();
            foreach (var elem in list1)
            {
                allVertexes.Add(elem);
            }

            List<Edge> list2 = Repo.GetAllEdges();
            foreach (var elem in list2)
            {
                allEdges.Add(elem);
            }

            Sort(allEdges);
        }

        public void Start()
        {
            while (allVertexes.Count != passedVertexes.Count)
            {
                Edge edge = allEdges.First();
                allEdges.Remove(edge);

                List<Vertex> list = Repo.GetVertexes(edge);
                Vertex v1 = list[0];
                Vertex v2 = list[1];

                if (!(passedVertexes.Contains(v1) && passedVertexes.Contains(v2)))
                {
                    passedEdges.Add(edge);

                    if (!passedVertexes.Contains(v1)) passedVertexes.Add(v1);
                    if (!passedVertexes.Contains(v2)) passedVertexes.Add(v2);
                }
            }

            PrintVertexes(passedVertexes);
            PrintEdges(passedEdges);
        }

        private void Sort(List<Edge> array)
        {
            for (int i = 0; i < array.Count; i++)
            {
                bool breakFlag = true;
                for (int j = 0; j < array.Count - (i + 1); j++)
                {
                    if (array[j].Value > array[j + 1].Value)
                    {
                        Edge temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        breakFlag = false;
                    }
                }
                if (breakFlag) break;
            }
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
