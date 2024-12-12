using AdventureGame.Helpers;

namespace AdventureGame.Scenes.Endings;

internal sealed class MentorshipInitiative : BaseScene
{
    public override void Play()
        => new ConversationBuilder().Say("You launch a mentorship program, connecting students with experienced professionals.")
                                    .Say("The initiative provides guidance and real-world insights to aspiring young minds.")
                                    .SudoUser("Seeing students grow with their mentors is incredibly rewarding.")
                                    .Say("Your program creates lasting connections and shapes future careers.")
                                    .Say("THE END - Mentorship Champion Ending")
                                    .Init();
}
