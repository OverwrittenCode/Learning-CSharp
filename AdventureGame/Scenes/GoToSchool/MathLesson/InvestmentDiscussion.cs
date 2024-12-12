using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class InvestmentDiscussion : BaseScene
{
    public InvestmentDiscussion()
    {
        Choices.Add(new("Ask about safe investments", () => new ConservativeInvesting()));
        Choices.Add(new("Inquire about high-risk options", () => new HighRiskInvesting()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("In times of economic uncertainty, investment strategies become crucial.")
                                    .Say("The teacher explains various investment options and their potential outcomes.")
                                    .SudoUser("How can we balance risk and security in our investments?")
                                    .Init();
}
