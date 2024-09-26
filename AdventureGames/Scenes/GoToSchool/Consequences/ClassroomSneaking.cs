using AdventureGames.Entities.Collectables;
using AdventureGames.Scenes.GoToSchool.MathLesson;

namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class ClassroomSneaking : BaseScene
{
    public override void Play()
    {
        Choice quiteObservationChoice =
            new("Quietly observe the class", () => new QuietObservation());

        var conversation = new ConversationBuilder()
            .Say("You carefully open the classroom door, trying not to make a sound.")
            .Say("The teacher's back is turned as they write on the blackboard.")
            .SudoTeacher(
                "So when we look at this point on the graph where the tangent touches the circle's radius..."
            )
            .SudoUser("(thinking) If I'm quick, maybe I can slip into my seat...")
            .Say("You quietly take your seat.")
            .SudoTeacher(
                "...we can clearly see that it meets at a right angle.",
                $"Ah, {Game.User} I completely forgot about you. Your homework?"
            )
            .Pause();

        if (Game.Teacher.GetItem<Homework>() is not null)
        {
            conversation
                .SudoUser("Sir, I have given you my homework already, remember?")
                .SudoTeacher("Let's see...")
                .Say("The teacher find your homework that you handed in.")
                .SudoTeacher(
                    "How could I possibly forget? Forgive me. Nice job on getting everything correct, I marked it earlier."
                );

            Choices.Add(quiteObservationChoice);
        }
        else if (!Game.User.GiveItem<Homework>(Game.Teacher))
        {
            conversation
                .SudoTeacher($"{Game.User.ToString().ToUpperInvariant()}! Where is your homework?")
                .SudoUser("Well, you see...")
                .Say("The teacher looks at you sceptically.")
                .Pause()
                .SudoTeacher("I'm waiting for an explanation.");

            Choices.Add(
                new(
                    "Come clean",
                    () =>
                    {
                        new ConversationBuilder()
                            .SudoUser(
                                "Look, I'll admit it. I don't know where it is, and I know I deserve a detention for it."
                            )
                            .SudoTeacher("You know the drill.")
                            .Init();

                        return new Detention();
                    }
                )
            );
            Choices.Add(
                new(
                    "Make up a complete lie",
                    () =>
                    {
                        new ConversationBuilder()
                            .SudoUser(
                                "I was on the way here when a pigeon decided to take a number 2 on my homework sheet. Really sir..."
                            )
                            .SudoTeacher("Do you think I'm stupid?")
                            .Say("You stay quiet")
                            .Init();

                        return new SeriousConsequences();
                    }
                )
            );
        }
        else
        {
            conversation
                .Perform(() => Game.Teacher.InteractWithItem<Homework>(true))
                .SudoTeacher("Nice job, all the studying is paying off I can see.");

            Choices.Add(quiteObservationChoice);
        }

        conversation.Init();
    }
}
