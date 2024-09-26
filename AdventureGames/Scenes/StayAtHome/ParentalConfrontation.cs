using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class ParentalConfrontation : BaseScene
{
    public ParentalConfrontation()
    {
        Choices.Add(new("Ask to join the resistance", () => new FamilyResistance()));
        Choices.Add(new("Demand to be left out of it", () => new ProtectedIgnorance()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "Your parents find you in the basement, surrounded by evidence of their secret activities."
            )
            .SudoFather("We never wanted you to find out this way...")
            .SudoMother("We were trying to protect you.")
            .SudoUser("Protect me from what? What's really going on?")
            .Say("Your parents exchange a worried look before your father sighs deeply.")
            .SudoFather("It's time we told you the truth about the resistance...")
            .Init();
    }
}
