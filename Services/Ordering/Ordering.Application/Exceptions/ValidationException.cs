using System.Net.Security;
using FluentValidation.Results;

namespace Ordering.Application.Exceptions;

public class ValidException : ApplicationException
{
    public ValidException() : base("One or more validation failures have occured")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}