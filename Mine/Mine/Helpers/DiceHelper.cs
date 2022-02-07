using System;

namespace Mine.Helpers
{

    /// <summary>
    /// DiceHelper simulates a dice roll.
    /// </summary>
    public static class DiceHelper
    {
        //Create a Random object.
        private static Random rnd = new Random();
        //Bool to determine if roll is random
        public static bool ForceRollsToNotRandom = false;
        //Default forced random value.
        public static int ForcedRandomValue = 1;

        /// <summary>
        /// Take in the amount of times the dice should be rolled and the amount of sided die. 
        /// Function will add the total based on the rolls and number of sides on the dice.
        /// </summary>
        /// <param name="rolls"></param>
        /// <param name="dice"></param>
        /// <returns></returns>
        public static int RollDice(int rolls, int dice)
        {
            if (ForceRollsToNotRandom)
            {
                return rolls * ForcedRandomValue;
            }
            if (rolls < 1)
            {
                return 0;
            }
            if (dice < 1)
            {
                return 0;
            }
            var myReturn = 0;
            for (var i = 0; i < rolls; i++)
            {
                myReturn += rnd.Next(1, dice + 1);
            }
            return myReturn;
        }
    }
}