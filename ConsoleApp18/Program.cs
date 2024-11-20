using System.Reflection;

class MainClass
{
    static object academyGroupInstance;
    static Type academyGroupType;
    static Assembly studentAssembly; 
    static Type studentType;        
    static bool running = true;

    public static void Main()
    {
        try
        {
            LoadAssemblyAndType();

            while (running)
            {
                ShowMenu();
                HandleMenuChoice();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void LoadAssemblyAndType()
    {
        string dllFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DLL");
        string academyGroupDllPath = Path.Combine(dllFolderPath, "Academy_GroupDLL.dll");
        string studentDllPath = Path.Combine(dllFolderPath, "StudentDLL.dll");

        if (!File.Exists(academyGroupDllPath))
        {
            Console.WriteLine("Сборка Academy_GroupDLL.dll не найдена.");
            Environment.Exit(1);
        }

        Assembly academyGroupAssembly = Assembly.LoadFrom(academyGroupDllPath);
        academyGroupType = academyGroupAssembly.GetType("Academy_GroupDLL.Academy_Group");
        if (academyGroupType == null)
        {
            Console.WriteLine("Класс Academy_Group не найден в сборке Academy_GroupDLL.dll.");
            Environment.Exit(1);
        }

        academyGroupInstance = Activator.CreateInstance(academyGroupType);

        if (!File.Exists(studentDllPath))
        {
            Console.WriteLine("Сборка StudentDLL.dll не найдена.");
            Environment.Exit(1);
        }

 
        studentAssembly = Assembly.LoadFrom(studentDllPath);
        studentType = studentAssembly.GetType("StudentDLL.Student");
        if (studentType == null)
        {
            Console.WriteLine("Класс Student нп найден в сборке StudentDLL.dll.");
            Environment.Exit(1);
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("1. Добавить студента");
        Console.WriteLine("2. Удалить студента");
        Console.WriteLine("3. Редактировать студента");
        Console.WriteLine("4. Показать группу");
        Console.WriteLine("5. Сохранить данные");
        Console.WriteLine("6. Загрузить данные");
        Console.WriteLine("7. Поиск студента");
        Console.WriteLine("8. Сортировать студентов");
        Console.WriteLine("0. Выход");
        Console.Write("Ваш выбор: ");
    }

    private static void HandleMenuChoice()
    {
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                AddStudent();
                break;
            case "2":
                RemoveStudent();
                break;
            case "3":
                EditStudent();
                break;
            case "4":
                InvokeMethod("Print");
                break;
            case "5":
                InvokeMethod("Save");
                break;
            case "6":
                InvokeMethod("Load");
                break;
            case "7":
                SearchStudent();
                break;
            case "8":
                SortStudents();
                break;
            case "0":
                running = false;
                break;
            default:
                Console.WriteLine("Неверный выбор, попробуйте снова.");
                break;
        }
    }

    private static void AddStudent()
    {
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        Console.Write("Введите фамилию: ");
        string surname = Console.ReadLine();
        Console.Write("Введите телефон: ");
        string phone = Console.ReadLine();
        Console.Write("Введите возраст: ");
        if (!int.TryParse(Console.ReadLine(), out int age))
        {
            Console.WriteLine("Ошибка: возраст должен быть числом.");
            return;
        }
        Console.Write("Введите средний балл: ");
        if (!double.TryParse(Console.ReadLine(), out double average))
        {
            Console.WriteLine("Ошибка: средний балл должен быть числом.");
            return;
        }
        Console.Write("Введите номер группы: ");
        if (!int.TryParse(Console.ReadLine(), out int numberOfGroup))
        {
            Console.WriteLine("Ошибка: номер группы должен быть числом.");
            return;
        }

        try
        {
            
            var studentInstance = Activator.CreateInstance(studentType, name, surname, age, phone, average, numberOfGroup);
            InvokeMethod("Add", studentInstance);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении студента: {ex.Message}");
        }
    }

    private static void EditStudent()
    {
        Console.Write("Введите фамилию студента для редактирования: ");
        string editSurname = Console.ReadLine();

        Console.Write("Введите новое имя: ");
        string newName = Console.ReadLine();
        Console.Write("Введите новую фамилию: ");
        string newSurname = Console.ReadLine();
        Console.Write("Введите новый телефон: ");
        string newPhone = Console.ReadLine();
        Console.Write("Введите новый возраст: ");
        if (!int.TryParse(Console.ReadLine(), out int newAge))
        {
            Console.WriteLine("Ошибка: возраст должен быть числом.");
            return;
        }
        Console.Write("Введите новый средний балл: ");
        if (!double.TryParse(Console.ReadLine(), out double newAverage))
        {
            Console.WriteLine("Ошибка: средний балл должен быть числом.");
            return;
        }
        Console.Write("Введите новый номер группы: ");
        if (!int.TryParse(Console.ReadLine(), out int newNumberOfGroup))
        {
            Console.WriteLine("Ошибка: номер группы должен быть числом.");
            return;
        }

        try
        {
            var newStudent = Activator.CreateInstance(studentType, newName, newSurname, newAge, newPhone, newAverage, newNumberOfGroup);
            InvokeMethod("Edit", editSurname, newStudent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при редактировании студента: {ex.Message}");
        }
    }

    private static void InvokeMethod(string methodName, params object[] parameters)
    {
        MethodInfo method = academyGroupType.GetMethod(methodName);
        if (method == null)
        {
            Console.WriteLine($"Метод {methodName} не найден.");
            return;
        }

        method.Invoke(academyGroupInstance, parameters);
    }

    private static void SortStudents()
    {
        Console.WriteLine("Выберите критерий для сортировки:");
        Console.WriteLine("1. Имя");
        Console.WriteLine("2. Фамилия");
        Console.WriteLine("3. Возраст");
        Console.Write("Ваш выбор: ");

        if (int.TryParse(Console.ReadLine(), out int criterion))
        {
            try
            {
                InvokeMethod("Sort", criterion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сортировке студентов: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: ввод должен быть числом.");
        }
    }

    private static void SearchStudent()
    {
        Console.WriteLine("Выберите критерий для поиска:");
        Console.WriteLine("1. Имя");
        Console.WriteLine("2. Фамилия");
        Console.WriteLine("3. Телефон");
        Console.WriteLine("4. Возраст");
        Console.WriteLine("5. Средний балл");
        Console.WriteLine("6. Номер группы");
        Console.Write("Ваш выбор: ");

        if (int.TryParse(Console.ReadLine(), out int criterion))
        {
            try
            {
                InvokeMethod("Search", criterion);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске студентов: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Ошибка: ввод должен быть числом.");
        }
    }

    private static void RemoveStudent()
    {
        Console.Write("Введите фамилию студента для удаления: ");
        string surname = Console.ReadLine();
        try
        {
            InvokeMethod("Remove", surname);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении студента: {ex.Message}");
        }
    }



}
