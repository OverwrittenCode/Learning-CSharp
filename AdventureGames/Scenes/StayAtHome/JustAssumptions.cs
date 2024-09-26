namespace AdventureGames.Scenes.StayAtHome;

/// <inheritdoc/>
internal sealed class JustAssumptions : BaseScene
{
    /// <summary>
    /// Description.
    /// </summary>
    public JustAssumptions()
    {
        Choices.Add(new Choice("Explore the house", () => new HouseExploration()));
        Choices.Add(new Choice("Sneak out to meet Jack", () => new MeetJack()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You've decided to stay at home, ignoring the school's letter.")
            .Say("As the day progresses, you start to feel restless.")
            .SudoUser("Maybe I should have gone to school after all...")
            .Say("You hear your parents talking in hushed tones downstairs.")
            .Say("Something about the conversation makes you uneasy.")
            .Init();
    }
}
