using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class ClassInsights : BaseScene
{
    public ClassInsights()
    {
        Choices.Add(new("Share your observations", () => new ClassroomRevelation()));
        Choices.Add(new("Keep your insights to yourself", () => new SilentKnowledge()));
    }

    public override void Play()
        => new ConversationBuilder().Say("As you continue observing, you start to notice patterns.")
                                    .Say("Some students seem to know more than they're letting on.")
                                    .Say("The teacher keeps glancing nervously at the door.")
                                    .SudoUser("(thinking) Something's not right here...")
                                    .Say("You've gathered some interesting insights. What will you do with this information?")
                                    .Init();
}
