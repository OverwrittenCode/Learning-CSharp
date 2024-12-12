using AdventureGame.Helpers;
using AdventureGame.Scenes.StayAtHome;

namespace AdventureGame.Scenes.GoToSchool.Consequences;

internal sealed class GoHome : BaseScene
{
    public GoHome()
    {
        Choices.Add(new("Explain to your parents", () => new ParentalConfrontation()));
        Choices.Add(new("Hide the truth", () => new SecretKeeper()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You decide to go home instead of returning to school.")
                                    .Say("As you approach your house, you see your parents' car in the driveway.")
                                    .SudoUser("Oh no, they're home early. What am I going to tell them?")
                                    .Init();
}
