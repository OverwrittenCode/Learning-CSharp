namespace AdventureGames.Scenes.Endings;

internal sealed class FamilySafety : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "You convince your family to prioritize safety over involvement in the resistance."
            )
            .Say("Together, you find ways to stay informed while avoiding direct conflict.")
            .SudoUser("We can make a difference in safer ways.")
            .Say("Your family becomes a quiet support network for others seeking a middle ground.")
            .Say("THE END - Family First Ending")
            .Init();
    }
}
