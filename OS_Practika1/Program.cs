using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Xml.Linq;
namespace OS_Praktika1
{
  [Serializable]
  class Person
  {
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
      Name = name;
      Age = age;
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.BackgroundColor = ConsoleColor.Black;
      int filecount;
      string dirMovedFileToZip = "";
      string dirName = "";
      string fileName;
      Start();
      void Start()
      {
        string choice;
      start:
        Console.Clear();
        choice = "";
        Console.WriteLine(">> Выберите действие:");
        Console.WriteLine("\n 1. Вывести инфо о дисках");
        Console.WriteLine("\n 2. Работа с каталогами и их файлами");
        Console.WriteLine("\n 0. Выйти из программы");
        Console.WriteLine("\n >>");
        choice = (Console.ReadLine());
        Console.Clear();
        switch (choice)
        {
          case "1":
            GetDrive();
            Console.Read();
            break;
          case "2":
            DirectoryStaff();
            break;
          case "0":
            ExitMenu();
            break;

        }
        goto start;
      }
      // Главное меню
      void GetDrive()
      {
        Console.WriteLine("Перечень дисков на компьютере:");
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
          Console.WriteLine($"\nНазвание: {drive.Name}");
          Console.WriteLine($"Тип: {drive.DriveType}");
          if (drive.IsReady)
          {
            Console.WriteLine($"Объем диска: {drive.TotalSize}");
            Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
            Console.WriteLine($"Метка: {drive.VolumeLabel}");
          }
          Console.WriteLine("\n >> Нажмите клавишу Enter для продолжения");
        }
      }
      void ExitMenu()
      {
        Environment.Exit(0);
      }
      void DirectoryStaff()
      {
        string catalog = "";
      NameSet:
        if (catalog == "0") goto DirectoryEnd;
        if (dirName == ".")
        {
          dirName = "";
          goto DirectoryEnd;
        }

        Console.Clear();
        if (System.IO.Directory.Exists(dirName))
        {
          Console.Clear();
          Console.WriteLine(">> Выберите действие с каталогом *" + dirName + "*:\n");
          Console.WriteLine("\n Получить информацию:");
          Console.WriteLine("\n   1. Показать информацию о каталоге");
          Console.WriteLine("\n   2. Показать содержимое каталога");
          Console.WriteLine("\n\n Редактировать подкаталоги:");
          Console.WriteLine("\n   3. Создать подкаталог в выбранном каталоге");
          Console.WriteLine("\n   4. Удалить подкаталог в выбранном каталоге");
          Console.WriteLine("\n   5. Переместить файлы данного каталога в другой (несуществующий) каталог");
          Console.WriteLine("\n\n Перемещение по каталогам:");
          Console.WriteLine("\n   6. Перейти в подкаталог данного каталога");
          Console.WriteLine("\n   7. Перейти в родительский каталог данного подкаталога");
          Console.WriteLine("\n   8. Задать новый адрес каталога");
          Console.WriteLine("\n   9. Работа с файлами каталога");
          Console.WriteLine("\n\n   0. Назад в главное меню");
          Console.WriteLine("\n >>");
          catalog = (Console.ReadLine());
          Console.Clear();
          switch (catalog)
          {
            case "1":
              GetDirectoryInfo();
              Console.Read();
              break;
            case "2":
              GetDirectoryFiles();
              Console.Read();
              break;
            case "3":
              CreateDirectory();
              break;
            case "4":
              DeleteDirectory();
              break;
            case "5":
              MoveDirectory();
              break;
            case "6":
              GoToDirectory();
              break;
            case "7":
              GoToParentDirectory();
              break;
            case "8":
              SetDirectoryName();
              break;
            case "9":
              FileStaff();
              break;
            case "0":

              break;
          }
          goto NameSet;
        }
        else SetDirectoryName();
        goto NameSet;
      DirectoryEnd:;
      }

      // Работа с каталогами
      void SetDirectoryName()
      {
      SetDirectoryName:
        Console.WriteLine("(Начало работы) Введите путь к каталогу:\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        dirName = Console.ReadLine();
        if (dirName == ".") goto End;
        if (!System.IO.Directory.Exists(dirName))
        {
          Console.WriteLine("\nДанного каталога не существует! (Нажмите клавишу Enter, чтобы продолжить)");
          Console.ReadLine();
          Console.Clear();
          goto SetDirectoryName;
        }
      End:;
      }
      void GetDirectoryInfo()
      {
        Console.Clear();
        DirectoryInfo dirInfo = new DirectoryInfo(dirName);
        Console.WriteLine($"Название каталога: {dirInfo.Name}");
        Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
        Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
        Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
      }
      void GetDirectoryFiles()
      {
        if (System.IO.Directory.Exists(dirName))
        {
          Console.WriteLine("Подкаталоги:");
          string[] dirs = System.IO.Directory.GetDirectories(dirName);
          foreach (string s in dirs)
          {
            Console.WriteLine(s);
          }
          Console.WriteLine();
          Console.WriteLine("Файлы:");
          string[] files = System.IO.Directory.GetFiles(dirName);
          foreach (string s in files)
          {
            Console.WriteLine(s);
          }
        }
      }
      void CreateDirectory()
      {
        string path = dirName;
      CreateDirectory:
        if (System.IO.Directory.Exists(dirName))
        {
          Console.WriteLine("Подкаталоги:");
          string[] dirs = System.IO.Directory.GetDirectories(dirName);
          foreach (string s in dirs)
          {
            Console.WriteLine(s);
          }
        }
        Console.WriteLine("\nВведите название подкаталога, который требуется СОЗДАТЬ в данном каталоге:");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n");
        string subpath = Console.ReadLine();
        if (subpath == ".") goto End;
        if (subpath == "")
        {
          Console.WriteLine("\nИмя подкаталога пустое");
          goto CreateDirectory;
        }
        string adress = path + "/" + subpath;
        if (!System.IO.Directory.Exists(adress))
        {
          System.IO.Directory.CreateDirectory(adress);
          Console.Clear();
          Console.WriteLine("Подкаталог " + subpath + " создан! (Нажмите клавишу Enter, чтобы продолжить)");
          Console.ReadLine();
        }
        else
        {
          Console.WriteLine("\nДанный подкаталог уже существует! (Нажмите клавишу Enter, чтобы продолжить)\n");
          Console.ReadLine();
          Console.Clear();
          goto CreateDirectory;
        }
      End:;
      }
      void DeleteDirectory()
      {
        string answer;
        string path = dirName;
      DeleteDirectory:
        if (System.IO.Directory.Exists(dirName))
        {
          Console.WriteLine("Подкаталоги:");
          string[] dirs = System.IO.Directory.GetDirectories(dirName);
          foreach (string s in dirs)
          {
            Console.WriteLine(s);
          }
        }
        Console.WriteLine("Введите название подкаталога, который требуется УДАЛИТЬ в данном каталоге:");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n");
        string subpath = Console.ReadLine();
        if (subpath == ".") goto End;
        if (subpath == "")
        {
          Console.WriteLine("\nИмя подкаталога пустое! (Нажмите клавишу Enter, чтобы продолжить)");
          Console.ReadLine();
          Console.Clear();
          goto DeleteDirectory;
        }
        string adress = path + "/" + subpath;
        if ((System.IO.Directory.Exists(adress)))
        {
          Console.WriteLine("\nВы действительно хотите удалить подкаталог " + subpath + "?");
          Console.WriteLine("Выберите 1 для ДА или 0 для НЕТ\n");
          answer = Console.ReadLine();
          if (answer == "1")
          {
            System.IO.Directory.Delete(adress, true);
            Console.Clear();
            Console.WriteLine("\nКаталог " + adress + " удален! (Нажмите клавишу Enter, чтобы продолжить)");
            Console.ReadLine();
          }

        }
        else
        {
          Console.WriteLine("\nДанного каталог не существует! (Нажмите клавишу Enter, чтобы продолжить)\n");
          Console.ReadLine();
          Console.Clear();
          goto DeleteDirectory;
        }
      End:;
      }
      void GoToDirectory()
      {
        string adress;
        string path = dirName;
        string subpath;
      GoToDirectory:
        if (System.IO.Directory.Exists(dirName))
        {
          Console.WriteLine("Подкаталоги:");
          string[] dirs = System.IO.Directory.GetDirectories(dirName);
          foreach (string s in dirs)
          {
            Console.WriteLine(s);
          }
        }
        Console.WriteLine("\nВыберите подкаталог, в который хотите переместиться (Введите название подкаталога)\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        subpath = Console.ReadLine();
        if (subpath == ".") goto End;
        adress = path + "/" + subpath;
        if (System.IO.Directory.Exists(adress))
        {
          dirName = adress;
          Console.Clear();
          Console.WriteLine("Вы находитесь в каталоге по адресу *" + adress + "*!\nНажмите клавишу Enter для продолжения...");
          Console.ReadLine();
        }
        else
        {
          Console.WriteLine("\nДанного подкаталога не существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto GoToDirectory;
        }
      End:;
      }
      void GoToParentDirectory()
      {
        DirectoryInfo dirInfo = new DirectoryInfo(dirName);
        string adress = $"{dirInfo.Parent}";
        dirName = adress;
        Console.WriteLine("Вы находитесь в каталоге по адресу *" + dirName + "*!\nНажмите клавишу Enter для продолжения...");
        Console.ReadLine();

      }
      void MoveDirectory()
      {
      begin:
        Console.WriteLine("В какой новый каталог (пропишите путь) переместить файлы выбранного каталога?\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        string newPath = Console.ReadLine();
        if (newPath != ".")
        {
          DirectoryInfo dirInfo = new DirectoryInfo(dirName);
          if (!Directory.Exists(newPath))
          {
            dirInfo.MoveTo(newPath);
            Console.Clear();
            Console.WriteLine("Файлы перемещены!");
            Console.ReadLine();
          }
          else
          {
            Console.WriteLine("Данный каталог существует, пропишите путь к несуществующему каталогу");
            goto begin;
          }
        }
        newPath = "";
      }
      void GetFileCount()
      {
        filecount = 0;
        string[] files = System.IO.Directory.GetFiles(dirName);
        foreach (string s in files)
        {
          filecount = filecount + 1;
        }
      }

      // Работа с файлами каталога
      void FileStaff()
      {
        string fileNumber = "";
      FileStaff:
        if ((fileNumber == "0"))
        {
          fileNumber = "";
          goto FileEnd;
        }
        fileNumber = "";
        Console.Clear();
        Console.WriteLine(">> Выберите действие с файлами каталога *" + dirName + "*:\n");
        Console.WriteLine("\n 1. Получить информацию о файле из выбранного каталога");
        Console.WriteLine("\n 2. Создать файл в выбранном каталоге");
        Console.WriteLine("\n 3. Удалить файл в выбранном каталоге");
        Console.WriteLine("\n 4. Переместить файл данного каталога в другой");
        Console.WriteLine("\n 5. Скопировать файл из выбранного каталога в другой");
        Console.WriteLine("\n 6. Отредактировать файл");
        Console.WriteLine("\n 7. Создать архив");
        Console.WriteLine("\n 8. Создать XML файл");
        Console.WriteLine("\n 0. Назад в предыдущее меню");
        Console.WriteLine("\n");
        Console.WriteLine("\n >>");
        fileNumber = (Console.ReadLine());
        Console.Clear();
        switch (fileNumber)
        {
          case "1":

            GetFileInfo();
            break;
          case "2":
            CreateFile();
            break;
          case "3":
            DeleteFile();
            break;
          case "4":
            MoveFile();
            break;
          case "5":
            CopyFile();
            break;
          case "6":
            EditFileStaff();
            break;
          case "7":
            CreateArchive();
            break;
          case "8":
            CreateXml();
            break;
          case "0":
            break;
        }
        goto FileStaff;
      FileEnd:;
      }
      void GetFileInfo()
      {
        GetFileCount();
        if (filecount == 0)
        {
          Console.WriteLine("В данном каталоге нет файлов! (Нажмите клавишу Enter для продолжения...)");
          Console.ReadLine();
          Console.Clear();
          goto End;
        }
      GetFileInfo:
        Console.WriteLine("Файлы выбранного каталога для получения информации:\n");
        string[] files = System.IO.Directory.GetFiles(dirName);
        foreach (string s in files)
        {
          Console.WriteLine(s);
        }
        Console.WriteLine("\nВведите название выбранного файла\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        fileName = Console.ReadLine();
        if (fileName == ".") goto End;
        Console.Clear();
        string fileDirName = dirName + "/" + fileName;
        FileInfo fileInf = new FileInfo(fileDirName);
        if (fileInf.Exists)
        {
          Console.WriteLine("Имя файла: {0}", fileInf.Name);
          Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
          Console.WriteLine("Размер: {0}", fileInf.Length);
          Console.WriteLine("\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
        }
        else
        {
          Console.WriteLine("Файла с данным названием не существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto GetFileInfo;
        }
      End:;
      }
      void CreateFile()
      {
      CreateFile:
        Console.WriteLine("Введите название нового файла\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        fileName = Console.ReadLine();
        if (fileName == ".") goto End;
        Console.Clear();
        string fileDirName = dirName + "/" + fileName;
        FileInfo fileInf = new FileInfo(fileDirName);
        if (!fileInf.Exists)
        {
          fileInf.Create();
          Console.WriteLine("\nФайл " + fileDirName + " создан! (Нажмите клавишу Enter для продолжения...)");
          Console.ReadLine();
        }
        else
        {
          Console.WriteLine("Файл с данным названием уже существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto CreateFile;
        }
      End:;
      }
      void DeleteFile()
      {
        GetFileCount();
        string answer;
        if (filecount == 0)
        {
          Console.WriteLine("В данном каталоге нет файлов! (Нажмите клавишу Enter для продолжения...)");
          Console.ReadLine();
          Console.Clear();
          goto End;
        }
      DeleteFile:
        Console.WriteLine("Файлы выбранного каталога для удаления:\n");
        string[] files = System.IO.Directory.GetFiles(dirName);
        foreach (string s in files)
        {
          Console.WriteLine(s);
        }
        Console.WriteLine("Введите название файла для УДАЛЕНИЯ\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        fileName = Console.ReadLine();
        if (fileName == ".") goto End;
        Console.Clear();
        string fileDirName = dirName + "/" + fileName;
        FileInfo fileInf = new FileInfo(fileDirName);
        if (fileInf.Exists)
        {
          Console.WriteLine("\nВы действительно хотите удалить файл " + fileDirName + "?");
          Console.WriteLine("Выберите 1 для ДА или 0 для НЕТ\n");
          answer = Console.ReadLine();
          if (answer == "1")
            fileInf.Delete();
          Console.WriteLine("\nФайл удален! (Нажмите клавишу Enter, чтобы продолжить)");
          Console.ReadLine();
        }
        else
        {
          Console.WriteLine("Файла с данным названием не существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto DeleteFile;
        }
      End:;
      }
      void MoveFile()
      {
        string newDirName = "";
        GetFileCount();
        if (filecount == 0)
        {
          Console.WriteLine("В данном каталоге нет файлов! (Нажмите клавишу Enter для продолжения...)");
          Console.ReadLine();
          Console.Clear();
          goto End;
        }
      MoveFile:
        Console.WriteLine("Файлы выбранного каталога для перемещения:\n");
        string[] files = System.IO.Directory.GetFiles(dirName);
        foreach (string s in files)
        {
          Console.WriteLine(s);
        }
        Console.WriteLine("Введите название файла для перемещения\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        fileName = Console.ReadLine();
        if (fileName == ".") goto End;
        string fileDirName = dirName + "/" + fileName;
        FileInfo fileInf = new FileInfo(fileDirName);
        if (fileInf.Exists)
        {
          Console.WriteLine("\nВведите адрес нового каталога для перемещения выбранного файла\n");
          Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
          newDirName = Console.ReadLine();
          string NewFileDirName = newDirName + "/" + fileName;
          if (newDirName == ".") goto End;
          if (!File.Exists(NewFileDirName))
          {
            Console.Clear();
            fileInf.MoveTo(NewFileDirName);
            Console.WriteLine("Файл " + fileName + " успешно перемещен в каталог " + newDirName + " Нажмите клавишу Enter для продолжения...");
            Console.ReadLine();
          }
          else
          {
            Console.WriteLine("Файла с данным названием уже присутствует в папке для перемещения!\n Нажмите клавишу Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
            goto MoveFile;
          }
        }
        else
        {
          Console.WriteLine("Файла с данным названием не существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto MoveFile;
        }
      End:;
      }
      void CopyFile()
      {
        string newDirName = "";
        GetFileCount();
        if (filecount == 0)
        {
          Console.WriteLine("В данном каталоге нет файлов! (Нажмите клавишу Enter для продолжения...)");
          Console.ReadLine();
          Console.Clear();
          goto End;
        }
      CopyFile:
        Console.WriteLine("Файлы выбранного каталога для копирования:\n");
        string[] files = System.IO.Directory.GetFiles(dirName);
        foreach (string s in files)
        {
          Console.WriteLine(s);
        }
        Console.WriteLine("Введите название файла для копирования\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        fileName = Console.ReadLine();
        if (fileName == ".") goto End;
        string fileDirName = dirName + "/" + fileName;
        FileInfo fileInf = new FileInfo(fileDirName);
        if (fileInf.Exists)
        {
          Console.WriteLine("\nВведите адрес нового каталога для копирования выбранного файла\n");
          Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
          newDirName = Console.ReadLine();
          string NewFileDirName = newDirName + "/" + fileName;
          if (newDirName == ".") goto End;
          if (!File.Exists(NewFileDirName))
          {
            Console.Clear();
            fileInf.CopyTo(NewFileDirName, true);
            Console.WriteLine("Файл " + fileName + " успешно копирован в каталог " + newDirName + " Нажмите клавишу Enter для продолжения...");
            Console.ReadLine();
          }
          else
          {
            Console.WriteLine("Файла с данным названием уже присутствует в папке для копирования!\n Нажмите клавишу Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
            goto CopyFile;
          }
        }
        else
        {
          Console.WriteLine("Файла с данным названием не существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto CopyFile;
        }
      End:;
      }
      void CreateArchive()
      {
      begin:
        Console.WriteLine("Введите название для нового архива (без расширения):\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        string ArchiveName = Console.ReadLine();
        string ArchiveDir = dirName + "/" + ArchiveName;
        string ArchivePath = dirName + "/" + ArchiveName + ".zip";
        FileInfo fileInf = new FileInfo(ArchivePath);
        if (ArchiveName != ".")
          if (!fileInf.Exists)
          {
            {
              if (!System.IO.Directory.Exists(ArchiveDir))
              {
                System.IO.Directory.CreateDirectory(ArchiveDir);
                ZipFile.CreateFromDirectory(ArchiveDir, ArchivePath);
                System.IO.Directory.Delete(ArchiveDir, true);
                Console.WriteLine("Архив *" + ArchiveName + "* создан!");
                Console.ReadLine();
              }
            }
          }
          else
          {
            Console.WriteLine("Архив с таким названием существует!");
            Console.ReadLine();
            Console.Clear();
            goto begin;
          }
      }

      // Редактирование файлов
      void EditFileStaff()
      {
        GetFileCount();
        string Extension;
        if (filecount == 0)
        {
          Console.WriteLine("В данном каталоге нет файлов! (Нажмите клавишу Enter для продолжения...)");
          Console.ReadLine();
          Console.Clear();
          goto End;
        }
      EditFile:
        Console.WriteLine("Файлы выбранного каталога для редактирования:");
        string[] files = System.IO.Directory.GetFiles(dirName);
        foreach (string s in files)
        {
          Console.WriteLine(s);
        }
        Console.WriteLine("Введите название файла для редактирования\n");
        Console.WriteLine("(Введите точку, чтобы вернуться назад)\n>>");
        fileName = Console.ReadLine();
        if (fileName == ".") goto End;
        Console.Clear();
        string fileDirName = dirName + "/" + fileName;
        FileInfo fileInf = new FileInfo(fileDirName);
        if (fileInf.Exists)
        {
          fileName = fileDirName;
          Extension = fileInf.Extension;
          if (Extension == ".txt") EditTxt();
          if (Extension == ".json") EditJson();
          if (Extension == ".xml") EditXml();
          if (Extension == ".zip") EditZip();
        }
        else
        {
          Console.WriteLine("Файла с данным названием не существует, введите корректное название!\n Нажмите клавишу Enter для продолжения...");
          Console.ReadLine();
          Console.Clear();
          goto EditFile;
        }
      End:;
      }
      void EditTxt()
      {
        string TxtNumber = "";
      begin:
        if (TxtNumber == "0") goto End;
        TxtNumber = "";
        Console.Clear();
        Console.WriteLine(">> Выберите действие с файлом по адресу " + fileName + ":\n");
        Console.WriteLine("\n 1. Прочитать файл");
        Console.WriteLine("\n 2. Записать в файл строку, введённую пользователем");
        Console.WriteLine("\n 0. Назад");
        Console.WriteLine("\n(Введите точку, чтобы вернуться назад)\n>>");
        TxtNumber = Console.ReadLine();
        if (TxtNumber == ".") goto End;
        Console.Clear();
        switch (TxtNumber)
        {
          case "1":
            using (FileStream fstream = File.OpenRead(fileName))
            {
              // преобразуем строку в байты
              byte[] array = new byte[fstream.Length];
              // считываем данные
              fstream.Read(array, 0, array.Length);
              // декодируем байты в строку
              string textFromFile = System.Text.Encoding.Default.GetString(array);
              Console.WriteLine($"Текст из файла: {textFromFile}");
              Console.ReadLine();
            }
            break;
          case "2":
            Console.Clear();
            Console.WriteLine("Введите строку для записи в файл:\n>>");
            string text = Console.ReadLine();

            // запись в файл
            using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
              // преобразуем строку в байты
              byte[] array = System.Text.Encoding.Default.GetBytes(text);
              // запись массива байтов в файл
              fstream.Write(array, 0, array.Length);
              Console.WriteLine("Текст записан в файл");
            }
            break;
          case "0":
            break;
        }
        goto begin;
      End:;
        TxtNumber = "";
      }
      void EditJson()
      {
        string JsonNumber = "";
      begin:
        if (JsonNumber == "0") goto End;
        JsonNumber = "";
        Console.Clear();
        Console.WriteLine(">> Выберите действие с файлом по адресу " + fileName + ":\n");
        Console.WriteLine("\n 1. Прочитать файл");
        Console.WriteLine("\n 2. Записать в файл строку, введённую пользователем");
        Console.WriteLine("\n 3. Выполнить сериализацию объекта в формате JSON и записать в файл.");
        Console.WriteLine("\n 4. Выполнить десериализацию объекта.");
        Console.WriteLine("\n 0. Назад");
        Console.WriteLine("\n(Введите точку, чтобы вернуться назад)\n>>");
        JsonNumber = Console.ReadLine();
        if (JsonNumber == ".") goto End;
        Console.Clear();
        switch (JsonNumber)
        {
          case "1":
            using (FileStream fstream = File.OpenRead(fileName))
            {
              // преобразуем строку в байты
              byte[] array = new byte[fstream.Length];
              // считываем данные
              fstream.Read(array, 0, array.Length);
              // декодируем байты в строку
              string textFromFile = System.Text.Encoding.Default.GetString(array);
              Console.WriteLine($"Текст из файла: {textFromFile}");
              Console.ReadLine();
            }
            break;
          case "2":
            Console.Clear();
            Console.WriteLine("Введите строку для записи в файл:\n>>");
            string text = Console.ReadLine();
            using (FileStream fstream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
              // преобразуем строку в байты
              byte[] array = System.Text.Encoding.Default.GetBytes(text);
              // запись массива байтов в файл
              fstream.Write(array, 0, array.Length);
              Console.WriteLine("Текст записан в файл");
            }
            break;
          case "3":
            Serialization();
            break;
          case "4":
            Deserialization();
            break;
          case "0":
            break;
        }
        goto begin;
      End:;
      }
      void EditZip()
      {
        string fileDirName = dirName + "/" + fileName;
        string zipNumber = "";
        string targetFolder = dirName + "/newUnexpected" + fileName;
      begin:
        if (zipNumber == "0") goto End;
        Console.Clear();
        Console.WriteLine(">> Выберите действие с файлом по адресу " + fileName + ":\n");
        Console.WriteLine("\n 1. Показать список файлов в архиве");
        Console.WriteLine("\n 2. Переместить файл в данный архив");
        Console.WriteLine("\n 3. Копировать файл в данный архив");
        Console.WriteLine("\n 4. Удалить файл из данного архива");
        Console.WriteLine("\n 5. Разархивировать файлы");
        Console.WriteLine("\n 6. Удалить архив со всеми файлами");
        Console.WriteLine("\n 0. Назад");
        Console.WriteLine("\n(Введите точку, чтобы вернуться назад)\n>>");
        zipNumber = Console.ReadLine();
        if (zipNumber == ".") goto End;
        Console.Clear();
        switch (zipNumber)
        {
          case "1":
            Console.WriteLine("Список файлов в данном архиве\n");
            using (ZipArchive zipArchive = ZipFile.OpenRead(fileName))
            {
              foreach (ZipArchiveEntry entry in zipArchive.Entries)
              {
                Console.WriteLine(entry.FullName);
              }
              Console.ReadLine();
            }
            break;
          case "2":
            CopyFileToZip();
            FileInfo fileInf = new FileInfo(dirMovedFileToZip);
            if (dirMovedFileToZip != ".") fileInf.Delete();
            break;
          case "3":
            CopyFileToZip();
            break;
          case "4":
            DeleteFileFromArchieve();
            break;
          case "5":
            DeArchieve();
            break;
          case "0":
            break;
        }
        goto begin;
      End:;
      }
      void EditXml()
      {
        string XmlNumber = "";
      begin:
        if ((XmlNumber == "0"))
        {
          XmlNumber = "";
          goto End;
        }
        XmlNumber = "";
        Console.Clear();
        Console.WriteLine(">> Выберите действие с файлами каталога *" + dirName + "*:\n");
        Console.WriteLine("\n 1. Прочитать файл");
        Console.WriteLine("\n 2. Записать в файл новые данные");
        Console.WriteLine("\n 0. Назад в предыдущее меню");
        Console.WriteLine("\n");
        Console.WriteLine("\n >>");
        XmlNumber = (Console.ReadLine());
        Console.Clear();
        switch (XmlNumber)
        {
          case "1":
            ReadXmlFile();
            break;
          case "2":
            EditXmlFile();
            break;
          case "0":
            break;
        }
        goto begin;
      End:;
      }
      void Serialization()
      {
        // объект для сериализации
        Person person = new Person("Tom", 29);
        Console.WriteLine("Объект создан");

        // создаем объект BinaryFormatter
        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
          formatter.Serialize(fs, person);
          Console.WriteLine("Объект сериализован");
        }
        Console.ReadLine();
      }
      void Deserialization()
      {
        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        // десериализация из файла people.dat
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
          Person newPerson = (Person)formatter.Deserialize(fs);

          Console.WriteLine("Объект десериализован");
          Console.WriteLine($"Имя: {newPerson.Name} --- Возраст: {newPerson.Age}");
        }
        Console.ReadLine();
      }

      // Работа с архивом
      void CopyFileToZip()
      {
      beginAdd:
        Console.WriteLine("Введите путь к каталогу файла, который надо добавить");
        Console.WriteLine("\n(Введите точку, чтобы вернуться назад)\n>>");
        string DirOfFileToAdd = Console.ReadLine();
        if (DirOfFileToAdd != ".")
        {
          if (System.IO.Directory.Exists(DirOfFileToAdd))
          {
            Console.WriteLine("\nВведите название файла для добавления\n");
            Console.WriteLine("Файлы:\n");
            string[] files = System.IO.Directory.GetFiles(dirName);
            foreach (string s in files)
            {
              Console.WriteLine(s);
            }
            Console.WriteLine("\n>>");
            string nameFileToAdd = Console.ReadLine();
            string FileDirToAdd = DirOfFileToAdd + "/" + nameFileToAdd;
            FileInfo fileInf = new FileInfo(FileDirToAdd);
            if (fileInf.Exists)
              using (ZipArchive zipArchive = ZipFile.Open(fileName, ZipArchiveMode.Update))
              {
                // вызов метода для добавления файла в архив
                zipArchive.CreateEntryFromFile(FileDirToAdd, nameFileToAdd);
                Console.WriteLine(FileDirToAdd);
                Console.ReadLine();
                dirMovedFileToZip = FileDirToAdd;
              }
            else
            {
              Console.WriteLine("Данного файла не существует!");
              Console.ReadLine();
              Console.Clear();
              goto beginAdd;
            }
          }
          else
          {
            Console.WriteLine("Каталога не существует!");
            Console.ReadLine();
            Console.Clear();
            goto beginAdd;
          }
        }
        dirMovedFileToZip = ".";
      }
      void DeArchieve()
      {
      begin:
        Console.WriteLine("Введите путь к каталогу, в который требуется разархивировать архив:");
        Console.WriteLine("\n(Введите точку, чтобы вернуться назад)\n>>");
        string directoryPath = Console.ReadLine();
        if (directoryPath != ".")
        {
          if (System.IO.Directory.Exists(dirName))
          {
            ZipFile.ExtractToDirectory(fileName, directoryPath);
            Console.Clear();
            Console.WriteLine("Архив разархивирован.");
            Console.ReadLine();
          }
          else
          {
            Console.WriteLine("Каталога не существует");
            Console.ReadLine();
            goto begin;
          }
        }
      }
      void DeleteFileFromArchieve()
      {
        string answer;
        using (ZipArchive zipArchive = ZipFile.Open(fileName, ZipArchiveMode.Update))
        {
          Console.WriteLine("Введите название выбранного файла из архива для удаления:\n");
          Console.WriteLine("Файлы из архива:");
          foreach (ZipArchiveEntry entry in zipArchive.Entries)
          {
            Console.WriteLine(entry.FullName);
          }
          Console.WriteLine("\n(Введите точку, чтобы вернуться назад)\n>>");
          string nameDeleteFile = Console.ReadLine();
          Console.WriteLine("\nВы действительно хотите удалить файл " + nameDeleteFile + " из архива?");
          Console.WriteLine("Выберите 1 для ДА или 0 для НЕТ\n");
          answer = Console.ReadLine();
          if (answer == "1")
            if (nameDeleteFile != ".") zipArchive.Entries.FirstOrDefault(x => x.Name == nameDeleteFile)?.Delete();
        }
      }
      void CreateXml()
      {
        string XmlName;
        Console.WriteLine("Введите имя для нового Xml файла");
        XmlName = Console.ReadLine();
        XDocument xdoc = new XDocument();
        // создаем корневой элемент
        XElement MainElement = new XElement("Elements");
        // создаем первый элемент
        XElement Element = new XElement("Example");
        // создаем атрибут
        XAttribute NameAttr = new XAttribute("Name", "ExampleName");
        XElement CompanyElem = new XElement("Company", "ExampleCompany");
        XElement PriceElem = new XElement("Price", "ExamplePrice");
        // добавляем атрибут и элементы в первый элемент
        Element.Add(NameAttr);
        Element.Add(CompanyElem);
        Element.Add(PriceElem);

        // добавляем в корневой элемент
        MainElement.Add(Element);
        // добавляем корневой элемент в документ
        xdoc.Add(MainElement);
        //сохраняем документ
        string XmlDirName = dirName + "/" + XmlName + ".xml";
        xdoc.Save(XmlDirName);
        Console.Clear();
        Console.WriteLine("Файл " + XmlDirName + " создан!");
        Console.ReadLine();
      }
      
      void EditXmlFile()
      {
        XDocument xdoc = XDocument.Load(fileName);
        XElement root = xdoc.Element("Elements");
        Console.WriteLine("Введите название элемента:\n>>");
        string Element = Console.ReadLine();
        Console.WriteLine("Введите название атрибута 1:\n>>");
        string Attribute1 = Console.ReadLine();
        Console.WriteLine("Введите значение атрибута 1:\n>>");
        string AttributeValue1 = Console.ReadLine();
        Console.WriteLine("Введите название атрибута 2:\n>>");
        string Attribute2 = Console.ReadLine();
        Console.WriteLine("Введите значение элемента:\n>>");
        string AttributeValue2 = Console.ReadLine();
        Console.WriteLine("Введите название атрибута 3:\n>>");
        string Attribute3 = Console.ReadLine();
        Console.WriteLine("Введите значение атрибута 3:\n>>");
        string AttributeValue3 = Console.ReadLine();
        root.Add(new XElement(Element,
                    new XAttribute(Attribute1, AttributeValue2),
                    new XElement(Attribute2, AttributeValue2),
                    new XElement(Attribute3, AttributeValue2)));
        xdoc.Save(fileName);
        // выводим xml-документ на консоль
        Console.Clear();
        Console.WriteLine(xdoc);

        Console.Read();
      }
      void ReadXmlFile()
      {
        XDocument xdoc = XDocument.Load(fileName);
        Console.WriteLine(xdoc);
        Console.ReadLine();

      }
    }
  }
}
