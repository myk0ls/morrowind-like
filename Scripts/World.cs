using Godot;
using System;

public partial class World : Node3D
{
	Player player;
    InventoryInterface InventoryInterface;
    PackedScene pickup;
    CustomSignals customSignals;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		player = GetNode<Player>("/root/World/Player");
        InventoryInterface = GetNode<InventoryInterface>("/root/World/UI/InventoryInterface");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
        pickup = GD.Load<PackedScene>("res://Scenes/pickup.tscn");

        InventoryInterface.SetPlayerInventoryData(player.inventoryData);
        InventoryInterface.SetEquipInventoryData(player.EquipInventoryData);

        customSignals.ToggleInventory += ToggleInventoryInterface;

        foreach (Node node in GetTree().GetNodesInGroup("external_inventory"))
        {
            Chest container = node as Chest;
            container.ToggleInventory += ToggleInventoryInterface;
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void ToggleInventoryInterface(Chest chest)
    {
        InventoryInterface.Visible = !InventoryInterface.Visible;

        if (InventoryInterface.Visible)
        {
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }
        else
            Input.MouseMode = Input.MouseModeEnum.Captured;

        if (chest != null && InventoryInterface.Visible == true)
        {
            InventoryInterface.SetExternalInventory(chest);
        }
        else
        {
            InventoryInterface.ClearExternalInventory();
        }
    }

    public void ToggleInventoryInterface()
    {
        InventoryInterface.Visible = !InventoryInterface.Visible;

        if (InventoryInterface.Visible)
        {
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }
        else
        {
            Input.MouseMode = Input.MouseModeEnum.Captured;
            InventoryInterface.ClearExternalInventory();
        }
    }

    public void _on_inventory_interface_drop_slot_data(SlotData slotData)
    {
        Pickup pick_up = pickup.Instantiate() as Pickup;
        pick_up.slotData = slotData;
        pick_up.Position = player.GetDropPosition();
        AddChild(pick_up);
    }
}
