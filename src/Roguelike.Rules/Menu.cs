namespace Roguelike.Rules;

public enum MenuOption {
    NewRun,
    Quit
}

public class Menu {
    public List<MenuOption> Options { get; } = [MenuOption.NewRun, MenuOption.Quit];
}