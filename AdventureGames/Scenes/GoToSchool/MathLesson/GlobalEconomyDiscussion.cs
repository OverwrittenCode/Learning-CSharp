namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class GlobalEconomyDiscussion : BaseScene
{
    public GlobalEconomyDiscussion()
    {
        Choices.Add(new("Ask about international relations", () => new WorldPolitics()));
        Choices.Add(new("Inquire about global market trends", () => new MarketAnalysis()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher(
                "The global economy is interconnected. What happens in one country affects others..."
            )
            .Say(
                "The teacher explains concepts like trade wars, currency fluctuations, and global market trends."
            )
            .SudoUser("It's overwhelming to think about how complex the world economy is.")
            .Init();
    }
}
