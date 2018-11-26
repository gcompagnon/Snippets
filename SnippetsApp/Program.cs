using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Hello World!");
            
            String s = "Hello   the       World some of the  trick and   tips!";
            Console.Out.WriteLine(s.TrimAndReduce());

            //DataGraphUtil

            BinaryNode ex = new BinaryNode();
            ex.Data = "nodeA";
            Console.WriteLine("JSON form of an Binary Graph: ");
            Console.WriteLine(ex.toJSONString());


            Console.ReadKey();
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
