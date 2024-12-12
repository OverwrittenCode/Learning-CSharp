using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class ProtectedIgnorance : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You choose to stay out of your parents' dangerous activities.")
                                    .Say("They agree to keep you in the dark about the resistance.")
                                    .Say("Life goes on, but you're always aware of the underlying tension.")
                                    .SudoUser("Sometimes, ignorance really is bliss... right?")
                                    .Say("You live a relatively normal life, always wondering what could have been.")
                                    .Say("THE END - Protected Ignorance Ending")
                                    .Init();
}
