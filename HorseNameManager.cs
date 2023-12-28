using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseNameManager : MonoBehaviour
{
    private static readonly List<string> horseNames = new()
    {
        "LootinLou", "GoldGrub", "BanditBob", "HorseThif", "GangstaGus",
        "DustyDebt", "WantedWes", "BigBounty", "RobbinRay", "HeistHank",
        "GunsGerry", "OutlawOllie", "SaddleSam", "MugshotMax", "JailbreakJ",
        "RogueRider", "BandanaBen", "BurglarBill", "CashClint", "CrookCarl",
        "Desperado", "EscaperEd", "FelonFrank", "GrifterGus", "HijackHal",
        "HoodlumHue", "JackerJoe", "KidnapKen", "LarcenLars", "MobsterMoe",
        "MuggerMick", "PiratePete", "PlunderPat", "RansackRon", "RascalRay",
        "RifleRick", "RobberRob", "RustlerRuss", "Scoundrel", "ShooterSid",
        "SmugglerSy", "SnatcherStu", "SwindlerSly", "ThiefTheo", "VandalVic",
        "LawlessLee", "BanditBart", "GallopingG", "QuickDrawQ", "RiderRex",
        "CactusCal", "GrittyGrit", "SpurSpencer", "TrailblazeT", "BuckarooBud",
        "WildWestWes", "MustangMac", "CowboyClyde", "PonyPaul", "WranglerRay",
        "BroncoBill", "RancherRaul", "StampedeSid", "TumbleweedT", "SagebrushSal",
        "OutbackOzzy", "DakotaDan", "BisonBuck", "NevadaNed", "CheyenneChet",
        "LaramieLou", "MontanaMonty", "GamblerGil", "ProspectorP", "BrawlerBrad",
        "GauchoGary", "PrairiePete", "SierraSam", "CoyoteCarl", "RodeoRonny",
        "HoofHank", "CorralCarl", "LassoLarry", "RoundupRuss", "ChapsChet",
        "SheriffShaw", "DeputyDave", "MarshallMat", "RangerRick", "VigilanteVic",
        "OutlawOrson", "BanditBeau", "RustlerRudy", "GunslingerG", "DesperDan",
        "BountyBert", "DuelDuke", "HorseshoeHal", "SaddlebackS", "TrailTrent"
    };


    private static List<int> usedHorseNamesIndexes = new();

    private static int counter = 0;

    public static string GetRandomHorseName()
    {
        int randomIndex;
        // Generate a new random index, if it's already in use.
        do
        {
            randomIndex = Random.Range(0, horseNames.Count);
        } while (usedHorseNamesIndexes.Contains(randomIndex));

        // Marking the name as used.
        usedHorseNamesIndexes.Add(randomIndex);

        return horseNames[randomIndex];
    }

    public static void ResetUsedNamesList()
    {
        usedHorseNamesIndexes.Clear();
    }

    public static List<string> GetInstantiatedHorsesNames()
    {
        List<string> usedHorses = new();
        foreach (var n in usedHorseNamesIndexes)
        {
            usedHorses.Add(horseNames[n]);
        }

        //counter++;
        //Debug.Log("Called: " + counter);
        return usedHorses;
    }
}