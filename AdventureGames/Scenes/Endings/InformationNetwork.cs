namespace AdventureGames.Scenes.Endings;

internal sealed class InformationNetwork : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You establish a network of informants to gather and share critical information.")
            .Say("Your network becomes a valuable resource for those seeking the truth.")
            .SudoUser("Knowledge is power, and we must use it wisely.")
            .Say("Your information network plays a crucial role in shaping events.")
            .Say("THE END - Information Network Ending")
            .Init();
    }
}
