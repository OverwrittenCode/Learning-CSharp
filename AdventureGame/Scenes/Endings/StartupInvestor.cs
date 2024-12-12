using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class StartupInvestor : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You decide to invest in innovative startups, taking calculated risks.")
                                    .Say("Some investments fail, but others become wildly successful.")
                                    .SudoUser("High risk, high reward. It's a thrilling journey.")
                                    .Say("Your success allows you to become a mentor for new entrepreneurs.")
                                    .Say("THE END - Venture Capitalist Ending")
                                    .Init();
}
