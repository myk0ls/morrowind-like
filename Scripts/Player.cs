using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float Sensitivity = 1.5f;

	CustomSignals customSignals;

	RayCast3D InteractRay;
	Camera3D camera;
	Timer StaminaTimer;
	PlayerStats playerStats;

    [Export] public InventoryData inventoryData;
    [Export] public InventoryData EquipInventoryData;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {
        camera = GetNode<Camera3D>("Camera3D");
        Input.MouseMode = Input.MouseModeEnum.Captured;
		StaminaTimer = GetNode<Timer>("StaminaTimer");
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		InteractRay = GetNode<RayCast3D>("Camera3D/InteractRay");
		playerStats = GetNode<PlayerStats>("/root/PlayerStats");
		playerStats.Instance.player = this;

		StaminaTimer.Timeout += RestoreStamina;
		StaminaTimer.Start();
	}

    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;


		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_foward", "move_backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            InputEventMouseMotion motion = @event as InputEventMouseMotion;
            Rotation = new Vector3(Rotation.X, Rotation.Y - motion.Relative.X / 1000 * Sensitivity, Rotation.Z);
            camera.Rotation = new Vector3(Mathf.Clamp(camera.Rotation.X - motion.Relative.Y / 1000 * Sensitivity, -2, 2), camera.Rotation.Y, camera.Rotation.Z);
        }

        if (Input.IsActionJustPressed("esc"))
        {
            if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
            else DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }

		if (Input.IsActionJustPressed("mmb"))
		{
			if (Input.MouseMode == Input.MouseModeEnum.Captured)
				Input.MouseMode = Input.MouseModeEnum.Confined;
			else Input.MouseMode = Input.MouseModeEnum.Captured;
		}
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
		if (Input.IsActionJustPressed("inventory"))
		{
			customSignals.EmitSignal(nameof(customSignals.ToggleInventory));
		}

		if (Input.IsActionJustPressed("interact"))
		{
			Interact();
		}
    }

	public void Interact()
	{
		if (InteractRay.IsColliding())
			InteractRay.GetCollider().Call("PlayerInteract");
	}

	public Vector3 GetDropPosition()
	{
		var direction = -camera.GlobalTransform.Basis.Z;
		return camera.GlobalPosition + direction;
	}

	public void RestoreStamina()
	{
        if (playerStats.Stamina < 100)
        {
            playerStats.Stamina += 1;
        }
        customSignals.EmitSignal(nameof(customSignals.UpdateStamina));
    }
}
