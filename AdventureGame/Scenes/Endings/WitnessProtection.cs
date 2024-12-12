using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class WitnessProtection : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("Your knowledge puts you at risk, and you're placed in a witness protection program.")
                                    .Say("You leave behind your old life, starting anew with a different identity.")
                                    .SudoUser("It's not easy, but at least I'm safe now.")
                                    .Say("Your information helps authorities, but you wonder about those you left behind.")
                                    .Say("THE END - New Identity Ending")
                                    .Init();
}
