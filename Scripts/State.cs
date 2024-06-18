using Godot;
using System;

[GlobalClass]
public partial class State : Node
{
    // Called when the node enters the scene tree for the first time.
    public StateMachine machine;
    public PlayerStats playerStats;
    public CustomSignals customSignals;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    }

    public virtual void Update() { }

    public virtual void Exit() { }
    public virtual void Enter() { }
}
