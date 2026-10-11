namespace Roguelike.Rules;

public enum StatusType { Burn }

public class Status(StatusType kind, int stacks) {
    public StatusType Kind { get; } = kind;
    public int Stacks { get; set; } = stacks;

    public void Tick(Character owner) {
        switch (Kind) {
            case StatusType.Burn:
                owner.TakeDamage(Stacks);
                Stacks--;
                break;
        }
    }
}