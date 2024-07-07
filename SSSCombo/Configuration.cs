using Dalamud.Configuration;
using Dalamud.Plugin;
using System;

namespace SSSCombo
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 1;

        public bool Enabled { get; set; } = true;
        public bool Draggable { set; get; } = false;
        public bool Full { set; get; } = false;

        public int ComboDifficulty { set; get; } = 1;

        // the below exist just to make saving less cumbersome
        [NonSerialized]
        private IDalamudPluginInterface? PluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.PluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.PluginInterface!.SavePluginConfig(this);
        }
    }
}
