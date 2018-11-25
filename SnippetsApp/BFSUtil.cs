using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

/// <summary>
/// Breadth first traversak / recherche en largeur
/// </summary>
namespace SnippetsApp
{
    #region INTERNAL DATA STRUCTURE
    /// <summary>
    /// Node Class, with 2 branchs
    /// </summary>

    [DataContract]
    internal class BinaryNode
    {
        private object data;
        private BinaryNode left;
        private BinaryNode right;
        [DataMember]
        public object Data { get => data; set => data = value; }
        [DataMember]
        internal BinaryNode Left { get => left; set => left = value; }
        [DataMember]
        internal BinaryNode Right { get => right; set => right = value; }
        internal int Depth { get { return (left is null && right is null ? 0 : Math.Min(left.Depth, right.Depth) + 1) ; } }

        internal void toJSON()
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(BinaryNode));
            ser.WriteObject(stream1, this);
        }
    }

    #endregion
    #region PUBLIC
    internal class BFSUtil
    {
        public static IEnumerable<T> BreadthFirstTopDownTraversal<T>(T root, Func<T, IEnumerable<T>> children)
        {
            var q = new Queue<T>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                T current = q.Dequeue();
                yield return current;
                foreach (var child in children(current))
                    q.Enqueue(child);
            }
        }

    #endregion
    #region PUBLIC
    internal List<LinkedList<BinaryNode>> FindLevelLinkList(BinaryNode root)
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

}