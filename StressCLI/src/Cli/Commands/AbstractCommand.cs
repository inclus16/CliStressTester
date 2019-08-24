using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace StressCLI.src.Cli.Commands
{
    abstract class AbstractCommand
    {
        protected List<string> ValidationErrors = new List<string>();
        protected void Validate(object dto)
        {
            ValidationContext validationContext = new ValidationContext(dto);
            List<ValidationResult> results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(dto, validationContext, results, true))
            {
                foreach (var error in results)
                {
                    ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }

        protected bool HaveValidationErrors()
        {
            return ValidationErrors.Count > 0;
        }

        protected void PrintValidationErrors()
        {
            ValidationErrors.AsParallel().ForAll(x =>
            {
                CliNotifier.PrintError(x);
            });
        }
    }
}
