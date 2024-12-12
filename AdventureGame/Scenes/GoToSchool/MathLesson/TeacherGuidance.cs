using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class TeacherGuidance : BaseScene
{
    public TeacherGuidance()
    {
        Choices.Add(new("Ask to join the resistance", () => new UndergroundResistance()));
        Choices.Add(new("Request more information", () => new InformationGathering()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("I'm confused and scared. Can you help me understand what's happening?")
                                    .Say("The teacher's expression softens, showing concern.")
                                    .SudoTeacher("These are difficult times. Let me explain what I can...")
                                    .Say("The teacher begins to cautiously share some information.")
                                    .Init();
}
