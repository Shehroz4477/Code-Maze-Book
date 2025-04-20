using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions;

/// <summary>
/// Represents the base exception type for scenarios where a requested resource is not found.
/// Inherit from this class to create specific not-found exceptions (e.g., UserNotFoundException, CompanyNotFoundException).
/// </summary>
public abstract class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    protected NotFoundException(string message) :base(message)
    {
    }
}
