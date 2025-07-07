using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassRegistration.Application.Interfaces;
using ClassRegistration.ConsoleApplication.Helpers;


namespace ClassRegistration.ConsoleApplication
{
    public class Menu
    {

        UserInput userInput = new UserInput();

        private readonly IRegistrationService _registrationService;

        public Menu(IRegistrationService RegistrationService)
        {
            _registrationService = RegistrationService;
        }


        public async Task ShowMenu()
        {

            while (true)
            {
                Console.WriteLine("+----------WELCOME TO THE Class Registration System----------+");
                Console.WriteLine("1. Create a Class");
                Console.WriteLine("2. Register student to a class");
                Console.WriteLine("3. Remove student from a class");
                Console.WriteLine("4. See which class a student is enrolled to");
                Console.WriteLine("5. Display available classes");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Type in the number of what you want to do: ");
                Console.WriteLine("+------------------------------------------+");

                int res;

                while (!int.TryParse(Console.ReadLine(), out res) || res < 1 || res > 7)
                {
                    Console.WriteLine("Invalid input! Enter 1-7:");
                }

                (bool Success, string Message) actionOutcome;


                    switch (res)
                    {
                        case 1:
                            var classname = userInput.PromptUserInput("Enter the name of your class you want to register: ");
                            var classtype = userInput.PromptUserInput("Enter the type of class you want to register: ");
                            var maxoccupancy = userInput.PromptUserInputInt("Enter the maximum occupancy of the class: ");

                            actionOutcome = await _registrationService.AddClass(classname, classtype, maxoccupancy);

                            Console.WriteLine(actionOutcome.Message);
                            break;

                        case 2:
                            var studentname = userInput.PromptUserInput("Enter the name of your student: ");
                            var classnameinput = userInput.PromptUserInput("Enter the type of class you want to register: ");

                            actionOutcome = await _registrationService.AddStudentToClass(studentname, classnameinput);

                            Console.WriteLine(actionOutcome.Message);
                            break;

                        case 3:
                            var studentnameinput = userInput.PromptUserInput("Enter the name of your student: ");
                            var classnameinputremove = userInput.PromptUserInput("Enter the type of class you want to deregister: ");

                            actionOutcome = await _registrationService.RemoveStudentFromClass(studentnameinput, classnameinputremove);

                            Console.WriteLine(actionOutcome.Message);

                            break;

                        case 4:
                            var studentnamecheck = userInput.PromptUserInput("Enter the name of your student: ");
                            var outcomelists = await _registrationService.StudentEnrolledInClasses(studentnamecheck);

                            if (outcomelists.Count == 0)
                            {
                                Console.WriteLine("Student hasn't signed up to any classes");
                            }

                            foreach (var list in outcomelists)
                            {
                                Console.WriteLine($"{studentnamecheck} is enrolled in: ");
                                Console.WriteLine("Class name: " + list.ClassName);
                                Console.WriteLine("Class type: " + list.ClassType);
                                Console.WriteLine("Class capacity: " + list.EnrolledStudents.Count + "/" + list.MaxOccupancy);
                                Console.WriteLine("-----------------------\n");
                            }

                            break;

                        case 5:
                            var alllists = await _registrationService.GetAvailableClasses();
                            foreach (var list in alllists)
                            {
                                Console.WriteLine("Class name: " + list.ClassName);
                                Console.WriteLine("Class type: " + list.ClassType);
                                Console.WriteLine("Class capacity: " + list.EnrolledStudents.Count + "/" + list.MaxOccupancy);
                                Console.WriteLine("-----------------------\n");
                            }
                            break;

                        case 6:
                            Console.WriteLine("Goodbye!");
                            Thread.Sleep(1000);
                            return;
                    }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
