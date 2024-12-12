using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class SolutionBrainstorming : BaseScene
{
    public SolutionBrainstorming()
    {
        Choices.Add(new("Propose innovative ideas", () => new InnovativeSolutions()));
        Choices.Add(new("Suggest traditional approaches", () => new TraditionalApproaches()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("Let's think about potential solutions to our economic challenges.")
                                    .Say("The class begins to brainstorm ideas, from conservative to radical.")
                                    .SudoUser("What if we tried something completely new?")
                                    .Init();
}
