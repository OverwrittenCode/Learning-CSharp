using AdventureGames.Scenes.StayAtHome;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ClassDismissal : BaseScene
{
    public ClassDismissal()
    {
        Choices.Add(new("Go to next class", () => new NextClass()));
        Choices.Add(new("Return home", () => new JustAssumptions()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("The bell rings, signaling the end of class.")
            .SudoTeacher(
                "Remember to review today's lesson. It's more important than you might think."
            )
            .SudoUser("(thinking) That was... intense. What now?")
            .Init();
    }
}
