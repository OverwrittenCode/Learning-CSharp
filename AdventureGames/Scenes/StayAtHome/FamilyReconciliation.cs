using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.StayAtHome;

internal sealed class FamilyReconciliation : BaseScene
{
    public FamilyReconciliation()
    {
        Choices.Add(new("Join the family's efforts", () => new FamilyResistance()));
        Choices.Add(new("Suggest a compromise", () => new FamilyCompromise()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You take a deep breath and approach your parents.")
            .SudoUser("I'm sorry for reacting that way. Can we talk about this?")
            .SudoMother("Of course, dear. We're sorry too for keeping this from you.")
            .SudoFather("Let's sit down and discuss this as a family.")
            .Init();
    }
}
