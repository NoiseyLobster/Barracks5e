using Newtonsoft.Json;
using System.Linq;

namespace Barracks5e
{
    public class Person
    {
        public Class AdventureClass { get; private set; }

        public Race FantasyRace { get; private set; }

        private int Strength { get; set; }

        public int StrengthMod
        {
            get { return ConvertStatToModifier(Strength); }
        }

        private int Dexterity { get; set; }

        public int DexterityMod
        {
            get { return ConvertStatToModifier(Dexterity); }
        }

        private int Constitution { get; set; }

        public int ConstitutionMod
        {
            get { return ConvertStatToModifier(Constitution); }
        }

        private int Intelligence { get; set; }

        public int IntelligenceMod
        {
            get { return ConvertStatToModifier(Intelligence); }
        }

        private int Wisdom { get; set; }

        public int WisdomMod
        {
            get { return ConvertStatToModifier(Wisdom); }
        }

        private int Charisma { get; set; }

        public int CharismaMod
        {
            get { return ConvertStatToModifier(Charisma); }
        }

        private List<int> AbilityScores { get; set; }

        private const string FANTASY_RACE_STATS_FILE_LOCATION = @"Content\BaseStats\FantasyRace.json";

        public Person(Race race, Class adventureClass)
        {
            AdventureClass = adventureClass;
            FantasyRace = race;

            List<int> abilityScores = new(6);
            Random randomNum = new();
            for (int i = 0; i < 6; i++)
            {
                List<int> diceRolls = [];

                //roll a six-sided dice four times
                diceRolls.Add(randomNum.Next(1, 7));
                diceRolls.Add(randomNum.Next(1, 7));
                diceRolls.Add(randomNum.Next(1, 7));
                diceRolls.Add(randomNum.Next(1, 7));

                //remove the lowest value
                diceRolls.Remove(diceRolls.Min());

                //store the sum of the rest as our ability score
                abilityScores.Add(diceRolls.Sum());
            }

            //sort our ability scores to make assignment easier
            AbilityScores = abilityScores.OrderByDescending(abilityScore => abilityScore).ToList();

            //lookup the default stats for whichever fantasy race this person belongs to
            using StreamReader reader = new(FANTASY_RACE_STATS_FILE_LOCATION);
            string json = reader.ReadToEnd();
            List<FantasyRaceJson> fantasyRaces = JsonConvert.DeserializeObject<List<FantasyRaceJson>>(json) ?? [];

            //filter for the selected fantasy race
            FantasyRaceJson selectedRace = fantasyRaces?.Find(fantasyRace => fantasyRace.Race == race) ?? new();
            AbilityScoreModifiersJson selectedRaceAbilityScoreBuffs = selectedRace?.DefaultStats.AbilityScores ?? new();

            //assign ability scores based on selected class
            switch (adventureClass)
            {
                case Class.Barbarian:
                    Strength = selectedRaceAbilityScoreBuffs.StrengthBuff + AbilityScores[0];
                    Constitution = selectedRaceAbilityScoreBuffs.ConstitutionBuff + AbilityScores[1];
                    Dexterity = selectedRaceAbilityScoreBuffs.DexterityBuff + AbilityScores[2];
                    Wisdom = selectedRaceAbilityScoreBuffs.WisdomBuff + AbilityScores[3];
                    Charisma = selectedRaceAbilityScoreBuffs.CharismaBuff + AbilityScores[4];
                    Intelligence = selectedRaceAbilityScoreBuffs.IntelligenceBuff + AbilityScores[5];
                    break;
                //TODO: Other class assignments
                default:
                    Strength = selectedRaceAbilityScoreBuffs.StrengthBuff + AbilityScores[0];
                    Constitution = selectedRaceAbilityScoreBuffs.ConstitutionBuff + AbilityScores[1];
                    Dexterity = selectedRaceAbilityScoreBuffs.DexterityBuff + AbilityScores[2];
                    Wisdom = selectedRaceAbilityScoreBuffs.WisdomBuff + AbilityScores[3];
                    Charisma = selectedRaceAbilityScoreBuffs.CharismaBuff + AbilityScores[4];
                    Intelligence = selectedRaceAbilityScoreBuffs.IntelligenceBuff + AbilityScores[5];
                    break;
            }
        }

        private static int ConvertStatToModifier(int stat)
        {
            return (stat - 10) / 2; //integer math
        }
    }
}
