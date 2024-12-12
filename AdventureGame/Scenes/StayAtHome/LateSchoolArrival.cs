using AdventureGame.Helpers;
using AdventureGame.Scenes.GoToSchool.Consequences;

namespace AdventureGame.Scenes.StayAtHome;

internal sealed class LateSchoolArrival : BaseScene
{
    public LateSchoolArrival()
    {
        Choices.Add(new("Take the long route", () => new ClassroomSneaking()));
        Choices.Add(
            new(
                "Take the short route",
                () =>
                {
                    new ConversationBuilder().SudoUser("Let's take that route")
                                             .Say("You turn away from Jack and walk forwards")
                                             .SudoTeacher($"{Game.User.ToString().ToUpperInvariant()}! This is the last time you act up in school!")
                                             .SudoUser("Wait what? Why am I the only one in trouble?")
                                             .SudoTeacher("Is there a ghost among us?")
                                             .SudoUser("No! J-")
                                             .Pause()
                                             .Say("You turn around and Jack is gone. He thought you meant the long route by \"that\" route.")
                                             .Init();

                    return new PrincipalOffice();
                }
            )
        );
    }

    public override void Play()
    {
        Utils.CentreMessage("Your story is changing.");

        new ConversationBuilder().Say("You and Jack arrive at school late.")
                                 .Say("The hallways are empty, and you can hear classes in session.")
                                 .SudoJack("What should we do? We're really late.")
                                 .SudoUser("We need to be careful. We don't want to get into more trouble.")
                                 .Say("You hear footsteps approaching from around the corner.")
                                 .SudoJack(
                                      "We need to think of something!",
                                      "I know a shorter route to class, but they might see us.",
                                      "But if we take the longer route we might be in big trouble!"
                                  )
                                 .Init();
    }
}
