using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects;

public abstract record CompanyForManipulationDto
{
    [Required(ErrorMessage = "Company Name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Company Name is 30 characters.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Company Address is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Address is 60 characters")]
    public string? Address { get; init; }
    public string? Country { get; init; }
    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
}
