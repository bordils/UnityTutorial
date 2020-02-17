using UnityEngine;

public class Hacker : MonoBehaviour
{
    int level;
    const string menuHint = "Type menu at any time to go back";
    string[] level1Passwords = {"abcd", "easy", "pass", "what", "twat" };
    string[] level2Passwords = { "abcd1234", "medium", "passenger", "whatelse", "twathead" };
    string[] level3Passwords = { "complexity", "harderthanhard", "password", "nespressowhatelse", "twatsucker" };
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
            Terminal.WriteLine(menuHint);
            AskForPassword();
        }
    }

    void HandleWin()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    void ShowLevelReward()
    {
        Terminal.WriteLine("You hacked the game");
        switch(level)
        {
            case 1:
                Terminal.WriteLine("Get a Book...");
                Terminal.WriteLine(@"
    _______
   /      /
  /      //
 /______//
(______(/
");
                break;
            case 2:
                Terminal.WriteLine("Get a ...");
                break;
            case 3:
                Terminal.WriteLine("");
                break;
            default:
                Debug.LogError("Forbidden Case");
                break;
        }
    }

    void HandleMainMenu(string input)
    {
        bool isValidLevel = (input == "1" || input =="2" || input == "3");
        if (isValidLevel)
        {
            level = int.Parse(input);
            AskForPassword();
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

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        SetRandomPassword();

        Terminal.ClearScreen();
        Terminal.WriteLine("Enter your Password. Hint: " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("invalid level");
                break;
        }
    }
}
