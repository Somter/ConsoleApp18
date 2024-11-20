namespace PersonDLL
{
    public class Person
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Phone { get; set; } = "";
        public int Age { get; set; } = 0;

        public Person() { }

        public Person(string value_name, string value_surname, string value_phone, int value_age)
        {
            Name = value_name;
            Surname = value_surname;
            Phone = value_phone;
            Age = value_age;
        }

        public void Print()
        {
            Console.WriteLine($"Имя - {Name}\nФамилия - {Surname}\nТелефон - {Phone}\nВозраст - {Age}");
        }
    }
}
