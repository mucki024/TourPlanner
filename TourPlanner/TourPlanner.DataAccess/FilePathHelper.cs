using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccess
{
    internal static class FilePathHelper
    {
        private const string myRelativePath = nameof(FilePathHelper) + ".cs";
        private static string? lazyValue;
        public static string Value => lazyValue ??= calculatePath();

        private static string calculatePath()
        {
            string pathName = GetSourceFilePathName();

            return pathName.Substring(0, pathName.Length - myRelativePath.Length);
        }
        public static string GetSourceFilePathName([System.Runtime.CompilerServices.CallerFilePath] string? callerFilePath = null) //
    => callerFilePath ?? "";
        
    }
}
