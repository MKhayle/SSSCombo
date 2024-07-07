using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using Dalamud.Game.ClientState.Objects;
using Lumina;

namespace SSSCombo
{
    public class Services
    {
        [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
        [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
        [PluginService] internal static IFramework Framework { get; private set; } = null!; 
        [PluginService] internal static IGameInteropProvider GameInteropProvider { get; private set; } = null!;
        [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
        [PluginService] internal static IClientState ClientState { get; private set; }
        [PluginService] internal static IDataManager Data { get; private set; }
        [PluginService] internal static IBuddyList BuddyList { get; private set; } = null!; 
        [PluginService] internal static ITargetManager TargetManager { get; private set; } = null!;
        [PluginService] internal static ICondition Condition { get; private set; } = null!;
        [PluginService] internal static IPluginLog Log { get; private set; } = null!;
        [PluginService] internal static IDutyState DutyState { get; private set; } = null!;
    }
}
