using AdventureGame.Helpers;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class AtticDiscovery : BaseScene
{
    public AtticDiscovery()
    {
        Choices.Add(new("Confront your parents", () => new FamilyConfrontation()));
        Choices.Add(new("Keep the discovery a secret", () => new SecretKeeper()));
    }

    public override void Play()
        => new ConversationBuilder().Say("In the attic, you find a hidden compartment.")
                                    .Say("Inside, there are documents about a resistance movement.")
                                    .Say("You recognize your parents' handwriting on some of the papers.")
                                    .SudoUser("My parents... are part of the resistance?")
                                    .Say("You hear someone coming up the stairs.")
                                    .Say("Quickly, you have to decide what to do with this information.")
                                    .Init();
}
