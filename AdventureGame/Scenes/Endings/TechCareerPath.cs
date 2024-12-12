using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class TechCareerPath : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You decide to pursue a career in technology, recognizing its growing importance.")
                                    .Say("Over the years, you develop skills in programming and artificial intelligence.")
                                    .SudoUser("Technology can be a powerful tool for positive change.")
                                    .Say("Your innovations contribute to solving complex economic and social issues.")
                                    .Say("THE END - Tech Innovator Ending")
                                    .Init();
}
