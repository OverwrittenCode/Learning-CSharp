using AdventureGame.Entities.Collectables;
using AdventureGame.Helpers;
using AdventureGame.Scenes.GoToSchool.Consequences;

namespace AdventureGame.Scenes.GoToSchool.MathLesson;

internal sealed class MathClass : BaseScene
{
    public override void Play()
    {
        ConversationBuilder? conversation = new ConversationBuilder().Say("You enter the math class. The teacher looks stern.")
                                                                     .SudoTeacher("Alright class, homework out. Now.")
                                                                     .Say("You reach for your bag, feeling a sense of dread...");

        var homework = Game.User.GetItem<Homework>();

        if (homework is null)
        {
            conversation.SudoUser("Huh? Where is it?")
                        .SudoTeacher("No homework again? This is becoming a habit.")
                        .SudoUser("I... I must have forgotten it at home.")
                        .Say("The teacher looks disappointed and angry.")
                        .SudoTeacher(
                             "You will serve your detention today after this class as it will be held in here.",
                             "Looks like it's your lucky day - you'll be keeping me company at free time."
                         )
                        .Pause()
                        .SudoUser("Free time? That's only 5 minutes anyways...")
                        .SudoTeacher("Class, for today your free time will be extended to 30 minutes.");

            Choices.Add(new("Accept the punishment", () => new Detention()));
            Choices.Add(new("Argue with the teacher", () => new ChallengeTheTeacher()));
        }
        else
        {
            conversation.SudoUser("Ah yes, here it is")
                        .Perform(
                             () =>
                             {
                                 Game.User.GiveItem<Homework>(Game.Teacher);
                                 Game.Teacher.InteractWithItem<Homework>(true);
                             }
                         )
                        .Pause()
                        .SudoTeacher("Well done. I see someone has been paying attention.")
                        .Say("You feel a sense of relief and accomplishment.");

            Choices.Add(new("Continue with the class", () => new ClassDiscussion()));
        }

        conversation.Init();
    }
}
