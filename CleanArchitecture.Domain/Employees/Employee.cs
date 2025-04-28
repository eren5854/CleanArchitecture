namespace CleanArchitecture.Domain.Employees;
public sealed class Employee
{
    public Employee()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public string FullName => string.Join(" ", FirstName, LastName);

    public string Email { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
}

public sealed record Address
{
    //Eğer record parantez olara tanımlanırsa, içerisindeki property'ler zorunlu olur.
    //Eğer süslü parantezle tanımlanırsa, property'ler isteğe bağlı olur.
    public string? TCNo { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Town { get; set; }
}
