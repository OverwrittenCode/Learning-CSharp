namespace AdventureGames.Scenes.Endings;

internal sealed class LimitedResistance : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You agree to help your family in small, low-risk ways.")
            .Say("Over time, your cautious contributions make a difference.")
            .SudoUser("Every little bit helps, and we're staying safe.")
            .Say("Your family appreciates your support while respecting your boundaries.")
            .Say("THE END - Cautious Supporter Ending")
            .Init();
    }
}
