using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Students_Data3
{
    class Program
    {
        private static string FilePath = "E:\\Visual studio 2022 Env\\Students_Data3\\Students_Data3\\student_data.txt";
        private static List<Student> students = new List<Student>();

        private static void ManageStudentData()
        {
            if (File.Exists(FilePath))
            {
                ReadFileContent();
                DisplayMenu();
            }
            else
            {
                Console.WriteLine("Student data file does not exist.");
            }
        }

        private static void ReadFileContent()
        {
            try
            {
                string[] lines = File.ReadAllLines(FilePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');
                    if (data.Length == 2)
                    {
                        string studentName = data[0].Trim();
                        string studentClass = data[1].Trim();
                        students.Add(new Student(studentName, studentClass));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading the file: " + e.Message);
            }
        }

        private static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Display sorted student data");
                Console.WriteLine("2. Search for a student by name");
                Console.WriteLine("3. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplaySortedStudents();
                        break;
                    case 2:
                        SearchStudentByName();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }

        private static void DisplaySortedStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No student data found.");
                return;
            }

            List<Student> sortedStudents = students.OrderBy(s => s.StudentName).ToList();

            Console.WriteLine("\nSorted Student Data:");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"{student.StudentName}, {student.StudentClass}");
            }
        }

        private static void SearchStudentByName()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No student data found.");
                return;
            }

            Console.WriteLine("\nEnter student name to search:");
            string searchName = Console.ReadLine().Trim();

            var foundStudents = students.Where(s =>
                s.StudentName.Split(' ').Any(name =>
                    name.Equals(searchName, StringComparison.OrdinalIgnoreCase)
                ) || s.StudentName.Equals(searchName, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (foundStudents.Count > 0)
            {
                Console.WriteLine($"\nFound {foundStudents.Count} student(s) with the name '{searchName}':");
                foreach (var student in foundStudents)
                {
                    Console.WriteLine($"{student.StudentName}, {student.StudentClass}");
                }
            }
            else
            {
                Console.WriteLine($"No student found with the name '{searchName}'.");
            }
        }

        static void Main(string[] args)
        {
            ManageStudentData();
        }
    }

    class Student
    {
        public string StudentName { get; }
        public string StudentClass { get; }

        public Student(string studentName, string studentClass)
        {
            StudentName = studentName;
            StudentClass = studentClass;
        }
    }
}
