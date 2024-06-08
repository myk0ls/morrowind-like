using Godot;
using System;

public partial class World : Node3D
{
	Player player;
    InventoryInterface InventoryInterface;

    CustomSignals customSignals;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		player = GetNode<Player>("/root/World/Player");
        InventoryInterface = GetNode<InventoryInterface>("/root/World/UI/InventoryInterface");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");

        InventoryInterface.SetPlayerInventoryData(player.inventoryData);

        customSignals.ToggleInventory += ToggleInventoryInterface;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void ToggleInventoryInterface()
    {
        InventoryInterface.Visible = !InventoryInterface.Visible;

        if (InventoryInterface.Visible)
        {
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }
        else
            Input.MouseMode = Input.MouseModeEnum.Captured;
    }
}
