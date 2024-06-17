using Godot;
using System;

[GlobalClass]
public partial class InventoryDataEquip : InventoryData
{
    public override SlotData DropSlotData(SlotData grabbedSlotData, int index)
    {
        if (grabbedSlotData.Item is not ItemDataEquip)
            return grabbedSlotData;
        return base.DropSlotData(grabbedSlotData, index);
    }

    public override SlotData DropSingleSlotData(SlotData grabbedSlotData, int index)
    {
        if (grabbedSlotData.Item is not ItemDataEquip)
            return grabbedSlotData;

        return base.DropSingleSlotData(grabbedSlotData, index);
    }

    public void CalculateDefence()
    {
        int sum = 0;
        var tree = (SceneTree)Engine.GetMainLoop();
        PlayerStats playerStat = (PlayerStats)tree.Root.GetNode("/root/PlayerStats");
        foreach (SlotData slotData in base.SlotDatas)
        {
            var item = slotData.Item as ItemDataEquip;
            sum += item.Defence;
        }
        playerStat.Defence = sum;
        GD.Print("Current defence: " + playerStat.Defence);
    }
}
