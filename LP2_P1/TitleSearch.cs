﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LP2_P1
{
    public class TitleSearch
    {
        private IEnumerable<TitleBasics> namedTitles;
        private State listState = State.Unordered;
        private int skipNumber = 30;
        private int displayedAmount = 0;

        public void SearchTitle(string wantedTitle)
        {
            namedTitles = FileLoader.LoadTitleBasics()
                .Where(c => c.PrimaryTitle.ToLower()
                .Contains(wantedTitle.Trim().ToLower()))
                .Select(c => c).ToList();

            SearchMenu();
        }

        private void SearchMenu()
        {
            ConsoleKey key = ConsoleKey.L;

            if(!namedTitles.Any(c => c.TConst != null))
            {
                Console.Clear();
                Console.WriteLine("No titles found, returning to main " +
                    "menu...");
                return;
            }
            UpdatePage();

            while (key != ConsoleKey.B)
            {
                key = ConsoleKey.D0;
                Console.CursorLeft = 1;
                Console.Write(">");

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (namedTitles.Count() / skipNumber > 0)
                        {
                            skipNumber += 30;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (skipNumber > 30)
                        {
                            skipNumber -= 30;
                            UpdatePage();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (Console.CursorTop > 1)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop -= 1;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (Console.CursorTop < displayedAmount)
                        {
                            Console.CursorLeft -= 2;
                            Console.Write(" ");
                            Console.CursorTop += 1;
                        }
                        break;

                    case ConsoleKey.O:
                        Sorter.Sort(ref namedTitles);
                        UpdatePage();
                        break;

                    case ConsoleKey.R:
                        ReverseOrder();
                        break;

                    case ConsoleKey.Enter:
                        TitleDetails.DisplayInfo(
                            namedTitles.ElementAt(Console.CursorTop - 1));
                        UpdatePage();
                        UpdatePage();
                        break;

                    case ConsoleKey.B:
                        Console.Clear();
                        Console.WriteLine("Going back to the previous menu." +
                            "\nPress any key to continue.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Press any key to " +
                            "return to selection.");
                        Console.ReadKey();
                        UpdatePage();
                        break;
                }
            }
            Console.Clear();
        }

        // Sorts searched items by Type
        private void UpdatePage()
        {
            PrintResults(namedTitles.SkipLast(namedTitles.Count() - skipNumber)
                .Skip(skipNumber - 30).Select(c => c)
                .ToList());
        }

        private void ReverseOrder()
        {
            if (listState != State.Unordered)
                listState = listState == State.Descending ?
                    State.Ascending : State.Descending;

            namedTitles = namedTitles.Reverse();
            UpdatePage();
        }
        private void PrintResults(IEnumerable<TitleBasics> titlesToDisplay)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");

            Console.CursorLeft = 0;
            Console.Write("    Name");
            Console.CursorLeft = 100;
            Console.Write("Type");
            Console.CursorLeft = 150;
            Console.Write($"State : {listState}");

            Console.ResetColor();

            Console.WriteLine("");

            displayedAmount = titlesToDisplay.Count();

            for (int i = 0; i < titlesToDisplay.Count(); i++)
            {
                Console.CursorLeft = 0;
                Console.Write($"   {titlesToDisplay.ElementAt(i).PrimaryTitle}");
                Console.CursorLeft = 100;
                Console.WriteLine($"| {titlesToDisplay.ElementAt(i).Type}");
            }

            Console.WriteLine("\n 'O' to order " +
                "\n 'R' to reverse the order " +
                "\n 'B' to go back to previous menu" +
                "\n 'ENTER' to select title" +
                "\n '->' for previous page" +
                "\n '<-' for next page \n");

            Console.CursorTop = 1;
        }

        private enum State { Ascending, Descending, Unordered };
    }
}