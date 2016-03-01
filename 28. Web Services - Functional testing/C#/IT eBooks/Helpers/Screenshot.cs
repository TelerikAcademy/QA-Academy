using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ITeBooks
{
    public static class Screenshot
    {
        /// <summary>
        /// Get screeshot of the screen
        /// </summary>
        /// <param name="FileName">File name without extension</param>
        /// <param name="AppendDateTime">If true current DateTime is appended to file name</param>
        /// <param name="FilePath">Path where file will be saved (if null current folder is used)</param>
        public static void GetFullScreen(string FileName, bool AppendDateTime = true, string FilePath = null) 
        {
            var path = string.Empty;
            var fileName = string.Empty;

            if (FilePath == null)
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            else 
            {
                path = FilePath;
            }

            if (AppendDateTime)
            {
                fileName = string.Format("{0}_{1}.png", FileName, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
            }
            else
            {
                fileName = string.Format("{0}.png", FileName);
            }

            var fullPath = Path.Combine(path, fileName);
            var width = SystemInformation.VirtualScreen.Width;
            var height = SystemInformation.VirtualScreen.Height;
            Bitmap act = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(act);
            g.CopyFromScreen(0, 0, 0, 0, act.Size, CopyPixelOperation.SourceCopy);

            Bitmap bm = new Bitmap(act);

            string outputFileName = fullPath;
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    bm.Save(memory, ImageFormat.Png);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            act.Dispose();
            g.Dispose();
            bm.Dispose();
        }
    }
}
