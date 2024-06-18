using Godot;
using System;

public partial class UI : Control
{
	ProgressBar HealthBar;
    ProgressBar StaminaBar;
    CustomSignals customSignals;
	PlayerStats playerStats;
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		playerStats = GetNode<PlayerStats>("/root/PlayerStats");
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		HealthBar = GetNode<ProgressBar>("HealthBar");
        StaminaBar = GetNode<ProgressBar>("StaminaBar");

		UpdateHealthBar();
		UpdateStaminaBar();

		customSignals.UpdateHealth += UpdateHealthBar;
        customSignals.UpdateStamina += UpdateStaminaBar;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateHealthBar()
	{
		HealthBar.Value = playerStats.Health;
	}
    public void UpdateStaminaBar()
    {
		StaminaBar.Value = playerStats.Stamina;
    }
}
