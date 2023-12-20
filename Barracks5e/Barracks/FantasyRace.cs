using System.Runtime.Serialization;

namespace Barracks5e
{
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
        List<Languages> Languages = [];

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