using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// Breadth first traversak / recherche en largeur
/// </summary>
namespace SnippetsApp
{
    #region INTERNAL BINARY TREE DATA STRUCTURE
    /// <summary>
    /// Node Class, with 2 branchs
    /// </summary>

    [DataContract]
    public class BinaryNode
    {
        private object _data;
        private int _depth = -1;
        private BinaryNode _left;
        private BinaryNode _right;
        [DataMember]
        public object Data { get => _data; set => _data = value; }
        [DataMember]
        public BinaryNode Left { get => _left; set => _left = value; }
        [DataMember]
        public BinaryNode Right { get => _right; set => _right = value; }
        [DataMember]
        public int Depth
        {
            get
            {
                if (_depth > -1)
                {
                    return _depth;
                }
                else if (_left is null && _right is null)
                {
                    return (_depth = 0);
                }
                else
                {//recursive
                    return (_depth = Math.Max(_left.Depth, _right.Depth) + 1);
                }
            }
            set => _depth = value;
        }
        [IgnoreDataMember]
        public IEnumerable<BinaryNode> Children { get { return new[] { Left, Right }.Where(x => x != null); } }

        internal MemoryStream toJSON()
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BinaryNode));
            ser.WriteObject(stream, this);
            return stream;
        }
        internal String toJSONString()
        {
            MemoryStream stream = this.toJSON();
            byte[] json = stream.ToArray();
            stream.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        static internal BinaryNode fromJSON(String json)
        {
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return fromJSON(ms);
        }
        static internal BinaryNode fromJSON(Stream stream)
        {
            BinaryNode deserialized = new BinaryNode();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserialized.GetType());
            deserialized = ser.ReadObject(stream) as BinaryNode;
            stream.Close();
            return deserialized;
        }
    }

    #endregion
    #region PUBLIC Breadth-First Search
    public static class BFSUtils
    {
        /// <summary>
        /// Generic BFS using the root (1rst node) and its children (enum of nodes)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static IEnumerable<T> BreadthFirstTopDownTraversal<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var q = new Queue<T>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                T current = q.Dequeue();
                yield return current;
                foreach (var child in children(current))
                {
                    q.Enqueue(child);
                }
            }
        }

        #endregion
        #region PUBLIC
        public static List<LinkedList<BinaryNode>> FindLevelLinkList(BinaryNode root)
        {
            Queue<BinaryNode> q = new Queue<BinaryNode>();
            // List of all nodes starting from root.
            List<BinaryNode> list = new List<BinaryNode>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                BinaryNode current = q.Dequeue();
                if (current == null)
                {
                    Console.WriteLine(current);
                    continue;
                }
                q.Enqueue(current.Left);
                q.Enqueue(current.Right);
                list.Add(current);
            }

            // Add tree nodes of same depth into individual LinkedList. Then add all LinkedList into a List
            LinkedList<BinaryNode> LL = new LinkedList<BinaryNode>();
            List<LinkedList<BinaryNode>> result = new List<LinkedList<BinaryNode>>();
            LL.AddLast(root);
            int currentDepth = 0;
            foreach (BinaryNode node in list)
            {
                if (node != root)
                {
                    if (node.Depth == currentDepth)
                    {
                        LL.AddLast(node);
                    }
                    else
                    {
                        result.Add(LL);
                        LL = new LinkedList<BinaryNode>();
                        LL.AddLast(node);
                        currentDepth++;
                    }
                }
            }

            // Add the last linkedlist
            result.Add(LL);
            return result;
        }
        #endregion
    }

    /// <summary>
    /// Implementation of Graph BFS
    /// </summary>
    public class BFSGraphUtils
    {

        //total no of vertices
        private readonly int Vertices;
        //adjency list array for all vertices.
        private readonly List<Int32>[] adj;
        /* Example : vertices=4
         *      0->[1,2]
         *      1->[2]
         *      2->[0,3]
         *      3->[]
         */

        //constructor
        public BFSGraphUtils(int v)
        {
            Vertices = v;
            adj = new List<Int32>[v];
            //Instantiate adjacecny list for all vertices
            for (int i = 0; i < v; i++)
            {
                adj[i] = new List<Int32>();
            }

        }

        //Add edge from v->w
        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
        }

        //Print BFS traversal
        //s-> start node
        //BFS uses queue as a base.
        public void BFS(int s)
        {
            bool[] visited = new bool[Vertices];

            //create queue for BFS
            Queue<int> queue = new Queue<int>();
            visited[s] = true;
            queue.Enqueue(s);

            //loop through all nodes in queue
            while (queue.Count != 0)
            {
                //Dequeue a vertex from queue and print it.
                s = queue.Dequeue();
                Console.WriteLine("next->" + s);

                //Get all adjacent vertices of s
                foreach (Int32 next in adj[s])
                {
                    if (!visited[next])
                    {
                        visited[next] = true;
                        queue.Enqueue(next);
                    }
                }

            }
        }

        //DFS traversal 
        // DFS uses stack as a base.
        public void DFS(int s)
        {
            bool[] visited = new bool[Vertices];

            //For DFS use stack
            Stack<int> stack = new Stack<int>();
            visited[s] = true;
            stack.Push(s);

            while (stack.Count != 0)
            {
                s = stack.Pop();
                Console.WriteLine("next->" + s);
                foreach (int i in adj[s])
                {
                    if (!visited[i])
                    {
                        visited[i] = true;
                        stack.Push(i);
                    }
                }
            }
        }

        public void PrintAdjacecnyMatrix()
        {
            for (int i = 0; i < Vertices; i++)
            {
                Console.Write(i + ":[");
                string s = "";
                foreach (var k in adj[i])
                {
                    s = s + (k + ",");
                }
                s = s.Substring(0, s.Length - 1);
                s = s + "]";
                Console.Write(s);
                Console.WriteLine();
            }
        }

    }
}