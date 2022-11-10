using System;
using System.IO;
namespace fotored
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Camera";     //add input of directory in future
            string subpath = @"1\1";
            DirectoryInfo dirinf = new DirectoryInfo(path);
            dirinf.CreateSubdirectory(subpath);
            if (Directory.Exists(path))
            {
                Console.WriteLine("files: ");
                string[] files = Directory.GetFiles(path);
                int numoffile = files.Length;
                

                for (int i = 0; numoffile > i; i++)
                {
                    FileInfo filein = new FileInfo(files[i]);
                    if (filein.Exists)
                    {

                        Console.WriteLine("Write Time: {0}, name {1}", filein.LastWriteTime, filein.Name);
                    }

                }

                for (int i = 0; numoffile >= i; i++)                                //comparing files by creation time
                {
                    if (i + 1 == numoffile)
                    {
                        break;
                    }
                    FileInfo filedate = new FileInfo(files[i]);
                    FileInfo filedate2 = new FileInfo(files[i+1]);
                    DateTime date1 = new DateTime();
                    DateTime date2 = new DateTime();
                    date1 = filedate.LastWriteTime;
                    date2 = filedate2.LastWriteTime;
                    TimeSpan date3 = date2 - date1;
                    string name = filedate.Name;

                    if (date3.Minutes <= 3.5)           //if the time difference is less than 4 minutes, then we throw it into one prepared folder
                    {
                        string newpath = (path + @"\" + subpath + @"\" + name);     //the path to move the file to
                        FileInfo filetrans = new FileInfo(files[i]);
                        if (filetrans.Exists)                                       //check
                        {
                           
                            filetrans.CopyTo(newpath, true);                        //copy
                        }
                    }
                    else                                                            //otherwise, we create a new folder and drop it there
                    {
                        string newpath = (path + @"\" + subpath + @"\" + name);     //the path to move the file to
                        FileInfo filetrans1 = new FileInfo(files[i]);               //we transfer the file with which the others were compared

                        if (filetrans1.Exists)
                        {

                            filetrans1.CopyTo(newpath, true);
                        }
                        subpath = subpath + "1";
                        dirinf.CreateSubdirectory(subpath);
                        string newpath1 = (path + @"\" + subpath + @"\" + name);
                        FileInfo filetrans = new FileInfo(files[i+1]);               //we transfer the file created later for 4 minutes to a new folder

                        if (filetrans.Exists)
                        {
                           
                            filetrans.CopyTo(newpath1, true);
                        }
                    }




                }




            }

            


        }
    }

}