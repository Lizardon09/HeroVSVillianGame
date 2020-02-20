using System;
using System.Collections.Generic;
using System.Text;

namespace HerosVSVillians.Model
{
    public enum PowerType
    {
        Strength,
        Magic,
        Intelligence,
        Weapon
    }
    public class CharacterPower
    {
        public PowerType BasePower { get; set; }
        public static Dictionary<PowerType,KeyValuePair<List<PowerType>, List<PowerType>>> PowerEffectivness { get; set; }

        public CharacterPower(PowerType power)
        {
            BasePower = power;
        }

        public int GetStrengthScore(List<CharacterPower> opposingpowers)
        {
            int score = 0;
            foreach (var powertype in PowerEffectivness.GetValueOrDefault(BasePower).Key)
            {
                if (opposingpowers.Exists(x => x.BasePower == powertype))
                    score++;
            }
            return score;
        }

        public int GetWeakScore(List<CharacterPower> opposingpowers)
        {
            int score = 0;
            foreach (var powertype in PowerEffectivness.GetValueOrDefault(BasePower).Value)
            {
                if (opposingpowers.Exists(x => x.BasePower == powertype))
                    score++;
            }
            return score;
        }

    }
}
