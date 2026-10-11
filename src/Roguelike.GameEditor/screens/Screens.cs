using Godot;

public enum Screen { Menu, Battle }

public static class Screens
{
    private static string PathFor(Screen screen) => screen switch
    {
        Screen.Menu => "res://screens/menu/main_menu.tscn",
        Screen.Battle => "res://screens/battle/battle_screen.tscn",
        _ => throw new System.ArgumentOutOfRangeException(nameof(screen)),
    };

    public static void GoTo(Node from, Screen screen) =>
        from.GetTree().ChangeSceneToFile(PathFor(screen));
}