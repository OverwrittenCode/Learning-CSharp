using AdventureGames.Entities.Collectables;
using AdventureGames.Scenes.StayAtHome;

namespace AdventureGames.Scenes.GoToSchool;

internal sealed class JourneyToSchool : BaseScene
{
    public JourneyToSchool()
    {
        Choices.Add(
            new(
                "Give money to the homeless, continue to school",
                () =>
                {
                    Utils.CentreMessage("Your story is changing.");

                    new ConversationBuilder()
                        .SudoUser(
                            "I'm so sorry, I only have a shilling to spare, it's all I have... really."
                        )
                        .Say("You give them one shilling")
                        .Perform(() => Game.User.GiveMoney(Game.John, 0.12M))
                        .SudoJohn(
                            "God bless you young man! Your parents taught you well. It's ok, I understand."
                        )
                        .Say("You walk away from them...")
                        .SudoUser("God I wish I could sacrifice and donate more")
                        .Init();

                    return new SchoolArrival();
                }
            )
        );

        Choices.Add(
            new(
                "Steal their money, continue to school",
                () =>
                {
                    new ConversationBuilder()
                        .Say("You see their money stash: just under 3 shillings.")
                        .SudoJohn("What do you think you're doing?")
                        .Say("You steal all their money and make a run for it.")
                        .Perform(() => Game.User.StealMoney(Game.John))
                        .SudoJohn("HEY, COME BACK HERE!")
                        .Say(
                            $"{Game.John} trips you up, all of your money is scattered across the ground."
                        )
                        .Perform(() =>
                        {
                            Game.John.StealMoney(Game.User);

                            Homework? homework = Game.User.GetItem<Homework>();

                            if (homework != null)
                            {
                                Game.User.Inventory.Remove(homework);
                            }
                        })
                        .Pause()
                        .Say("Tell me, was it really worth it?")
                        .Pause()
                        .Say("Wow... you now have no money for lunch.")
                        .Init();

                    return new SchoolArrival();
                }
            )
        );

        Choices.Add(
            new(
                "Return Home",
                () =>
                {
                    new ConversationBuilder()
                        .SudoUser("Sorry, got something important to do!")
                        .SudoJohn("*scoffs* Kids these days...")
                        .Say("You swiftly jog back home,")
                        .Say("You are panting; out of breath.")
                        .SudoFather(
                            "School set on fire son?",
                            "I've never seen this one run so fast!"
                        )
                        .Say("You continue gasping for air.")
                        .SudoFather("Come on now, leave some oxygen for the rest of us!")
                        .Pause()
                        .Say("You cringe at his miserable attempt at a joke and walk back inside.")
                        .SudoFather("One day he will laugh at my jokes...")
                        .Pause()
                        .Say("Your mother approaches you and whispers:")
                        .SudoMother("You'll get used to it.")
                        .SudoUser("Does it get easier?")
                        .SudoMother(
                            "No, his jokes get worse by the day.",
                            "Yesterday, he said something weird about some toaster and an egg?"
                        )
                        .SudoUser("...")
                        .Init();

                    return new JustAssumptions();
                }
            )
        );
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say(
                """
                You are on your way to school, and see a homeless person.
                They look like they haven't slept in days nor had any shelter in the long lasting winter of the year.
                The wind is very strong, and they cling onto their sign for desperation.
                """
            )
            .SudoJohn(
                "Hey kid, can you spare any change? Could really need some right now for some food and a drink."
            )
            .Init();
    }
}
