namespace CleanArchitecture.Domain.Employees;

public sealed record Address
{
    //Eğer record parantez olara tanımlanırsa, içerisindeki property'ler zorunlu olur.
    //Eğer süslü parantezle tanımlanırsa, property'ler isteğe bağlı olur.
   
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Town { get; set; }
    public string? FullAddress { get; set; }


}
