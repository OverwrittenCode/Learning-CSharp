using AdventureGames.Scenes.Endings;
using AdventureGames.Scenes.GoToSchool.Consequences;

namespace AdventureGames.Scenes.GoToSchool.MathLesson;

internal sealed class EvasiveTactics : BaseScene
{
    public EvasiveTactics()
    {
        Choices.Add(new("Continue to deflect", () => new Detention()));
        Choices.Add(new("Admit partial knowledge", () => new PartialConfession()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("I'm not sure what you mean. I just overheard some rumours...")
            .Say("The teacher looks sceptical but seems unsure how to proceed.")
            .SudoTeacher("Rumours can be dangerous. Be careful what you repeat.")
            .Init();
    }
}
