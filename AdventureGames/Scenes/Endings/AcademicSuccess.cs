namespace AdventureGames.Scenes.Endings;

internal sealed class AcademicSuccess : BaseScene
{
    public override void Play()
    {
        new ConversationBuilder()
            .Say("Your dedication to your studies leads to academic success.")
            .Say("You excel in your exams and gain recognition for your achievements.")
            .SudoUser("Hard work and perseverance always pay off.")
            .Say("Your academic success opens doors to new opportunities.")
            .Say("THE END - Academic Success Ending")
            .Init();
    }
}
