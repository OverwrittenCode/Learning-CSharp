using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class UniversityPreparation : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You dedicate yourself to preparing for university.")
                                    .Say("Your hard work pays off as you're accepted into a prestigious economics program.")
                                    .SudoUser("I'm ready to dive deep into understanding our complex economic systems.")
                                    .Say("Your academic journey sets you on a path to become an influential economist.")
                                    .Say("THE END - Academic Economist Ending")
                                    .Init();
}
