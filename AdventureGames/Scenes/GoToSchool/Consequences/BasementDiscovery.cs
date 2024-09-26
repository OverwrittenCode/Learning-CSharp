using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class BasementDiscovery : BaseScene
{
    public BasementDiscovery()
    {
        Choices.Add(
            new(
                "Continue listening",
                () =>
                {
                    new ConversationBuilder()
                        .Say("You accidentally make a noise, and they turn to see you.")
                        .SudoUser("I... I can explain...")
                        .SudoTeacher("How much did you hear?")
                        .Say("You notice a mix of fear and determination in their eyes.")
                        .SudoUser("Um...")
                        .Pause()
                        .SudoTeacher("You know what? How about you join us.")
                        .SudoUser("Wait, what?")
                        .Pause()
                        .SudoTeacher(
                            "Join us. Forget everything else. We can all make a difference. Even if that means fighting with fear."
                        );

                    return new UndergroundResistance();
                }
            )
        );
        Choices.Add(new("Leave and pretend you saw nothing", () => new InnerConflict()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You cautiously make your way down to the school basement.")
            .Say("In a dimly lit corner, you see a group of teachers huddled around a radio.")
            .SudoUser("(whispering) What's going on down here?")
            .Say("You overhear fragments of a coded message being transmitted.")
            .SudoTeacher(
                "The resistance needs more support. We can't let the government continue like this."
            )
            .SudoPrinciple("But it's dangerous. If we're caught...")
            .Init();
    }
}
