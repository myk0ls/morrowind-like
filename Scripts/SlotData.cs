using Godot;
using System;

[GlobalClass]
public partial class SlotData : Resource
{
    const int MaxStackSize = 99;

    [Export] public ItemData Item { get; set; }

    [Export(PropertyHint.Range, "1,99,")] public int Quantity { get; set; }

    public SlotData() : this(null, 1) { }

    public SlotData(ItemData Item, int Quantity)
    {
        this.Item = Item;
        SetQuantity(Quantity);
    }

    void SetQuantity(int value)
    {
        Quantity = value;
        if (Quantity > 1 && (Item.Stackable == false))
        {
            Quantity = 1;
            GD.PushError("%s not stackable", Item.Name);
        }
    }

    public bool CanMergeWith(SlotData otherSlotData)
    {
        return (Item == otherSlotData.Item && Item.Stackable == true && (Quantity < MaxStackSize));
    }

    public bool CanFullyMergeWith(SlotData otherSlotData)
    {
        return (Item == otherSlotData.Item && Item.Stackable == true && (Quantity + otherSlotData.Quantity < MaxStackSize)) ;
    }

    public void FullyMergeWith(SlotData otherSlotData)
    {
        Quantity += otherSlotData.Quantity;
    }

    public SlotData CreateSingleSlotData()
    {
        SlotData NewSlotData = (SlotData)Duplicate();
        NewSlotData.Quantity = 1;
        Quantity -= 1;
        return NewSlotData;
    }
}
