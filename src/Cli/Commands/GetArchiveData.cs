
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StressCLI.src.Cli.Commands
{
    class GetArchiveData : ICommand
    {
        public void Cancel()
        {
           // throw new NotImplementedException();
        }

        public void Execute()
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            engine.ExecuteFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Charts.py"), scope);
        }

        public string GetName()
        {
            return "get-archive-data";
        }

        public string GetValidationError()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            return true;
        }

        public void PrepareData()
        {
           // throw new NotImplementedException();
        }

        public void SetData(string[] args)
        {
         //   throw new NotImplementedException();
        }

        public void Validate()
        {
          //  throw new NotImplementedException();
        }
    }
}
