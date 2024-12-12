using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class UneasyNormalcy : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You try to go back to your normal life, pretending you know nothing.")
                                    .Say("But the knowledge of the resistance and your parents' involvement haunts you.")
                                    .Say("Every conversation feels loaded with hidden meanings.")
                                    .SudoUser("How long can I keep pretending?")
                                    .Say("You live in a state of constant tension, caught between two worlds.")
                                    .Say("THE END - Uneasy Normalcy Ending")
                                    .Init();
}
