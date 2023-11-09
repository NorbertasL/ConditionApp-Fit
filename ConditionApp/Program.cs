namespace ConditionApp {
    internal class Program {

        private const int NUMBER_OF_STUDENTS = 4;
        private const Int16 MIN_MARK = 0;
        private const Int16 MAX_MARK = 100;
        static void Main(string[] args) {

            Console.WriteLine((int)Grade.FAIL);

            int totalMarks = 0;
            string[] studentMarkData = new string[NUMBER_OF_STUDENTS];
            int topStudentId = -1;//Assuming ids start at 0, so -1 means empty;
            int topMark = MIN_MARK-1; 

            int worstMark = MAX_MARK+1;
            int worstStudent = -1;//Assuming ids start at 0, so -1 means empty;
            Dictionary<string, int> markOccurance = new Dictionary<string, int>();


            for (int studentId = 1; studentId <= NUMBER_OF_STUDENTS; studentId++) {
                int studentMark = GetValidUserMark($"Please enter Student #{studentId} Mark:");

                string studentMarkMessage = $"Student #{studentId} Marks achived: {studentMark}, Grade achived: ";
                if (studentMark >= (int)Grade.DISTINCTION) {
                    studentMarkMessage += Grade.DISTINCTION;
                    CreateOrAddGradeOccurance(markOccurance, Grade.DISTINCTION.ToString());
                }
                else if (studentMark >= (int)Grade.MERIT) {
                    studentMarkMessage += Grade.MERIT;
                    CreateOrAddGradeOccurance(markOccurance, Grade.MERIT.ToString());
                }
                else if (studentMark >= (int)Grade.PASS) {
                    studentMarkMessage += Grade.PASS;
                    CreateOrAddGradeOccurance(markOccurance, Grade.PASS.ToString());
                }
                else {
                    studentMarkMessage += Grade.FAIL;
                    CreateOrAddGradeOccurance(markOccurance, Grade.FAIL.ToString());
                }
                Console.WriteLine(studentMarkMessage);

                totalMarks += studentMark;
                studentMarkData[studentId - 1] = studentMarkMessage;


                //Gets top mark and student, will only store 1 student
                if(topMark < studentMark) {
                    topStudentId = studentId;
                    topMark = studentMark;
                }

                //Get min mark and student, will only store 1 student
                if (worstMark > studentMark) {
                    worstStudent = studentId;
                    worstMark = studentMark;
                }
            }

            Console.WriteLine($"Average mark is: {totalMarks / NUMBER_OF_STUDENTS}");
            Console.WriteLine($"Top Student was #{topStudentId} with the mark of {topMark}.");
            Console.WriteLine($"Worst Student was #{worstStudent} with the mark of {worstMark}.");

            string seperator = new('-', 50);

            Console.WriteLine(seperator);
            Console.ReadKey();//Using for pause
            Console.Write("\r"); //ReadKey generates a space, so we just going back to start line

            foreach (var studentData in studentMarkData) {
                Console.WriteLine(studentData.ToString());
            }
            Console.WriteLine(seperator);
            foreach (var mark in markOccurance) {
                Console.WriteLine($"{mark.Key} : {mark.Value} ");
            }
            Console.ReadKey();//Using for pause
            Console.Write("\r"); //ReadKey generates a space, so we just going back to start line
        }

        static void CreateOrAddGradeOccurance(Dictionary<string, int> markOccurance, string key) {
            if (markOccurance.ContainsKey(key)){
                markOccurance[key]++;
            }
            else {
                markOccurance.Add(key, 1);
            }
        }
        static int GetValidUserMark(string message) {
            bool isValidValue = false;
            int userInput;
            do {
                Console.Write(message);
                //Validates that the input is an int
                if (!int.TryParse(Console.ReadLine(), out userInput)){
                    Console.Write("\rInvalid mark try again! ");
                    continue;
                }
                //Validates that the inpute is withing range
                if(userInput > MAX_MARK || userInput < MIN_MARK) {
                    Console.Write($"\rUser mark have to be between {MIN_MARK}-{MIN_MARK}! ");
                    continue;
                }
                isValidValue = true;//Could just break out, but will keep it like this in case more validation need to be added
            } while (!isValidValue);
            return userInput;
        }
    }
    //Stores the Grade discription and the min value to achive it
    enum Grade {
        DISTINCTION = 80,
        MERIT = 65,
        PASS = 50,
        FAIL = 0
    }
}