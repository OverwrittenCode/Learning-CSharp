using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class FamilyResistance : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You confront your parents about their involvement in the resistance.")
                                    .SudoUser("I know what you've been doing. I want to help.")
                                    .SudoMother("We wanted to protect you, but you're old enough to understand now.")
                                    .SudoFather("It's dangerous, but we're fighting for a better future.")
                                    .Say("Together, your family becomes a cornerstone of the resistance movement.")
                                    .Say("THE END - Family Resistance Ending")
                                    .Init();
}
