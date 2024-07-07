using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using SSSCombo.Windows;
using ImGuiNET;
using SSSCombo.Enums;
using System;
using static FFXIVClientStructs.FFXIV.Common.Component.BGCollision.MeshPCB;
using System.Collections.Generic;
using Dalamud.Interface.Textures.TextureWraps;

namespace SSSCombo
{
    public partial class SSSCombo : IDalamudPlugin
    {
        public string Name => "SSS Combo";
        private const string CommandName = "/ssscombo";
        public int SSSCounter = 0;
        public bool Dead = false;
        public float currentVulnTimer = 0;
        public float vulnTimer = 0;
        public string ComboTimer = "0";
        public List<IDalamudTextureWrap> fullPictures = new();
        public List<IDalamudTextureWrap> letterPictures = new();
        public Configuration Configuration { get; init; }

        public WindowSystem WindowSystem = new("SSSCombo");

        private ConfigWindow ConfigWindow { get; init; }
        private MainWindow MainWindow { get; init; }

        public SSSCombo(
            IDalamudPluginInterface pluginInterface,
            ICommandManager commandManager)
        {
            pluginInterface.Create<Services>();
            Configuration = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

            this.Configuration.Initialize(Services.PluginInterface);

            // you might normally want to embed resources and load them from the manifest stream
            foreach (var name in Enum.GetNames<Styles.Ranks>())
            {

                var fullPath = File.ReadAllBytes(Path.Combine(Services.PluginInterface.AssemblyLocation.Directory?.FullName!, $"full/{name}.png"));
                var fullImage = Services.TextureProvider.CreateFromImageAsync(fullPath);
                fullPictures.Add(fullImage.Result);

                var imagePath = File.ReadAllBytes(Path.Combine(Services.PluginInterface.AssemblyLocation.Directory?.FullName!, $"letters/{name}.png"));
                var letterImage = Services.TextureProvider.CreateFromImageAsync(imagePath);
                letterPictures.Add(letterImage.Result);
            }

            ConfigWindow = new ConfigWindow(this);
            MainWindow = new MainWindow(this, fullPictures, letterPictures);
            
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);

            commandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Opens the settings for the SSS Combo plugin."
            });

            Services.Framework.Update += OnceUponAFrame;
            Services.PluginInterface.UiBuilder.Draw += DrawUI;
            Services.PluginInterface.UiBuilder.OpenMainUi += DrawMainUI;
            Services.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            if (Configuration.Enabled) MainWindow.IsOpen = true;
            //else MainWindow.Toggle();
            
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();
            
            ConfigWindow.Dispose();
            MainWindow.Dispose();
            Services.Framework.Update -= this.OnceUponAFrame;
            Services.CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            //in response to the slash command, just display our main ui

            if (args == "plus") SSSCounter++;
            else if (args == "reset") SSSCounter = 0;
            else if (args == "minus") SSSCounter--;
            else ConfigWindow.IsOpen = true;
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            ConfigWindow.IsOpen = true;
        }

        public void DrawMainUI()
        {
            MainWindow.IsOpen = true;
        }
    }
}
