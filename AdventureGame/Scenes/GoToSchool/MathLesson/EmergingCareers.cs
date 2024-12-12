using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class EmergingCareers : BaseScene
{
    public EmergingCareers()
    {
        Choices.Add(new("Focus on technology careers", () => new TechCareerPath()));
        Choices.Add(new("Explore sustainable industries", () => new GreenEconomyFocus()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("The job market is rapidly evolving. Let's look at some emerging career fields.")
                                    .Say("The teacher discusses new industries and job opportunities.")
                                    .SudoUser("These sound exciting, but also challenging to prepare for.")
                                    .Init();
}
