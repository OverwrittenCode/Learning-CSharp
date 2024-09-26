namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class EconomicForecast : BaseScene
{
    public EconomicForecast()
    {
        Choices.Add(new("Discuss optimistic scenarios", () => new OptimisticOutlook()));
        Choices.Add(new("Explore pessimistic predictions", () => new PessimisticOutlook()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher(
                "Economic forecasting is a complex but essential part of planning for the future."
            )
            .Say("The teacher presents various economic models and their predictions.")
            .SudoUser("How accurate have these forecasts been in the past?")
            .Init();
    }
}
