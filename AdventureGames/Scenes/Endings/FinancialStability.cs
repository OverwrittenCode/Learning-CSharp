namespace AdventureGames.Scenes.Endings;

internal sealed class FinancialStability : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "You focus on building long-term financial stability through careful planning and saving."
            )
            .Say("Your disciplined approach helps you weather economic uncertainties.")
            .SudoUser("It's not about getting rich quick, it's about building a secure future.")
            .Say("Your financial wisdom becomes valuable to friends and family seeking advice.")
            .Say("THE END - Financial Stability Guru Ending")
            .Init();
    }
}
