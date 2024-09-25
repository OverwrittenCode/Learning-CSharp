namespace AdventureGames.Scenes;

/// <inheritdoc/>
public sealed class Outro : BaseScene
{
    /// <summary>
    /// The last scene of the game
    /// </summary>
    public Outro() { }

    public override void Play()
    {
        Game.Say("Thanks for playing. Press any key to close...");

        Console.ReadLine();
    }
}
