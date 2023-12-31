using Newtonsoft.Json;
using System.Linq;

namespace Barracks5e
{
    public class Person(Race race)
    {
        #region Properties

        public Race FantasyRace { get; protected set; } = race;

        public int Level { get; protected set; }

        public int HitPoints { get; protected set; }

        public int ArmorClass { get; protected set; }

        #region Weapon & Equipment

        public Hand WeaponEncumberance 
        {
            get 
            {
                if(EquippedWeaponRight == Weapon.Unarmed && EquippedWeaponLeft == Weapon.Unarmed)
                {
                    return Hand.Empty;
                }
                else if(EquippedWeaponRight != Weapon.Unarmed && EquippedWeaponLeft != Weapon.Unarmed)
                {
                    return Hand.Both;
                }
                else if(EquippedWeaponRight != Weapon.Unarmed && EquippedWeaponLeft == Weapon.Unarmed)
                {
                    return Hand.Right;
                }
                else 
                {
                    return Hand.Left;
                }
            }
        }

        public Weapon EquippedWeaponRight { get; protected set; } = Weapon.Unarmed;

        public Weapon EquippedWeaponLeft { get; protected set; } = Weapon.Unarmed;

        public Armor EquippedArmor { get; protected set; } = Armor.Unarmored;

        public bool IsShieldEquipped { get; set; } = false;

        #endregion

        #region Hit Dice Properties

        protected int HitDiceType { get; set; } = 6;

        protected int HitDiceCount { get; set; }

        protected int HitDiceRemaining { get; set; }

        #endregion

        #region Ability Score Properties

        protected int Strength { get; set; }

        public int StrengthMod
        {
            get { return ConvertStatToModifier(Strength); }
        }

        protected int Dexterity { get; set; }

        public int DexterityMod
        {
            get { return ConvertStatToModifier(Dexterity); }
        }

        protected int Constitution { get; set; }

        public int ConstitutionMod
        {
            get { return ConvertStatToModifier(Constitution); }
        }

        protected int Intelligence { get; set; }

        public int IntelligenceMod
        {
            get { return ConvertStatToModifier(Intelligence); }
        }

        protected int Wisdom { get; set; }

        public int WisdomMod
        {
            get { return ConvertStatToModifier(Wisdom); }
        }

        protected int Charisma { get; set; }

        public int CharismaMod
        {
            get { return ConvertStatToModifier(Charisma); }
        }

        protected List<int> AbilityScores { get; set; } = GenerateAbilityScores();

        #endregion

        #region Proficiencies

        protected List<ArmorType> ArmorProficiencies { get; set; } = [];

        protected List<WeaponType> WeaponProficiencies { get; set; } = [];

        protected List<ToolType> ToolProficiencies { get; set; } = [];

        protected List<AbilityScoreType> SavingThrowProficiencies { get; set; } = [];

        protected List<Resistance> Resistances { get; set; } = [];

        #endregion

        #endregion

        #region Private/Internal Methods

        private static int ConvertStatToModifier(int stat)
        {
            return (stat - 10) / 2; //integer math
        }

        #endregion

        #region Protected/Public Methods

        protected static List<int> GenerateAbilityScores()
        {
            List<int> abilityScores = new(6);
            for (int i = 0; i < 6; i++)
            {
                //roll a six-sided dice four times and remove the lowest from the set
                List<int> diceRolls = DiceHelper.RollWithExclusions(6, 4, 1);

                //store the sum of the rest as our ability score
                abilityScores.Add(diceRolls.Sum());
            }

            //sort our ability scores to make assignment easier and return
            return abilityScores.OrderByDescending(abilityScore => abilityScore).ToList();
        }

        protected static FantasyRace FetchDefaultBuffsByRace(Race fantasyRace)
        {
            using StreamReader reader = new(@"Stats\FantasyRace\FantasyRace.json");
            string json = reader.ReadToEnd();

            List<FantasyRaceJson> statsCollection = JsonConvert.DeserializeObject<List<FantasyRaceJson>>(json) ?? [];

            //filter for the selected fantasy race
            FantasyRaceJson selection = statsCollection?.Find(selection => selection.Race == fantasyRace) ?? new();

            return new FantasyRace(selection);
        }

        #endregion
    }
}
