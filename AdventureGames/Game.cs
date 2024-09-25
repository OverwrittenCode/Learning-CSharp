using AdventureGames.Entities.Collectables;
using AdventureGames.Entities.Humanoids;
using AdventureGames.Scenes;
using Common.Utils;

namespace AdventureGames;

public class Game
{
    private static BaseScene? CurrentScene;

    /// <summary>
    /// The person playing the game
    /// </summary>
    public static readonly Player User;

    /// <summary>
    /// Mother of <see cref="User"/>
    /// </summary>
    public static readonly Player Mother;

    /// <summary>
    /// Father of <see cref="User"/>
    /// </summary>
    public static readonly Player Father;

    /// <summary>
    /// A homeless man on the streets
    /// </summary>
    public static readonly Player John;

    /// <summary>
    /// Math teacher of <see cref="User"/>
    /// </summary>
    public static readonly Player Teacher;

    /// <summary>
    /// Friend of <see cref="User"/>
    /// </summary>
    public static readonly Player Jack;

    /// <summary>
    /// ???
    /// </summary>
    public static readonly Player UnknownUser;

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
            if (Console.ReadLine() is string name and { Length: > 0 })
            {
                User = new Player(name, "You", 1.30M, ConsoleColor.DarkBlue);
                Console.CursorVisible = false;
                break;
            }
        }

        Mother = new("Mother", "Your Mother", 2.70M, ConsoleColor.Green);
        Father = new("Father", "Your Father", 2.10M, ConsoleColor.Magenta);
        John = new("John", "A homeless man on the street", 0.15M, ConsoleColor.DarkGray);
        Teacher = new("Teacher", "Your maths teacher", 5M, ConsoleColor.Blue);
        Jack = new("Jack", "Your friend", 1.70M, ConsoleColor.DarkMagenta);
        UnknownUser = new("Unknown", "???", 0M, ConsoleColor.DarkYellow);

        User.Inventory.Add(new Homework());

        Console.WriteLine();
        Say($"Welcome, {User.Name}!");
    }

    /// <summary>
    /// <inheritdoc cref="Utils.TypewriterEffect(String)"/><br/>
    /// The foreground colour is set to <see cref="Constants.NarratorColour"/>.
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    public static void Say(string message)
    {
        ConsoleUtils.HighlightConsoleLine(
            message,
            Constants.NarratorColour,
            Utils.TypewriterEffect
        );
    }

    /// <inheritdoc cref="Say(String)"/><br/>
    /// <param name="speed">Affects the default characters displayed per iteration.</param>
    public static void Say(string message, double speed)
    {
        ConsoleUtils.HighlightConsoleLine(
            message,
            Constants.NarratorColour,
            (string message) => Utils.TypewriterEffect(message, speed)
        );
    }

    /// <summary>
    /// Begins the scene sequence.
    /// </summary>
    public static void Start()
    {
        while (CurrentScene != null)
        {
            Console.WriteLine();

            // modify
            Utils.CentreMessage($"Scene: [{CurrentScene.GetType().Name}]");

            CurrentScene.Play();
            CurrentScene = CurrentScene.GetNextScene();
        }
    }
}
