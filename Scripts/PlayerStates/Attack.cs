using Godot;
using System;

public partial class Attack : State
{
    Player player;
    Weapon weapon;

    public override void _Ready()
    {
        base._Ready();
    }

    public override void Enter()
    {
        GD.Print("attackos state blet");
        player = GetNode<Player>("/root/World/Player");
        weapon = player.GetNode<Weapon>("Camera3D/Weapon");
        //weapon.animationPlayer.AnimationFinished += ("attack") => DoPassive();
        //weapon.animationPlayer.Connect(nameof(weapon.animationPlayer.AnimationFinished), Callable());
    }

    public override void Update()
    {
        if (!weapon.animationPlayer.IsPlaying() &&
            weapon.CanAttack == true &&
            playerStats.Stamina >= 30)
        {
            playerStats.Stamina -= 30;
            weapon.animationPlayer.Play("attack");
            customSignals.EmitSignal(nameof(customSignals.UpdateStamina));
        }
        else
            DoPassive();

    }

    public override void Exit()
    {

    }

    public void DoPassive()
    {
        machine.TransitionTo("Passive");
    }

    public void OnAnimationPlayerAnimationFinished(string animName)
    {
        if (animName == "attack")
            DoPassive();
        if (animName == "blockExit")
            DoPassive();
    }
}
