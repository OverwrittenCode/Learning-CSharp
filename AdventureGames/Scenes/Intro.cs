using AdventureGames.Entities.Collectables;
using AdventureGames.Scenes.StayAtHome;

namespace AdventureGames.Scenes;

internal sealed class Intro : BaseScene
{
    public Intro()
    {
        Choices.Add(new("Yes", () => new LetterRecieved()));
        Choices.Add(
            new(
                "No",
                () =>
                {
                    new ConversationBuilder()
                        .SudoUser("Yeah yeah yeah, I'll read it in my room")
                        .SudoFather("You better")
                        .Say("You go upstairs into your room")
                        .Pause()
                        .SudoFather(
                            "Kid thinks I was born yesterday, I better check what he's doing"
                        )
                        .Say("He quietly goes upstairs to overhear your rant.")
                        .Pause()
                        .SudoUser(
                            "He thinks I'm actually reading that? Pft.",
                            "I'd rather watch Jack throw another frisbee at that bald maths teacher."
                        )
                        .SudoFather("What was that?")
                        .SudoUser("Nothing, I swear.")
                        .SudoFather("Watch your tone young man.")
                        .Init();

                    // maybe drop the letter, not sure yet

                    return new JustAssumptions();
                }
            )
        );
    }

    public override void Play()
    {
        Utils.CentreMessage("Backstory");

        new ConversationBuilder()
            .Say(
                """
                The Great Depression, 1929.  
                London, United Kingdom.

                You are a teenager during The Great Depression.  
                Economy rates are falling, the world has gone into uproar.

                You've been hearing stories of businesses closing down, and families struggling to make ends meet.  
                Even your neighbours have stopped visiting as frequently.

                You've just finished eating your favourite food — something simple, but comforting.
                Unfortunately, it feels like this may be the last decent meal you'll have for a while.

                It would be a die for to have that same slice of pie you, your mother and father shared between the 3 of you last night.
                Your parents have been rationing what little they can save.

                As you sit in silence, the weight of uncertainty grows heavier.
                """
            )
            .SudoMother("Don't worry. This will all be over soon.")
            .Say(
                """
                They don't sound too sure, though.
                """
            )
            .Perform(() =>
            {
                Game.Father.Inventory.Add(new Letter());
                Game.Father.GiveItem<Letter>(Game.User);
            })
            .SudoFather("It's from the school, sent to all the students")
            .Pause()
            .Say(
                """
                The envelope feels cold in your hand, the paper slightly rough. You see the school's seal pressed into the wax.

                You pause, staring at the letter. Should you open it?  
                Maybe it's just more rules, or worse — more bad news. 
                But there's something in your father's eyes that makes you uneasy.

                Do you open the letter?
                """
            )
            .Init();
    }
}
