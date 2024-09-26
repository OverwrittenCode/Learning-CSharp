namespace AdventureGames.Scenes.StayAtHome;

internal sealed class MeetJack : BaseScene
{
    public MeetJack()
    {
        Choices.Add(new("Investigate Jack's claims", () => new SecretInvestigation()));
        Choices.Add(new("Convince Jack to go to school", () => new LateSchoolArrival()));
    }

    public override void Play()
    {
        new ConversationBuilder()
            .Say("You sneak out of the house and meet Jack at your usual spot.")
            .SudoJack("I thought you weren't coming to school today!")
            .SudoUser("I wasn't planning to, but I couldn't stay at home.")
            .SudoJack("You won't believe what I heard...")
            .Say("Jack leans in close, looking around nervously.")
            .SudoJack("There's something weird going on at school. In the basement.")
            .SudoUser("What do you mean?")
            .SudoJack("I overheard the teachers talking about a 'resistance'. It sounds dangerous.")
            .Init();
    }
}
