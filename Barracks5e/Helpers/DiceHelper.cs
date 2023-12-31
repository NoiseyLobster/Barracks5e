namespace Barracks5e
{
    public class DiceHelper
    {
        private static readonly Random RandomNumberGenerator = new();

        public static int Roll(int sides, DiceRollType diceRollType = DiceRollType.Standard)
        {
            //excludes upper boundary on random number roll, thus increment by 1
            int roll = RandomNumberGenerator.Next(1, sides + 1);
            if(diceRollType != DiceRollType.Standard) 
            {
                int secondRoll = RandomNumberGenerator.Next(1, sides + 1);
                roll = (diceRollType == DiceRollType.Advantage) ? 
                    Math.Max(roll, secondRoll) :
                    Math.Min(roll, secondRoll);
            }

            return roll;
        }

        public static List<int> Roll(int sides, int numOfDice)
        {
            List<int> diceRolls = [];

            //iterate once per number of dice requested
            for (int i = 0; i < numOfDice; i++)
            {
                diceRolls.Add(DiceHelper.Roll(sides));
            }

            return diceRolls;
        }

        public static int RollWithAdvantage(int sides)
        {
            List<int> diceRoll = RollWithExclusions(sides, 2, 1, true);
            return diceRoll.First();
        }

        public static int RollWithDisadvantage(int sides)
        {
            List<int> diceRoll = RollWithExclusions(sides, 2, 1, false);
            return diceRoll.First();
        }

        public static List<int> RollWithExclusions(int sides, int numOfDice, int numToExclude, bool excludeLowest = true)
        {
            List<int> diceRolls = Roll(sides, numOfDice);

            for (int i = 0; i < numToExclude; i++)
            {
                if (excludeLowest)
                {
                    //remove the lowest value
                    diceRolls.Remove(diceRolls.Min());
                }
                else
                {
                    //remove the highest value
                    diceRolls.Remove(diceRolls.Max());
                }
            }

            return diceRolls;
        }
    }

    public enum DiceRollType
    {
        Standard,
        Advantage,
        Disadvantage
    }
}