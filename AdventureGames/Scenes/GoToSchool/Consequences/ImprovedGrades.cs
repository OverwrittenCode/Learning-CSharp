namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class ImprovedGrades : BaseScene
{
    public ImprovedGrades()
    {
        Choices.Add(new("Go home", () => new GoHome()));
        Choices.Add(new("Stay to help clean up", () => new UnexpectedDiscovery()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("Your productive use of detention time pays off.")
            .Say("The teacher returns, looking surprised at your progress.")
            .SudoTeacher("Well done. You're free to go now.")
            .SudoUser("Thank you, sir.")
            .Say("As you pack up, you notice the teacher looking worried.")
            .Init();
    }
}
