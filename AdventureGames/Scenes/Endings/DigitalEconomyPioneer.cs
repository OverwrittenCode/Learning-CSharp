namespace AdventureGames.Scenes.Endings;

internal sealed class DigitalEconomyPioneer : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You spearhead the development of a revolutionary digital currency system.")
            .Say("Your innovation transforms the way people think about and use money.")
            .SudoUser("Technology can democratize finance and empower individuals.")
            .Say("Your work ushers in a new era of economic possibilities.")
            .Say("THE END - Digital Economy Pioneer Ending")
            .Init();
    }
}
