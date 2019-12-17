using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public static class SearchMenu
    {
        public static IEnumerable<TitleRatings> titleRatingsList = 
            FileLoader.LoadTitleRatings();
        public static IEnumerable<TitleBasics> titleBasicsList =
            FileLoader.LoadTitleBasics();

        public static IEnumerable<TitleBasics> searchResults;

        private static string wantedTitle;
        private static ICollection<TitleType> types = new List<TitleType>();
        private static ICollection<TitleGenre?> genres =
            new List<TitleGenre?>();
        private static bool? isAdult;
        // Creates a IEnumerable of all the titles
        private static IEnumerable<TitleBasics> titleBasicsEnum =
            FileLoader.LoadTitleBasics();
        // Creates a IEnumerable of all the ratings
        private static IEnumerable<TitleRatings> titleRatingsEnum =
            FileLoader.LoadTitleRatings();

        public static void MenuLoop()
        {
            // Variable for input key
            ConsoleKey key;

            // While loop until the user presses 'Q'
            do
            {
                // Displays information on the console
                Console.Clear();
                Console.WriteLine("\n  1. Search Title\n" +
                                  "  2. Search People\n" +
                                  "  Q. Quit\n");

                // Sets the value of the vaiable 'key' to the input of the user
                key = Console.ReadKey().Key;

                // Performs a task acording to the input given
                switch (key)
                {
                    // Opens a sub-menu for the user to specify what he wants
                    case ConsoleKey.D1:
                        wantedTitle = default;
                        TitleSearch();
                        break;

                    // ***not implemented***
                    case ConsoleKey.D2:
                        UserInterface.NotImplemented();
                        break;

                    // Exits the program with a message
                    case ConsoleKey.Q:
                        Quit();
                        break;

                    // if the input is not expected tells the user
                    default:
                        break;
                }
            } while (key != ConsoleKey.Q);
        }

        private static void Titles(string wantedTitle, TitleType[] type,
            bool? adult, int? startDate, int? endDate,
            TitleGenre?[] genres, int? runtime1, int? runtime2,
            int? rating1, int? rating2)
        {

            // Checks if the method received a title to search for
            if (wantedTitle != null)
                // Checks the titles list for the title given
                titleBasicsEnum = titleBasicsEnum.Where
                    (c => c.PrimaryTitle.Trim().ToLower().Contains
                    (wantedTitle.Trim().ToLower()));

            // Checks if the method received a base runtime
            if (runtime1.HasValue)
                // Gets all the titles with a runtime equal or lower than given
                titleBasicsEnum = titleBasicsEnum.Where
                    (c => c.RuntimeMinutes >= runtime1);

            // Checks if the method received a max runtime
            if (runtime2.HasValue)
                // Gets all the titles with a runtime higher or equl than given
                titleBasicsEnum = titleBasicsEnum.Where
                    (c => c.RuntimeMinutes <= runtime2);

            // Checks if the method received a base date
            if (startDate.HasValue)
<<<<<<< HEAD
                // Gets all titles where the year is equal or lower than given
                titleBasicsEnum = titleBasicsEnum.Where
=======
                searchResults = searchResults.Where
>>>>>>> UIRework-TitleSearchOnly
                    (c => c.StartYear >= startDate);

            // Checks if the method received a maximum date
            if (endDate.HasValue)
<<<<<<< HEAD
                // Gets all titles where the year is equal or lower than given
                titleBasicsEnum = titleBasicsEnum.Where
=======
                searchResults = searchResults.Where
>>>>>>> UIRework-TitleSearchOnly
                    (c => c.EndYear <= endDate);

            // Checks if the types array has anything inside
            if (type.Length > 0)
<<<<<<< HEAD
                // Gets all the titles where the type equals the type given
                titleBasicsEnum = titleBasicsEnum.Where
=======
                searchResults = searchResults.Where
>>>>>>> UIRework-TitleSearchOnly
                    (c => type.Any(a => a == c.Type));

            // Checks if the adult bool is not null
            if (adult.HasValue)
<<<<<<< HEAD
                // Gets all the titles where adult equals
                titleBasicsEnum = titleBasicsEnum.Where
=======
                searchResults = searchResults.Where
>>>>>>> UIRework-TitleSearchOnly
                    (c => c.IsAdult == adult);

            // A loop the size of the Genres array
            for (int i = 0; i < genres.Length - 1; i++)
<<<<<<< HEAD
                // Gets all the titles where the genres are the same has one of
                // the genres given
                titleBasicsEnum =
                    from title in titleBasicsEnum
=======
                searchResults =
                    from title in searchResults
>>>>>>> UIRework-TitleSearchOnly
                    where title.Genres.Contains(genres[i])
                    select title;

            IEnumerable<(TitleBasics, TitleRatings)> mixedList =
                titleBasicsEnum.GroupJoin(titleRatingsEnum.Where(c => true),
                title2 => title2.TConst, rating2 => rating2.Tconst, (t, r) =>
                new { Title = t, Rating = r.Where(y => y.Tconst.Contains(t.TConst)) })
                .Select(x => (x.Title, x.Rating.FirstOrDefault()));

            // Checks if the method received a lowest rating
            if (rating1.HasValue)
                // Gets all the titles and ratings where the average rating is
                // equal or higher than the value given
                mixedList = mixedList.Where(c => c.Item2.AverageRating >= rating1);

            // Checks if the method received a highest rating
            if (rating2.HasValue)
                // Gets all the titles and ratings where the average rating is
                // equal or lower than the value given
                mixedList = mixedList.Where(c => c.Item2.AverageRating <= rating2);

            // Creates an instance of the TitleSearch class
            TitleSearch searcher = new TitleSearch();

<<<<<<< HEAD
            // Goes to the SearchTitle method passing the list of titles and ratings
            searcher.SearchTitle(mixedList);
=======
            searcher.SearchTitle(searchResults);
>>>>>>> UIRework-TitleSearchOnly
        }

        private static void TitleSearch()
        {
<<<<<<< HEAD
            // Creates the variables to be passed as arguments to the 'Titles'
            // method
            int? start = null;
            int? end = null;
            int? runtime1 = null;
            int? runtime2 = null;
            int? ratingLow = null;
            int? ratingHigh = null;

            // Cleans the console
=======
>>>>>>> UIRework-TitleSearchOnly
            Console.Clear();

            // Defines a variable for the user input
            ConsoleKey key;

            // Uses the static UserInterface class to display information to
            //the user
            UserInterface.PrintSearchBar();
            UserInterface.PrintTypeSelection(types, genres, isAdult);

            // Sets the cursor postition to on the position 2 of the console
            Console.CursorTop = 2;

            // A while loop running while the user doesn't press Q
            do
            {
                // Sets the cursor to the position 1 of the screen
                Console.CursorLeft = 1;
                // Displays an arrow at the position of the cursor
                Console.Write(">");

                // Assigns the key variable the key the user pressed
                key = Console.ReadKey().Key;

                // Checks if the key is the up arrow
                if (key == ConsoleKey.UpArrow && Console.CursorTop > 2)
                {
                    // Uses the UserInterface class to clear the arrow
                    UserInterface.ClearSpace();
                    // Moves the cursor one position up
                    Console.CursorTop -= 1;
                }
                // Checks if the key is the down arrow
                else if (key == ConsoleKey.DownArrow && Console.CursorTop < 55)
                {
                    // Uses the UserInterface class to clear the arrow
                    UserInterface.ClearSpace();
                    // Moves the cursor one position down
                    Console.CursorTop += 1;
                }
                // Checks if the user pressed Enter
                else if (key == ConsoleKey.Enter)
                {
                    // Checks if the cursor is on the second position
                    if (Console.CursorTop == 2)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Displays to the user a visual search bar
                        UserInterface.PrintSearchBar();
                        // Sets the color of the console acording to a id
                        UserInterface.ColorSetup(4);
<<<<<<< HEAD
                        // Reads the input of the user and saves it on the
                        // wantedtitle variable
=======
                        wantedTitle = default;
>>>>>>> UIRework-TitleSearchOnly
                        wantedTitle = Console.ReadLine();
                        // Resets the color of the console
                        Console.ResetColor();
                    }
                    // Checks if the position of the cursor is between 5 and 14
                    if (Console.CursorTop >= 5 && Console.CursorTop <= 14)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Creates a variable equal to the cursor top minus
                        // the minimum cursor position to enter this if
                        int index = Console.CursorTop - 5;
                        // Checks if the types list contains the selected type
                        if (types.Contains((TitleType)index))
                            // If it's already there it removes it from the list
                            types.Remove((TitleType)index);
                        // If it's not in the list it adds it
                        else
                            types.Add((TitleType)index);

                        // Uses the UserInterface class to display the choosen
                        // options
                        UserInterface.PrintTypeSelection(types, genres, isAdult);
                        // Resets the cursor position to the index plus 5
                        Console.CursorTop = index + 5;
                    }
                    // Checks if the position of the cursor is 16
                    if (Console.CursorTop == 16)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Cycles isAdult between false, true and adult
                        if (isAdult == true) isAdult = false;
                        else if (isAdult == null) isAdult = true;
                        else if (isAdult == false) isAdult = null;

                        // Displays to the user the decisions
                        UserInterface.PrintTypeSelection(types, genres, isAdult);
                        // Resets the cursor position back to 16
                        Console.CursorTop = 16;
                    }
                    // Checks if the position of the cursor is at 18
                    if (Console.CursorTop == 18)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Changes the color of the console acording to a id
                        UserInterface.ColorSetup(3);
                        // Creates a string that stores the input of the user
                        // and splits it by space
                        string[] date = Console.ReadLine().Split(' ');

                        // Checks if the first string is valid (is bigger than
                        // one, has size of 4 digits and tries to parse it)
                        if (date.Length >= 1 && date[0].Length == 4 &&
                            int.Parse(date[0]) != 0)
                            // Sets the value of start to the parsed string
                            start = int.Parse(date[0]);
                        else
                            // Resets the string back to null
                            start = null;

                        // Checks if the second string is valid (is equal to
                        // two, has size of 4 digits and tries to parse it)
                        if (date.Length == 2 && date[1].Length == 4 &&
                            int.Parse(date[1]) != 0)
                            // Sets the value of start to the parsed string
                            end = int.Parse(date[1]);
                        else
                            // Resets the string back to null
                            end = null;

                        // Resets color of the console
                        Console.ResetColor();
                    }
                    // Checks if the cursor is at 20
                    if (Console.CursorTop == 20)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Changes the color of the console acording to a id
                        UserInterface.ColorSetup(3);
                        // Creates a string that stores the input of the user
                        // and splits it by space
                        string[] runtime = Console.ReadLine().Split(' ');

                        // Checks if the first string is valid 
                        if (runtime.Length >= 1 && runtime[0].Length > 0 &&
                            int.Parse(runtime[0]) != 0)
                            // Sets the value of start to the parsed string
                            runtime1 = int.Parse(runtime[0]);
                        else
                            // Resets the string back to null
                            runtime1 = null;

                        // Checks if the second string is valid 
                        if (runtime.Length == 2 && runtime[0].Length > 0 &&
                            int.Parse(runtime[1]) != 0)
                            // Sets the value of start to the parsed string
                            runtime2 = int.Parse(runtime[1]);
                        else
                            // Resets the string back to null
                            runtime2 = null;

                        // Resets the color of the console
                        Console.ResetColor();
                    }
                    // Checks if the cursor is at 22
                    if (Console.CursorTop == 22)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Changes the color of the console acording to a id
                        UserInterface.ColorSetup(3);
                        // Creates a string that stores the input of the user
                        // and splits it by space
                        string[] rating = Console.ReadLine().Split(' ');

                        // Checks if the first string is valid 
                        if (rating.Length >= 1 && rating[0].Length > 0 &&
                            int.Parse(rating[0]) != 0)
                            // Sets the value of start to the parsed string
                            ratingLow = int.Parse(rating[0]);
                        else
                            // Resets the string back to null
                            ratingLow = null;

                        // Checks if the second string is valid 
                        if (rating.Length == 2 && rating[1].Length > 0 &&
                            int.Parse(rating[1]) != 0)
                            // Sets the value of start to the parsed string
                            ratingHigh = int.Parse(rating[1]);
                        else
                            // Resets the string back to null
                            ratingHigh = null;

                        // Resets the color of the console
                        Console.ResetColor();
                    }
                    // Checks if the cursor is between 24 and 51
                    if (Console.CursorTop >= 24 && Console.CursorTop <= 51)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Creates a variable for the index of the current genre
                        // the user is currently at
                        int indexes = Console.CursorTop - 24;
                        // Checks if the types list contains the selected type
                        if (genres.Contains((TitleGenre)indexes))
                            // If it's already in there it removes it
                            genres.Remove((TitleGenre)indexes);
                        else
                            // If it's not in the list it adds it
                            genres.Add((TitleGenre)indexes);

                        // Uses the UserInterface class to display the choosen
                        // options
                        UserInterface.PrintTypeSelection(types, genres, isAdult);
                        // Resets the cursor position to the index plus 24
                        Console.CursorTop = indexes + 24;
                    }
                    // Checks if the cursor is between 53 and 55
                    if (Console.CursorTop >= 53 && Console.CursorTop <= 55)
                    {
                        // Resets the windows and buffer size of the console
                        UserInterface.ResizeWindow();
                        // Enters the 'Titles' method giving it all the values
                        // the user choose, if left empty passes as null
                        Titles(wantedTitle, types.ToArray(), isAdult, start,
                            end, genres.ToArray(), runtime1,
                            runtime2, ratingLow, ratingHigh);

                        // Sets the key variable to Q
                        key = ConsoleKey.Q;
                    }
                }
            } while (key != ConsoleKey.Q);
        }

        public static void Quit()
        {
            // Uses the UserInterface to display a message to the user while quitting
            UserInterface.QuitMessage();
            // Checks for any input from the user
            Console.ReadKey(true);
            // Closes the program
            Environment.Exit(0);
        }
    }
}