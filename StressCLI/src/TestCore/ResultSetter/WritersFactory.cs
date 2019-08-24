
using StressCLI.src.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.TestCore.ResultSetter
{
    public class WritersFactory
    {
        public static AbstractWriter GetWriter(ResultWriter writer)
        {
            switch(writer)
            {
                case ResultWriter.Console:
                    return new ConsoleWriter();
                case ResultWriter.File:
                    return new FileWriter();
                default:
                    throw new ArgumentOutOfRangeException($"Writer {writer.ToString()} is not supported");
            }
        }
    }
}
