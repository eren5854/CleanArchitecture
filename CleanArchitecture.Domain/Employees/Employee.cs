using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Employees;
public sealed class Employee : Entity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public PersonalInformation? PersonalInformation { get; set; }
    public Address? Address { get; set; }
}
