using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class QuietObservation : BaseScene
{
    public QuietObservation()
    {
        Choices.Add(new("Continue observing", () => new ClassInsights()));
        Choices.Add(new("Whisper to a classmate", () => new ClassroomWhispers()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You decide to stay quiet and observe the class discussion.")
                                    .Say("You notice tension in some students' faces as they speak.")
                                    .Say("Others seem hesitant to share their thoughts.")
                                    .SudoUser("(thinking) There's more going on here than just economics...")
                                    .Init();
}
