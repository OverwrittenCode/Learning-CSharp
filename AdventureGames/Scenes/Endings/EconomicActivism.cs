namespace AdventureGames.Scenes.Endings;

internal sealed class EconomicActivism : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "Your concerns about unfair trade practices lead you to become an economic activist."
            )
            .Say("You organize campaigns and raise awareness about global economic inequalities.")
            .SudoUser("We can create a fairer world economy, one step at a time.")
            .Say("Your efforts contribute to policy changes and increased public awareness.")
            .Say("THE END - Economic Activist Ending")
            .Init();
    }
}
