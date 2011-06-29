// -----------------------------------------------------------------------
// <copyright file="DirectoryEnumerator.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Linq;

namespace Mod02_AdvProgramming.PhotoAlbums
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class DirectoryEnumerator //acrescentou-se static
    {
        public static IEnumerable<FileInfo> GetDirectoryEnumeratorEager(this DirectoryInfo di) //acrescentou-se this
        {
            //throw new NotImplementedException();
            var fichs = di.EnumerateFiles();

            foreach (var subdir in di.EnumerateDirectories())
            {
                fichs.Concat(subdir.GetDirectoryEnumeratorEager());
            }

            return fichs;
        }

        public static IEnumerable<FileInfo> GetDirectoryEnumeratorLazy(this DirectoryInfo di) //acrescentou-se this
        {
            //throw new NotImplementedException();
            //foreach (var fich in di.EnumerateFiles())
            //{
            //    yield return fich;
            //}

            //foreach (var subdir in di.EnumerateDirectories())
            //{
            //    foreach (var fich in subdir.GetDirectoryEnumeratorLazy())
            //    {
            //        yield return fich;
            //    }
            //}

            return
                di.EnumFilesExcepcaoAbafada().Concat(di.EnumDirsExcepcaoAbafada().SelectMany(dir => dir.GetDirectoryEnumeratorLazy()));
                //di.EnumerateFiles().Concat(di.EnumerateDirectories().SelectMany(dir => dir.GetDirectoryEnumeratorLazy()));
        }

        public static IEnumerable<FileInfo> EnumFilesExcepcaoAbafada(this DirectoryInfo di)
        {
            IEnumerable<FileInfo> fichs;
            try
            {
                fichs = di.EnumerateFiles();
                //yield return di.EnumerateFiles();
            }
            catch (Exception)
            {
                //return default(IEnumerable<FileInfo>);
                //return new IEnumerable<FileInfo>();
                yield break;
            }
            foreach (var fich in fichs)
            {
                yield return fich;
            }
        }

        public static IEnumerable<DirectoryInfo> EnumDirsExcepcaoAbafada(this DirectoryInfo di)
        {
            IEnumerable<DirectoryInfo> dirs;
            try
            {
                dirs = di.EnumerateDirectories();
                //yield return di.EnumerateDirectories();
            }
            catch (Exception)
            {
                //return default(IEnumerable<DirectoryInfo>);
                //return new IEnumerable<DirectoryInfo>();
                yield break;
            }
            foreach (var dir in dirs)
            {
                yield return dir;
            }
        }

        public static IEnumerable<FileInfo> GetDirectoryEnumerator(this DirectoryInfo di) //acrescentou-se this
        {
            //throw new NotImplementedException();
            return di.GetDirectoryEnumeratorLazy();
        }

        public static IEnumerable<string> GetDirectoryImages(this DirectoryInfo di) //acrescentou-se this
        {
            //throw new NotImplementedException();
            return di.GetDirectoryEnumerator().Where(fich => (
                    fich.FullName.ToLower().EndsWith(".jpg") || fich.FullName.ToLower().EndsWith(".gif")
                    || fich.FullName.ToLower().EndsWith(".png") || fich.FullName.ToLower().EndsWith(".jpeg")
                )).Select(fich => fich.FullName);
        }
    }
}
