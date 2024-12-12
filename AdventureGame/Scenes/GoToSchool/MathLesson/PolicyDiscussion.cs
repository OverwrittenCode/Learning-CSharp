using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class PolicyDiscussion : BaseScene
{
    public PolicyDiscussion()
    {
        Choices.Add(new("Express concern about current policies", () => new ControversialDiscussion()));
        Choices.Add(new("Ask about potential solutions", () => new SolutionBrainstorming()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("How are government policies affecting our current economic situation?")
                                    .SudoTeacher("That's a complex question. Let's break it down...")
                                    .Say("The teacher begins explaining various economic policies and their impacts.")
                                    .Init();
}
