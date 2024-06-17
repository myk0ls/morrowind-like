using Godot;
using System;

[GlobalClass]
public partial class ItemDataConsumable : ItemData
{
    [Export] public int HealValue;
    [Export] public int StaminaValue;
    public override void Use(Node target)
    {

        base.Use(target);
        Player player = target as Player;

        var tree = (SceneTree)Engine.GetMainLoop();
        PlayerStats playerStat = (PlayerStats)tree.Root.GetNode("/root/PlayerStats");

        if (HealValue > 0)
            playerStat.Health += HealValue;
        if (StaminaValue > 0)
            playerStat.Stamina += StaminaValue;
        GD.Print("health = " + playerStat.Health);
        GD.Print("stamina = " + playerStat.Stamina);
        GD.Print("");
    }

}
