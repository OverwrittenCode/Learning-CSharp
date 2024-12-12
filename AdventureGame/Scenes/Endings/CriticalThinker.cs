using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class CriticalThinker : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("Your critical thinking skills lead you to question the status quo.")
                                    .Say("You become a thought leader, challenging conventional wisdom.")
                                    .SudoUser("We must always question and seek the truth.")
                                    .Say("Your insights inspire others to think critically and act wisely.")
                                    .Say("THE END - Critical Thinker Ending")
                                    .Init();
}
