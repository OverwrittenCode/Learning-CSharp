namespace AdventureGames.Scenes.Endings;

internal sealed class AllianceAnalysis : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You analyse the alliances and relationships between different factions.")
            .Say("Your insights help navigate the complex political landscape.")
            .SudoUser("Understanding alliances is key to strategic decision-making.")
            .Say("Your alliance analysis becomes a valuable asset to those in power.")
            .Say("THE END - Alliance Analysis Ending")
            .Init();
    }
}
