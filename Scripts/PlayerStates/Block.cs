using Godot;
using System;

public partial class Block : State
{
    Player player;
    Weapon weapon;
    float DefaultSpeed;
    float BlockedSpeed = 3f;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void Enter()
    {
        GD.Print("blockados state blet");
        player = GetNode<Player>("/root/World/Player");
        weapon = player.GetNode<Weapon>("Camera3D/Weapon");
        DefaultSpeed = player.Speed;
    }

    public override void Update()
    {
        if (Input.IsActionPressed("rmb") && !(weapon.animationPlayer.CurrentAnimation == "blockExit"))
        {
            weapon.animationPlayer.Play("blockHold");
            player.Speed = BlockedSpeed;
        }
        else
        {
            weapon.animationPlayer.PlayBackwards("blockExit");
            player.Speed = DefaultSpeed;
        }
    }

    public override void Exit()
    {
    }
}
