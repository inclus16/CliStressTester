using StressCLI.src.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace StressCLI.src.Entities.Parser
{
    internal class RandomSeeder:IRandomSeeder
    {
        private readonly Dictionary<RandomDataType, string> Mapping;

        private string[] RusNamesDictionary;

        private string[] EngNamesDictionary;

        private int RusNamesDictionaryLength;

        private int EngNamesDictionaryLength;

        public RandomSeeder()
        {
            Mapping = new Dictionary<RandomDataType, string>
            {
                {RandomDataType.Email,"%EMAIL%" },
                {RandomDataType.NameEng,"%NAME_ENG%" },
                {RandomDataType.NameRus,"%NAME_RUS%" },
                {RandomDataType.Number,"%NUMBER%" }
            };
            InitDictionaries();

        }

        private void InitDictionaries()
        {

            RusNamesDictionary = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "russian_names.csv"));
            EngNamesDictionary = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "foreign_names.csv"));
            RusNamesDictionaryLength = RusNamesDictionary.Length;
            EngNamesDictionaryLength = EngNamesDictionary.Length;
        }



        public string SetRandom(string data)
        {
            string dataWithRandom = data;
            foreach (KeyValuePair<RandomDataType, string> keyValue in Mapping)
            {
                if (dataWithRandom.Contains(keyValue.Value))
                {
                    dataWithRandom = dataWithRandom.Replace(keyValue.Value, GetRandomDataByType(keyValue.Key));
                }
            }
            return dataWithRandom;
        }

        private string GetRandomDataByType(RandomDataType randomDataType)
        {
            Random random = new Random();
            switch (randomDataType)
            {
                case RandomDataType.NameEng:
                    return EngNamesDictionary[random.Next(0, EngNamesDictionaryLength)];
                case RandomDataType.NameRus:
                    return RusNamesDictionary[random.Next(0, RusNamesDictionaryLength)];
                case RandomDataType.Email:
                    return EngNamesDictionary[random.Next(0, EngNamesDictionaryLength)] + "@mail.com";
                case RandomDataType.Number:
                    return random.Next(0, int.MaxValue).ToString();
                default:
                    throw new ArgumentOutOfRangeException($"Random data type :{randomDataType.ToString()} is not supported");

            }
        }

    }
}
