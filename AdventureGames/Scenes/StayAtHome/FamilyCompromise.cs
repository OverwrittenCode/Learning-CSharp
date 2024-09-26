using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class FamilyCompromise : BaseScene
{
    public FamilyCompromise()
    {
        Choices.Add(new("Agree to limited involvement", () => new LimitedResistance()));
        Choices.Add(new("Focus on family safety", () => new FamilySafety()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser(
                "I understand your position, but I'm worried about our safety. Can we find a middle ground?"
            )
            .SudoFather("We appreciate your concern. What do you suggest?")
            .SudoMother("We're open to ideas that keep us together and safe.")
            .Init();
    }
}
