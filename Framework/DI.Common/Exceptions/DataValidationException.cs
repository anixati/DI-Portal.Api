using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DI.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        public DataValidationException(List<ValidationResult> errors) : base("Database entity validation failed")
        {
            if (errors != null && errors.Any())
                Errors = errors.Select(x => $"{x.ErrorMessage} - {string.Join(",", x.MemberNames)}").ToList();
        }

        public List<string> Errors { get; private set; }
    }

    [Serializable]
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string msg) : base(msg)
        {
        }
    }
}