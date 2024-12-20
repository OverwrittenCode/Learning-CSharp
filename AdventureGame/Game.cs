using AdventureGame.Entities.Collectables;
using AdventureGame.Entities.Humanoids;
using AdventureGame.Helpers;
using AdventureGame.Scenes;
using Common.Utils;

namespace AdventureGame;

internal sealed class Game
{
    /// <summary>
    ///     The person playing the game
    /// </summary>
    public static readonly Player User;

    /// <summary>
    ///     Mother of <see cref="User" />
    /// </summary>
    public static readonly Player Mother;

    /// <summary>
    ///     Father of <see cref="User" />.
    /// </summary>
    public static readonly Player Father;

    /// <summary>
    ///     A homeless man on the streets.
    /// </summary>
    public static readonly Player John;

    /// <summary>
    ///     Math teacher of <see cref="User" />.
    /// </summary>
    public static readonly Player Teacher;

    /// <summary>
    ///     Friend of <see cref="User" />.
    /// </summary>
    public static readonly Player Jack;

    /// <summary>
    ///     Head Teacher of the School.
    /// </summary>
    public static readonly Player Principle;

    private static BaseScene? CurrentScene;

    /// <summary>
    ///     <inheritdoc cref="Utils.TypewriterEffect(String)" /><br />
    ///     The foreground colour is set to <see cref="Constants.NarratorColour" />.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    public static void Say(string message) => ConsoleUtils.HighlightConsoleLine(message, Constants.NarratorColour, Utils.TypewriterEffect);

    /// <inheritdoc cref="Say(String)" />
    /// <br />
    /// <param name="speed">Affects the default characters displayed per iteration.</param>
    public static void Say(string message, double speed) => ConsoleUtils.HighlightConsoleLine(message, Constants.NarratorColour, msg => Utils.TypewriterEffect(msg, speed));

    /// <summary>
    ///     Begins the scene sequence.
    /// </summary>
    public static void Start()
    {
        while (CurrentScene != null)
        {
            Console.WriteLine();
            CurrentScene.Play();
            CurrentScene = CurrentScene.GetNextScene();
        }

        Say("Thanks for playing. Press any key to close...");
        Console.ReadLine();
    }

    static Game()
    {
        CurrentScene = new Intro();

        Console.CursorVisible = false;
        Utils.CentreMessage("ADVENTURE GAME");

        Say("What is your name?");
        Console.CursorVisible = true;

        while (true)
        {
            Console.Write("> ");
            if (Console.ReadLine() is { Length: > 0 } name)
            {
                User = new(name, "You", 1.30M, ConsoleColor.DarkBlue);
                Console.CursorVisible = false;
                break;
            }
        }

        Mother = new("Mother", "Your Mother", 2.70M, ConsoleColor.Green);
        Father = new("Father", "Your Father", 2.10M, ConsoleColor.Magenta);
        John = new("John", "A homeless man on the street", 0.15M, ConsoleColor.DarkGray);
        Teacher = new("Teacher", "Your maths teacher", 5M, ConsoleColor.Blue);
        Jack = new("Jack", "Your friend", 1.70M, ConsoleColor.DarkMagenta);
        Principle = new("Principle", "Your head teacher", 4M, ConsoleColor.DarkYellow);

        if (User.Name == "Kyle Thapar")
        {
            const string Message = "The creator cannot play.";

            new ConversationBuilder().Say(Message)
                                     .SudoMother(Message)
                                     .SudoFather(Message)
                                     .SudoJohn(Message)
                                     .SudoTeacher(Message)
                                     .SudoJack(Message)
                                     .SudoPrinciple(Message)
                                     .Say("[ALL]: /ban Kyle Thapar")
                                     .Say("THE END - Creator Banned Ending")
                                     .Init();

            Environment.Exit(0);
        }

        User.Inventory.Add(new Homework());

        Console.WriteLine();
        Say($"Welcome, {User.Name}!");
    }
}
