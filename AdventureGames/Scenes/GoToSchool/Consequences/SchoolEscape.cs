namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class SchoolEscape : BaseScene
{
    public SchoolEscape()
    {
        Choices.Add(new Choice("Go home", () => new GoHome()));
        Choices.Add(new Choice("Explore the city", () => new CityExploration()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You manage to slip past the teacher and out of the school.")
            .Say("The streets are eerily quiet.")
            .SudoUser("I'm free, but now what?")
            .Say("You feel a mix of excitement and fear.")
            .Init();
    }
}
