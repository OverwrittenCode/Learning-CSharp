using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class DeviceShutdown : BaseScene
{
    public DeviceShutdown()
    {
        Choices.Add(new("Confess to your parents", () => new ParentalConfrontation()));
        Choices.Add(new("Pretend nothing happened", () => new UneasyNormalcy()));
    }

    public override void Play()
        => new ConversationBuilder().Say("Panicking, you quickly shut down the device.")
                                    .Say("The basement falls silent, but your mind is racing.")
                                    .SudoUser("What have I stumbled into?")
                                    .Say("You hear your parents calling for you from upstairs.")
                                    .Say("You're left wondering if they heard anything and what consequences await.")
                                    .Init();
}
