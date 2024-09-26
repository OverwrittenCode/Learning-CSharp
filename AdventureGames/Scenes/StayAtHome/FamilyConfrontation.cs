using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class FamilyConfrontation : BaseScene
{
    public FamilyConfrontation()
    {
        Choices.Add(new("Join the resistance", () => new FamilyResistance()));
        Choices.Add(new("Refuse to get involved", () => new FamilyRift()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("Mom, Dad, I know about the resistance. I found the documents.")
            .SudoMother("Oh dear... We never wanted you to find out like this.")
            .SudoFather("Son, we need to talk. This is very serious.")
            .Init();
    }
}
