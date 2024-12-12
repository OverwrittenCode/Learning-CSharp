using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class LocalEconomyDiscussion : BaseScene
{
    public LocalEconomyDiscussion()
    {
        Choices.Add(new("Ask about helping local businesses", () => new CommunityInitiative()));
        Choices.Add(new("Inquire about job prospects", () => new CareerGuidance()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("The local economy has been hit hard by the crisis. Let's look at some specifics...")
                                    .Say("The teacher explains the impact on local businesses and employment rates.")
                                    .SudoUser("I had no idea it was affecting our town so much.")
                                    .Init();
}
