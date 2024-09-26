namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ClassDiscussion : BaseScene
{
    public ClassDiscussion()
    {
        Choices.Add(new("Participate actively", () => new ClassParticipation()));
        Choices.Add(new("Stay quiet and observe", () => new QuietObservation()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher("Now, let's discuss the economic implications of the current crisis.")
            .Say("The class seems tense. Some students exchange worried glances.")
            .SudoTeacher(
                "Who can tell me how the stock market crash has affected local businesses?"
            )
            .Init();
    }
}
