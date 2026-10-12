namespace Roguelike.Rules;

public class Map {
    public List<MapNode> Nodes { get; } = [];

    public MapNode? Current { get; private set; }

    public List<MapNode> Choices =>
        Current == null
            ? Nodes.Where(node => node.Step == 0).ToList()
            : Current.Next.Select(Get).ToList();

    public bool IsFinished => Current?.Kind == NodeKind.Boss;

    public MapNode Get(int id) => Nodes.First(node => node.Id == id);

    public bool MoveTo(int id) {
        var node = Get(id);
        if (!Choices.Contains(node)) return false;
        Current = node;
        return true;
    }

    public static Map FirstFloor() {
        const int steps = 4;
        const int lanes = 3;
        var map = new Map();

        for (int step = 0; step < steps; step++) {
            for (int lane = 0; lane < lanes; lane++) {
                map.Nodes.Add(new MapNode(step * lanes + lane, step, lane, NodeKind.Fight));
            }
        }
        var boss = new MapNode(steps * lanes, steps, 1, NodeKind.Boss);
        map.Nodes.Add(boss);

        foreach (var node in map.Nodes) {
            if (node.Kind == NodeKind.Boss) continue;
            if (node.Step == steps - 1) {
                node.Next.Add(boss.Id);
                continue;
            }
            int nextStepStart = (node.Step + 1) * lanes;
            for (int lane = node.Lane - 1; lane <= node.Lane + 1; lane++) {
                if (lane >= 0 && lane < lanes) node.Next.Add(nextStepStart + lane);
            }
        }
        return map;
    }
}