using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Textures.TextureWraps;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SSSCombo.Windows;

public class MainWindow : Window, IDisposable
{
    private List<IDalamudTextureWrap>? LetterImage;
    private List<IDalamudTextureWrap>? FullImage;
    private SSSCombo Plugin;

    public MainWindow(SSSCombo plugin, List<IDalamudTextureWrap> letterImage, List<IDalamudTextureWrap> fullImage) : base(
        "SSSCombo counter", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize
        //| ImGuiWindowFlags.NoMouseInputs | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoTitleBar  
        )
    {
        this.Plugin = plugin;
        this.LetterImage = letterImage;
        this.FullImage = fullImage;
    }

    public void Dispose()
    {
        LetterImage = null;
        FullImage = null;
    }

    public override void Draw()
    {
        if (!this.Plugin.Configuration.Full)
        {
            this.SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(133, 115),
                MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
            };
        }
        else
        {
            this.SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(462, 266),
                MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
            };
        }

        if (!this.Plugin.Configuration.Draggable)
        {
            this.Flags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoMouseInputs | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize;
            this.AllowClickthrough = true;
        }
        else
        {
            this.Flags = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.AlwaysAutoResize;
            this.AllowClickthrough = false;
        }

        if (this.Plugin.Configuration.Enabled)
        {
            ImGui.Text($"current vuln timer: {Plugin.currentVulnTimer}");
            ImGui.Text($"Dead ? {Plugin.Dead.ToString()}");
            ImGui.Text($"current rank:{Plugin.SSSCounter.ToString()}");
            if(!Plugin.Configuration.Full) ImGui.Image(this.FullImage[Plugin.SSSCounter].ImGuiHandle, new Vector2(this.FullImage[Plugin.SSSCounter].Width, this.FullImage[Plugin.SSSCounter].Height));
            else ImGui.Image(this.LetterImage[Plugin.SSSCounter].ImGuiHandle, new Vector2(this.LetterImage[Plugin.SSSCounter].Width, this.LetterImage[Plugin.SSSCounter].Height));
        }
        else
        {

        }

    }
}
