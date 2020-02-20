using System;
using System.Collections.Generic;
using System.Text;

namespace HerosVSVillians.Model
{
    public enum UniverseType 
    {
        DC,
        Marvel,
    }

    public enum CharacterType
    {
        SuperHero,
        Villian,
    }

    public class Character
    {
        public UniverseType Universe { get; set; }
        public CharacterType Type { get; set; }
        public List<CharacterPower> Powers { get; set; }
        public int ID { get; set; }
        public static int AccumilatedID { get; set; }
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int BaseDefense { get; set; }
        public int BaseDamage { get; set; }
        public int MovementSpeed { get; set; }
        public int AttackRange { get; set; }
        public string EnterBattleLine { get; set; }
        public string WinnerBattleLine { get; set; }
        public string LoserBattleLine { get; set; }

        public Character(UniverseType universe, CharacterType type, List<CharacterPower> powers, string name, int health, int defense, int damage, int speed, int range, string enterline, string winnerline, string loserline)
        {
            Universe = universe;
            Type = type;
            Powers = powers;
            Name = name;
            HealthPoints = health;
            BaseDefense = defense;
            BaseDamage = damage;
            MovementSpeed = speed;
            AttackRange = range;
            EnterBattleLine = enterline;
            WinnerBattleLine = winnerline;
            LoserBattleLine = loserline;
            AccumilatedID++;
            ID = AccumilatedID;
        }

        public void DisplayCharacter()
        {
            Console.WriteLine($"\nCharacter Bio:\n\n" +
                              $"\nName: {Name} ({Universe})" +
                              $"\nHP: {HealthPoints}/100" +
                              $"\nBaseDefense: {BaseDefense}/10" +
                              $"\nBaseAttack: {BaseDamage}/10" +
                              $"\nMovementSpeed: {MovementSpeed}/10" +
                              $"\nAttackRange: {AttackRange}/10\n");
        }

    }
}
