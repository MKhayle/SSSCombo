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
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "SSS Combo";
        private const string CommandName = "/ssscombo";
        public int SSSCounter = 0;
        public List<IDalamudTextureWrap> fullPictures = new();
        public List<IDalamudTextureWrap> letterPictures = new();

        [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
        [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
        [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
        public Configuration Configuration { get; init; }

        public WindowSystem WindowSystem = new("SSSCombo");

        private ConfigWindow ConfigWindow { get; init; }
        private MainWindow MainWindow { get; init; }

        public Plugin(
            IDalamudPluginInterface pluginInterface,
            ICommandManager commandManager)
        {
            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

            this.Configuration.Initialize(PluginInterface);

            // you might normally want to embed resources and load them from the manifest stream
            foreach (var name in Enum.GetNames<Styles.Ranks>())
            {

                var fullPath = File.ReadAllBytes(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, $"full/{name}.png"));
                var fullImage = TextureProvider.CreateFromImageAsync(fullPath);
                fullPictures.Add(fullImage.Result);

                var imagePath = File.ReadAllBytes(Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, $"letters/{name}.png"));
                var letterImage = TextureProvider.CreateFromImageAsync(imagePath);
                letterPictures.Add(letterImage.Result);
            }

            ConfigWindow = new ConfigWindow(this);
            MainWindow = new MainWindow(this, fullPictures, letterPictures);
            
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);

            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Opens the settings for the SSS Combo plugin."
            });

            PluginInterface.UiBuilder.Draw += DrawUI;
            PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            if(Configuration.Enabled) MainWindow.IsOpen = true;
            
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();
            
            ConfigWindow.Dispose();
            MainWindow.Dispose();
            
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            //in response to the slash command, just display our main ui
            ConfigWindow.IsOpen = true;

            if (args == "plus") SSSCounter++;
            if (args == "reset") SSSCounter = 0;
            if (args == "minus") SSSCounter--;
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            ConfigWindow.IsOpen = true;
        }
    }
}
