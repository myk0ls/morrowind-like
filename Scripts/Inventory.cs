using Godot;
using System;

public partial class Inventory : PanelContainer
{
	PackedScene Slot;
	InventoryData INV;
	GridContainer ItemGrid;
    CustomSignals customSignals;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Slot = GD.Load<PackedScene>("res://Scenes/slot.tscn");
		ItemGrid = GetNode<GridContainer>("MarginContainer/ItemGrid");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");

		//customSignals.SlotClicked += Slot
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    public void SetInventoryData(InventoryData data)
	{
		data.InventoryUpdated += PopulateItemGrid;
		PopulateItemGrid(data);

    }

	public void ClearInventoryData(InventoryData data)
	{
        data.InventoryUpdated -= PopulateItemGrid;

    }


    public void PopulateItemGrid(InventoryData data)
	{
		foreach (var item in ItemGrid.GetChildren())
		{
			item.QueueFree();
		}

		foreach (var slotData in data.SlotDatas)
		{
			var slot = (Slot)Slot.Instantiate();
			ItemGrid.AddChild(slot);

			//customSignals.Connect(nameof(customSignals.SlotClicked), data.OnSlotClicked);
			//customSignals.SlotClicked += data.OnSlotClicked;
			slot.SlotThing += data.OnSlotClicked;

			if (slotData != null)
			{
				slot.SetSlotData(slotData);
			}
		}
	}

    public void CalculateDefence(InventoryData data)
    {
		if (data.SlotDatas.Length > 0)
		{
			int sum = 0;
			PlayerStats playerStat = GetNode<PlayerStats>("/root/PlayerStats");
			foreach (SlotData slotData in data.SlotDatas)
			{
				var item = slotData.Item as ItemDataEquip;
				sum += item.Defence;
			}
			playerStat.Defence = sum;
			GD.Print("Current defence: " + playerStat.Defence);
		}
    }
}
