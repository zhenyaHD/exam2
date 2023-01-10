using System;
using System.Text;

namespace Preparing
{
    class Program
    {
        static void Main()
        {
            string[] array = new string[] { "Carmen", "Bob", "Clue", "Apple", "Abey" };
            PrintArray(array);

            ABC abc = new ABC();

            string[] newArray = abc.Sort(array).ToArray();
            PrintArray(newArray);
        }

        static void GenerateFile()
        {
            int[] array = GenerateArray();
            string[] lines = new string[array.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = array[i].ToString();
            }

            string path = Path.Combine(Environment.CurrentDirectory, "A.txt");
            File.WriteAllLines(path, lines);
        }

        static int[] GenerateArray(int size = 10)
        {
            int[] array = new int[size];
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(1, 100);
            }
            return array;
        }

        static int[] GetArray()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "A.txt");
            string[] lines = File.ReadAllLines(path);

            int[] array = new int[lines.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(lines[i]);
            }

            return array;
        }

        static void PrintArray(int[] array)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (var elem in array)
            {
                sb.Append($"{elem}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            Console.WriteLine(sb.ToString());
        }

        static void PrintArray(string[] array)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[");
            foreach (var elem in array)
            {
                sb.Append($"{elem}, ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");

            Console.WriteLine(sb.ToString());
        }

        static void CreatePrimGraph()
        {
            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            Vertex d = new Vertex("D");

            Repo.AddVertex(a);
            Repo.AddVertex(b);
            Repo.AddVertex(c);
            Repo.AddVertex(d);

            Repo.AddConnection(a, b, new Edge(1));
            Repo.AddConnection(b, c, new Edge(2));
            Repo.AddConnection(c, d, new Edge(3));
            Repo.AddConnection(a, d, new Edge(200));
            Repo.AddConnection(b, d, new Edge(200));
            Repo.AddConnection(a, c, new Edge(200));
        }

        static void CreateWalkGraph()
        {
            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            Vertex d = new Vertex("D");
            Vertex e = new Vertex("E");
            Vertex f = new Vertex("F");

            Repo.AddVertex(a);
            Repo.AddVertex(b);
            Repo.AddVertex(c);
            Repo.AddVertex(d);
            Repo.AddVertex(e);
            Repo.AddVertex(f);

            Repo.AddConnection(a, c, new Edge(1));
            Repo.AddConnection(c, f, new Edge(2));
            Repo.AddConnection(f, e, new Edge(3));
            Repo.AddConnection(e, b, new Edge(4));
            Repo.AddConnection(b, a, new Edge(5));
            Repo.AddConnection(a, d, new Edge(6));
            Repo.AddConnection(d, f, new Edge(7));
            Repo.AddConnection(d, e, new Edge(8));
        }

        static void CreateDijkstraGraph()
        {
            Vertex a = new Vertex("A");
            Vertex b = new Vertex("B");
            Vertex c = new Vertex("C");
            Vertex d = new Vertex("D");
            Vertex e = new Vertex("E");
            Vertex f = new Vertex("F");

            Repo.AddVertex(a);
            Repo.AddVertex(b);
            Repo.AddVertex(c);
            Repo.AddVertex(d);
            Repo.AddVertex(e);
            Repo.AddVertex(f);

            Repo.AddConnection(a, b, new Edge(12));
            Repo.AddConnection(b, c, new Edge(1));
            Repo.AddConnection(c, d, new Edge(6));
            Repo.AddConnection(d, e, new Edge(45));
            Repo.AddConnection(e, a, new Edge(2));
            Repo.AddConnection(a, f, new Edge(8));
            Repo.AddConnection(f, b, new Edge(1));
        }
    }

    public class Vertex
    {
        public string Name { get; set; }

        public Vertex(string name)
        {
            Name = name;
        }
    }

    public class Edge
    {
        public int Value { get; set; }

        public Edge(int value)
        {
            Value = value;
        }
    }

    public static class Repo
    {
        public class Connection
        {
            public List<Vertex> Pair { get; set; }
            public Edge Edge { get; set; }
        }

        private static List<Vertex> allVertexes = new List<Vertex>();
        private static List<Connection> connections = new List<Connection>();

        public static void AddVertex(Vertex vertex)
        {
            allVertexes.Add(vertex);
        }

        public static void AddConnection(Vertex source, Vertex dest, Edge edge)
        {
            connections.Add(new Connection
            {
                Pair = new List<Vertex> { source, dest },
                Edge = edge
            });
        }

        public static List<Vertex> GetAllVertexes()
        {
            return allVertexes;
        }

        public static List<Vertex> GetVertexes(Edge edge)
        {
            Connection info = connections.FirstOrDefault(x => x.Edge == edge);
            return info.Pair;
        }

        public static List<Edge> GetAllEdges()
        {
            return connections.Select(x => x.Edge).ToList();
        }

        public static Vertex GetVertexByName(string name)
        {
            return allVertexes.FirstOrDefault(x => x.Name == name);
        }

        public static Edge GetEdgeByValue(int value)
        {
            Connection info = connections.FirstOrDefault(x => x.Edge.Value == value);

            if (info == null) return null;
            else return info.Edge;
        }

        public static bool IsAnyConnection(Vertex source, Vertex dest)
        {
            Connection info = connections.FirstOrDefault(x => x.Pair.Contains(source) && x.Pair.Contains(dest));

            if (info == null || source == dest) return false;
            else return true;
        }

        public static Edge GetConnection(Vertex source, Vertex dest)
        {
            Connection info = connections.FirstOrDefault(x => x.Pair.Contains(source) && x.Pair.Contains(dest));

            if (info == null) return null;
            else return info.Edge;
        }

        public static List<(Edge, Vertex)> GetConnections(Vertex vertex)
        {
            List<(Edge, Vertex)> result = new List<(Edge, Vertex)>();
            List<Connection> infos = GetInfos(vertex);

            foreach (Connection info in infos)
            {
                if (info.Pair[0] == vertex)
                {
                    result.Add((info.Edge, info.Pair[1]));
                }
                else result.Add((info.Edge, info.Pair[0]));
            }

            return result;
        }

        private static List<Connection> GetInfos(Vertex vertex)
        {
            List<Connection> result = new List<Connection>();

            foreach (var elem in connections)
                if (elem.Pair.Contains(vertex))
                    result.Add(elem);

            return result;
        }
    }

    public class MyList<T>
    {
        private class Node<T>
        {
            public T Value { get; private set; }

            public Node<T> Next { get; set; }
            public Node<T> Prev { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node<T> Head = null;
        private Node<T> Tail = null;
        public int Count = 0;

        public void Add(T value)
        {
            Node<T> node = new Node<T>(value);

            if (Head == null)
            {
                Head = node;
            }
            else if (Tail == null)
            {
                Tail = node;
                Head.Next = Tail;
                Tail.Prev = Head;
            }
            else
            {
                Tail.Next = node;
                node.Prev = Tail;
                Tail = node;
            }
            Count++;
        }

        public T GetFirst()
        {
            return Head.Value;
        }

        public T GetLast()
        {
            if (Count == 1) return Head.Value;
            else return Tail.Value;
        }

        public void RemoveFirst()
        {
            if (Head != null)
            {
                if (Head.Next != null)
                {
                    Node<T> next = Head.Next;
                    next.Prev = null;
                    Head = next;
                }
                else
                {
                    Head = null;
                }
                Count--;
            }
        }

        public void RemoveLast()
        {
            if (Count == 1) RemoveFirst();
            else
            {
                if (Head != null)
                {
                    if (Tail.Prev != Head)
                    {
                        Node<T> node = Tail.Prev;
                        node.Next = null;
                        Tail = node;
                    }
                    else
                    {
                        Tail = null;
                        Head.Next = null;
                    }
                }
                Count--;
            }
        }

        public bool Contains(T data)
        {
            Node<T> cur = Head;
            for (int i = 0; i < Count; i++)
            {
                if (cur.Value.Equals(data)) return true;
                else cur = cur.Next;
            }
            return false;
        }
    }
}