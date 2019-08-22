using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace StressCLI.src.Cli.Commands.Validation
{
    class FileExists:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string path = value.ToString();
            if (!File.Exists(path))
            {
                return new ValidationResult($"File not found at path :{path}");
            }
            return ValidationResult.Success;
        }
    }
}
