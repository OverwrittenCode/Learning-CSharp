using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class CitizenJournalist : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You decide to become a citizen journalist, reporting on the events around you.")
                                    .Say("Your reports bring attention to important issues and spark public debate.")
                                    .SudoUser("The truth must be told, no matter the cost.")
                                    .Say("Your work as a citizen journalist makes a significant impact on society.")
                                    .Say("THE END - Citizen Journalist Ending")
                                    .Init();
}
