using Godot;
using System;

public partial class Attack : State
{
    Player player;
    Weapon weapon;
    float DefaultSpeed;
    float AttackingSpeed = 3f;

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
        weapon.animationPlayer.Play("attackStart");
        DefaultSpeed = player.Speed;
        player.Speed = AttackingSpeed;
        player.StaminaTimer.Stop();
    }

    public override void Update()
    {
        /*
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
        */
        if (Input.IsActionJustReleased("lmb") && !(weapon.animationPlayer.CurrentAnimation == "attackFinish"))
        {
            playerStats.Stamina -= 30;
            weapon.animationPlayer.Play("attackFinish");
            player.Speed = DefaultSpeed;
            player.StaminaTimer.Start();
            customSignals.EmitSignal(nameof(customSignals.UpdateStamina));
        }
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
        if (animName == "attackFinish")
            DoPassive();
        if (animName == "blockExit")
            DoPassive();
    }
}
