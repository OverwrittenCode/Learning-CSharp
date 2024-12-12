using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class EconomicRecovery : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You become involved in developing and implementing economic recovery strategies.")
                                    .Say("Your ideas help businesses adapt and thrive in the changing economic landscape.")
                                    .SudoUser("With the right approach, we can turn challenges into opportunities.")
                                    .Say("Your efforts contribute to a robust and resilient economic rebound.")
                                    .Say("THE END - Economic Recovery Architect Ending")
                                    .Init();
}
