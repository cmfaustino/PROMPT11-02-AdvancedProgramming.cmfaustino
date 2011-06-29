using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mod02_AdvProgramming.PhotoAlbums
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Listing c:\windows desdendant files in as eager way
            //foreach (FileInfo fileInfo in DirectoryEnumerator.GetDirectoryEnumeratorEager(new DirectoryInfo("c:\\windows")))
            //{
            //    Console.WriteLine(fileInfo.FullName);
            //}

            //// Listing c:\windows desdendant files in as lazy way
            //foreach (FileInfo fileInfo in DirectoryEnumerator.GetDirectoryEnumeratorLazy(new DirectoryInfo("c:\\windows")))
            //{
            //    Console.WriteLine(fileInfo.FullName);
            //}

            // Listing c:\windows desdendant files with an extension method for DirectoryInfo
            foreach (FileInfo fileInfo in new DirectoryInfo("c:\\windows").GetDirectoryEnumerator())
            {
                Console.WriteLine(fileInfo.FullName);
            }
        }
    }
}
