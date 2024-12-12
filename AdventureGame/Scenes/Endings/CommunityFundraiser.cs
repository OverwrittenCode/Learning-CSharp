using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class CommunityFundraiser : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You organize a community fundraiser to support a local cause.")
                                    .Say("Your efforts bring the community together and raise significant funds.")
                                    .SudoUser("Together, we can make a difference.")
                                    .Say("Your community fundraiser becomes an annual event, making a lasting impact.")
                                    .Say("THE END - Community Fundraiser Ending")
                                    .Init();
}
