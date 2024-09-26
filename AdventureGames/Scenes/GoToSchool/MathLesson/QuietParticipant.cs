using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class QuietParticipant : BaseScene
{
    public QuietParticipant()
    {
        Choices.Add(new("Document the protest", () => new CitizenJournalist()));
        Choices.Add(new("Reflect on the movement", () => new SilentObserver()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You join the protest, blending into the crowd.")
            .Say("While others chant loudly, you observe and listen intently.")
            .SudoUser("(thinking) There's so much passion here. I wonder where this will lead...")
            .Init();
    }
}
