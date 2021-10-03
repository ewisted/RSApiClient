using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.Hiscores.Models
{
    public abstract record HiscoresLiteSnapshotBase
    {
        public Skill Overall { get; init; }
        public Skill Attack { get; init; }
        public Skill Defence { get; init; }
        public Skill Strength { get; init; }
        public Skill Hitpoints { get; init; }
        public Skill Ranged { get; init; }
        public Skill Prayer { get; init; }
        public Skill Magic { get; init; }
        public Skill Cooking { get; init; }
        public Skill Woodcutting { get; init; }
        public Skill Fletching { get; init; }
        public Skill Fishing { get; init; }
        public Skill Firemaking { get; init; }
        public Skill Crafting { get; init; }
        public Skill Smithing { get; init; }
        public Skill Mining { get; init; }
        public Skill Herblore { get; init; }
        public Skill Agility { get; init; }
        public Skill Thieving { get; init; }
        public Skill Slayer { get; init; }
        public Skill Farming { get; init; }
        public Skill Runecrafting { get; init; }
        public Skill Hunter { get; init; }
        public Skill Construction { get; init; }
        public Activity LeaguePoints { get; init; }
        public Activity BountyHunterHunter { get; init; }
        public Activity BountyHunterRogue { get; init; }
        public Activity ClueScrollsAll { get; init; }
        public Activity ClueScrollsBeginner { get; init; }
        public Activity ClueScrollsEasy { get; init; }
        public Activity ClueScrollsMedium { get; init; }
        public Activity ClueScrollsHard { get; init; }
        public Activity ClueScrollsElite { get; init; }
        public Activity ClueScrollsMaster { get; init; }
        public Activity LastManStanding { get; init; }
        public Activity SoulWarsZeal { get; init; }
        public Boss AbyssalSire { get; init; }
        public Boss AlchemicalHydra { get; init; }
        public Boss BarrowsChests { get; init; }
        public Boss Bryophyta { get; init; }
        public Boss Callisto { get; init; }
        public Boss Cerberus { get; init; }
        public Boss ChambersOfXeric { get; init; }
        public Boss ChambersOfXericChallengeMode { get; init; }
        public Boss ChaosElemental { get; init; }
        public Boss ChaosFanatic { get; init; }
        public Boss CommanderZilyana { get; init; }
        public Boss CorporealBeast { get; init; }
        public Boss CrazyArchaeologist { get; init; }
        public Boss DagannothPrime { get; init; }
        public Boss DagannothRex { get; init; }
        public Boss DagannothSupreme { get; init; }
        public Boss DerangedArchaeologist { get; init; }
        public Boss GeneralGraardor { get; init; }
        public Boss GiantMole { get; init; }
        public Boss GrotesqueGuardians { get; init; }
        public Boss Hespori { get; init; }
        public Boss KalphiteQueen { get; init; }
        public Boss KingBlackDragon { get; init; }
        public Boss Kraken { get; init; }
        public Boss Kreearra { get; init; }
        public Boss KrilTsutsaroth { get; init; }
        public Boss Mimic { get; init; }
        public Boss Nightmare { get; init; }
        public Boss PhosanisNightmare { get; init; }
        public Boss Obor { get; init; }
        public Boss Sarachnis { get; init; }
        public Boss Scorpia { get; init; }
        public Boss Skotizo { get; init; }
        public Boss Tempoross { get; init; }
        public Boss TheGauntlet { get; init; }
        public Boss TheCorruptedGauntlet { get; init; }
        public Boss TheatreOfBlood { get; init; }
        public Boss TheatreOfBloodHardMode { get; init; }
        public Boss ThermonuclearSmokeDevil { get; init; }
        public Boss TzkalZuk { get; init; }
        public Boss TztokJad { get; init; }
        public Boss Venenatis { get; init; }
        public Boss Vetion { get; init; }
        public Boss Vorkath { get; init; }
        public Boss Wintertodt { get; init; }
        public Boss Zalcano { get; init; }
        public Boss Zulrah { get; init; }
    }
}
