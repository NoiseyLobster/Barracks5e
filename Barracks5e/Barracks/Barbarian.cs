using System.Runtime.CompilerServices;

namespace Barracks5e
{
    public class Barbarian : Person
    {
        protected int ProficiencyBonus
        {
            get { return Math.Min((Level - 1) / 4 + 2, 6); }
        }

        #region Rage Properties

        public bool IsRaging { get; protected set; }

        protected int RageCount
        {
            get
            {
                int rageCount = 2;

                if (Level >= 20)
                {
                    rageCount = 9999;
                }
                else if (Level >= 17)
                {
                    rageCount = 6;
                }
                else if (Level >= 12)
                {
                    rageCount = 5;
                }
                else if (Level >= 6)
                {
                    rageCount = 4;
                }
                else if (Level >= 3)
                {
                    rageCount = 3;
                }

                return rageCount;
            }
        }

        protected int RagesRemaining { get; set; }

        protected int RageTurnsRemaining { get; set; } = 10;

        protected int RageDamage
        {
            get
            {
                int rageDamage = 2;
                if (Level >= 16)
                {
                    rageDamage = 4;
                }
                else if (Level >= 9)
                {
                    rageDamage = 3;
                }

                return rageDamage;
            }
        }

        #endregion

        public Barbarian(Race race) : base(race)
        {
            FantasyRace selectedRace = FetchDefaultBuffsByRace(race);

            Level = 1;

            Strength = selectedRace.StrengthBuff + AbilityScores[0];
            Constitution = selectedRace.ConstitutionBuff + AbilityScores[1];
            Dexterity = selectedRace.DexterityBuff + AbilityScores[2];
            Wisdom = selectedRace.WisdomBuff + AbilityScores[3];
            Charisma = selectedRace.CharismaBuff + AbilityScores[4];
            Intelligence = selectedRace.IntelligenceBuff + AbilityScores[5];

            ArmorClass = EquippedArmor == Armor.Unarmored ? 10 + DexterityMod + ConstitutionMod : Equipment.ConvertArmorToAC(EquippedArmor);
            if (IsShieldEquipped)
            {
                ArmorClass += 2;
            }

            HitDiceType = 12;
            HitDiceCount = 1;
            HitDiceRemaining = 1;

            HitPoints = 12 + ConstitutionMod;

            ArmorProficiencies.Add(ArmorType.LightArmor);
            ArmorProficiencies.Add(ArmorType.MediumArmor);
            ArmorProficiencies.Add(ArmorType.Shield);

            WeaponProficiencies.Add(WeaponType.SimpleWeapons);
            WeaponProficiencies.Add(WeaponType.MartialWeapons);

            SavingThrowProficiencies.Add(AbilityScoreType.Strength);
            SavingThrowProficiencies.Add(AbilityScoreType.Constitution);

            //TODO skill proficiency assignment

            RagesRemaining = RageCount;
        }

        public void Rage(bool isRaging = true)
        {
            IsRaging = isRaging;
            if (isRaging)
            {
                Resistances.Add(Resistance.Bludgeoning);
                Resistances.Add(Resistance.Piercing);
                Resistances.Add(Resistance.Slashing);
            }
            else
            {
                Resistances.Remove(Resistance.Bludgeoning);
                Resistances.Remove(Resistance.Piercing);
                Resistances.Remove(Resistance.Slashing);
            }
        }

        public int AttackTarget()
        {
            //TODO implement
            return 0;
        }

        public int SavingThrow(AbilityScoreType abilityScoreType, DiceRollType rollType = DiceRollType.Standard, bool isProficient = false)
        {
            return AbilityCheck(abilityScoreType, rollType, isProficient);
        }

        public int AbilityCheck(AbilityScoreType abilityScoreType, DiceRollType rollType = DiceRollType.Standard, bool isProficient = false)
        {
            int bonus = isProficient ? ProficiencyBonus : 0;

            switch (abilityScoreType)
            {
                case AbilityScoreType.Strength:
                    rollType = IsRaging ? DiceRollType.Advantage : rollType;
                    bonus += StrengthMod;
                    break;
                case AbilityScoreType.Dexterity:
                    bonus += DexterityMod;
                    break;
                case AbilityScoreType.Constitution:
                    bonus += ConstitutionMod;
                    break;
                case AbilityScoreType.Charisma:
                    bonus += CharismaMod;
                    break;
                case AbilityScoreType.Wisdom:
                    bonus += WisdomMod;
                    break;
                case AbilityScoreType.Intelligence:
                    bonus += IntelligenceMod;
                    break;
            }

            return DiceHelper.Roll(20, rollType) + bonus;
        }
    }
}