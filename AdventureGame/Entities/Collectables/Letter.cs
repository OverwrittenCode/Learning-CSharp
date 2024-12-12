namespace AdventureGame.Entities.Collectables;

/// <inheritdoc />
internal sealed class Letter : CollectableBase
{
    private const string Cryptic = """
                                   ---

                                   GDQJHU LQ EDVHPHQW, 
                                   SURFHHG FDXWLRXVOB.

                                   IRRATIONAL ORDER CONCEALS. 
                                   SHIFT BACK EACH BY THE POSITION OF THE FIRST.
                                   ADVENTURES ARE ALWAYS WAITING FOR THE SMART ONES.
                                   """;

    private static readonly string Body = $"""
                                           [Address]: Sherlock Rd.
                                           [From]: St. Thapar Catholic School

                                           Dear {Game.User.Name},

                                           As you know from the current state of this country, things are getting out of order. 
                                           That is why it is imperative that I do my job and maintain order.

                                           School days can and will continue as normal regardless of whatever the news is telling you. 
                                           I am your teacher, I know better.

                                           We will start tomorrow sharp at 7:00 AM, do not be late to class 
                                           You know the punishment for lateness after your friend Jack disobeyed me.

                                           Additionally, these rules will be added immediately:
                                               1. Secondly, talking about the news is strictly forbidden.
                                                  Do not test me.
                                           
                                               2. Whispering is forbidden outside of free time.
                                           
                                               3. Firstly, your free time will be reduced from 20 minutes, to 5 minutes. 
                                                  You can thank your friend Jack for that.
                                           
                                               4. Thirdly, lunch prices will be increased by 3%
                                           
                                               5. Take the first rule very seriously.
                                                  Do not test me.
                                           """;

    /// <summary>
    ///     A letter from the user's school teacher with regard to the current state of the country.
    ///     <para>But something seems off about this letter...</para>
    /// </summary>
    public Letter() : base(
        $"""
         {Body}

         {Cryptic}
         """
    ) { }

    public override void Interact()
    {
        Game.Say(Body);
        Game.Say($"\n{Cryptic}", 0.3);
    }
}
