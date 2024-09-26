using Common.Utils;

namespace AdventureGames.Scenes;

/// <summary>
/// Represents a branching decision in a scene, providing a description and the logic for transitioning
/// to the next scene based on the player's choice.
/// </summary>
/// <param name="Description">
/// The text displayed to the player in the console, explaining this choice as part of the <see cref="BaseScene.Choices"/>.
/// </param>
/// <param name="NextSceneFactory">
/// A function that returns the next <see cref="BaseScene"/> to transition to when the player selects this choice.
/// <para>This function defines the actions and consequences that follow the choice.</para>
/// </param>
internal readonly record struct Choice(string Description, Func<BaseScene> NextSceneFactory) { }

/// <summary>
/// <para>
/// <b><see cref="BaseScene"/></b>
/// Represents the scenes in the adventure game.
/// It provides a structure for presenting choices to the player and transitioning between scenes.
/// </para>
/// </summary>
internal abstract class BaseScene
{
    protected List<Choice> Choices { get; } = [];

    /// <summary>
    /// Begins the scene. This should lead up to the <see cref="Choices"/> shown afterwards.
    /// </summary>
    public abstract void Play();

    /// <summary>
    /// Presents the <see cref="Choices"/> to the player and returns the next scene based on their selection.
    /// </summary>
    /// <returns>The next scene, or null if <see cref="Choices"/> is empty.</returns>
    public BaseScene? GetNextScene()
    {
        const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        Utils.ContinueOnEnter();

        if (Choices.Count == 0)
        {
            return null;
        }

        if (Choices.Count == 1)
        {
            return Choices[0].NextSceneFactory.Invoke();
        }

        Utils.CentreMessage("CHOICES");

        for (var i = 0; i < Choices.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{Letters[i]}]: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(Choices[i].Description);
        }

        ConsoleUtils.HighlightConsoleLine("\nEnter your choice", Constants.UserNoticeColour);

        Console.CursorVisible = true;

        var choiceLetters = Letters[..Choices.Count];

        while (true)
        {
            Console.Write("> ");
            if (
                Console.ReadLine()?.ToUpper().Trim() is string input and { Length: 1 }
                && choiceLetters.IndexOf(input) is int index and >= 0
            )
            {
                Console.CursorVisible = false;

                Console.ResetColor();
                Console.WriteLine();

                return Choices[index].NextSceneFactory.Invoke();
            }
        }
    }
}
