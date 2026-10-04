using Roguelike.Rules;

Console.WriteLine("Welcome to Hadeon!");

var player = new Player(40);
var enemy = new Enemy(15, 4);
var battle = new Battle(player, enemy);
Console.WriteLine($"Player Health: {player.Health} | Enemy Health: {enemy.Health}\nPress Enter to take a turn");
Console.ReadLine();

while (player.IsAlive && enemy.IsAlive) {
    battle.ProcessOneTurn();
    if (player.IsAlive && !enemy.IsAlive) {
        Console.WriteLine($"Player Health: {player.Health} | Enemy Health: {enemy.Health}\nYou won!");
    } else if (!player.IsAlive && enemy.IsAlive) {
        Console.WriteLine($"Player Health: 0 | Enemy Health: {enemy.Health}\nYou lost!");
    } else if (!player.IsAlive && !enemy.IsAlive) {
        Console.WriteLine($"Player Health: 0 | Enemy Health: 0\nYou both died!");
    } else {
        Console.WriteLine($"Player Health: {player.Health} | Enemy Health: {enemy.Health}\nPress Enter to take a turn");
    }
    Console.ReadLine();
}