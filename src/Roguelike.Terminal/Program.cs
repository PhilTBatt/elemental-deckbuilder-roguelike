using Roguelike.Rules;

Console.WriteLine("Welcome to Hadeon!");

var game = new Game();

while (!game.HasQuit && game.Playthrough?.Battle?.State != BattleState.EnemyWins) {
    Console.WriteLine("Choose an option:");
    foreach (var option in game.Menu.Options) {
        Console.WriteLine($"- {option}");
    }
    var response =  Console.ReadLine();
    if (Enum.TryParse<MenuOption>(response, out var menuOption) && Enum.IsDefined(menuOption)) {
        game.ChooseMenuOption(menuOption);
    } else {
        Console.WriteLine("Invalid option.");
        continue;
    }

    if (game.Playthrough?.Battle?.State == BattleState.EnemyWins) Console.WriteLine("Game Over!");
};