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
                Console.WriteLine("6. Register a student");
                Console.WriteLine("7. Exit");
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
                            var classnameinput = userInput.PromptUserInput("Enter the name of class you want to register: ");

                            actionOutcome = await _registrationService.AddStudentToClass(classnameinput, studentname);

                            Console.WriteLine(actionOutcome.Message);
                            break;

                        case 3:
                            var studentnameinput = userInput.PromptUserInput("Enter the name of your student: ");
                            var classnameinputremove = userInput.PromptUserInput("Enter the type of class you want to deregister: ");

                            actionOutcome = await _registrationService.RemoveStudentFromClass(classnameinputremove, studentnameinput);

                            Console.WriteLine(actionOutcome.Message);

                            break;

                        case 4:
                            var studentnamecheck = userInput.PromptUserInput("Enter the name of your student: ");
                            var outcomelists = await _registrationService.StudentEnrolledInClasses(studentnamecheck);

                            if (outcomelists == null)
                            {
                                Console.WriteLine("Student hasn't signed up to any classes");
                            break;
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
                            if ( alllists == null)
                            {
                                Console.WriteLine("There are no available classes");
                            }
                            foreach (var list in alllists)
                            {
                                Console.WriteLine("Class name: " + list.ClassName);
                                Console.WriteLine("Class type: " + list.ClassType);
                                Console.WriteLine("Class capacity: " + list.EnrolledStudents.Count + "/" + list.MaxOccupancy);
                                Console.WriteLine("-----------------------\n");
                            }
                            break;

                        case 6:
                        var inputstudentname = userInput.PromptUserInput("Enter the name of the student: ");

                        actionOutcome = await _registrationService.AddStudent(inputstudentname);

                        Console.WriteLine(actionOutcome.Message);
                        
                        break;


                            case 7:
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
