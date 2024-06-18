using Godot;
using System;

public partial class Passive : State
{
    Player player;
    Weapon weapon;
    public override void _Ready()
    {
    }

    public override void Enter()
    {
        GD.Print("pasyvus");
        player = GetNode<Player>("/root/World/Player");
        weapon = player.GetNode<Weapon>("Camera3D/Weapon");
    }
    
    public override void Update()
    {
        if (Input.IsActionJustPressed("lmb"))
        {
            DoAttack();
        }

        if (Input.IsActionJustPressed("rmb"))
        {
            DoBlock();
        }
    }

    public override void Exit() 
    {

    }

    public void DoAttack()
    {
        machine.TransitionTo("Attack");
    }

    public void DoBlock()
    {
        machine.TransitionTo("Block");
    }

}
