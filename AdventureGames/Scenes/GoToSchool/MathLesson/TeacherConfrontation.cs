namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class TeacherConfrontation : BaseScene
{
    public TeacherConfrontation()
    {
        Choices.Add(new("Demand answers", () => new IntenseInterrogation()));
        Choices.Add(new("Ask for guidance", () => new TeacherGuidance()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You stay behind after class, determined to get answers from your teacher.")
            .SudoUser(
                "Sir, I know something's going on. The resistance, the secret meetings... What's really happening?"
            )
            .Say("The teacher's face pales, clearly caught off guard by your direct approach.")
            .SudoTeacher(
                "I... I don't know what you're talking about. Where did you hear these things?"
            )
            .Init();
    }
}
