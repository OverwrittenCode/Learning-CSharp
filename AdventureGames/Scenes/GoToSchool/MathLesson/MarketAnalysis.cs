namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class MarketAnalysis : BaseScene
{
    public MarketAnalysis()
    {
        Choices.Add(new("Discuss investment strategies", () => new InvestmentDiscussion()));
        Choices.Add(new("Ask about economic forecasts", () => new EconomicForecast()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher("Let's analyze current market trends and their implications...")
            .Say("The teacher explains various market indicators and their significance.")
            .SudoUser("It's fascinating how interconnected everything is.")
            .Init();
    }
}
