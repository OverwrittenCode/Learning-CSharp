namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class CityAdventure : BaseScene
{
    public CityAdventure()
    {
        Choices.Add(new("Return to school", () => new LateReturn()));
        Choices.Add(new("Go home", () => new GoHome()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "You spend the day exploring the city, witnessing the effects of the economic crisis first hand."
            )
            .Say("You see long lines at soup kitchens and closed shops with 'For Sale' signs.")
            .SudoUser("I had no idea things were this bad...")
            .Init();
    }
}
