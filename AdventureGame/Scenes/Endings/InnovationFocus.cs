using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class InnovationFocus : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You dedicate yourself to fostering innovation in various sectors of the economy.")
                                    .Say("Your efforts lead to breakthroughs in technology, energy, and sustainable practices.")
                                    .SudoUser("Innovation is the key to solving our biggest challenges.")
                                    .Say("Your work inspires a new generation of problem-solvers and entrepreneurs.")
                                    .Say("THE END - Innovation Champion Ending")
                                    .Init();
}
