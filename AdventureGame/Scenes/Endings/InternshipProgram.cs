using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class InternshipProgram : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You successfully establish an internship program with local businesses.")
                                    .Say("The program provides valuable experience for students and fresh ideas for companies.")
                                    .SudoUser("This bridge between education and industry is exactly what we needed.")
                                    .Say("Your initiative becomes a model for other schools, boosting the local economy.")
                                    .Say("THE END - Education-Industry Partnership Ending")
                                    .Init();
}
