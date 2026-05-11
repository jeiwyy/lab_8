internal class MenuItem
{
    private string _name;
    private string _description;
    private int _weight;
    private double _price;
    private bool _available;

    public MenuItem()
    {
        Name = "Сладовар";
        Description = "Светлое нефильрованное 4°";
        Weight = 500;
        Price = 249.9;
        Available = true;
    }

    public MenuItem(string _name, string _description, int _weight,
        double _price, bool _available)
    {
        Name = _name;
        Description = _description;
        Weight = _weight;
        Price = _price;
        Available = _available;
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    public bool Available
    {
        get
        {
            return _available;
        }
        set
        {
            _available = value;
        }
    }

    public int Weight
    {
        get
        {
            return _weight;
        }
        set
        {
            if (value < 0)
            {
                bool isCorrect = false;
                string input = "";
                while (!isCorrect)
                {
                    Console.Write("Введите вес блюда: ");
                    input = Console.ReadLine();
                    isCorrect = int.TryParse(input, out value);
                    if (!isCorrect || value <= 0)
                    {
                        Console.WriteLine("Ошибка ввода!");
                        isCorrect = false;
                    }
                }
            }
            _weight = value;
        }
    }

    public double Price
    {
        get
        {
            return _price;
        }
        set
        {
            if (value < 0)
            {
                bool isCorrect = false;
                string input = "";
                while (!isCorrect)
                {
                    Console.Write("Введите цену блюда: ");
                    input = Console.ReadLine();
                    isCorrect = double.TryParse(input, out value);
                    if (!isCorrect || value <= 0)
                    {
                        Console.WriteLine("Ошибка ввода!");
                        isCorrect = false;
                    }
                }
            }
            _price = value;
        }
    }

    public override string ToString()
    {
        string s = "";
        if (Available)
        {
            s = "Да";
        }
        else
        {
            s += "Нет";
        }
        string weightStr = $"{Weight} гр.";
        string priceStr = $"{Price}₽";
        return $"{Name,-25} | {weightStr,-10} | {priceStr,-10}" +
            $" | {s,-7} | {Description}";
    }

}