namespace AdventureGames.Scenes.Endings;

internal sealed class InformationGathering : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You focus on gathering information to understand the situation better.")
            .Say("Your efforts uncover hidden truths and provide valuable insights.")
            .SudoUser("Knowledge is the key to making informed decisions.")
            .Say("Your information-gathering skills become highly sought after.")
            .Say("THE END - Information Gathering Ending")
            .Init();
    }
}
