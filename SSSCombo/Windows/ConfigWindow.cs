using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SSSCombo.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    public ConfigWindow(SSSCombo plugin) : base(
        "SSS Combo Config",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(200, 150);
        this.SizeCondition = ImGuiCond.Always;

        this.Configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void Draw()
    {
        // can't ref a property, so use a local copy
        var enabledValue = this.Configuration.Enabled;
        var draggableValue = this.Configuration.Draggable;
        var fullValue = this.Configuration.Full;
        if (ImGui.Checkbox("Enabled", ref enabledValue))
        {
            this.Configuration.Enabled = enabledValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Draggable", ref draggableValue))
        {
            this.Configuration.Draggable = draggableValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Full", ref fullValue))
        {
            this.Configuration.Full = fullValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }

        if (ImGui.BeginCombo("##combo", this.Configuration.ComboDifficulty.ToString())) // The second parameter is the label previewed before opening the combo.
        {
            foreach (var diff in Enums.ComboDifficulties.ToList())
            {
                bool is_selected = (current_item == items[n]); // You can store your selection however you want, outside or inside your objects
                if (ImGui.Selectable(items[n], is_selected)
                    current_item = items[n];
                if (is_selected)
                    ImGui.SetItemDefaultFocus();   // You may set the initial focus when opening the combo (scrolling + for keyboard navigation support)
            }
            ImGui.EndCombo();
        }
    }
}
