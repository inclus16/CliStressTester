using StressCLI.src.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;

namespace StressCLI.Tests
{
    public class RandomSeederTests
    {
        private readonly IRandomSeeder RandomSeeder;

        public RandomSeederTests()
        {
            RandomSeeder = Program.BuildServiceProvider().GetService<IRandomSeeder>();
        }

        [Fact]
        public void TestDifferentEngNames()
        {
            List<string> datas = new List<string>();
            datas.Add("%NAME_ENG%");
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            Assert.True(datas.Distinct().Count() == datas.Count);
        }

        [Fact]
        public void TestDifferentRusNames()
        {
            List<string> datas = new List<string>();
            datas.Add("%NAME_RUS%");
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            Assert.True(datas.Distinct().Count() == datas.Count);
        }

        [Fact]
        public void TestDifferentEmails()
        {
            List<string> datas = new List<string>();
            datas.Add("%EMAIL%");
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            Assert.True(datas.Distinct().Count() == datas.Count);
        }

        [Fact]
        public void TestDifferentNumbers()
        {
            List<string> datas = new List<string>();
            datas.Add("%NUMBER%");
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            datas.Add(RandomSeeder.SetRandom(datas[0]));
            Assert.True(datas.Distinct().Count() == datas.Count);
        }
    }
}
