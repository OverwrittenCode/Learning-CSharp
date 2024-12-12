using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class MarketSpeculator : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You become adept at predicting market trends and making quick trades.")
                                    .Say("Your bold strategies lead to both big wins and significant losses.")
                                    .SudoUser("The market is unpredictable, but that's what makes it exciting.")
                                    .Say("Your experiences shape you into a seasoned financial expert.")
                                    .Say("THE END - Market Guru Ending")
                                    .Init();
}
