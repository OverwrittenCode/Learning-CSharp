using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class DeepInvestigation : BaseScene
{
    public DeepInvestigation()
    {
        Choices.Add(new("Contact the resistance", () => new UndergroundResistance()));
        Choices.Add(new("Confront your parents", () => new FamilyConfrontation()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You spend hours poring over the documents you found.")
                                    .Say("Piecing together the information, you start to understand the scale of the resistance.")
                                    .SudoUser("This is bigger than I ever imagined...")
                                    .Init();
}
