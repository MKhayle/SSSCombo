using Dalamud.Plugin;
using System.Collections.Generic;
using Dalamud.Game.ClientState.Statuses;
using Dalamud.Plugin.Services;
using System.Linq;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using Dalamud.Game.ClientState.Objects.SubKinds;

namespace SSSCombo
{
    public unsafe partial class SSSCombo : IDalamudPlugin
    {
        //More stealing from Zeffuro
        private void OnActionUsed(uint sourceId, IntPtr sourceCharacter, IntPtr pos, IntPtr effectHeader,
            IntPtr effectArray, IntPtr effectTrail)
        {
            _onActionUsedHook?.Original(sourceId, sourceCharacter, pos, effectHeader, effectArray, effectTrail);

            IPlayerCharacter? player = Services.ClientState.LocalPlayer;
            if (player == null || sourceId != player.GameObjectId) { return; }

            int actionId = Marshal.ReadInt32(effectHeader, 0x8);
            
            Services.Log.Debug($"{(uint)actionId}");
            //Grab GCD here?
        }
        
        private void OnCast(uint sourceId, IntPtr ptr)
        {
            _onCastHook?.Original(sourceId, ptr);

            IPlayerCharacter? player = Services.ClientState.LocalPlayer;
            if (player == null || sourceId != player.GameObjectId) { return; }

            int value = Marshal.ReadInt16(ptr);
            uint actionId = value < 0 ? (uint)(value + 65536) : (uint)value;
            
            Services.Log.Debug($"{(uint)actionId}");
        }
        
        public enum TimelineItemType
        {
            Action = 0,
            CastStart = 1,
            CastCancel = 2,
            OffGCD = 3,
            AutoAttack = 4
        }
        
    }
}
