using System;
using System.Collections.Generic;
using System.Linq;

namespace SnippetsApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Out.WriteLine("Hello World!");

            String s = "Hello   the       World some of the  trick and   tips!";
            Console.Out.WriteLine(s.TrimAndReduce());

            //test methods DataGraphUtil
            BinaryNode ex = new BinaryNode();
            ex.Data = "nodeA";
            Console.WriteLine("JSON form of an Binary Graph: ");
            Console.WriteLine(ex.toJSONString());

            //test methods BFSGraphUtils
            BFSGraphUtils graph = new BFSGraphUtils(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);
            //Print adjacency matrix
            graph.PrintAdjacecnyMatrix();

            Console.WriteLine("BFS traversal starting from vertex 2:");
            graph.BFS(2);
            Console.WriteLine("DFS traversal starting from vertex 2:");
            graph.DFS(2);


            Console.ReadKey();
        }


        public static void testBFS()
        {
            BinaryNode root = new BinaryNode();
            root.Data = "nodeA";

            foreach (var node in BFSUtils.BreadthFirstTopDownTraversal(root, node => node.Children))
            {
                Console.WriteLine(node);
            }
        }

    }

    //Extension methods must be defined in a static class
    public static class StringExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static string TrimAndReduce(this string str)
        {
            return ConvertWhitespacesToSingleSpaces(str).Trim();
        }

        public static string ConvertWhitespacesToSingleSpaces(this string value)
        {
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", " ");
        }
    }

}
