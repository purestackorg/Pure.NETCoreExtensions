using System.IO;

namespace Pure.NetCoreExtensions
{
    public static class PathHelper
    {
        static string RootPath = "";
        static PathHelper()
        {
            RootPath = Directory.GetCurrentDirectory();
        }

        public static string GetBaseRootPath()
        {
            return RootPath;
        }

        public static string Combine(params string[] paths)
        {
            return System.IO.Path.Combine(paths);
        }
        public static string CombineWithRootPath(string path)
        {
            return System.IO.Path.Combine(GetBaseRootPath(), path);
        }
        public static string MapPath(string path)
        {
            return CombineWithRootPath(path.Replace("~/", ""));
        }


    }
}
