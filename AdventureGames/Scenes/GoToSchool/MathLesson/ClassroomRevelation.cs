using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ClassroomRevelation : BaseScene
{
    public ClassroomRevelation()
    {
        Choices.Add(new("Support the teacher", () => new UndergroundResistance()));
        Choices.Add(new("Report to authorities", () => new AuthoritiesInvolved()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser(
                "I've noticed some strange things going on. Is there something we should know?"
            )
            .Say("The class falls silent. The teacher looks shocked.")
            .SudoTeacher("I... I don't know what you mean.")
            .Say("You see a mix of fear and relief in some of your classmates' eyes.")
            .SudoUser("I think it's time we talked about what's really happening.")
            .Init();
    }
}
