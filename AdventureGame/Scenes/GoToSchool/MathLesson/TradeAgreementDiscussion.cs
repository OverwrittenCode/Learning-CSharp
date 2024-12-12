using AdventureGame.Helpers;
using AdventureGame.Scenes.Endings;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class TradeAgreementDiscussion : BaseScene
{
    public TradeAgreementDiscussion()
    {
        Choices.Add(new("Express concern about unfair trade practices", () => new EconomicActivism()));
        Choices.Add(new("Discuss benefits of free trade", () => new GlobalEconomyDiscussion()));
    }

    public override void Play()
        => new ConversationBuilder().SudoTeacher("Trade agreements shape the global economy. Let's discuss their impact.")
                                    .Say("The teacher explains various trade agreements and their effects on different countries.")
                                    .SudoUser("How do these agreements affect our local businesses?")
                                    .Init();
}
