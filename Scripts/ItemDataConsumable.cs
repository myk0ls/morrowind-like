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
        CustomSignals customSignals = (CustomSignals)tree.Root.GetNode("/root/CustomSignals");

        if (HealValue > 0)
        {
            playerStat.Health += HealValue;
            customSignals.EmitSignal(nameof(customSignals.UpdateHealth));
        }
        if (StaminaValue > 0)
        {
            playerStat.Stamina += StaminaValue;
            customSignals.EmitSignal(nameof(customSignals.UpdateStamina));
        }
    }

}
