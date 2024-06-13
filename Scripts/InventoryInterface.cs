using Godot;
using System;
using System.Text.RegularExpressions;

public partial class InventoryInterface : Control
{
	Inventory PlayerInventory;
	Inventory ExternalInvetory;
	SlotData grabbedSlotData;
	Slot grabbedSlot;
	Chest chestOwner;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerInventory = GetNode<Inventory>("PlayerInventory");
		ExternalInvetory = GetNode<Inventory>("ExternalInventory");
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

	public void SetExternalInventory(Chest chest)
	{
		//GD.Print(chest);
		chestOwner = chest;
		InventoryData inventoryData = chest.inventoryData;

        inventoryData.InventoryInteract += OnInventoryInteract;
        ExternalInvetory.SetInventoryData(inventoryData);

		ExternalInvetory.Show();
    }

    public void ClearExternalInventory()
    {
        //GD.Print(chest);
		if (chestOwner != null)
		{
			InventoryData inventoryData = chestOwner.inventoryData;

			inventoryData.InventoryInteract -= OnInventoryInteract;
			ExternalInvetory.ClearInventoryData(inventoryData);

			ExternalInvetory.Hide();
			chestOwner = null;
		}
    }

	public void _on_gui_input(InputEvent @event)
	{

	}
}
