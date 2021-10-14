using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Word_Analyzer
{
    class Program
    {
        static readonly string textFilePath = @"C:\Users\Naydevi\Desktop\Ivan_Vazov_-_Pod_igoto_-_1773-b.txt";
        //static readonly string textFilePath = @"C:\Users\Naydevi\Desktop\Test.txt";
        static void Main(string[] args)
        {
            char[] separators = new char[] { ' ', '.', '—', ',', ')', ':' , '?', '!', '…'};

            
            string shortestWord, longestWord;


            string text = File.ReadAllText(textFilePath, Encoding.UTF8);
            Console.OutputEncoding = Encoding.UTF8;
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            

            long time1 = Tasks.runSynchronous(words);
            long time2 = Tasks.runParallel(words);

            Console.WriteLine($"synchronous tasks: {time1}");
            Console.WriteLine($"parallel tasks: {time2}");

            

            Console.ReadLine();
        }
    }
}
