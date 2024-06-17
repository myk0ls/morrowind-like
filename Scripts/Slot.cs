using Godot;
using System;

public partial class Slot : PanelContainer
{
	[Signal]
	public delegate void SlotThingEventHandler(int index, int button);

	TextureRect TextureSlot;
	Label QuantityLabel;
    CustomSignals customSignals;

    public override void _Ready()
	{
		TextureSlot = GetNode<TextureRect>("MarginContainer/TextureRect");
        QuantityLabel = GetNode<Label>("QuantityLabel");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public void SetSlotData(SlotData slotData)
	{
		var itemData = slotData.Item;
		TextureSlot.Texture = (Texture2D)itemData.Texture;
		TooltipText = String.Format("{0}\n{1}", itemData.Name, itemData.Description);

		if (slotData.Quantity > 1)
		{
			QuantityLabel.Text = String.Format("x{0}", slotData.Quantity);
			QuantityLabel.Show();
		}
		else
			QuantityLabel.Hide();
    }

	void _on_gui_input(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
		{
			InputEventMouseButton evento = @event as InputEventMouseButton;
            if ((evento.ButtonIndex == MouseButton.Left || evento.ButtonIndex == MouseButton.Right) &&
				evento.IsPressed())
				{
				//customSignals.EmitSignal(nameof(customSignals.SlotClicked), GetIndex(), (int)evento.ButtonIndex);
				EmitSignal(nameof(SlotThing), GetIndex(), (int)evento.ButtonIndex);
				}
        }
	}
}
