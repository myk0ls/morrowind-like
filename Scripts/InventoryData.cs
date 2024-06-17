using Godot;
using System;
using System.ComponentModel.DataAnnotations;

[GlobalClass]
public partial class InventoryData : Resource
{

    [Signal]
    public delegate void InventoryInteractEventHandler(InventoryData inv, int index, int button);
    [Signal]
    public delegate void InventoryUpdatedEventHandler(InventoryData inv);
    [Signal]
    public delegate void DefenceUpdateEventHandler(InventoryData inv);

    public int MaxSlots = 99;
    [Export] public SlotData[] SlotDatas { get; set; }

    public InventoryData() : this(99) { }

    public InventoryData(int maxSlots)
    {
        this.SlotDatas = new SlotData[maxSlots];
    }


    public void OnSlotClicked(int index, int mouseButton)
    {

        GD.Print("inventory interact, " + this.SlotDatas[index] + " ," + mouseButton);
        EmitSignal(nameof(InventoryInteract), this, index, mouseButton);
    }

    public SlotData GrabSlotData(int index)
    {
        var slot = SlotDatas[index];

        if (slot != null)
        {
            SlotDatas[index] = null;
            EmitSignal(nameof(InventoryUpdated), this);
            return slot;
        }
        else 
            return null;
    }

    public virtual SlotData DropSlotData(SlotData grabbedSlotData, int index)
    {
        var slot = SlotDatas[index];
        SlotData returnSlotDatta = null;
        if ((slot != null) && slot.CanFullyMergeWith(grabbedSlotData))
        {
            slot.FullyMergeWith(grabbedSlotData);
        }
        else
        {
            SlotDatas[index] = grabbedSlotData;
            returnSlotDatta = slot;
        }
        EmitSignal(nameof(InventoryUpdated), this);
        return returnSlotDatta;
    }

    public virtual SlotData DropSingleSlotData(SlotData grabbedSlotData, int index)
    {
        var slot = SlotDatas[index];

        if (slot == null)
        {
            SlotDatas[index] = grabbedSlotData.CreateSingleSlotData();
        }
        else if (slot.CanMergeWith(grabbedSlotData))
            slot.FullyMergeWith(grabbedSlotData);
        EmitSignal(nameof(InventoryUpdated), this);

        if (grabbedSlotData.Quantity > 0)
        {
            return grabbedSlotData;
        }
        else
            return null;
    }

    public void UseSlotData(int index)
    {
        var slot = SlotDatas[index];

        if (slot == null)
            return;

        if (slot.Item is ItemDataConsumable)
        {
            slot.Quantity -= 1;
            if (slot.Quantity < 1)
            {
                SlotDatas[index] = null;
            }
        }

        GD.Print(slot.Item.Name);

        var tree = (SceneTree)Engine.GetMainLoop();
        PlayerStats playerStat = (PlayerStats)tree.Root.GetNode("/root/PlayerStats");
        playerStat.UseSlotData(slot);

        EmitSignal(nameof(InventoryUpdated), this);
    }

    public bool PickUpSlotData(SlotData slotData)
    {
        for (int i = 0; i < SlotDatas.Length; i++)
        {
            if (SlotDatas[i] != null && SlotDatas[i].CanFullyMergeWith(slotData) == true)
            {
                SlotDatas[i].FullyMergeWith(slotData);
                EmitSignal(nameof(InventoryUpdated), this);
                return true;
            }
        }

        for (int i = 0; i < SlotDatas.Length; i++)
        {
            if (SlotDatas[i] == null)
            {
                SlotDatas[i] = slotData;
                EmitSignal(nameof(InventoryUpdated), this);
                return true;
            }
        }

        return false;
    }
}
