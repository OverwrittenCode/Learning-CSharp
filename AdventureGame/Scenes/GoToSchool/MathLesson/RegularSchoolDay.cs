using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class RegularSchoolDay : BaseScene
{
    public RegularSchoolDay()
    {
        Choices.Add(new("Focus on studies", () => new AcademicSuccess()));
        Choices.Add(new("Reflect on earlier discussions", () => new CriticalThinker()));
    }

    public override void Play()
        => new ConversationBuilder().Say("The rest of the school day passes relatively normally.")
                                    .Say("You attend your classes, but your mind keeps drifting back to the earlier discussions.")
                                    .SudoUser("(thinking) Everything seems so normal, yet so much is happening beneath the surface.")
                                    .Init();
}
