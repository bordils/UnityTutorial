using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    int level;
    string password;
    enum Screen {MainMenu, Password, Win };
    Screen currentScreen = Screen.MainMenu;
    // Start is called before the first frame update
    void Start()
    { 
        ShowMainMenu("Mr. Robot");
    }

    void ShowMainMenu(string name)
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello, " + name + ".");
        Terminal.WriteLine("What Institution do you want to bring down today?");
        Terminal.WriteLine("");
        Terminal.WriteLine("    1. CIA");
        Terminal.WriteLine("    2. FBI");
        Terminal.WriteLine("    3. NSA");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your Selection:");
        Terminal.WriteLine("");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu("Mr. Robot");
        }
        else if (currentScreen == Screen.MainMenu)
        {
            HandleMainMenu(input);
        } 
        else if (currentScreen == Screen.Password)
        {
            HandlePassword(input);
        }
        
    }

    void HandlePassword(string input)
    {
        if (input == password)
        {
            HandleWin();
        }
        else
        {
            Terminal.WriteLine("WRONG PASSWORD, TRY AGAIN");
        }
    }

    void HandleWin()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine("You hacked the game");
        Terminal.WriteLine("write menu to go back to main menu");
    }

    void HandleMainMenu(string input)
    {
        if (input == "1")
        {
            password = "abcd";
            level = 1;
            StartGame();
        }
        else if (input == "2")
        {
            password = "abcd1234";
            level = 2;
            StartGame();
        }
        else if (input == "3")
        {
            password = "complexity";
            level = 3;
            StartGame();
        }
        else if (input == "007")
        {
            ShowMainMenu("Mr. Bond");
        }
        else
        {
            Terminal.WriteLine("Please, choose a valid level");
        }
    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine("You have chosen level " + level);
        Terminal.WriteLine("Please Enter your Password");
    }
}
