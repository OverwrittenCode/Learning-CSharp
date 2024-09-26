using AdventureGames.Scenes.Endings;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class WorldPolitics : BaseScene
{
    public WorldPolitics()
    {
        Choices.Add(new("Discuss international alliances", () => new AllianceAnalysis()));
        Choices.Add(new("Ask about trade agreements", () => new TradeAgreementDiscussion()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoTeacher("International relations play a crucial role in the global economy.")
            .Say("The teacher explains how political decisions affect economic outcomes worldwide.")
            .SudoUser("It's incredible how a decision in one country can impact the entire world.")
            .Init();
    }
}
