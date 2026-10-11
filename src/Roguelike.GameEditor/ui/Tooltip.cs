using Godot;
using System;
public static class Tooltip
{
	private const int WrapWidth = 240;
	private const int ShortText = 36;

	public static Control Build(string text)
	{
		var box = new VBoxContainer();
		box.AddThemeConstantOverride("separation", 4);

		var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		for (int i = 0; i < lines.Length; i++)
		{
			if (i > 0) box.AddChild(new HSeparator());

			var colon = lines[i].IndexOf(':');
			if (colon > 0)
			{
				var title = lines[i].Substring(0, colon).Trim();
				var body = lines[i].Substring(colon + 1).Trim();
				box.AddChild(MakeLabel(title, 14, Keywords.ColorFor(title), wrap: false));
				if (body.Length > 0) box.AddChild(MakeLabel(body, 13, new Color("bfc7d6"), wrap: body.Length > ShortText));
			}
			else
			{
				box.AddChild(MakeLabel(lines[i], 13, new Color("e4e7ee"), wrap: lines[i].Length > ShortText));
			}
		}
		return box;
	}

	private static Label MakeLabel(string text, int size, Color color, bool wrap)
	{
		var label = new Label { Text = text };
		label.AddThemeFontSizeOverride("font_size", size);
		label.AddThemeColorOverride("font_color", color);
		if (wrap)
		{
			label.AutowrapMode = TextServer.AutowrapMode.WordSmart;
			label.CustomMinimumSize = new Vector2(WrapWidth, 0);
		}
		return label;
	}
}
