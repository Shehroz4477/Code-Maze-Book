using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects;

public record EmployeeForCreationDto
{
    [Required(ErrorMessage = "Employee Name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Employee Age is a required field.")]
    public int Age { get; init; }

    [Required(ErrorMessage = "Employee Position is a required field.")]
    [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
    public string? Position { get; init; }
}
