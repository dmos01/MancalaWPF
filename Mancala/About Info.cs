using System;

namespace Mancala
{
    class AboutInfo
    {
        public const string ReleaseDate = "30 January 2020";
        public const string Creator = "Created by Daniel Spencer.";
        public const string Licenses = "Icon from http://ztreasureisle.wikia.com/wiki/File:Green_Stone-icon.png";

        /// <summary>
        /// Uses Assembly version. Not to be confused with Package Version or Assembly File Version.
        /// </summary>
        public static string ThreePartVersionNumber
        {
            get
            {
                Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return v.Major + "." + v.Minor + "." + v.Build;
            }
        }

        /// <summary>
        /// Uses Assembly version. Not to be confused with Package Version or Assembly File Version.
        /// </summary>
        public static string FourPartVersionNumber
        {
            get
            {
                Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;
            }
        }
    }
}