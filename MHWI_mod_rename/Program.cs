using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace MHWI_Rename
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("拖拽文件夹到此处：");
            string dir = Console.ReadLine();
            Console.WriteLine("输入防具编号（格式：000_0000）：");
            string code = Console.ReadLine();
            FindFile(dir, code);
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
        }
        // 递归遍历文件夹和文件
        public static void FindFile(string dirPath, string code)
        {
            var dir = new DirectoryInfo(dirPath);
            try
            {
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    string path = d.FullName;
                    if (Check(path))
                    {
                        path = Tihuan(d.FullName, code);
                        try
                        {
                            Directory.Move(d.FullName, path);
                            Console.WriteLine("文件夹：" + path);
                        }
                        catch (Exception){}
                    }
                    FindFile(path, code);
                }
                foreach (FileInfo f in dir.GetFiles())
                {
                    if (Check(f.FullName))
                    {
                        string newpath = Tihuan(f.FullName, code);
                        File.Move(f.FullName, newpath);
                        Console.WriteLine("文件：" + newpath);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        // 替换编号
        public static string Tihuan(string str, string code)
        {
            string pattern = "[0-9]{3}_[0-9]{4}";
            var rgx = new Regex(pattern);
            return rgx.Replace(str, code);
        }
        // 检查文件及文件夹是否包含关键字
        public static bool Check(string str)
        {
            Regex reg = new Regex(@"[0-9]{3}_[0-9]{4}");
            Match match = reg.Match(str);
            if (match.Success) return true;
            else return false;
        }
    }
}