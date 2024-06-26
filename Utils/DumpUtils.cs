using System.Collections.Generic;
using System.IO;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using DynamicMaps.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace DynamicMaps.Utils
{
    public static class DumpUtils
    {
        public static void DumpExtracts()
        {
            var gameWorld = Singleton<GameWorld>.Instance;
            var scavExfils = gameWorld.ExfiltrationController.ScavExfiltrationPoints;
            var pmcExfils = gameWorld.ExfiltrationController.ExfiltrationPoints;

            var dump = new List<MapMarkerDef>();

            foreach (var scavExfil in scavExfils)
            {
                var dumped = new MapMarkerDef
                {
                    Category = "Extract",
                    ShowInRaid = false,
                    ImagePath = "Markers/exit.png",
                    Text = scavExfil.Settings.Name.BSGLocalized(),
                    Position = MathUtils.ConvertToMapPosition(scavExfil.transform),
                    Color = Color.Lerp(Color.yellow, Color.red, 0.5f)
                };

                dump.Add(dumped);
            }

            foreach (var pmcExfil in pmcExfils)
            {
                var dumped = new MapMarkerDef
                {
                    Category = "Extract",
                    ShowInRaid = false,
                    ImagePath = "Markers/exit.png",
                    Text = pmcExfil.Settings.Name.BSGLocalized(),
                    Position = MathUtils.ConvertToMapPosition(pmcExfil.transform),
                    Color = Color.green
                };

                dump.Add(dumped);
            }

            var mapName = GameUtils.GetCurrentMapInternalName();
            var dumpString = JsonConvert.SerializeObject(dump, Formatting.Indented);
            File.WriteAllText(Path.Combine(Plugin.Path, $"{mapName}-extracts.json"), dumpString);

            Plugin.Log.LogInfo("Dumped extracts");
        }

        public static void DumpSwitches()
        {
            var switches = GameObject.FindObjectsOfType<Switch>();
            var dump = new List<MapMarkerDef>();

            foreach (var @switch in switches)
            {
                if (!@switch.Operatable || !@switch.HasAuthority)
                {
                    continue;
                }

                var dumped = new MapMarkerDef
                {
                    Category = "Switch",
                    ImagePath = "Markers/lever.png",
                    Text = @switch.name,
                    Position = MathUtils.ConvertToMapPosition(@switch.transform)
                };

                dump.Add(dumped);
            }

            var mapName = GameUtils.GetCurrentMapInternalName();
            var dumpString = JsonConvert.SerializeObject(dump, Formatting.Indented);
            File.WriteAllText(Path.Combine(Plugin.Path, $"{mapName}-switches.json"), dumpString);

            Plugin.Log.LogInfo("Dumped switches");
        }

        public static void DumpLocks()
        {
            var objects = GameObject.FindObjectsOfType<WorldInteractiveObject>();
            var dump = new List<MapMarkerDef>();
            var i = 1;

            foreach (var locked in objects)
            {
                if (string.IsNullOrEmpty(locked.KeyId) || !locked.Operatable)
                {
                    continue;
                }

                var dumped = new MapMarkerDef
                {
                    Text = $"door {i++}",
                    Category = "LockedDoor",
                    ImagePath = "Markers/door_with_lock.png",
                    Position = MathUtils.ConvertToMapPosition(locked.transform),
                    AssociatedItemId = locked.KeyId,
                    Color = Color.yellow
                };

                dump.Add(dumped);
            }

            var mapName = GameUtils.GetCurrentMapInternalName();
            var dumpString = JsonConvert.SerializeObject(dump, Formatting.Indented);
            File.WriteAllText(Path.Combine(Plugin.Path, $"{mapName}-locked.json"), dumpString);

            Plugin.Log.LogInfo("Dumped locks");
        }
    }
}
