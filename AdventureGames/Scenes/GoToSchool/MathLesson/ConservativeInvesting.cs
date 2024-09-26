using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ConservativeInvesting : BaseScene
{
    public ConservativeInvesting()
    {
        Choices.Add(new("Discuss long-term savings", () => new FinancialStability()));
        Choices.Add(new("Ask about government bonds", () => new GovernmentBonds()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher(
                "Conservative investing focuses on preserving capital and minimizing risk."
            )
            .Say("The teacher explains various low-risk investment options.")
            .SudoUser("How can we balance safety with the need for growth?")
            .Init();
    }
}
