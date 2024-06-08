using Godot;
using System;

[GlobalClass]
public partial class ItemData : Resource
{
    [Export] public string Name { get; set; }

    [Export(PropertyHint.MultilineText)] public string Description { get; set; }

    [Export] public bool Stackable { get; set; }

    [Export] public Texture Texture { get; set; }

    public ItemData() : this("", "", false, null) { }

    public ItemData(string Name, string Description, bool Stackable, Texture texture)
    {
        this.Name = Name;
        this.Description = Description;
        this.Stackable = Stackable;
        this.Texture = texture;
    }

}

