using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class FurtherDiscussion : BaseScene
{
    public FurtherDiscussion()
    {
        Choices.Add(new("Ask about government policies", () => new PolicyDiscussion()));
        Choices.Add(new("Inquire about personal finance", () => new FinanceAdvice()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("I have more questions about the economic situation.")
                                    .SudoTeacher("I'm glad you're so interested. What would you like to know?")
                                    .Init();
}
