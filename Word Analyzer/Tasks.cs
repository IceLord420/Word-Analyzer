using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace Word_Analyzer
{
    static class Tasks
    {
        private static Stopwatch stopwatch = new Stopwatch();

        public static long runSynchronous(string[] words)
        {
            stopwatch.Restart();

            numberOfWords(words);
            shortestWord(words);
            longestWord(words);
            averageWordLength(words);
            fiveMostCommonWords(words);
            fiveLeastCommonWords(words);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public static long runParallel(string[] words)
        {
               List<Thread> threads = new List<Thread>();
               threads.Add(new Thread(numberOfWords));
               threads.Add(new Thread(shortestWord));
               threads.Add(new Thread(longestWord));
               threads.Add(new Thread(averageWordLength));
               threads.Add(new Thread(fiveMostCommonWords));
               threads.Add(new Thread(fiveLeastCommonWords));

               stopwatch.Restart();
               foreach (var thread in threads) thread.Start(words);

               foreach (var thread in threads) thread.Join();
               stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        private static void fiveLeastCommonWords(object array)
        {
            string[] words = (string[])array;
            List<string> commonWords = new List<string>();

            foreach (var item in words)
                commonWords.Add(item);


            string[] top5commonWords = commonWords.GroupBy(w => w)
                                    .OrderByDescending(gr => gr.Count())
                                    .Take(5)
                                    .Select(sl => sl.Key)
                                    .ToArray();

            Console.WriteLine("Top 5 most common words:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i + 1}. {top5commonWords[i]}");
            }
        }

        private static void fiveMostCommonWords(object array)
        {
            string[] words = (string[])array;
            List<string> commonWords = new List<string>();

            foreach (var item in words)
                commonWords.Add(item);

            string[] top5commonWordsReverse = commonWords.GroupBy(w => w)
                            .OrderBy(gr => gr.Count())
                            .Take(5)
                            .Select(sl => sl.Key)
                            .ToArray();

            Console.WriteLine("Top 5 least common words:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i + 1}. {top5commonWordsReverse[i]}");
            }
        }

        private static void averageWordLength(object array)
        {
            string[] words = (string[])array;
            int counterWords = 0, avrWordLength = 0;

            foreach (var item in words)
            {
                avrWordLength += item.Length;
                counterWords++;
            }

            Console.WriteLine("avrWordLength:" + avrWordLength / counterWords);
        }

        private static void longestWord(object array)
        {
            string[] words = (string[])array;
            string longestWord = words[0].ToString();

            foreach (var item in words)
                if (longestWord.Length < item.Length) longestWord = item;

            Console.WriteLine("longestWord:" + longestWord);
        }

        private static void shortestWord(object array)
        {
            string[] words = (string[])array;
            string shortestWord = words[0].ToString();

            foreach (var item in words)
                if (shortestWord.Length > item.Length) shortestWord = item;

            Console.WriteLine("shortestWord:" + shortestWord);
        }

        private static void numberOfWords(object array)
        {
            string[] words = (string[])array;
            int counterWords = 0;
            foreach (var item in words)
                counterWords++;

            Console.WriteLine("words:" + counterWords);
        }


    }
}
