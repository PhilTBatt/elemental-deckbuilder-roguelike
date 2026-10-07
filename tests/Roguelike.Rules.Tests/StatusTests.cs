namespace Roguelike.Rules.Tests;

public class StatusTests {
    [Fact]
    public void New_Status_Keeps_Its_Kind_And_Stacks() {
        var burn = new Status(StatusType.Burn, 3);

        Assert.Equal(StatusType.Burn, burn.Kind);
        Assert.Equal(3, burn.Stacks);
    }

    [Fact]
    public void Burn_Deals_Damage_Equal_To_Its_Stacks() {
        var owner = new Character(20);
        var burn = new Status(StatusType.Burn, 3);

        burn.EndTurn(owner);

        Assert.Equal(17, owner.Health);
    }

    [Fact]
    public void Burn_Loses_One_Stack_When_It_Ticks() {
        var owner = new Character(20);
        var burn = new Status(StatusType.Burn, 3);

        burn.EndTurn(owner);

        Assert.Equal(2, burn.Stacks);
    }

    [Fact]
    public void Burn_Of_3_Deals_3_Then_2_Then_1() {
        var owner = new Character(20);
        var burn = new Status(StatusType.Burn, 3);

        burn.EndTurn(owner);
        Assert.Equal(17, owner.Health);

        burn.EndTurn(owner);
        Assert.Equal(15, owner.Health);

        burn.EndTurn(owner);
        Assert.Equal(14, owner.Health);
        Assert.Equal(0, burn.Stacks);
    }

    [Fact]
    public void Block_Absorbs_Burn() {
        var owner = new Character(20);
        owner.AddBlock(5);
        var burn = new Status(StatusType.Burn, 3);

        burn.EndTurn(owner);

        Assert.Equal(20, owner.Health);
        Assert.Equal(2, owner.Block);
        Assert.Equal(2, burn.Stacks);
    }

    [Fact]
    public void Burn_Bigger_Than_The_Block_Deals_The_Rest() {
        var owner = new Character(20);
        owner.AddBlock(2);
        var burn = new Status(StatusType.Burn, 5);

        burn.EndTurn(owner);

        Assert.Equal(17, owner.Health);
        Assert.Equal(0, owner.Block);
    }

    [Fact]
    public void Burn_Can_Kill_Its_Owner() {
        var owner = new Character(2);
        var burn = new Status(StatusType.Burn, 5);

        burn.EndTurn(owner);

        Assert.False(owner.IsAlive);
    }
}