using System;
using System.Collections.Generic;
using Assets.scripts;
using UnityEngine;
using UnityEngine.Analytics;
using Random = System.Random;

public class Hacker : MonoBehaviour
{
    private const string GREETING_MESSAGE = "Hi, Player!";
    public const string MENU_HINT = "Enter \"menu\" to get back to menu.\n";

    [Header("Observed fields")] 
    private int chosenLevel;
    private string password = "";
    private int levelCount;

    private List<Level> Levels = new List<Level>{
        Level.CreateLevel("Press 1 for sex shop \n",
            "╔══╗\n" +
            "╚╗╔╝\n" +
            "╔╝(¯`v´¯)\n" +
            "╚══`.¸.[You]\n", new string[]{"dildo", "cockring", "flogger", "cuffs", "rope"}),
        Level.CreateLevel("Press 2 for Tantric Workshop \n",
            ".♥/(,\")\\.(\".)♥★\n" +
            "..★/♥\\♥/█\\♥★\n" +
            ".♥_| |__| |_ ♥\n", new string[]{"tissues", "scents", "altar", "musicbox", "condoms"}),
        Level.CreateLevel("Press 3 for Swingers Orgy \n",
            "▀██▀─▄███▄─▀██─██▀██▀▀█\n" +
            "─██─███─███─██─██─██▄█\n" +
            "─██─▀██▄██▀─▀█▄█▀─██▀█\n" +
            "▄██▄▄█▀▀▀─────▀──▄██▄▄█\n", new string[]{ "chlamydia", "pregnancy", "relationship", "jealousy", "ecstasy" })
    };

    private Screen screen = Screen.MainMenu;

    enum Screen
    {
        MainMenu,
        Password,
        WinScreen
    }

    void Start()
    {
        levelCount = Levels.Count;

        ShowMainMenu(GREETING_MESSAGE);
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu(GREETING_MESSAGE);
            return;
        }

        switch (screen)
        {
            case Screen.MainMenu:
                ManageMainMenuInput(input);
                break;
            case Screen.Password:
                ManagePasswordInput(input);
                break;
        }
    }

    private void ShowMainMenu(string greetingMessage)
    {
        screen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine(
                greetingMessage + "\n" +
               "What would you like to hack? \n" +
               "\n" +
                ListLevelDescriptions() +
               "\n" +
                MENU_HINT +
               "Enter your selection:");
    }

    private string ListLevelDescriptions()
    {
        string result = "";

        for (int i = 0; i < Levels.Count; i++)
        {
            result += Levels[i].LevelDescription;
        }

        return result;
    }

    private void ShowPasswordScreen()
    {
        Terminal.WriteLine("Chosen Level " + chosenLevel);
        Terminal.WriteLine(MENU_HINT);
        Terminal.WriteLine("Hint word is: " + ScramblePassword());
        Terminal.WriteLine("Enter password:");
    }

    private void ShowWinScreen()
    {
        screen = Screen.WinScreen;
        Terminal.ClearScreen();
        Terminal.WriteLine("Congratulations, you broke the password!");
        Terminal.WriteLine(Levels[chosenLevel -1].WinItem);
        Terminal.WriteLine("Type menu to start again. : ");
    }

    private void ManageMainMenuInput(string input)
    {
        int parsedInput;

        if (int.TryParse(input, out parsedInput)
            && parsedInput <= levelCount
            && parsedInput >= 0)
        {
            chosenLevel = parsedInput;
            StartGame();
        }
        else
        {
            ShowMainMenu("Choose valid location to Hack.");
        }
    }

    private void ManagePasswordInput(string input)
    {
        if (input == password)
        {
            ShowWinScreen();
        }
        else
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("Thats wrong password, try again.");
            ShowPasswordScreen();
        }
    }

    private void StartGame()
    {
        screen = Screen.Password;
        password = Levels[chosenLevel - 1].Words[new Random().Next(0, Levels[chosenLevel -1].Words.Length)];
        Terminal.ClearScreen();
        ShowPasswordScreen();
    }

    private string ScramblePassword()
    {
        var passwordInChars = password.ToCharArray();
        for (int i = 0; i < password.Length; i++)
        {
            var rnd = new Random().Next(0, password.Length);
            var tempLetter = passwordInChars[rnd];
            passwordInChars[rnd] = passwordInChars[i];
            passwordInChars[i] = tempLetter;
        }

        return new string(passwordInChars);
    }
}
