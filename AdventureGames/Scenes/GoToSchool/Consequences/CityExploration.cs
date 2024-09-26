namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class CityExploration : BaseScene
{
    public CityExploration()
    {
        Choices.Add(new("Return to school", () => new LateReturn()));
        Choices.Add(new("Continue exploring", () => new CityAdventure()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You find yourself in the bustling city streets.")
            .Say("The sights and sounds of the city are both exciting and overwhelming.")
            .SudoUser("I've never seen the city like this during school hours...")
            .Init();
    }
}
