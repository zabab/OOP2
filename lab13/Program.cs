using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace lab13
{
    public class PVPLog
    {
        public const string path = @"pvp_log.txt";
        public StreamReader freader;
        public StreamWriter fwriter;
        public PVPLog() 
        { }
        public void Read()
        {
            freader = new StreamReader(path, System.Text.Encoding.Default);
            string line;
            while ((line = freader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            freader.Close();
        }
        public void Write(object obj)
        {
            fwriter = new StreamWriter(path, true, System.Text.Encoding.Default);
            fwriter.WriteLine("Session's time: {0}, {1}", DateTime.Now, obj + "\nEnd session.\n\n");
            fwriter.Close();
            
        }
        public void Find(string str)
        {
            int countSessions = 0;
            foreach (string line in File.ReadLines(path))
            {
                if (line.Contains(str))
                {
                    Console.WriteLine(line);
                }
                if (line.Contains("Session"))
                {
                    countSessions++;
                }
            }
            Console.WriteLine("Session's count: {0}", countSessions);
        }
    }
    public static class PVPDiskInfo
    {
        static DriveInfo[] drives;
        static PVPDiskInfo()
        {
            drives = DriveInfo.GetDrives();
        }
        public static string GetFreeSpace(string title)
        {
            string freeSpace = "";
            foreach (DriveInfo drive in drives)
            {
                if (title == drive.Name)
                {
                    freeSpace = ("Drive's free space: " + drive.TotalFreeSpace);
                }
            } 
            return freeSpace;
        }
        public static string GetFileSystem(string title)
        {
            string fileSystem = "";
            foreach (DriveInfo drive in drives)
            {
                if (title == drive.Name)
                {
                    fileSystem = ("Drive's type: " + drive.DriveType);
                }
            } 
            return fileSystem;
        }
        public static string GetAllInfo()
        {
            string info = "";
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    info += ("Drive's title: " + drive.Name + ", " +
                             "Drive's space: " + drive.TotalSize + ", " + 
                             "Drive's free space: " + drive.TotalFreeSpace + ", " +
                             "Drive's label: " + drive.VolumeLabel);
                }
            }
            return info;
        }
    }
    public class PVPFileInfo
    {
        public FileInfo file;
        public PVPFileInfo(string path)
        {
            file = new FileInfo(path);
        }
        public string GetFullPath()
        {
            return file.DirectoryName;
        }
        public string GetFileInfo()
        {
            return ("File's name: " + file.Name + ", " +
                    "File's extension: " + file.Extension + ", " +
                    "File's length: " + file.Length);
        }
        public string GetCreationTime()
        {
            return ("File's creation date: " + file.CreationTime);
        }
    }
    public static class PVPDirInfo
    {
        public static int countDirs = 0;
        public static string GetCountSubDirectories(string directory)
        {
            string[] dirs = Directory.GetDirectories(directory);
            int count = 0;
            foreach (string dir in dirs)
            {
                count++;
            }
            return ("Sub directory's count: " + count);
        }
        public static string GetCreationTime(string directory)
        {
            return Convert.ToString(Directory.GetCreationTime(directory));
        }
        public static string GetCountFiles(string directory)
        {
            string[] files = Directory.GetFiles(directory);
            int count = 0;
            foreach (string file in files)
            {
                count++;
            }
            return ("File's count in this directory: " + count);
        }
        public static string GetCountParentDirectories(string directory)
        {
            if (Directory.Exists(directory))
            {
                countDirs++;
                return GetCountParentDirectories(Convert.ToString(Directory.GetParent(directory)));;   
            }
            else
            {
                return ("Count of parent's directories: " + countDirs);
            }
        }
    }
    public static class PVPFileManager
    {
        public static void Task(string drive, string user_dir, string user_ex)
        {
            string list =  (PVPDirInfo.GetCountSubDirectories(drive) + "\n" + PVPDirInfo.GetCountFiles(drive));
            string dir = Convert.ToString(Directory.CreateDirectory(@"C:\PWL\study\OOP\lab13\PVPInspect"));
            StreamWriter fwriter = new StreamWriter(dir + "\\pvpdirinfo.txt", true, System.Text.Encoding.Default);
            fwriter.Write(list);
            fwriter.Close();
            FileInfo file = new FileInfo(dir + "\\pvpdirinfo.txt");
            if (file.Exists)
            {
                file.CopyTo(dir + "\\pvpdirinfonew.txt", true);
                file.Delete();      
            }
            string newDir = Convert.ToString(Directory.CreateDirectory(@"C:\PWL\work_file"));
            string[] dirs = Directory.GetFiles(user_dir);
            for (int i = 0; i < dirs.Length; i++)
            {
             
                if (dirs[i].Contains(user_ex))
                {
                    string path = dirs[i];
                    FileInfo fileInf = new FileInfo(path);
                    if (fileInf.Exists)
                    {
                        dirs[i] = dirs[i].Remove(0, 11);
                        fileInf.MoveTo(@"C:\PWL\work_file\" + dirs[i]);    
                    }
                }
            }
            string oldPath = @"C:\PWL\work_file";
            dir += "\\pvpfiles";
            Directory.Move(oldPath, dir);
            string zip = @"C:\PWL\result.zip";
            string extract = @"C:\PWL\study\OOP\lab13\extract";
            ZipFile.CreateFromDirectory(dir, zip);
            ZipFile.ExtractToDirectory(zip, extract);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PVPLog logger = new PVPLog();
            PVPFileInfo file = new PVPFileInfo(@"C:\PWL\study\OOP\lab13\pvp_log.txt");
            PVPFileManager.Task(@"C:\", @"C:\PWL\txt", ".txt"); // запускать очень осторожно, не забыв про создание файлов в txt, удаление zip
            logger.Write(PVPDirInfo.GetCountParentDirectories(@"C:\PWL"));
            logger.Write(PVPDirInfo.GetCountSubDirectories(@"C:\PWL"));
            logger.Write(PVPDirInfo.GetCountFiles(@"C:\PWL"));
            logger.Write(PVPDirInfo.GetCreationTime(@"C:\PWL"));
            logger.Write(PVPDiskInfo.GetFreeSpace("C:\\"));
            logger.Write(PVPDiskInfo.GetFileSystem("E:\\"));
            logger.Write(PVPDiskInfo.GetAllInfo());
            logger.Write(file.GetFullPath());
            logger.Write(file.GetFileInfo());
            logger.Write(file.GetCreationTime());
            logger.Find("18");
        }
    }
}
