using AdventureGame.Helpers;
using AdventureGame.Scenes.GoToSchool.MathLesson;
using AdventureGame.Scenes.StayAtHome;

namespace AdventureGame.Scenes.GoToSchool;

internal sealed class SchoolArrival : BaseScene
{
    public SchoolArrival()
    {
        Choices.Add(new("Go to math class", () => new MathClass()));
        Choices.Add(new("Return home", () => new JustAssumptions()));
    }

    public override void Play()
    {
        Utils.CentreMessage("Your story is changing.");

        new ConversationBuilder().Say("You arrive at school, the atmosphere feels tense.")
                                 .Say("Students are whispering in hushed tones, some looking worried.")
                                 .SudoUser("Something feels off...")
                                 .Say("You see your friend Jack in the distance, looking anxious.")
                                 .Init();
    }
}
