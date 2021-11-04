using System;
using System.IO;
using System.Text;
using TodorStoykovEmployees.Business;

namespace TodorStoykovEmployeesProjectsConsoleApp
{
    

    class Program
    {
        static void ParseFile()
        {
            Console.WriteLine("Find which two employees worked together the longest!\n");

            Console.WriteLine("Please enter the path for the file and press Enter. \n\n");

            Console.WriteLine("File path:");

            string FilePath = Console.ReadLine();


            StreamReader stream_reader = new StreamReader(File.OpenRead(FilePath));

            string DateFormat = "yyyy/MM/dd, MM/dd/yyyy, MM/dd/yyyy HH:mm:ss, yyyy-MM-dd, yyyy-MM-dd HH:mm:ss.fff, yyyy-MM-dd HH:mm:ss";

            Console.WriteLine("\n\n");

            try
            {
                EmployeeProjectCollection AllEmplyeesProjectFromTheFile = new EmployeeProjectCollection();

                AllEmplyeesProjectFromTheFile.GetAllEmployeesProjectsFromStream(stream_reader, ",", DateFormat);
                int EmployeeIdPairOne = 0;
                int EmployeeIdPairTwo = 0;
                int ProjectIdWorkedTogether = 0;
                int MaxDaysWoredTogether = 0;

                bool FoundIt = AllEmplyeesProjectFromTheFile.FindThePairOfEmployeesWorkingLongest(ref EmployeeIdPairOne, ref EmployeeIdPairTwo, ref ProjectIdWorkedTogether, ref MaxDaysWoredTogether);

                if (FoundIt)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("EmployeeId#1 \t EmployeeId#2 \t ProjectId \t Days worked");
                    Console.WriteLine("----------------------------------------------------------------------");

                    StringBuilder DataFounce = new StringBuilder()
                                                .Append(EmployeeIdPairOne)
                                                .Append(" \t\t ")
                                                .Append(EmployeeIdPairTwo)
                                                .Append(" \t\t ")
                                                .Append(ProjectIdWorkedTogether)
                                                .Append(" \t\t ")
                                                .Append(MaxDaysWoredTogether);


                    Console.WriteLine(DataFounce.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unfortunately there was an error reading the file! Error message: \n");
                Console.Write(ex.Message + "\n\n");
                Console.WriteLine("Please try again with different file! \n");
            }
        }


        static void Main(string[] args)
        {
            ParseFile();

            Console.WriteLine("\n\n\n\n Please press enter to Exit the application");
            Console.ReadKey();
        }
    }
}
