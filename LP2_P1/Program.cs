﻿using System;

namespace LP2_P1
{
    internal class Program
    {    
        public const int WindowWidth = 200;
        public const int WindowHeight = 60;

        private static void Main(string[] args)
        {      
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetWindowPosition(0, 0);
            
            Console.Title = "MyIMDBSearcher";
            Console.CursorVisible = false;

            SearchMenu.MenuLoop();

            //Searcher searcher = new Searcher();

            //searcher.SelectSearch();
            
            Console.ResetColor();
        }
    }
}