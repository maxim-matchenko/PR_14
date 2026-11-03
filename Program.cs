using System;

namespace DealerCenterApp
{
    // Структура «Дилерский центр» 
    struct DealerCenter
    {
        // скрытие внутренней реализации структуры
        private string model;
        private string brand;
        private int year;
        private string equipment;
        private decimal price;

        //  свойства для доступа на чтение (перезапись)
        public string Model => model;
        public string Brand => brand;
        public int Year => year;
        public string Equipment { get => equipment; set => equipment = value; }
        public decimal Price { get => price; set => price = value; }

        // Конструктор структуры
        public DealerCenter(string model, string brand, int year, string equipment, decimal price)
        {
            this.model = model;
            this.brand = brand;
            this.year = year;
            this.equipment = equipment;
            this.price = price;
        }
        // Метод ввода данных членов структуры
        public static DealerCenter InfoAboutObjStruct()
        {
            Console.Write("Введите фирму (производителя): ");
            string brand = Console.ReadLine();
            Console.Write("Введите модель автомобиля: ");
            string model = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Введите комплектацию: ");
            string equipment = Console.ReadLine();
            Console.Write("Введите цену (руб.): ");
            decimal price = decimal.Parse(Console.ReadLine());
            return new DealerCenter(model, brand, year, equipment, price);
        }
        // Метод вывода информации об объекте
        public void PrintInfoObjStruct()
        {
            Console.WriteLine($"Автомобиль: {brand} {model} | Год: {year} | Комплектация: {equipment} | Цена: {price:N2} руб.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Переменная для контроля повторного запуска
            string select;
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
                    // Создание массива структур
                    DealerCenter[] cars = new DealerCenter[n];
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
                    
                    if (cars.Length > 0)// Проверка случайного события для первого автомобиля в списке (если массив не пуст)
                    {
                        TriggerRandomEvent(ref cars[0]);
                    }
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
                Console.Write("\nХотите выполнить программу еще раз? (да - 1 / нет - любая клавиша): ");
                select = Console.ReadLine();
            } while (select == "1"); 
            Console.WriteLine("\nПрограмма завершена. Нажмите Enter для выхода...");
            Console.ReadLine();
        }
        // Метод сортировки массива структур по возрастанию года выпуска 
        public static void SortedObjStruct(DealerCenter[] array)
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
        // Метод поиска данных в структуре по конкретной модели
        public static void FindObjStruct(DealerCenter[] array, string searchModel)
        {
            bool found = false;
            Console.WriteLine($"\nРезультаты поиска модели \"{searchModel}\":");
            foreach (var car in array)
            {
                if (car.Model.Equals(searchModel, StringComparison.OrdinalIgnoreCase))
                {
                    car.PrintInfoObjStruct();
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Автомобили с такой моделью не найдены.");
            }
        }
        // Метод поиска и вывода информации о наиболее дорогом автомобиле
        public static void FindMostExpensive(DealerCenter[] array)
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
        // Метод генерации случайного события, влияющего на цену или комплектацию
        public static void TriggerRandomEvent(ref DealerCenter car)
        {
            Random rnd = new Random();
            int eventType = rnd.Next(3); // 3 возможных сценария
            decimal changeValue = rnd.Next(50000, 150001); // Изменение цены от 50 до 150 тысяч
            switch (eventType)
            {
                case 0: // Скидка
                    car.Price -= changeValue;
                    if (car.Price < 0) car.Price = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nВНИМАНИЕ! На автомобиль {car.Brand} {car.Model} объявлена сезонная скидка! Цена снижена на {changeValue:N2} руб.");
                    break;
                case 1: // Повышение цены
                    car.Price += changeValue;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"\nВНИМАНИЕ! Из-за изменения курса валют цена на автомобиль {car.Brand} {car.Model} увеличилась на {changeValue:N2} руб.");
                    break;
                case 2: // Бонус к комплектации
                    car.Equipment = car.Equipment + " (+ Подарочный комплект зимней резины)";
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"\nВНИМАНИЕ! Акция от дилера! В комплектацию автомобиля {car.Brand} {car.Model} бесплатно добавлен зимний пакет.");
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Black; // Возвращаем цвет текста
            Console.Write("Актуальное состояние: ");
            car.PrintInfoObjStruct();
        }
        // Ообработки ошибок
        static void HandleError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[КРИТИЧЕСКАЯ ОШИБКА]: {ex.Message}");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}