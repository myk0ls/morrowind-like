using Godot;
using System;

public partial class Weapon : Node3D
{
	AnimationPlayer animationPlayer;
    Node3D Axe;
    Player player;

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
            GD.Print(MouseMov);
        }

        if (Input.IsActionPressed("lmb"))
        {
            animationPlayer.Play("attack");
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
        var mouseMovLength = MouseMov.Length();

        if (mouseMovLength > SwayThreshold || mouseMovLength < -SwayThreshold)
        {
            Rotation = Rotation.Lerp(MouseMov.Normalized() * SwayAmount + DefaultRotation, SwaySpeed * (float)delta);
        }
        else
            Rotation = Rotation.Lerp(DefaultRotation, SwaySpeed * (float)delta);

        if (player.IsOnFloor() && player.Velocity.Length() != 0)
        {
            var time = (float)Time.GetTicksMsec();

            //float bobOffset = (float)(StartY + Math.Sin((time * BobSpeed) * BobAmount * player.Velocity.Length()));
            float velocityLength = player.Velocity.Length();
            float angle = time * BobSpeed * velocityLength;
            float bobOffset = (float)Math.Sin(angle) * BobAmount + StartY;

            Position = new Vector3(Position.X, bobOffset, Position.Z);
        }
        else
            Position = Position.Lerp(new Vector3(Position.X, (float)StartY, Position.Z), BobSpeed);
    }
}
