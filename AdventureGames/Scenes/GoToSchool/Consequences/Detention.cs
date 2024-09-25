namespace AdventureGames.Scenes.GoToSchool.Consequences;

/// <inheritdoc/>
public sealed class Detention : BaseScene
{
    /// <summary>
    /// Wow. What did you do to end up here?
    /// </summary>
    public Detention()
    {
        Choices.Add(new Choice("Use the time to study", () => new ProductiveDetention()));
        Choices.Add(new Choice("Try to sneak out", () => new EscapeAttempt()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("Everyone has left the room for free time.")
            .SudoTeacher("I hope you'll use this time to reflect on your actions.")
            .Pause()
            .Say(
                "10 minutes has passed. The teacher leaves after marking all the homework, closing the door behind them."
            )
            .Init();
    }
}
