using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.Consequences;

internal sealed class SecretUnveiled : BaseScene
{
    public SecretUnveiled()
    {
        Choices.Add(new("Join the resistance", () => new UndergroundResistance()));
        Choices.Add(new("Pretend you saw nothing", () => new InnerConflict()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You overhear the conversation in the basement.")
                                    .SudoTeacher("The resistance needs more support. We can't let the government continue like this.")
                                    .Say("Principal: But it's dangerous. If we're caught...")
                                    .Say("You accidentally make a noise, and they turn to see you.")
                                    .SudoUser("I... I can explain...")
                                    .SudoTeacher("How much did you hear?")
                                    .Say("You notice a mix of fear and determination in their eyes.")
                                    .Init();
}
