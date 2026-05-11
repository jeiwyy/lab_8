internal class Database
{
    private string _path;

    public Database()
    {
        Path = "test.bin";
    }
    public Database(string _path)
    {
        Path = _path;
    }

    public string Path
    {
        get
        {
            return _path;
        }
        set
        {
            string resPath = "/home/jeiw/YP/Csharp/lab_8/DB/";
            bool isCorrect = false;
            while (!isCorrect)
            {
                if (value.EndsWith(".bin") && !value.Contains('/'))
                {
                    resPath += value;
                    if (!File.Exists(resPath))
                    {
                        using (BinaryWriter writer = new 
                            BinaryWriter(File.Open(resPath, FileMode.Create)))
                        {
                            writer.Write("ourDB");
                            writer.Write(0);
                        }
                        Console.WriteLine("Файл не существует, создан пустой");
                    }
                    isCorrect = true;              
                }
                else
                {
                    Console.WriteLine("Ошибка! Недопустимое название файла");
                    Console.Write("Введите имя файла: ");
                    value = Console.ReadLine();
                }
            }
            _path = resPath;
        }
    }

    private bool CheckFile(out int count)
    {
        string element = "";
        count = 0;
        bool isCorrect = false;
        try
        {
            using (BinaryReader reader = new
                BinaryReader(File.Open(Path, FileMode.Open)))
            {
                element = reader.ReadString();
                count = reader.ReadInt32();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Не удалось считать файл: " + e);
        }
        if (element == "ourDB" && count >= 0)
        {
            isCorrect = true;

        }
        else
        {
            Console.WriteLine("Данные в файле некорректны.");
        }
        return isCorrect;
    }

    public void AddItem()
    {
        int count = 0;
        List<MenuItem> menu = ReadData();
        MenuItem item = new MenuItem();
        Console.Write("Введите название блюда: ");
        item.Name = Console.ReadLine();
        Console.Write("Введите описание блюда: ");
        item.Description = Console.ReadLine();
        item.Price = -1;
        item.Weight = -1;
        Console.Write("Блюдо в наличии(y/n): ");
        string inp = Console.ReadLine();

        if (!string.IsNullOrEmpty(inp) && inp[0] == 'n')
        {
            item.Available = false;
        }
        if (CheckFile(out count))
        {
            try
            {
                using (BinaryWriter writer = new
                    BinaryWriter(File.Open(Path, FileMode.Create)))
                {
                    writer.Write("ourDB");
                    writer.Write(count + 1);
                    foreach(MenuItem oldItem in menu)
                    {
                        writer.Write(oldItem.Name);
                        writer.Write(oldItem.Description);
                        writer.Write(oldItem.Price);
                        writer.Write(oldItem.Weight);
                        writer.Write(oldItem.Available);
                    }
                    writer.Write(item.Name);
                    writer.Write(item.Description);
                    writer.Write(item.Price);
                    writer.Write(item.Weight);
                    writer.Write(item.Available);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при записи файла: " + e);
            }

        }
    }

    public void AddItem(MenuItem item)
    {
        int count = 0;
        List<MenuItem> menu = ReadData();
        if (CheckFile(out count))
        {
            try
            {
                using (BinaryWriter writer = new
                    BinaryWriter(File.Open(Path, FileMode.Create)))
                {
                    writer.Write("ourDB");
                    writer.Write(count + 1);
                    foreach(MenuItem oldItem in menu)
                    {
                        writer.Write(oldItem.Name);
                        writer.Write(oldItem.Description);
                        writer.Write(oldItem.Price);
                        writer.Write(oldItem.Weight);
                        writer.Write(oldItem.Available);
                    }
                    writer.Write(item.Name);
                    writer.Write(item.Description);
                    writer.Write(item.Price);
                    writer.Write(item.Weight);
                    writer.Write(item.Available);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при записи файла: " + e);
            }

        }
    }

    public void DeleteItem()
    {
        List<MenuItem> menu = ReadData();
        Console.Write("Введите название блюда: ");
        string input = Console.ReadLine();
        int removeItem = menu.RemoveAll(item => item.Name.Equals(input));
        if (removeItem == 0)
        {
            Console.WriteLine("Элемент с таким названием не найден");
        }
        else
        {
            using (BinaryWriter writer = new
                    BinaryWriter(File.Open(Path, FileMode.Create)))
                {
                    writer.Write("ourDB");
                    writer.Write(menu.Count);
                    foreach(MenuItem item in menu)
                    {
                        writer.Write(item.Name);
                        writer.Write(item.Description);
                        writer.Write(item.Price);
                        writer.Write(item.Weight);
                        writer.Write(item.Available);
                    }
                }
        }
    }

    public List<MenuItem> ReadData()
    {
        int count = 0;
        List<MenuItem> menu = new List<MenuItem>();
        if (CheckFile(out count))
        {
            if (count != 0)
            {
                try
                {
                    using (BinaryReader reader = new
                        BinaryReader(File.Open(Path, FileMode.Open)))
                    {
                        reader.ReadString();
                        reader.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            MenuItem item = new MenuItem();
                            item.Name = reader.ReadString();
                            item.Description = reader.ReadString();
                            item.Price = reader.ReadDouble();
                            item.Weight = reader.ReadInt32();
                            item.Available = reader.ReadBoolean();
                            menu.Add(item);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка при чтении файла: " + e);
                }
                
            }
        }
        return menu;   
    }

    public string MostExpensive()
    {
        List<MenuItem> menu = ReadData();
        string maxValue = (from item in menu
                        orderby item.Price descending
                        select item).FirstOrDefault().Name;
        return maxValue;
    }

    public double AveragePrice()
    {
        List<MenuItem> menu = ReadData();
        double avgPrice = (from item in menu
                        select item.Price).Average();
        return avgPrice;
    }

    public List<string> AvailableDishes()
    {
        List<MenuItem> menu = ReadData();
        IEnumerable<string> dishes = from item in menu
                                    where (item.Available)
                                    select item.Name;
        return dishes.ToList();
    }

    public List<MenuItem> SortAll()
    {
        List<MenuItem> menu = ReadData();
        IEnumerable<MenuItem> sortedMenu =
            from item in menu
            orderby item.Name.ToLower() ascending
            select item;
        return sortedMenu.ToList();
    }

    public void SortMenu()
    {
        List<MenuItem> sortedMenu = SortAll();
        Console.Write("Введите название для отсортированной БД: ");
        Database newDb = new Database(Console.ReadLine());
        foreach (MenuItem item in sortedMenu)
        {
            newDb.AddItem(item);
        }
        Console.WriteLine("Отсортированное меню: ");
        Console.WriteLine(newDb);
    }

    public override string ToString()
    {
        List<MenuItem> menu = ReadData();
        string header = $"{"Название",-25} | {"Масса",-10} | {"Цена",-10}"
            + $" | {"Наличие",-7} | {"Описание"}";
        string itemsStr = "";
        foreach (MenuItem item in menu)
        {
            itemsStr += item + "\n";
        }
        return $"\n{header}\n{itemsStr}\n";
    }
}