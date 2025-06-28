using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MAMEJoyMap
{
    class FileIO
    {
        public static string GetPath(string fileName)
        {
            string path = null;

            try
            {
                path = Path.GetDirectoryName(fileName);
            }
            catch
            {
            }

            return path;
        }

        public static string GetFile(string fileName)
        {
            string file = null;

            try
            {
                file = Path.GetFileNameWithoutExtension(fileName);
            }
            catch
            {
            }

            return file;
        }

        public static bool TryOpenFile(Form owner, string initialDirectory, string initialFileName, string extension, out string fileName)
        {
            fileName = null;

            try
            {
                OpenFileDialog fd = new OpenFileDialog();

                fd.Title = "Open File";
                fd.InitialDirectory = initialDirectory;
                fd.FileName = initialFileName;
                fd.Filter = String.Format("{0} Files (*{1})|*{2}|All Files (*.*)|*.*", extension.Substring(1, 1).ToUpper() + extension.Substring(2), extension, extension);
                fd.RestoreDirectory = true;
                fd.CheckFileExists = true;

                if (fd.ShowDialog(owner) == DialogResult.OK)
                {
                    fileName = fd.FileName;

                    return true;
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteEntry("TryOpenFile", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static bool TrySaveFile(Control parent, string initialDirectory, string initialFileName, string extension, out string fileName)
        {
            fileName = null;

            try
            {
                SaveFileDialog fd = new SaveFileDialog();

                fd.Title = "Save Layout";
                fd.InitialDirectory = initialDirectory;
                fd.FileName = initialFileName;
                fd.Filter = String.Format("{0} Files (*{1})|*{2}|All Files (*.*)|*.*", extension.Substring(1, 1).ToUpper() + extension.Substring(2), extension, extension);
                fd.OverwritePrompt = false;
                fd.RestoreDirectory = true;

                if (fd.ShowDialog(parent) == DialogResult.OK)
                {
                    fileName = fd.FileName;

                    return true;
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteEntry("TrySaveFile", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static bool TryOpenFolder(Control parent, string selectedPath, out string folder)
        {
            folder = null;

            try
            {
                FolderBrowserDialog fb = new FolderBrowserDialog();

                fb.SelectedPath = selectedPath;
                fb.ShowNewFolderButton = true;

                if (fb.ShowDialog(parent) == DialogResult.OK)
                {
                    folder = fb.SelectedPath;

                    return true;
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteEntry("TryOpenFolder", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static bool TryLoadImage(string fileName, out Bitmap bmp)
        {
            bmp = null;

            try
            {
                if (File.Exists(fileName))
                {
                    Bitmap bmpTemp = (Bitmap)Bitmap.FromFile(fileName);
                    bmp = new Bitmap(bmpTemp);
                    bmpTemp.Dispose();

                    return true;
                }
            }
            catch (Exception ex)
            {
                //LogFile.WriteEntry("TryLoadImage", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }
    }
}