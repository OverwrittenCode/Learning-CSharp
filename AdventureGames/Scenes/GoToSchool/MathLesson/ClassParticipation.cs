namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ClassParticipation : BaseScene
{
    public ClassParticipation()
    {
        Choices.Add(new("Share your thoughts", () => new ControversialDiscussion()));
        Choices.Add(new("Keep it academic", () => new AcademicDiscussion()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("I'd like to share my thoughts on this, sir.")
            .SudoTeacher("Go ahead, we're listening.")
            .Say("The class turns to look at you.")
            .Say("You feel a mix of nervousness and excitement.")
            .Init();
    }
}
