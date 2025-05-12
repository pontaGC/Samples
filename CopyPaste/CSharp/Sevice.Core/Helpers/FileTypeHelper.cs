using System.Diagnostics;
using Services.Core.Extensions;

namespace Services.Core.Helpers
{
    /// <summary>
    /// Helper for checking file type.
    /// </summary>
    public static class FileTypeHelper
    {
        #region Methods

        /// <summary>
        /// Checks whether or not the file has the specified file extension.
        /// </summary>
        /// <param name="targetFileName">The target file name (or path) to check.</param>
        /// <param name="expectedFileExtension">The expected file extension.</param>
        /// <returns><c>true</c>, if the <c>targetFileName</c>'s extension equals <c>expectedFileExtension</c>. Otherwise; <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool HasFileExtension(string targetFileName, string expectedFileExtension)
        {
            return Path.HasExtension(targetFileName) && Path.GetExtension(targetFileName) == expectedFileExtension;
        }

        /// <summary>
        /// Checks whether or not the file is a XML file.
        /// </summary>
        /// <param name="fileName">The source file name (or path) to check.</param>
        /// <returns><c>true</c>, if the <c>path</c> has XML file extension ("xml"). Otherwise; <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsXmlFile(string fileName)
        {
            return HasFileExtension(fileName, ".xml");
        }

        #endregion
    }
}
