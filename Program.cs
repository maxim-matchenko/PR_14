//****************************************************************************
//* Практическая работа N14                                                  *
//* Выполнил: Матченко M.C., группа 2-ИСП-оКФ                                *
//* Вариант 3                                                                *
//* Задание: Разработка программ с использованием структур                   *
//****************************************************************************
using System;
namespace PR_14
{
    struct DealerCenter// Структура «Дилерский центр» 
    {
        // Скрытие внутренней реализации структур
        private string model;
        private string brand;
        private int year;
        private string equipment;
        private decimal price;
        //Свойства для доступа на чтение
        public string Model => model;
        public string Brand => brand;
        public int Year => year;
        public string Equipment { get => equipment; set => equipment = value; }
        public decimal Price { get => price; set => price = value; }
        //Конструктор структуры       
        public DealerCenter(string model, string brand, int year, string equipment, decimal price)
        {
            this.model = model;
            this.brand = brand;
            this.year = year;
            this.equipment = equipment;
            this.price = price;
        }
        //Метод ввода данных членов структуры
        public static DealerCenter InfoAboutObjStruct()
        {
            string brand = "";
            string model = "";
            int year = 0;
            string equipment = "";

            while (string.IsNullOrWhiteSpace(brand))//Ввод бренда с защитой от пустоты
            {
                Console.Write("Введите фирму (производителя): ");
                brand = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(brand))
                {
                    Program.HandleError("Поле не может быть пустым. Попробуйте снова.");
                }
            }
            while (string.IsNullOrWhiteSpace(model))//Ввод модели
            {
                Console.Write("Введите модель автомобиля: ");
                model = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(model))
                {
                    Program.HandleError("Поле не может быть пустым. Попробуйте снова.");
                }
            }
            while (true)//Ввод года с проверкой на корректность числа
            {
                Console.Write("Введите год выпуска  ");
                string yearInput = Console.ReadLine();
                if (int.TryParse(yearInput, out year))
                {
                    if (year > DateTime.Now.Year)
                    {
                        Program.HandleError($"Год {year} выглядит некорректно. Укажите год до–{DateTime.Now.Year}.");
                        continue;
                    }
                    break;
                }

            }
            while (string.IsNullOrWhiteSpace(equipment))// Ввод комплектации
            {
                Console.Write("Введите комплектацию: ");
                equipment = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(equipment))
                {
                    Program.HandleError("Поле не может быть пустым. Попробуйте снова.");
                }
            }
            Console.Write("Введите цену (руб.): ");
            decimal price = decimal.Parse(Console.ReadLine());

            return new DealerCenter(model, brand, year, equipment, price);
        }
        public void PrintInfoObjStruct()// Метод вывода информации об объекте
        {
            Console.WriteLine($"Автомобиль: {brand} {model} | Год: {year} | Комплектация: {equipment} | Цена: {price} руб.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string select;// Переменная для контроля повторного запуска
            do
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Title = "Практическая работа № 14";
                    Console.Clear();
                    Console.WriteLine("Здравствуйте! Вас приветствует справочная система \"Дилерский центр\"");
                    Console.WriteLine("--- Новый расчет ---\n");
                    Console.Write("Введите количество автомобилей для ввода (n): ");
                    int n = int.Parse(Console.ReadLine());                 

                    DealerCenter[] cars = new DealerCenter[n];// Создание массива структур
                    // 1. Ввод данных с клавиатуры
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine($"\n--- Ввод данных для автомобиля №{i + 1} ---");
                        cars[i] = DealerCenter.InfoAboutObjStruct();
                    }
                    // 2. Сортировка и вывод по возрастанию года выпуска
                    SortedObjStruct(cars);
                    Console.WriteLine("\n=== Список автомобилей (отсортирован по возрастанию года выпуска) ===");
                    foreach (var car in cars)
                    {
                        car.PrintInfoObjStruct();
                    }
                    // 3. Вывод информации о конкретной модели
                    Console.Write("\nВведите модель автомобиля для поиска: ");
                    string searchModel = Console.ReadLine();
                    FindObjStruct(cars, searchModel);
                    // 4. Вывод информации о самом дорогом автомобиле
                    FindMostExpensive(cars);
                    // Проверка случайного события для первого автомобиля в списке (если массив не пуст)
                    if (cars.Length > 0)
                    {
                        TriggerRandomEvent(ref cars[0]);
                    }
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                Console.Write("\nХотите выполнить программу еще раз? (да - 1 / нет - любая клавиша): ");// Запрос на повторное выполнение программы
                select = Console.ReadLine();
            } while (select == "1"); 
            Console.WriteLine("\nПрограмма завершена. Нажмите Enter для выхода...");
            Console.ReadLine();
        }
        public static void SortedObjStruct(DealerCenter[] array)// Метод сортировки массива структур по возрастанию года выпуска
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i].Year > array[j].Year)
                    {
                        DealerCenter temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
        public static void FindObjStruct(DealerCenter[] array, string searchModel) // Метод поиска данных в структуре по конкретной модели
        {
            bool flag = false;
            Console.WriteLine($"\nРезультаты поиска модели \"{searchModel}\":");
            foreach (var car in array)
            {
                if (car.Model.Equals(searchModel, StringComparison.OrdinalIgnoreCase))
                {
                    car.PrintInfoObjStruct();
                    flag = true;
                }
            }
            if (!flag)
            {
                Console.WriteLine("Автомобили с такой моделью не найдены.");
            }
        }
        public static void FindMostExpensive(DealerCenter[] array)// Метод поиска и вывода информации о наиболее дорогом автомобиле
        {
            if (array == null || array.Length == 0) return;
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].Price > array[maxIndex].Price)
                {
                    maxIndex = i;
                }
            }
            Console.WriteLine("\n--- Информация о самом дорогом автомобиле ---");
            array[maxIndex].PrintInfoObjStruct();
        }
        public static void TriggerRandomEvent(ref DealerCenter car)// Метод генерации случайного события
        {
            Random rnd = new Random();
            int eventType = rnd.Next(3); // 3 возможных сценария
            decimal changeValue = rnd.Next(50000, 150001); // Изменение цены от 50 до 150 тысяч
            switch (eventType)
            {
                case 0: // Скидка
                    {
                        car.Price -= changeValue;
                        if (car.Price < 0) car.Price = 0;
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"\nВНИМАНИЕ! На автомобиль {car.Brand} {car.Model} объявлена скидка! Цена снижена на {changeValue} руб.");
                        break;
                    }
                case 1: // Повышение цены
                    {
                        car.Price += changeValue;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine($"\nВНИМАНИЕ! Цена на автомобиль {car.Brand} {car.Model} увеличилась на {changeValue} руб.");
                    }
                    break;
                case 2: // Бонус к комплектации
                    car.Equipment = car.Equipment + " (+ Подарочный комплект зимней резины)";
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"\nВНИМАНИЕ! Акция от дилера! В комплектацию автомобиля {car.Brand} {car.Model} бесплатно добавлен зимний пакет.");
                    
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Black; // Возвращаем цвет текста
            Console.Write("Актуальное состояние: \n");
            car.PrintInfoObjStruct();
        }
        static void HandleError(Exception ex)//  функция обработки ошибок
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nОШИБКА: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public static void HandleError(string message)//  функция обработки ошибок
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка {message}");
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
