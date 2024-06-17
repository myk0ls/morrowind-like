using Godot;
using System;

public partial class PlayerStats : Node
{
	// Called when the node enters the scene tree for the first time.
	public PlayerStats Instance;
	public Player player;

	public int Level = 1;
	public int Exp = 0;
	public int ExpToAdvance = 100;
	
	public int Health = 100;
	public int Stamina = 100;
	public int Defence = 0;
	
	public int Gold = 0;

	public override void _Ready()
	{
		Instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UseSlotData(SlotData slotData)
	{
		slotData.Item.Use(player);
	}
}
