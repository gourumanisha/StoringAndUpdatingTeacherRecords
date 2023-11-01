using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoringandUpdatingTeacherRecords
{
    public class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ClassAndSection { get; set; }
    }

    public class TeacherData
    {
        private const string DataFilePath = "teachers.txt";

        public static List<Teacher> ReadTeachersFromFile()
        {
            List<Teacher> teachers = new List<Teacher>();

            if (File.Exists(DataFilePath))
            {
                string[] lines = File.ReadAllLines(DataFilePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        teachers.Add(new Teacher
                        {
                            ID = int.Parse(parts[0]),
                            Name = parts[1],
                            ClassAndSection = parts[2]
                        });
                    }
                }
            }

            return teachers;
        }

        public static void WriteTeachersToFile(List<Teacher> teachers)
        {
            using (StreamWriter writer = new StreamWriter(DataFilePath))
            {
                foreach (Teacher teacher in teachers)
                {
                    writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.ClassAndSection}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Teacher> teachers = TeacherData.ReadTeachersFromFile();

            while (true)
            {
                Console.WriteLine("Teacher Records Management System");
                Console.WriteLine("1. Add Teacher 2. Update Teacher  3. View Teachers 4. Exit");


                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddTeacher(teachers);
                        break;

                    case 2:
                        UpdateTeacher(teachers);
                        break;

                    case 3:
                        ViewTeachers(teachers);
                        break;

                    case 4:
                        TeacherData.WriteTeachersToFile(teachers);
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        static void AddTeacher(List<Teacher> teachers)
        {
            Console.WriteLine("Enter Teacher ID:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Teacher Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Class and Section:");
            string classAndSection = Console.ReadLine();

            teachers.Add(new Teacher
            {
                ID = id,
                Name = name,
                ClassAndSection = classAndSection
            });

            Console.WriteLine("Teacher added successfully!");
        }

        static void UpdateTeacher(List<Teacher> teachers)
        {
            Console.WriteLine("Enter Teacher ID to update:");
            int id = int.Parse(Console.ReadLine());

            Teacher teacherToUpdate = teachers.Find(t => t.ID == id);

            if (teacherToUpdate == null)
            {
                Console.WriteLine("Teacher not found.");
                return;
            }

            Console.WriteLine("Enter updated Teacher Name:");
            teacherToUpdate.Name = Console.ReadLine();

            Console.WriteLine("Enter updated Class and Section:");
            teacherToUpdate.ClassAndSection = Console.ReadLine();

            Console.WriteLine("Teacher information updated successfully!");
        }

        static void ViewTeachers(List<Teacher> teachers)
        {
            Console.WriteLine("Teacher Records:");
            foreach (Teacher teacher in teachers)
            {
                Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, Class and Section: {teacher.ClassAndSection}");
            }
        }
    }
}

