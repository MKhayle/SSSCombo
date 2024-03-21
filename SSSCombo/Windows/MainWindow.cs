using System;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private IDalamudTextureWrap LetterImage;
    private IDalamudTextureWrap FullImage;
    private Plugin Plugin;

    public MainWindow(Plugin plugin, IDalamudTextureWrap letterImage, IDalamudTextureWrap fullImage) : base(
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
        this.LetterImage.Dispose();
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

        if (this.Plugin.Configuration.Clickthrough)
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
            //ImGui.Text("current rank:");
            if(Plugin.Configuration.Full) ImGui.Image(this.FullImage.ImGuiHandle, new Vector2(this.FullImage.Width, this.FullImage.Height));
            else ImGui.Image(this.LetterImage.ImGuiHandle, new Vector2(this.LetterImage.Width, this.LetterImage.Height));
        }

    }
}
