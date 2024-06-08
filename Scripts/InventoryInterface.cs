using Godot;
using System;
using System.Text.RegularExpressions;

public partial class InventoryInterface : Control
{
	Inventory PlayerInventory;
	SlotData grabbedSlotData;
	Slot grabbedSlot;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerInventory = GetNode<Inventory>("PlayerInventory");
		grabbedSlot = GetNode<Slot>("GrabbedSlot");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (grabbedSlot.Visible)
		{
			grabbedSlot.GlobalPosition = GetGlobalMousePosition() + new Vector2(5,5);
		}
	}

	public void SetPlayerInventoryData(InventoryData data)
	{
		data.InventoryInteract += OnInventoryInteract;
		PlayerInventory.SetInventoryData(data);
	}

	public void OnInventoryInteract(InventoryData inv, int index, int button)
	{
		//GD.Print(inv + " " + index + " " + button);
		if (grabbedSlotData is null && button is 1)
		{
			grabbedSlotData = inv.GrabSlotData(index);
		}
        else if (grabbedSlotData is not null && button is 1)
        {
            grabbedSlotData = inv.DropSlotData(grabbedSlotData, index);
        }
        else if (grabbedSlotData is null && button is 2)
        {
			return;
        }
        else if (grabbedSlotData is not null && button is 2)
        {
            grabbedSlotData = inv.DropSingleSlotData(grabbedSlotData, index);
        }

        //GD.Print(grabbedSlotData);
        UpdateGrabbedSlot();
	}

	public void UpdateGrabbedSlot()
	{
		if (grabbedSlotData != null)
		{
			grabbedSlot.Show();
			grabbedSlot.SetSlotData(grabbedSlotData);
		}
		else
			grabbedSlot.Hide();
	}
}
