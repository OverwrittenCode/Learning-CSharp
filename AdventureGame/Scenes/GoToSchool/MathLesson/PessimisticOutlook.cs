using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class PessimisticOutlook : BaseScene
{
    public PessimisticOutlook()
    {
        Choices.Add(new("Discuss preparedness strategies", () => new EconomicPreparedness()));
        Choices.Add(new("Question the forecast's accuracy", () => new DataSkepticism()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("Some economists predict challenging times ahead. Let's examine these forecasts.")
                                    .Say("The teacher presents some concerning economic predictions and their potential impacts.")
                                    .SudoUser("This is quite worrying. How can we prepare for such scenarios?")
                                    .Init();
}
