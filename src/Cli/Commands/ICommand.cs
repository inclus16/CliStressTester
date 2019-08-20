namespace StressCLI.src.Cli.Commands
{
    internal interface ICommand
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
