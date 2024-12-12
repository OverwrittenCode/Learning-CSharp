using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class SilentObserver : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You choose to remain a silent observer, watching events unfold without intervening.")
                                    .Say("As time passes, you reflect on the choices you didn't make.")
                                    .SudoUser("Sometimes, silence speaks louder than words.")
                                    .Say("Your role as a silent observer leaves you with a sense of contemplation.")
                                    .Say("THE END - Silent Observer Ending")
                                    .Init();
}
