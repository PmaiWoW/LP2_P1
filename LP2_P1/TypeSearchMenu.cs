using System;

namespace LP2_P1
{
    static class TypeSearchMenu
    {
        public static void Menu()
        {
            char answer;

            Console.WriteLine("1. Search Title\n" +
                              "2. Search People\n" +
                              "Q. Quit\n");


            char.TryParse(Console.ReadLine(), out answer);
            answer = char.ToUpper(answer);

            switch(answer)
            {
                case '1':
                    break;
                
                case '2':
                    break;

                case 'Q':
                    Quit();
                    break;

                default:
                    break;
            }
        }

        public static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
