using Godot;
using System;

public partial class CustomSignals : Node
{
    [Signal]
    public delegate void ToggleInventoryEventHandler();

    [Signal]
    public delegate void SlotClickedEventHandler(int index, int buttonIndex);

    [Signal]
    public delegate void UpdateHealthEventHandler();
    
    [Signal]
    public delegate void UpdateStaminaEventHandler();

}
