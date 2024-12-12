using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class OptimisticOutlook : BaseScene
{
    public OptimisticOutlook()
    {
        Choices.Add(new("Discuss potential innovations", () => new InnovationFocus()));
        Choices.Add(new("Explore economic recovery strategies", () => new EconomicRecovery()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("While challenges exist, there are also opportunities for growth and improvement.")
                                    .Say("The teacher outlines potential positive outcomes and strategies for economic recovery.")
                                    .SudoUser("It's encouraging to hear about these possibilities.")
                                    .Init();
}
