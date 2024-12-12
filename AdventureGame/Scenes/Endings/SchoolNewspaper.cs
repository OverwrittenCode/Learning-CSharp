using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class SchoolNewspaper : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You join the school newspaper and start reporting on important issues.")
                                    .Say("Your articles bring attention to matters that affect the student body.")
                                    .SudoUser("The pen is mightier than the sword.")
                                    .Say("Your work with the school newspaper earns you respect and recognition.")
                                    .Say("THE END - School Newspaper Ending")
                                    .Init();
}
