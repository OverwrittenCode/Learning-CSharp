namespace AdventureGames.Scenes.Endings;

internal sealed class GreenEconomyFocus : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You choose to focus on sustainable industries and the green economy.")
            .Say(
                "Your studies and later career revolve around renewable energy and sustainable development."
            )
            .SudoUser("We can build a prosperous future that doesn't compromise our planet.")
            .Say("Your work contributes to significant advancements in sustainable technologies.")
            .Say("THE END - Green Economy Pioneer Ending")
            .Init();
    }
}
