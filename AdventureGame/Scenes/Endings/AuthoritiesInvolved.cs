using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class AuthoritiesInvolved : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You decide to report the suspicious activities to the authorities.")
                                    .Say("An investigation is launched, and the school is turned upside down.")
                                    .Say("As the truth comes to light, you realize the situation is more complex than you thought.")
                                    .SudoUser("I hope I made the right choice...")
                                    .Say("The future remains uncertain, but you've set major changes in motion.")
                                    .Say("THE END - Whistle blower Ending")
                                    .Init();
}
