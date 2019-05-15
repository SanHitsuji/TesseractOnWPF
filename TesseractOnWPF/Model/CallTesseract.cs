using System;
using System.IO;

namespace TesseractOnWPF.Model
{
    public static class CallTesseract
    {

        /// <summary>
        /// Execute tesseract
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Recognizing(string path)
        {
            var text = "Fail to recognize";
            try
            {
                string strCmdText;


                var imagePath = path;
                var textPath = Path.GetFileNameWithoutExtension(path);
                strCmdText = $"/c tesseract {imagePath} {textPath}";
                var process = System.Diagnostics.Process.Start("CMD.exe", strCmdText);

                process.WaitForExit();
                var textWithExtension = Path.ChangeExtension(textPath, ".txt");
                text = ReadTextFile(textWithExtension);
                File.Delete(textWithExtension);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return string.Format($"exception {exception}");
            }

            return text;
        }

        /// <summary>
        /// Open and read text file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadTextFile(string path)
        {
            var text = string.Empty;
            try
            {   // Open the text file using a stream reader.
                using (var sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    text = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return text;
        }
    }
}