using System;
using System.IO;
namespace fotored
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\assas\Desktop\Camera";
            string subpath = @"1\1";
            DirectoryInfo dirinf = new DirectoryInfo(path);
            dirinf.CreateSubdirectory(subpath);
            if (Directory.Exists(path))
            {
                Console.WriteLine("Файлы: ");
                string[] files = Directory.GetFiles(path);
                int numoffile = files.Length;
                

                for (int i = 0; numoffile > i; i++)
                {
                    FileInfo filein = new FileInfo(files[i]);
                    if (filein.Exists)
                    {

                        Console.WriteLine("Дата создания: {0}, имя {1}", filein.LastWriteTime, filein.Name);
                    }

                }

                for (int i = 0; numoffile >= i; i++)          //делаем сравнение каждой строки с последующей по времени изменения файла
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

                    if (date3.Minutes <= 3.5)           //если разница во времени меньше 4минут то скидываем в заготовленную папку 
                    {
                        string newpath = (path + @"\" + subpath + @"\" + name);    //путь для перемещения файла -куда
                        FileInfo filetrans = new FileInfo(files[i]);
                        if (filetrans.Exists)  //проверка 
                        {
                           
                            filetrans.CopyTo(newpath, true);   //перемещение
                        }
                    }
                    else                       //иначе создаем новую папку и скидываем туда
                    {
                        string newpath = (path + @"\" + subpath + @"\" + name);     // путь для перещения файла
                        FileInfo filetrans1 = new FileInfo(files[i]);               // переносим файл с которого проверяли

                        if (filetrans1.Exists)
                        {

                            filetrans1.CopyTo(newpath, true);
                        }
                        subpath = subpath + "1";
                        dirinf.CreateSubdirectory(subpath);
                        string newpath1 = (path + @"\" + subpath + @"\" + name);
                        FileInfo filetrans = new FileInfo(files[i+1]);               // переносим файл созданный позже на 4минуты в новую папку

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