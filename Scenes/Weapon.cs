using Godot;
using System;

public partial class Weapon : Node3D
{
	public AnimationPlayer animationPlayer;
    CustomSignals customSignals;
    PlayerStats playerStats;
    Node3D Axe;
    Player player;
    Area3D area;
    public bool CanAttack = true;

    Vector3 MouseMov;

    float SwayThreshold = 20;
    float SwaySens = 2f;
    float SwayAmount = 0.15f;
    float SwaySpeed = 5;

    float StartY;
    float BobSpeed = 0.002f;
    float BobAmount = 0.06f;

    Vector3 DefaultRotation;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        Axe = GetNode<Node3D>("aXE");
        player = GetNode<Player>("/root/World/Player");
        DefaultRotation = Rotation;
        StartY = Position.Y;
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            InputEventMouseMotion motion = @event as InputEventMouseMotion;
            MouseMov = new Vector3(-motion.Relative.Y * SwaySens, -motion.Relative.X * SwaySens, 0);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        WeaponSwayBob(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
    }

    void WeaponSwayBob(double delta)
    {
        // sway
        var mouseMovLength = MouseMov.Length();

        if (mouseMovLength > SwayThreshold || mouseMovLength < -SwayThreshold)
        {
            Rotation = Rotation.Lerp(MouseMov.Normalized() * SwayAmount + DefaultRotation, SwaySpeed * (float)delta);
        }
        else
            Rotation = Rotation.Lerp(DefaultRotation, SwaySpeed * (float)delta);

        // bob
        if (player.IsOnFloor() && player.Velocity.Length() != 0)
        {
            var time = (float)Time.GetTicksMsec();

            float velocityLength = player.Velocity.Length();
            float angle = time * BobSpeed * velocityLength;
            float bobOffset = (float)Math.Sin(angle) * BobAmount + StartY;

            Position = new Vector3(Position.X, bobOffset, Position.Z);
        }
        else
            Position = Position.Lerp(new Vector3(Position.X, (float)StartY, Position.Z), BobSpeed);
    }

    public void OnArea3DBodyEntered(Node3D node)
    {
        if (animationPlayer.CurrentAnimation == "attackFinish")
        {
            GD.Print(node.ToString());
            GD.Print("KURWA");
        }
    }
}
