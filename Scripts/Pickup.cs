using Godot;
using System;

public partial class Pickup : RigidBody3D
{
	[Export]
	public SlotData slotData;
	Sprite3D sprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite3D>("Sprite3D");
		sprite.Texture = (Texture2D)slotData.Item.Texture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_area_3d_body_entered(Node node)
	{
		Player player = node as Player;
		if (player.inventoryData.PickUpSlotData(slotData))
			QueueFree();
	}
}
