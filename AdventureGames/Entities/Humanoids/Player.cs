using AdventureGames.Entities.Collectables;
using Common.Utils;

namespace AdventureGames.Entities.Humanoids;

internal sealed class Player : HumanoidBase
{
    private const int MaxLives = 3;
    private const int MaxHealth = 100;

    private decimal _money;
    private int _health;
    private int _lives;

    public string DisplayName { get; }

    /// <summary>
    /// Collectables that the Player holds.
    /// </summary>
    public List<CollectableBase> Inventory { get; private set; } = [];

    public int Lives
    {
        get => _lives;
        private set
        {
            var diff = value - _lives;

            if (diff != -1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "You may only reduce Lives by 1."
                );
            }

            _lives = value;
            Inform("Lost a life", $"{_lives}/{MaxLives}");

            if (_lives == 0)
            {
                Inform("No lives remaining; deceased.");

                if (DisplayName == "You")
                {
                    Inform("GAME OVER");
                    Environment.Exit(0);
                }

                if (Inventory.Count > 0)
                {
                    string inventoryDisplay = String.Join(
                        ", ",
                        Inventory.Select(item => item.Name)
                    );
                    Inform($"Dropped all items: {inventoryDisplay}");
                    Inventory.Clear();
                }

                Money = 0;
            }
        }
    }

    /// <summary>
    /// Gets or sets the Money.
    /// <para>When <b>set</b>: logs to the console.</para>
    /// </summary>
    public decimal Money
    {
        get => _money;
        set
        {
            decimal diff = value - _money;
            _money = value;

            Inform($"{GetDiffSymbol(diff)}{Math.Abs(diff):C}", $"{_money:C}");
        }
    }

    /// <summary>
    /// Gets or sets the Health (max 100).
    /// <para>When <b>set</b>: logs to the console.</para>
    /// </summary>
    public int Health
    {
        get => _health;
        set
        {
            var oldValue = _health;
            _health = Math.Max(value, 0);
            var diff = _health - oldValue;

            if (_health == 0)
            {
                Lives--;
                return;
            }

            if (diff > 0)
            {
                Inform($"Recovered {diff} health", $"{_health}/{MaxHealth} HP");
            }
            else if (diff < 0)
            {
                Inform($"Lost {Math.Abs(diff)} health", $"{_health}/{MaxHealth} HP");
            }
        }
    }

    /// <summary>
    /// Determines the appropriate symbol to represent a numerical difference.
    /// </summary>
    /// <typeparam name="T">A struct type that implements IComparable.</typeparam>
    /// <param name="diff">The difference to evaluate.</param>
    /// <returns>'+' if the difference is positive, '-' otherwise.</returns>
    private static char GetDiffSymbol<T>(T diff)
        where T : struct, IComparable<T>
    {
        double diffValue = Convert.ToDouble(diff);

        return diffValue > 0 ? '+' : '-';
    }

    /// <summary>
    /// Initializes a new instance of the Player class.
    /// </summary>
    /// <inheritdoc cref="HumanoidBase(String, String, ConsoleColor)"/>
    /// <param name="money">The initial amount of money the player has.</param>
    public Player(string name, string description, decimal money, ConsoleColor dialogueColour)
        : base(name, description, dialogueColour)
    {
        DisplayName = description == "You" ? "You" : name;

        _lives = MaxLives;
        _health = MaxHealth;
        _money = money;
    }

    /// <summary>
    /// Displays an informational message to player.
    /// </summary>
    /// <param name="text">The main message to display.</param>
    /// <param name="total">An optional total or summary to display alongside the message.</param>
    private void Inform(string text, string? total = null)
    {
        const int Speed = 2;

        string message = $"[Info for {DisplayName}]: {text}";

        Action<string>? provider = total is null
            ? Utils.TypewriterEffect
            : (string message) =>
            {
                int pipeColumn = Console.WindowWidth - 25;
                int messagePadding = Math.Max(0, pipeColumn - message.Length);
                string lineEnding = new string(' ', messagePadding) + $"| Total: {total}";

                Utils.TypewriterEffect(message, Speed, lineEnding);
            };

        ConsoleUtils.HighlightConsoleLine(message, ConsoleColor.White, provider);
    }

    public override string ToString() => Name;

    /// <summary>
    /// Performs an attack on another player.
    /// </summary>
    /// <param name="target">The player to attack.</param>
    /// <param name="damage">The amount of damage to inflict.</param>
    /// <param name="targetInformMessage">A message to inform the target. Defaults to informing them of the event.</param>
    /// <returns>A boolean indicating if the operation was a success.</returns>
    public bool Attack(Player target, int damage, string? targetInformMessage)
    {
        if (target == this)
        {
            Inform("Cannot attack yourself");
            return false;
        }

        target.Inform(targetInformMessage ?? $"{Name} attacked you");
        target.Health -= damage;

        return true;
    }

    public T? GetItem<T>()
        where T : CollectableBase => Inventory.Find(collectable => collectable is T) as T;

    /// <summary>
    /// Gives an item to another player.
    /// </summary>
    /// <param name="target">The player to give the item to.</param>
    /// <param name="item">The item to give.</param>
    /// <inheritdoc cref="Attack" path="/returns"/>
    public bool GiveItem<T>(Player target)
        where T : CollectableBase
    {
        if (target == this)
        {
            Inform("Cannot give item to yourself");
            return false;
        }

        var item = GetItem<T>();

        if (item is null)
        {
            Inform($"Item ({typeof(T).Name}) not found in their inventory");
            return false;
        }

        target.Inform($"Received an item from {Name} ({item.Name})");
        Inventory.Remove(item);
        target.Inventory.Add(item);

        return true;
    }

    /// <summary>
    /// Uses an item from the player's inventory.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the operation fails and <paramref name="orThrow"/> is true</exception>
    /// <param name="orThrow">If an error should throw if the operation fails</param>
    /// <inheritdoc cref="Attack" path="/returns"/>
    public bool InteractWithItem<T>(bool orThrow = false)
        where T : CollectableBase
    {
        var item = GetItem<T>();

        if (item is null)
        {
            var message = $"Item ({typeof(T).Name}) not found in your inventory.";

            if (orThrow)
            {
                throw new InvalidOperationException(message);
            }

            Inform(message);
            return false;
        }

        item.Interact();

        return true;
    }

    /// <summary>
    /// Gives money to another player.
    /// </summary>
    /// <param name="target">The player to give money to.</param>
    /// <param name="amount">The amount of money to give.</param>
    /// <inheritdoc cref="Attack" path="/returns"/>
    public bool GiveMoney(Player target, decimal amount)
    {
        if (target == this)
        {
            Inform("Cannot give money to yourself");
            return false;
        }

        decimal givenAmount = Math.Max(amount, Money);

        target.Inform($"Received {givenAmount:C} from {Name}");
        Money -= givenAmount;
        target.Money += givenAmount;

        return true;
    }

    /// <summary>
    /// Steals all money from another player.
    /// </summary>
    /// <param name="target">The player to steal from.</param>
    /// <inheritdoc cref="Attack" path="/returns"/>
    public bool StealMoney(Player target) => StealMoney(target, target.Money);

    /// <summary>
    /// Steals a specific amount of money from another player.
    /// </summary>
    /// <inheritdoc cref="StealMoney(Player)"/>
    /// <param name="amount">The amount of money to steal.</param>
    /// <inheritdoc cref="Attack" path="/returns"/>
    public bool StealMoney(Player target, decimal amount)
    {
        if (target == this)
        {
            Inform("Cannot steal money from yourself");
            return false;
        }

        decimal stolenAmount = Math.Min(amount, target.Money);

        target.Inform($"{stolenAmount:C} stolen by {Name}");
        target.Money -= stolenAmount;
        Money += stolenAmount;

        return true;
    }
}
