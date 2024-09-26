using AdventureGames.Scenes.Endings;
using AdventureGames.Scenes.StayAtHome;

namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class UnexpectedDiscovery : BaseScene
{
    public UnexpectedDiscovery()
    {
        Choices.Add(new("Investigate further", () => new SecretInvestigation()));
        Choices.Add(new("Pretend you didn't see anything", () => new InnerConflict()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                "While cleaning up, you accidentally knock over a stack of papers on the teacher's desk."
            )
            .Say("As you pick them up, you notice strange symbols and coded messages.")
            .SudoUser("What in the world...?")
            .Say("You hear footsteps approaching the classroom.")
            .Init();
    }
}
