using System.Collections;
using StudentDLL;

namespace Academy_GroupDLL
{
    public class Academy_Group
    {
        ArrayList students;
        int count;

        public Academy_Group()
        {
            students = new ArrayList();
            count = 0;
        }

        // Метод добавляет студентов в группу 
        public void Add(Student student)
        {
            students.Add(student);
            count++;
            Console.WriteLine("Студент " + student.Name + " " + student.Surname + " добавлен");

        }

        //Метод удаляет студентов из группы
        public void Remove(string student_surname)
        {
            for (int i = 0; i < students.Count; i++)
            {
                Student student = (Student)students[i];
                if (student.Surname.Equals(student_surname, StringComparison.OrdinalIgnoreCase))
                {
                    students.RemoveAt(i);
                    count--;
                    Console.WriteLine("Студент " + student_surname + " удалён из группы");
                    return;
                }
            }

            Console.WriteLine("Студент " + student_surname + " не найден в группе.");
        }


        // Метод позволяет редактировать студента
        public void Edit(string student_surname, Student NewStudent)
        {
            for (int i = 0; i < students.Count; i++)
            {
                Student student = (Student)students[i];
                if (student.Surname == student_surname)
                {
                    students[i] = NewStudent;
                    Console.WriteLine("Студент " + student_surname + " редактирован на студента " + NewStudent.Surname);
                    return;
                }

            }
        }

        // Выводим всю группу 
        public void Print()
        {
            for (int i = 0; i < students.Count; i++)
            {
                Student student = (Student)students[i];
                Console.WriteLine("\n");
                student.Print();
            }
        }

        //метод для сортировки студентов по заданому критерию 
        public void Sort(int sorting_сriteria)
        {
            if (sorting_сriteria == 1)
            {
                Console.WriteLine("Сортировка по имени:");
                students.Sort();
                foreach (Student student in students)
                    student.Print();
            }
            else if (sorting_сriteria == 2)
            {
                Console.WriteLine("\nСортировка по фамилии:");
                students.Sort(new Student.SortBySurname());
                foreach (Student student in students)
                    student.Print();
            }
            else if (sorting_сriteria == 3)
            {
                Console.WriteLine("\nСортировка по возрасту:");
                students.Sort(new Student.SortByAge());
                foreach (Student student in students)
                    student.Print();
            }
            else
            {
                Console.WriteLine("Не такого критерия!");
            }
        }
        // Сохраняем группу в файл
        public void Save()
        {
            using (StreamWriter write_to_file = new StreamWriter("Group.txt", false))
            {
                for (int i = 0; i < students.Count; i++)
                {
                    Student student = (Student)students[i];
                    string line = "Имя - " + student.Name + "\nSurname - " + student.Surname + "\nPhone - " + student.Phone + "\nAge - " + student.Age + "\nAverage - " + student.Average + "\nNumber of group - " + student.NumberOfGroup;

                    write_to_file.WriteLine(line);
                    write_to_file.WriteLine();
                }
            }
            Console.WriteLine("Данные о студентах сохранены в файл");
        }

        // Загружаем данные из файла
        public void Load()
        {
            students.Clear();
            count = 0;

            using (StreamReader reade_file = new StreamReader("Group.txt"))
            {
                string nameLine;
                while ((nameLine = reade_file.ReadLine()) != null)
                {
                    string surnameLine = reade_file.ReadLine();
                    string phoneLine = reade_file.ReadLine();
                    string ageLine = reade_file.ReadLine();
                    string averageLine = reade_file.ReadLine();
                    string groupLine = reade_file.ReadLine();

                    reade_file.ReadLine();

                    string name = nameLine.Split('-')[1].Trim();
                    string surname = surnameLine.Split('-')[1].Trim();
                    string phone = phoneLine.Split('-')[1].Trim();
                    int age = int.Parse(ageLine.Split('-')[1].Trim());
                    double average = double.Parse(averageLine.Split('-')[1].Trim());
                    int numberOfGroup = int.Parse(groupLine.Split('-')[1].Trim());

                    Student student = new Student(name, surname, age, phone, average, numberOfGroup);
                    students.Add(student);
                    count++;
                }
            }

            Console.WriteLine("Данные о студентах загружены из файла.");
        }

        // метод для поиска студентов по заданному критерию
        // Equals - метод сравнивет строки
        public void Search(int criterion_number)
        {
            bool found = false;
            switch (criterion_number)
            {
                case 1:

                    Console.Write("Введите имя для поиска: ");
                    string search_name = Console.ReadLine();
                    foreach (Student student in students)
                    {
                        if (student.Name.Equals(search_name, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Такой студент есть:");
                            Console.WriteLine("\n");
                            student.Print();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Такого студента нет.");
                    }
                    break;
                case 2:

                    Console.Write("Введите фамилию для поиска: ");
                    string search_surname = Console.ReadLine();
                    foreach (Student student in students)
                    {
                        if (student.Surname.Equals(search_surname, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Такой студент есть: ");
                            student.Print();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Такого студента нет.");
                    }
                    break;
                case 3:

                    Console.Write("Введите телефон для поиска: ");
                    string search_phone = Console.ReadLine();
                    foreach (Student student in students)
                    {
                        if (student.Phone.Equals(search_phone, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Такой студент есть: ");
                            student.Print();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Такого студента нет.");
                    }
                    break;
                case 4:

                    Console.Write("Введите возраст для поиска: ");
                    string search_age = Console.ReadLine();
                    foreach (Student student in students)
                    {
                        string str_age = student.Age.ToString();
                        if (str_age.Equals(search_age, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Такой студент есть: ");
                            student.Print();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Такого студента нет.");
                    }
                    break;
                case 5:

                    Console.Write("Введите средний бал для поиска: ");
                    string search_average = Console.ReadLine();
                    foreach (Student student in students)
                    {
                        string str_average = student.Average.ToString();
                        if (str_average.Equals(search_average, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Такой студент есть: ");
                            student.Print();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Такого студента нет.");
                    }
                    break;
                case 6:
                    Console.Write("Введите группу для поиска: ");
                    string search_number_of_group = Console.ReadLine();
                    foreach (Student student in students)
                    {
                        string str_number_of_group = student.NumberOfGroup.ToString();
                        if (str_number_of_group.Equals(search_number_of_group, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Такой студент есть: ");
                            student.Print();
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Такого студента нет.");
                    }
                    break;
            }
        }

        // реализцем метод Clone
        public object Clone()
        {
            Academy_Group clonedGroup = new Academy_Group();
            foreach (Student student in this.students)
            {
                clonedGroup.Add(student);
            }
            return clonedGroup;
        }

    }
}
