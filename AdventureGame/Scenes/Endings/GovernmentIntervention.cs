using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class GovernmentIntervention : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You advocate for government intervention to address economic issues.")
                                    .Say("Your efforts lead to policies that promote stability and growth.")
                                    .SudoUser("Sometimes, government action is necessary to ensure the common good.")
                                    .Say("Your work in government intervention makes a significant impact.")
                                    .Say("THE END - Government Intervention Ending")
                                    .Init();
}
