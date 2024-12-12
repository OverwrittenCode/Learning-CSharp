using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.Consequences;

internal sealed class SeriousConsequences : BaseScene
{
    public SeriousConsequences()
    {
        Choices.Add(new("Accept punishment", () => new Detention()));
        Choices.Add(new("Run away", () => new SchoolEscape()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("I've had enough of your lies. This is a serious matter.")
                                    .Say("The teacher looks furious and disappointed.")
                                    .SudoTeacher("You'll be serving detention for a week, and I'm calling your parents.")
                                    .SudoUser("(thinking) I've really messed up this time...")
                                    .Init();
}
