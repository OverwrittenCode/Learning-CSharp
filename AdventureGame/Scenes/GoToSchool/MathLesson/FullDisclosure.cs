using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class FullDisclosure : BaseScene
{
    public FullDisclosure()
    {
        Choices.Add(new("Join the resistance", () => new UndergroundResistance()));
        Choices.Add(new("Seek protection", () => new WitnessProtection()));
    }

    public override void Play()
        => new ConversationBuilder().SudoUser("I know about the resistance meetings in the basement. I want to understand what's really happening.")
                                    .Say("The teacher's expression shifts from shock to resignation.")
                                    .SudoTeacher("This is a dangerous situation. We need to talk somewhere private.")
                                    .Init();
}
