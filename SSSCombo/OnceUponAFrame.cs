using Dalamud.Plugin;
using System.Collections.Generic;
using Dalamud.Game.ClientState.Statuses;
using Dalamud.Plugin.Services;
using System.Linq;
using System;

namespace SSSCombo
{
    public unsafe partial class SSSCombo : IDalamudPlugin
    {
        private void OnceUponAFrame(Object _) 
        {
            if (Configuration.Enabled == false) MainWindow.IsOpen = false; else MainWindow.IsOpen = true;
            if (Configuration.Demo == true) { SSSCounter = 8; MainWindow.IsOpen = true; } 
            else if (SSSCounter == 8) SSSCounter = 0;

            var player = Services.ClientState.LocalPlayer;
            if (player == null) return; // Player is not logged in, nothing we can do.

            //var playerStatusList = Services.ClientState.LocalPlayer?.StatusList.ToList();
            //if (playerStatusList is null) return;

            //foreach (var playerStatus in playerStatusList) {
            //    Services.Log.Verbose($"The player's status list is: {playerStatus.GameData?.Name} ({playerStatus.GameData?.RowId}) at {playerStatus.GameData?.Icon}");
            //}

            StatusList statusList = Services.ClientState.LocalPlayer.StatusList;
            List<uint> vulnStatuses = new List<uint>([64, 202, 444, 563, 638, 714, 806, 893, 1054, 1208, 1402, 1412, 1597, 1789, 1845, 2213, 2347, 2912, 3361, 3366, 3557]);
            List<uint> vulnIcons = new List<uint>([15020, 17101, 17102, 17103, 17104, 17105, 17106, 17107, 17108, 17109, 17010, 17011, 17012, 17013, 17014, 17015, 17016, 18441, 18442, 18443, 18444, 18445, 18446, 18447, 18448, 18449, 18450, 18681, 18682, 18683, 18684, 18685, 18686, 18687, 18688, 18689, 19861, 19862, 19863]);

            if (Services.ClientState.LocalPlayer.IsDead)
            {
                SSSCounter = 0;
                Dead = true;
            }
            else
            {
                Dead = false;
            }

            foreach (var item in statusList)
            {
                if (vulnIcons.Contains(item.GameData.Icon))
                {
                    Services.Log.Verbose($"Statut {item.StatusId} / Icon : {item.GameData.Icon.ToString()}");
                    currentVulnTimer = item.RemainingTime;

                    if (currentVulnTimer > vulnTimer)
                    {
                        vulnTimer = currentVulnTimer;
                        SSSCounter -= 1;
                    }
                }
                else
                {
                    currentVulnTimer = 0;
                }
            }

            // RankDown();
            // RankUp();
            // RankReset();
            // https://github.com/goatcorp/Dalamud/blob/d41682b66e0da3eea80d40722a201a3fc2649303/Dalamud/Interface/Internal/Windows/ComponentDemoWindow.cs#L121
            //Random random = new Random();
            //SSSCounter = random.Next(6);



        }
    }
}
