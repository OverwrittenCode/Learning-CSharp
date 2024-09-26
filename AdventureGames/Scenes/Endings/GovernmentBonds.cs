namespace AdventureGames.Scenes.Endings;

internal sealed class GovernmentBonds : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("You decide to invest in government bonds, prioritizing safety over high returns.")
            .Say(
                "Over the years, your cautious approach provides stability during economic turbulence."
            )
            .SudoUser("It may not be exciting, but it's reliable.")
            .Say("Your financial prudence sets an example for others in uncertain times.")
            .Say("THE END - Stable Investor Ending")
            .Init();
    }
}
