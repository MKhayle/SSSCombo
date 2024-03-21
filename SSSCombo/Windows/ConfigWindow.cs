using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    public ConfigWindow(Plugin plugin) : base(
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
        var clickthroughValue = this.Configuration.Clickthrough;
        var fullValue = this.Configuration.Full;
        if (ImGui.Checkbox("Enabled", ref enabledValue))
        {
            this.Configuration.Enabled = enabledValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Clickthrough", ref clickthroughValue))
        {
            this.Configuration.Clickthrough = clickthroughValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        if (ImGui.Checkbox("Full", ref fullValue))
        {
            this.Configuration.Full = fullValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
    }
}
