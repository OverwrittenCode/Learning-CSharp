using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ControversialDiscussion : BaseScene
{
    public ControversialDiscussion()
    {
        Choices.Add(new("Stand by your opinion", () => new ClassroomDebate()));
        Choices.Add(new("Apologize and back down", () => new Detention()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("I think the government is hiding the true extent of the crisis.")
            .Say("The class falls silent. The teacher looks alarmed.")
            .SudoTeacher("That's a very serious accusation. Where did you hear this?")
            .Say("You feel all eyes on you, waiting for your response.")
            .Init();
    }
}
