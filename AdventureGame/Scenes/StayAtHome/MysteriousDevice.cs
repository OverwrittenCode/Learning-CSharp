using AdventureGame.Helpers;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class MysteriousDevice : BaseScene
{
    public MysteriousDevice()
    {
        Choices.Add(new("Try to operate the device", () => new DeviceActivation()));
        Choices.Add(new("Leave it alone and go upstairs", () => new ParentalConfrontation()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You approach the strange device and remove the sheet.")
                                    .Say("It looks like a complex radio with unfamiliar symbols and dials.")
                                    .SudoUser("This doesn't look like any radio I've ever seen...")
                                    .Say("You notice a note attached to it: 'DO NOT TOUCH - DANGER'")
                                    .Say("Your curiosity is piqued, but you're also wary of the warning.")
                                    .Init();
}
