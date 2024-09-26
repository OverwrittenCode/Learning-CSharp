using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class HeatedArgument : BaseScene
{
    public HeatedArgument()
    {
        Choices.Add(new("Apologize and back down", () => new Detention()));
        Choices.Add(new("Continue arguing", () => new PrincipalOffice()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("The debate escalates into a heated argument.")
            .SudoTeacher("That's enough! This kind of behavior is unacceptable.")
            .SudoUser("But sir, the truth needs to be heard!")
            .Say("The class watches in stunned silence as the situation intensifies.")
            .Init();
    }
}
