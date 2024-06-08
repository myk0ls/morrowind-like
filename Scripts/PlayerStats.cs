using Godot;
using System;

public partial class PlayerStats : Node
{
	// Called when the node enters the scene tree for the first time.
	public PlayerStats instance;

	public int Level = 1;
	public int Exp = 0;
	public int ExpToAdvance = 100;
	
	public int Health = 100;
	public int Fatigue = 100;
	
	public int Gold = 0;

	public override void _Ready()
	{
		instance = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
