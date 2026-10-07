namespace Roguelike.Rules;


public class Game {
    public Menu Menu { get; } = new Menu();

    public bool HasQuit { get; private set; } = false;

    public Playthrough? Playthrough { get; private set; } = null;

    public void ChooseMenuOption(MenuOption option) {
        switch (option) {
            case MenuOption.NewRun:
                var seed = Random.Shared.Next();
                Playthrough = new Playthrough(seed);
                Playthrough.StartPlaythrough();
                break;
            case MenuOption.Quit:
                HasQuit = true;
                break;
        }
    }
}