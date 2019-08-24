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
            
            if (value!=null && !File.Exists(value.ToString()))
            {
                return new ValidationResult($"File not found at path :{value.ToString()}");
            }
            return ValidationResult.Success;
        }
    }
}
