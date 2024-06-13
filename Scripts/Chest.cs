using Godot;
using System;

public partial class Chest : StaticBody3D
{

	[Signal]
	public delegate void ToggleInventoryEventHandler(Chest chest);

	[Export]
	public InventoryData inventoryData;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayerInteract()
	{
		EmitSignal(nameof(ToggleInventory), this);
	}
}
