using System.Runtime.Serialization;

namespace Barracks5e
{
    public class FantasyRace 
    {
        public Size Size { get; set; }

        public int Speed { get; set; }

        public int Darkvision { get; set; }

        public List<Language> Languages = [];

        public int StrengthBuff { get; set; } = 0;

        public int DexterityBuff { get; set; } = 0;

        public int ConstitutionBuff { get; set; } = 0;

        public int IntelligenceBuff { get; set; } = 0;

        public int WisdomBuff { get; set; } = 0;

        public int CharismaBuff { get; set; } = 0;

        public FantasyRace(FantasyRaceJson json) 
        {
            DefaultStatsJson stats = json.DefaultStats;
            
            Size = stats.Size;
            Speed = stats.Speed;
            Darkvision = stats.Darkvision;
            Languages = stats.Languages;

            StrengthBuff = stats.AbilityScores.StrengthBuff;
            DexterityBuff = stats.AbilityScores.DexterityBuff;
            ConstitutionBuff = stats.AbilityScores.ConstitutionBuff;
            IntelligenceBuff = stats.AbilityScores.IntelligenceBuff;
            WisdomBuff = stats.AbilityScores.WisdomBuff;
            CharismaBuff = stats.AbilityScores.CharismaBuff;
        } 
    }

    [DataContract]
    public class FantasyRaceJson
    {
        [DataMember(Name = "race")]
        public Race Race { get; set; }

        [DataMember(Name = "defaultStats")]
        public DefaultStatsJson DefaultStats { get; set; } = new DefaultStatsJson();
    }

    [DataContract]
    public class DefaultStatsJson
    {
        [DataMember(Name = "abilityScore")]
        public AbilityScoreModifiersJson AbilityScores { get; set; } = new AbilityScoreModifiersJson();

        [DataMember(Name = "size")]
        public Size Size { get; set; }

        [DataMember(Name = "speed")]
        public int Speed { get; set; }

        [DataMember(Name = "darkvision")]
        public int Darkvision { get; set; }

        [DataMember(Name = "languages")]
        public List<Language> Languages = [];

    }

    [DataContract]
    public class AbilityScoreModifiersJson
    {
        [DataMember(Name = "strength")]
        public int StrengthBuff { get; set; } = 0;

        [DataMember(Name = "dexterity")]
        public int DexterityBuff { get; set; } = 0;

        [DataMember(Name = "constitution")]
        public int ConstitutionBuff { get; set; } = 0;

        [DataMember(Name = "intelligence")]
        public int IntelligenceBuff { get; set; } = 0;

        [DataMember(Name = "wisdom")]
        public int WisdomBuff { get; set; } = 0;

        [DataMember(Name = "charisma")]
        public int CharismaBuff { get; set; } = 0;
    }
}