namespace Roguelike.Rules;

public enum NodeKind { Fight, Boss }

public class MapNode(int id, int step, int lane, NodeKind kind) {
    public int Id { get; } = id;
    public int Step { get; } = step;
    public int Lane { get; } = lane;
    public NodeKind Kind { get; } = kind;
    public List<int> Next { get; } = [];
}