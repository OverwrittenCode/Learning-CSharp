namespace AdventureGames.Scenes.GoToSchool.Consequences;

internal sealed class PrincipalDiscussion : BaseScene
{
    public PrincipalDiscussion()
    {
        Choices.Add(new("Accept the principal's decision", () => new Detention()));
        Choices.Add(
            new(
                "Investigate further",
                () =>
                {
                    new ConversationBuilder()
                        .SudoPrinciple(
                            "I'm giving you the benefit of the doubt kid. Next time there will be serious consequences. You're free to leave."
                        )
                        .Say("You leave their office and meet up with Jack again")
                        .Pause()
                        .SudoJack("Where were you?")
                        .SudoUser(
                            "Not important. Well, actually no it is important. I think the principal is hiding something!"
                        )
                        .SudoJack("What makes you think that?")
                        .SudoUser(
                            "This entire day has felt weird, and I managed to find my note on deciphering"
                        )
                        .SudoJack(
                            "Where are you going with this? Wait... what are you even on about?"
                        )
                        .SudoUser(
                            "Long story, but I received a weird letter this morning.",
                            "The ordering of the rules was off, the way it was written was weird.",
                            "You know how much our teacher hates riddles, it was like he wanted me to know something but couldn't say!",
                            "And at the bottom of the letter it said this strange message that looked like gibberish at first."
                        )
                        .Say("Jack continues to listen with confusion")
                        .Pause()
                        .SudoUser(
                            "That's when I knew something was up.",
                            "My parents taught me this technique to hide things inside messages if I was ever being threatened - it's called a cipher."
                        )
                        .SudoJack("Go on.")
                        .SudoUser(
                            "I don't know how I didn't see it before, but that's when I recalled my note:"
                        )
                        .Say("Shift each letter, a-z, back by a specific amount to decipher")
                        .SudoUser(
                            "And the letter talked about something about irrational order! Don't you see?"
                        )
                        .SudoJack("No, really I don't, please get to the point")
                        .SudoUser(
                            "Ok long story short the order of the rules was off, the letter said:"
                        )
                        .Say("SHIFT BACK BY THE POSITION OF THE FIRST")
                        .SudoUser(
                            "And then I looked at the rules, saw that rule 3 started with \"Firstly\", so I shifted each letter back by 3, and then it said:"
                        )
                        .Say("DANGER IN BASEMENT, PROCEED CAUTIOUSLY")
                        .SudoUser(
                            "(inhales) Which is exactly why I think our teacher is hiding something.",
                            "And if our teacher knows about it, our principle has to right?",
                            "They are up to something, now it FINALLY adds up!"
                        )
                        .SudoJack(
                            "(astonished) I'm amazed. You cracked it! Let's head to the basement now whilst we have the chance.",
                            "We need to get to the bottom of this."
                        )
                        .Init();

                    return new BasementDiscovery();
                }
            )
        );
    }

    public override void Play()
    {
        new ConversationBuilder()
            .SudoUser("Sir, I'm concerned about what's happening in our country.")
            .Say("The principal's expression softens slightly.")
            .SudoPrinciple("I understand your concerns, but we must maintain order.")
            .Say("You notice the principal glance nervously at a locked drawer.")
            .SudoUser("(thinking) What's he hiding?")
            .Init();
    }
}
