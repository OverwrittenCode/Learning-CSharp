namespace AdventureGames.Scenes.Endings;

internal sealed class DataSkepticism : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("Your questioning of economic forecasts leads you to become a data analyst.")
            .Say("You develop new methods for interpreting and validating economic data.")
            .SudoUser("Numbers can lie. It's our job to find the truth behind the statistics.")
            .Say("Your critical approach to data analysis influences economic policy and research.")
            .Say("THE END - Data Skeptic Ending")
            .Init();
    }
}
