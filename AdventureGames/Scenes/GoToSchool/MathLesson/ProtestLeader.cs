using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class ProtestLeader : BaseScene
{
    public ProtestLeader()
    {
        Choices.Add(new("Escalate the protest", () => new MassMovement()));
        Choices.Add(new("Negotiate with administration", () => new StudentRepresentative()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You step forward, rallying your fellow students.")
            .SudoUser("We demand transparency and action! Our future is at stake!")
            .Say("The crowd cheers, and you feel the weight of leadership on your shoulders.")
            .Init();
    }
}
