using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.TestCore.Parser
{
    class RandomSeeder
    {
        private readonly Dictionary<RandomDataType, string> Mapping;

        public RandomSeeder()
        {
            Mapping = new Dictionary<RandomDataType, string>
            {
                {RandomDataType.Email,"%EMAIL%" },
                {RandomDataType.NameEng,"%NAME_ENG%" },
                {RandomDataType.NameRus,"%NAME_RUS%" },
                {RandomDataType.Number,"%NUMBER%" }
            };
        }

    }
}
