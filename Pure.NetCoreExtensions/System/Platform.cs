#if !NET451
using System;
using System.Runtime.InteropServices;
#endif

namespace Pure.NetCoreExtensions
{
    public enum OSType
    {
        Windows,
        OSX,
        Linux
    }

    public static class Platform
    {
        public static OSType OS
        {
            get
            {
#if !NET451
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return OSType.Windows;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return OSType.OSX;
                else
                    return OSType.Linux;
#else
                if (Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32S || Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.WinCE)
                    return OSType.Windows;
                else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
                    return OSType.OSX;
                else
                    return OSType.Linux;
#endif
            }
        }


          static public string RuntimeType { get; } = GetRuntimeType();

        static private string GetRuntimeType()
        {
#if NET451
            return Type.GetType("Mono.Runtime") != null ? "Mono" : "CLR";
#else
            return "CoreCLR";
#endif
        }
        public static string OSDescription
        {
            get
            {
                return RuntimeInformation.OSDescription;
            }
        }
        public static string FrameworkDescription
        {
            get
            {
                return RuntimeInformation.FrameworkDescription;
            }
        }
        public static Architecture OSArchitecture
        {
            get {
                return RuntimeInformation.OSArchitecture;
            }
        }
        public static Architecture ProcessArchitecture
        {
            get
            {
                return RuntimeInformation.ProcessArchitecture;
            }
        }

        public static bool Is64BitOperatingSystem
        {
            get
            {
                return Environment.Is64BitOperatingSystem;
            }
        }
    }
}
