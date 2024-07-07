using System;
using System.Numerics;
using Dalamud.Interface.Components;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using SSSCombo.Enums;

namespace SSSCombo.Windows;

public class ConfigWindow : Window, IDisposable
{
    private Configuration Configuration;

    public ConfigWindow(SSSCombo plugin) : base(
        "SSS Combo Config",
        ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
        ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.Size = new Vector2(480, 260);
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
        var demo = this.Configuration.Demo;
        bool newplayers = this.Configuration.ComboDifficulty == 1 ? true : false;
        bool casuals = this.Configuration.ComboDifficulty == 2 ? true : false;
        bool raiders = this.Configuration.ComboDifficulty == 3 ? true : false;
        

        if (ImGui.Checkbox("Enabled", ref enabledValue))
        {
            this.Configuration.Enabled = enabledValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Draggable", ref draggableValue))
        {
            this.Configuration.Draggable = draggableValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Full words", ref fullValue))
        {
            this.Configuration.Full = fullValue;
            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        ImGui.SameLine();
        if (ImGui.Checkbox("Demo mode", ref demo))
        {
            this.Configuration.Demo = demo;

            // can save immediately on change, if you don't want to provide a "Save and Close" button
            this.Configuration.Save();
        }
        ImGuiComponents.HelpMarker("By default, the combo counter won't display outside of instances.\nYou can enable this demo mode to drag & place the counter's window to an appropriate position.");

        ImGui.Separator();
        ImGui.Text("Difficulty configuration");
        ImGui.Separator();
        if (ImGui.BeginTabBar("Tabs"))
        {
            if (ImGui.BeginTabItem("New players"))
            {
                ImGui.Text("This is the new players configuration, to reduce stress while improving.");
                ImGui.Separator();
                ImGui.BulletText("You will combo up by using your GCDs without drifting too much.");
                ImGui.SameLine();
                ImGuiComponents.HelpMarker("\"Drifting\" is when you delay your Global Cooldown between two weaponskills or spells.\nTry pressing your buttons faster for better combos!.");
                ImGui.BulletText("You will combo down when you're being hit by a Vulnerability debuff.");
                ImGui.BulletText("Upon dying, your c-c-c-c-combo breaks & resets. Don't die!");
                ImGui.Text("");

                if (ImGui.Checkbox("Choose new players difficulty", ref newplayers))
                {
                    this.Configuration.ComboDifficulty = 1;
                    // can save immediately on change, if you don't want to provide a "Save and Close" button
                    this.Configuration.Save();
                }
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Casuals"))
            {
                ImGui.Text("This is the casual players configuration, spicing it up a bit.");
                ImGui.Separator();
                ImGui.BulletText("You will combo up by using your GCDs without drifting excessively.");
                ImGui.SameLine();
                ImGuiComponents.HelpMarker("\"Drifting\" is when you delay your Global Cooldown between two weaponskills or spells.\nTry pressing your buttons faster for better combos!.");
                ImGui.BulletText("You will combo down when drifting or being hit by a Vulnerability debuff.");
                ImGui.BulletText("Upon dying, your c-c-c-c-combo breaks & resets. Don't die!");
                ImGui.Text("");

                if (ImGui.Checkbox("Choose casual players difficulty", ref casuals))
                {
                    this.Configuration.ComboDifficulty = 2;
                    // can save immediately on change, if you don't want to provide a "Save and Close" button
                    this.Configuration.Save();
                }
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Raiders"))
            {
                ImGui.Text("This is the extreme/savage raiders configuration.");
                ImGui.Separator();
                ImGui.BulletText("You will combo up by using your GCDs without drifting.");
                ImGui.SameLine();
                ImGuiComponents.HelpMarker("\"Drifting\" is when you delay your Global Cooldown between two weaponskills or spells.\nTry pressing your buttons faster for better combos!.");
                ImGui.BulletText("You will combo down when drifting or being hit by a Vulnerability debuff.");
                ImGui.BulletText("Upon dying, your c-c-c-c-combo breaks & resets. Don't die!");
                ImGui.Text("");

                if (ImGui.Checkbox("Choose raiders difficulty", ref raiders))
                {
                    this.Configuration.ComboDifficulty = 3;
                    // can save immediately on change, if you don't want to provide a "Save and Close" button
                    this.Configuration.Save();
                }
                ImGui.EndTabItem();
            }
            if (ImGui.BeginTabItem("Mentors"))
                {
                    ImGui.Text("Please remove your crown and take a shower.");
                    ImGui.EndTabItem();
                }
            ImGui.EndTabBar();
        }
    }
}
