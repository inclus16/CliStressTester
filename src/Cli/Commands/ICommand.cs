using System;
using System.Collections.Generic;
using System.Text;

namespace StressCLI.src.Cli.Commands
{
    interface ICommand
    {
        bool IsValid();

        string GetValidationError();

        void Execute();

        void SetData(string[] args);

        void PrepareData();

        string GetName();

        void Validate();

        void Cancel();
    }
}
