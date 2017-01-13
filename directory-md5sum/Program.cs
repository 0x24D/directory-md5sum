using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace directory_md5sum
{
    class Program
    {
        public static List<string> filesList = new List<string>();
        public static List<string> filessList = new List<string>();
        static void Main(string[] args)
        {
            bool allFilesMatch = true;
            string pathA = @"Literal\Path";
            string pathB = @"Literal\Path";
            DirSearch(pathA, filesList);
            DirSearch(pathB, filessList);
            for (int i = 0; i < filesList.Count; i++)
            {
                string sum1 = (checkMD5(filesList[i]));
                string sum2 = (checkMD5(filessList[i]));
                if (sum1.Equals(sum2))
                {

                }
                else
                {
                    allFilesMatch = false;
                }

            }
            if (allFilesMatch)
            {
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(1);
            }
        }
        static void DirSearch(string sDir, List<string> list)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        list.Add(f);
                    }
                    DirSearch(d, list);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        static string checkMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }
    }
}
