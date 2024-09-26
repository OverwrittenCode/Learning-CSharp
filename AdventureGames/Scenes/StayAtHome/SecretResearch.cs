using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class SecretResearch : BaseScene
{
    public SecretResearch()
    {
        Choices.Add(new("Contact the resistance", () => new UndergroundResistance()));
        Choices.Add(new("Inform the authorities", () => new AuthoritiesInvolved()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("Over the next few days, you secretly study the documents you found.")
            .Say("You piece together information about a nationwide resistance movement.")
            .Say("It seems your parents play a crucial role in coordinating local efforts.")
            .SudoUser("I had no idea things were this serious...")
            .Say("You realize you have a big decision to make.")
            .Init();
    }
}
