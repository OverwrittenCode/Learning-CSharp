using AdventureGame.Helpers;

namespace AdventureGame.Scenes.GoToSchool.Consequences;

internal sealed class ProductiveDetention : BaseScene
{
    public ProductiveDetention()
    {
        Choices.Add(new("Continue studying", () => new ImprovedGrades()));
        Choices.Add(new("Investigate the strange noises", () => new BasementDiscovery()));
    }

    public override void Play()
        => new ConversationBuilder().Say("You decide to use your detention time productively.")
                                    .Say("As you study, you start to understand the material better.")
                                    .SudoUser("Maybe this isn't so bad after all...")
                                    .Say("Suddenly, you hear strange noises coming from below.")
                                    .Say("It sounds like it's coming from the basement.")
                                    .Init();
}
