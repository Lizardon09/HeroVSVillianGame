using HerosVSVillians.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HerosVSVillians.Logic
{
    public interface IVersusLogic
    {
        double OffenseScore(Character character, Character againstcharacter);
        double DefenseScore(Character character, Character againstcharacter);
        void Clash(Character character, Character againstcharacter, string input);
    }
}
