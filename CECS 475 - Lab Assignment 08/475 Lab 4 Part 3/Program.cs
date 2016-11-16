using _475_Lab_4_Part_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _475_Lab_4_Part_3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programEnd = false;
            string studentName, standardName;
            int standardId, studentId;

            IBusinessLayer businessLayer = new BusinessLayer();
            IList<Standard> standardTable;
            IList<Student> studentTable;
            Student student;
            Standard standard;
            string option;

            while (!programEnd)
            {
                Console.WriteLine("1. Table Standard");
                Console.WriteLine("2. Table Student");
                Console.WriteLine("3. Exit Program");
                Console.Write("Please select from the above: ");
                int intTemp = Convert.ToInt32(Console.ReadLine());

                if (intTemp == 1)
                {
                    bool standardEnd = false;

                    while (!standardEnd)
                    {
                        Console.WriteLine("Standard Table Options:");
                        Console.WriteLine("1. Create Standard");
                        Console.WriteLine("2. Update Standard");
                        Console.WriteLine("3. Delete Standard");
                        Console.WriteLine("4. Display students under a standard ID");
                        Console.WriteLine("5. Display all standards");
                        Console.WriteLine("6. Exit menu.");
                        Console.Write("Selection: ");
                        int intInput = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine();

                        if (intInput == 1)
                        {

                            Console.WriteLine("Please enter the standard you wish to enter:");
                            Console.Write("Standard Name: ");
                            standardName = Console.ReadLine();

                            Standard stand = new Standard()
                            {
                                StandardName = standardName
                            };

                            businessLayer.addStandard(stand);
                        }
                        else if (intInput == 2)
                        {
                            Console.WriteLine("Enter the standard name you wish to update: ");
                            Console.Write("Standard Name: ");
                            standardName = Console.ReadLine();
                            Console.WriteLine();

                            standard = businessLayer.GetStandardByName(standardName);
                            Console.Write("Enter the updated standard name: ");
                            standard.StandardName = Console.ReadLine();
                            businessLayer.updateStandard(standard);

                        }
                        else if (intInput == 3)
                        {
                            Console.Write("Enter the standard name you wish to delete: ");
                            standardId = Convert.ToInt16(Console.ReadLine());
                            standard = businessLayer.GetStandardByID(standardId);
                            businessLayer.removeStandard(standard);
                        }
                        else if (intInput == 4)
                        {
                            Console.Write("Enter the standard ID you wish to access: ");
                            standardId = Convert.ToInt16(Console.ReadLine());
                            standard = businessLayer.GetStandardByID(standardId);
                            Console.WriteLine();
                            Console.WriteLine("Students in " + standard.StandardName + ": ");
                            Console.WriteLine();
                            Console.WriteLine("Students: " + "\t\t" + "ID:");
                            foreach (Student s in standard.Students)
                            {
                                Console.WriteLine(s.StudentName + "\t\t\t" + s.StudentID);
                            }
                            Console.WriteLine();
                        }
                        else if (intInput == 5)
                        {
                            standardTable = businessLayer.getAllStandards();
                            Console.WriteLine();
                            foreach (Standard newStandard in standardTable)
                            {
                                Console.WriteLine(newStandard.StandardId + " " + newStandard.StandardName);
                            }
                            Console.WriteLine();
                        }
                        else if (intInput == 6)
                        {
                            standardEnd = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid option.");
                        }
                    }
                }
                else if (intTemp == 2)
                {
                    bool studentEnd = false;

                    while (!studentEnd)
                    {
                        Console.WriteLine("Student Table Options:");
                        Console.WriteLine("1. Create Student");
                        Console.WriteLine("2. Update Student");
                        Console.WriteLine("3. Delete Student");
                        Console.WriteLine("4. Display all Students");
                        Console.WriteLine("5. Exit menu.");
                        Console.Write("Selection: ");
                        int input = Convert.ToInt32(Console.ReadLine());

                        if (input == 1)
                        {
                            Console.WriteLine("Please enter the student name and id.");
                            Console.Write("Name: ");
                            studentName = Console.ReadLine();

                            Console.Write("New student's standard ID: ");
                            standardId = Convert.ToInt16(Console.ReadLine());
                           
                            student = new Student
                            {
                                StudentName = studentName,
                                StandardId = standardId
                            };

                            standard = businessLayer.GetStandardByID(standardId);
                            standard.Students.Add(student);
                            businessLayer.addStudent(student);
                        }
                        else if (input == 2)
                        {
                            Console.Write("Would you like to update by student name or student id?: ");
                            option = Console.ReadLine();
                            Console.WriteLine();
                            if (option.Equals("name"))
                            {
                                Console.Write("Enter the student name you are searching for: ");
                                studentName = Console.ReadLine();
                                student = businessLayer.getStudentByName(studentName);
                                Console.WriteLine();
                                Console.Write("Enter the updated student name: ");
                                student.StudentName = Console.ReadLine();
                                businessLayer.updateStudent(student);
                            }
                            else if (option.Equals("id"))
                            {
                                Console.Write("Enter the student ID you are searching for: ");
                                studentId = Convert.ToInt16(Console.ReadLine());
                                student = businessLayer.getStudentByID(studentId);
                                Console.WriteLine();
                                Console.Write("Enter the updated student name: ");
                                student.StudentName = Console.ReadLine();
                                businessLayer.updateStudent(student);
                            }
                        }
                        else if (input == 3)
                        {
                            Console.Write("Enter the student ID you want to delete: ");
                            studentId = Convert.ToInt16(Console.ReadLine());
                            student = businessLayer.getStudentByID(studentId);
                            businessLayer.removeStudent(student);
                        }
                        else if (input == 4)
                        {
                            studentTable = businessLayer.getAllStudents();
                            Console.WriteLine();
                            foreach (Student studentLoop in studentTable)
                            {
                                Console.WriteLine(studentLoop.StudentName + " " + studentLoop.StudentID);
                            }
                            Console.WriteLine();
                        }
                        else if (input == 5)
                        {
                            studentEnd = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Thank you for using this program!");
                    programEnd = true;
                }
            }
        }
    }
}
