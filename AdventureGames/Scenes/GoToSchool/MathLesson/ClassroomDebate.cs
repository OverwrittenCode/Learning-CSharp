using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ClassroomDebate : BaseScene
{
    public ClassroomDebate()
    {
        Choices.Add(new("Stand your ground", () => new HeatedArgument()));
        Choices.Add(new("Back down", () => new Detention()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("The classroom erupts into a heated debate about the economic situation.")
            .SudoTeacher("Let's keep this civil, class.")
            .SudoUser("But sir, we can't ignore the reality of what's happening!")
            .Init();
    }
}
