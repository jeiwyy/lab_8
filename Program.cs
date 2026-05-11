using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        int option = 1;
        bool isCorrect = false;
        string input = "";
        string inp = "";
        Database DbFile = new Database();
        List<string> dishes = new List<string>();
        while (option != 0)
        {
            while (!isCorrect)
            {
                Console.WriteLine("Текущий файл: " + DbFile.Path[30..]);
                Console.WriteLine("1 - Сменить файл");
                Console.WriteLine("2 - Показать меню");
                Console.WriteLine("3 - Удалить позицию из меню");
                Console.WriteLine("4 - Добавить в меню позицию");
                Console.WriteLine("5 - Показать название самого дорого блюда");
                Console.WriteLine("6 - Показать среднюю цену блюд");
                Console.WriteLine("7 - Показать блюда в наличии");
                Console.WriteLine("8 - Отсортировать меню по алфавиту");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите опцию: ");

                input = Console.ReadLine();
                isCorrect = int.TryParse(input, out option);
                if (!isCorrect || option < 0 || option > 8)
                {
                    Console.WriteLine("Ошибка ввода!");
                    isCorrect = false;
                }
            }
            isCorrect = false;

            if (option == 1)
            {
                Console.Write("Введите название новой базы данных: ");
                inp = Console.ReadLine();
                DbFile = new Database(inp);
            }
            if (option == 2)
            {
                Console.WriteLine(DbFile);
            }
            if (option == 3)
            {
                DbFile.DeleteItem();
                Console.WriteLine("Меню после удаления:");
                Console.WriteLine(DbFile);
            }
            if (option == 4)
            {
                DbFile.AddItem();
                Console.WriteLine("Меню после добавления:");
                Console.WriteLine(DbFile);
            }
            if (option == 5)
            {
                Console.WriteLine("Самое дорогое блюдо: " + 
                    DbFile.MostExpensive());
            }
            if (option == 6)
            {
                Console.WriteLine("Средняя цена блюда: " + 
                    DbFile.AveragePrice());
            }
            if (option == 7)
            {
                dishes = DbFile.AvailableDishes();
                Console.WriteLine("Доступные блюда:");
                foreach(string item in dishes)
                {
                    Console.WriteLine(item);
                }
            }
            if (option == 8)
            {
                DbFile.SortMenu();
            }
        }
    }
}