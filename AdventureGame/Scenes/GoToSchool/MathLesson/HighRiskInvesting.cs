using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class HighRiskInvesting : BaseScene
{
    public HighRiskInvesting()
    {
        Choices.Add(new("Explore startup investments", () => new StartupInvestor()));
        Choices.Add(new("Discuss market speculation", () => new MarketSpeculator()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("High-risk investments offer the potential for significant returns, but also carry greater danger of loss.")
                                    .Say("The teacher outlines various high-risk investment strategies and their potential outcomes.")
                                    .SudoUser("The potential gains are exciting, but the risks seem daunting.")
                                    .Init();
}
