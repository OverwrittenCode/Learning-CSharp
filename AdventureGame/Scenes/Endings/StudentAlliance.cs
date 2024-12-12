using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class StudentAlliance : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You and Jack form a student alliance to address school and community issues.")
                                    .Say("Your group grows, becoming a positive force for change.")
                                    .SudoUser("Together, we can make our voices heard and create real change.")
                                    .Say("The alliance's successes inspire other students to get involved.")
                                    .Say("THE END - Student Leader Ending")
                                    .Init();
}
