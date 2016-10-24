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

            IBusinessLayer businessLayer = new BusinessLayer();

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

                    IList<Standard> standardTable;

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

                        if (intInput == 1)
                        {
                            string standardName;
                            int standardId;
                            Console.WriteLine("Please enter the standard you wish to enter:");
                            Console.Write("Standard Name: ");
                            standardName = Console.ReadLine();
                            Console.Write("Standard ID: ");
                            standardId = Convert.ToInt16(Console.ReadLine());

                            Standard stand = new Standard()
                            {
                                StandardName = standardName,
                                StandardId = standardId
                            };

                            businessLayer.addStandard(stand);
                        }
                        else if (intInput == 5)
                        {
                            standardTable = businessLayer.getAllStandards();
                            foreach (Standard standard in standardTable)
                            {
                                Console.WriteLine(standard.StandardId + " " + standard.StandardName);
                            }
                        }
                        else
                        {
                            standardEnd = true;
                        }
                    }
                }
                else if (intTemp == 2)
                {
                    bool studentEnd = false;
                }
                else
                {
                    Console.WriteLine("Thank you for using this program!");
                    programEnd = true;
                }
            }

            return;

        }
    }
}



/*
Standard stand = new Standard
{
    StandardName = "Test",
    StandardId = 324
};

stand.Students = new List<Student>
{
    new Student("Kevin Rodriguez", 612)
};

businessLayer.addStandard(stand);


Console.WriteLine("Existing departments:");
IList<Standard> departments = businessLayer.getAllStandards();
foreach (Standard department in departments)
{
    Console.WriteLine(department.StandardId + " " + department.StandardName);
}

IList<Student> students = businessLayer.getAllStudents();
foreach (Student student in students)
{
    Console.WriteLine(student.StudentName + " " + student.StudentID);
}

businessLayer.removeStandard(stand);

Console.WriteLine("Existing departments:");
departments = businessLayer.getAllStandards();
foreach (Standard department in departments)
{
    Console.WriteLine(department.StandardId + " " + department.StandardName);
}

IList<Student> students1 = businessLayer.getAllStudents();
foreach (Student student in students)
{
    Console.WriteLine(student.StudentName + " " + student.StudentID);
}

Console.ReadKey();
*/
