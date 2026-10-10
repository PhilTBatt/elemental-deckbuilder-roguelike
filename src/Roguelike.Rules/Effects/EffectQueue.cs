namespace Roguelike.Rules;

public class EffectQueue() {
    public List<QueuedEffect> Items {get; }= [];

    public bool IsEmpty => Items.Count == 0;

    public void AddToBottom(Effect effect, Character caster, Character target) =>
        Items.Add(new QueuedEffect(effect, caster, target));

    public void AddToTop(Effect effect, Character caster, Character target) =>
        Items.Insert(0, new QueuedEffect(effect, caster, target));

    public QueuedEffect TakeNext() {
        var next = Items[0];
        Items.RemoveAt(0);
        return next;
    }

    public void Clear() => Items.Clear();
}