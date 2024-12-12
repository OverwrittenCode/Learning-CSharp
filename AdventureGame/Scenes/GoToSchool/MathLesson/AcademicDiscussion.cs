using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class AcademicDiscussion : BaseScene
{
    public AcademicDiscussion()
    {
        Choices.Add(new("Continue the discussion", () => new DeepLearning()));
        Choices.Add(new("Ask about real-world applications", () => new RealWorldEconomics()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("The stock market crash has led to decreased consumer spending, affecting local businesses.")
                                    .SudoTeacher("Excellent point. Can you elaborate on that?")
                                    .Say("The class seems engaged, and you feel confident.")
                                    .Say("You notice Jack giving you a thumbs up from across the room.")
                                    .Init();
}
