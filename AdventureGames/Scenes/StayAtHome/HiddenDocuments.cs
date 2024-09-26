namespace AdventureGames.Scenes.StayAtHome;

internal sealed class HiddenDocuments : BaseScene
{
    public HiddenDocuments()
    {
        Choices.Add(new("Confront your parents", () => new ParentalConfrontation()));
        Choices.Add(new("Keep investigating in secret", () => new SecretResearch()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You carefully examine the old documents.")
            .Say("They contain coded messages, maps with marked locations, and lists of names.")
            .SudoUser("This looks like... a resistance network?")
            .Say("You recognize your parents' handwriting on some of the papers.")
            .Say("Suddenly, you hear footsteps coming down the basement stairs.")
            .Init();
    }
}
