namespace AdventureGames.Scenes.Endings;

internal sealed class LocalEconomyRevolution : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "You initiate a community-based economic system that prioritizes local businesses and resources."
            )
            .Say("The model proves successful and spreads to other communities.")
            .SudoUser("By focusing on our local strengths, we can build a more resilient economy.")
            .Say("Your innovative approach revitalizes struggling towns and cities.")
            .Say("THE END - Local Economy Revolutionary Ending")
            .Init();
    }
}
