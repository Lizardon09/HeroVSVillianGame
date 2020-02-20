using HerosVSVillians.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HerosVSVillians.Logic
{
    public class GameLogic : IVersusLogic
    {
        private static List<Character> Roster;
        private Character PlayerOne { get; set; }
        private Character PlayerTwo { get; set; }

        Array enumoptions;

        public GameLogic()
        {

            CharacterPower.PowerEffectivness = new Dictionary<PowerType, KeyValuePair<List<PowerType>, List<PowerType>>>();

            CharacterPower.PowerEffectivness.Add(PowerType.Strength, new KeyValuePair<List<PowerType>, List<PowerType>>(new List<PowerType>() { PowerType.Intelligence }, new List<PowerType>() { PowerType.Magic }));
            CharacterPower.PowerEffectivness.Add(PowerType.Magic, new KeyValuePair<List<PowerType>, List<PowerType>>(new List<PowerType>() { PowerType.Strength }, new List<PowerType>() { PowerType.Intelligence }));
            CharacterPower.PowerEffectivness.Add(PowerType.Intelligence, new KeyValuePair<List<PowerType>, List<PowerType>>(new List<PowerType>() { PowerType.Magic }, new List<PowerType>() { PowerType.Strength }));
            CharacterPower.PowerEffectivness.Add(PowerType.Weapon, new KeyValuePair<List<PowerType>, List<PowerType>>(new List<PowerType>() { PowerType.Strength }, new List<PowerType>() { PowerType.Intelligence }));

            Roster = new List<Character>();
            List<CharacterPower> powers;

            powers = new List<CharacterPower>() {new CharacterPower(PowerType.Strength)};
            Roster.Add(new Character(UniverseType.Marvel, CharacterType.SuperHero, powers, "Captain America", 50, 6, 3, 4, 4, "I can take this all day", "Justice allways prevails", "I was frozen today"));

            powers = new List<CharacterPower>() { new CharacterPower(PowerType.Strength) };
            Roster.Add(new Character(UniverseType.Marvel, CharacterType.SuperHero, powers, "Hulk", 80, 8, 6, 6, 1, "HULK SMASH!!!", "Puny punk", "RRAAAAHHHH!!!"));

            powers = new List<CharacterPower>() { new CharacterPower(PowerType.Weapon) };
            Roster.Add(new Character(UniverseType.DC, CharacterType.Villian, powers, "Joker", 30, 8, 6, 6, 1, "No one kills bats but me!!", "Oh no!! Im the joke!!", "HAHAHAHAHAHAAHA!!!"));

            powers = new List<CharacterPower>() { new CharacterPower(PowerType.Magic) };
            Roster.Add(new Character(UniverseType.DC, CharacterType.Villian, powers, "Black Adam", 55, 7, 8, 8, 9, "I will find the sorceror Shazam and kill him myself", "How could he be stronger than the old mans magic!!", "SHAZAM!!!!"));

        }

        public void StartGame()
        {
            string input;

            do
            {
                Console.WriteLine("Please select one of the options bellow:\n" +
                  "\n(0): 1P VS COMP\n" +
                  "\n(1): 1P VS 2P\n" +
                  "\n(2): End Game\n");

                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        break;

                    case "1":

                        Console.WriteLine("\nPlease select player One\n");
                        PlayerOne = SelectCharacter(input);

                        Console.WriteLine("\nPlease select player Two\n");
                        PlayerTwo = SelectCharacter(input);

                        PlayerOne.DisplayCharacter();
                        Console.WriteLine("\n\n--------------VS--------------\n\n");
                        PlayerTwo.DisplayCharacter();

                        Clash(PlayerOne, PlayerTwo, input);

                        break;

                    case "2":
                        break;

                    default:
                        Console.WriteLine("\nInvalid input, please try again\n");
                        break;
                }

            } while (input!="2");

            Console.WriteLine("\nGame closed...\n");

        }

        private void CreateCustomCharacter()
        {

            Console.WriteLine("");

        }

        public Character SelectCharacter(string input)
        {
            int test;
            List<Character> subselection;

            //---------------------------------------------------------------------------------------

            Console.WriteLine("\nPlease select character type:\n");

            enumoptions = Enum.GetNames(typeof(CharacterType));

            for (int i = 0; i < enumoptions.Length; i++)
            {
                Console.WriteLine($"\n({i}): {enumoptions.GetValue(i)}\n");
            }

            test = TestNumberInput(input, 0, enumoptions.Length);

            //---------------------------------------------------------------------------------------

            subselection = Roster.FindAll(x => x.Type == (CharacterType)Enum.Parse(typeof(CharacterType), enumoptions.GetValue(test).ToString()));

            Console.WriteLine("\nPlease select character from following:\n");

            for(int i=0; i<subselection.Count; i++)
            {
                Console.WriteLine($"\n({i}): {subselection[i].Name} ({subselection[i].Universe})\n");
            }

            test = TestNumberInput(input, 0, subselection.Count);
            return subselection[test];

        }

        public double OffenseScore(Character character, Character againstcharacter)
        {

            KeyValuePair<List<PowerType>, List<PowerType>> effectivnees;

            double temp = 0;
            temp = character.BaseDamage;
            temp += temp * ((character.AttackRange * 10) / 100);

            foreach (var power in character.Powers)
            {
                temp += power.GetStrengthScore(againstcharacter.Powers);
                temp -= power.GetWeakScore(againstcharacter.Powers);
            }

            return temp;
        }

        public double DefenseScore(Character character, Character againstcharacter)
        {
            double temp = 0;
            temp = character.BaseDefense + (character.BaseDefense*(character.HealthPoints/100));
            temp += temp * ((character.MovementSpeed * 10) / 100);

            foreach (var power in character.Powers)
            {
                temp += power.GetStrengthScore(againstcharacter.Powers);
                temp -= power.GetWeakScore(againstcharacter.Powers);
            }

            return temp;
        }

        public void Clash(Character playerOne, Character playerTwo, string input)
        {
            int actionone;
            int actiontwo;
            double playeronescore;
            double playertwoscore;

            Console.WriteLine($"\n(Player One) {playerOne.Name} choose action\n" +
                              $"\n(0): Attack (Player Two) {playerTwo.Name}" +
                              $"\n(1): Defend against (Player Two) {PlayerTwo.Name}\n");

            actionone = TestNumberInput(input, 0, 2);

            Console.WriteLine($"\n(Player Two) {playerTwo.Name} choose action\n" +
                  $"\n(0): Attack (Player One) {playerOne.Name}" +
                  $"\n(1): Defend against (Player One) {PlayerOne.Name}\n");

            actiontwo = TestNumberInput(input, 0, 2);

            if (actionone == 0)
                playeronescore = OffenseScore(playerOne, playerTwo);
            else
                playeronescore = DefenseScore(playerOne, playerTwo);

            if (actiontwo == 0)
                playertwoscore = OffenseScore(playerTwo, playerOne);
            else
                playertwoscore = DefenseScore(playerTwo, playerOne);

            if (playeronescore > playertwoscore)
            {
                Console.WriteLine($"\n(Player One) {playerOne.Name} WINS!\n");
                Console.WriteLine($"\n{playerOne.Name}: '{playerOne.WinnerBattleLine}'");
                Console.WriteLine($"\n{playerTwo.Name}: '{playerTwo.LoserBattleLine}'\n");
            }

            else if(playertwoscore > playeronescore)
            {
                Console.WriteLine($"\n(Player Two) {playerTwo.Name} WINS!\n\n");
                Console.WriteLine($"\n{playerTwo.Name}: '{playerTwo.WinnerBattleLine}'");
                Console.WriteLine($"\n{playerOne.Name}: '{playerOne.LoserBattleLine}'\n");
            }

            else
            {
                Console.WriteLine($"\nIts a DRAW!\n\n");
                Console.WriteLine($"\n{playerOne.Name}: '{playerOne.LoserBattleLine}'");
                Console.WriteLine($"\n{playerTwo.Name}: '{playerTwo.LoserBattleLine}'\n");
            }

        }

        private int TestNumberInput(string input, int min, int max)
        {
            int number;
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out number) && number >= min && number < max)
                    return number;
                Console.WriteLine("\nInvalid input, please try again\n");
            }
        }

    }
}
