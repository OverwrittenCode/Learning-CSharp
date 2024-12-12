using AdventureGame.Entities.Collectables;
using AdventureGame.Helpers;
using AdventureGame.Scenes.GoToSchool;
using AdventureGame.Scenes.StayAtHome;

namespace AdventureGame.Scenes;

internal sealed class LetterReceived : BaseScene
{
    public LetterReceived()
    {
        Choices.Add(
            new(
                "Go to School",
                () =>
                {
                    new ConversationBuilder().SudoUser("Mother, Father, I think I should go to school.").SudoFather("See you later.").SudoMother("Stay safe out there.").Init();

                    return new JourneyToSchool();
                }
            )
        );
        Choices.Add(
            new(
                "Stay at Home",
                () =>
                {
                    new ConversationBuilder()
                       .SudoUser("Just another meaningless letter, my teacher goes on about homework yet can't even count the rules properly... I'm staying at home.")
                       .SudoFather("Don't disrespect your teacher like that. I'm sure he just miscounted after a busy day.")
                       .SudoMother("Listen to your father, you ought to take the first rule of this household seriously.", "Wouldn't want to go to sleep hungry now would we?")
                       .SudoUser("Well I'm staying here anyways.")
                       .Init();

                    return new JustAssumptions();
                }
            )
        );
    }

    public override void Play() => Game.User.InteractWithItem<Letter>();
}
