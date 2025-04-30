using CleanArchitecture.Domain.Employees;
using FluentValidation;
using GenericRepository;
using Mapster;
using MediatR;
using TS.Result;

namespace CleanArchitecture.Application.Employees;
public sealed record EmployeeCreateCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    decimal Salary,
    PersonalInformation PersonalInformation,
    Address Address) : IRequest<Result<string>>;

public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
{
    public EmployeeCreateCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("Ad alanı boş olamaz")
            .MinimumLength(3)
            .WithMessage("Ad alanı en az 3 karakter olmalı.");
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Soyad alanı boş olamaz");
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Doğum tarihi alanı boş olamaz");
        RuleFor(x => x.Salary)
            .NotEmpty()
            .WithMessage("Maaş alanı boş olamaz");
        RuleFor(r => r.PersonalInformation.TCNo)
            .MinimumLength(11)
            .WithMessage("TC kimlik numarası 11 karakter olmalı")
            .MaximumLength(11)
            .WithMessage("TC kimlik numarası 11 karakter olmalı");

    }
}

internal sealed class EmployeeCreateCommandHandler(
    IEmployeeRepository employeeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        bool isEmployeeExists = await employeeRepository
            .AnyAsync(x => x.PersonalInformation.TCNo == request.PersonalInformation.TCNo, cancellationToken);

        if (isEmployeeExists)
            return Result<string>.Failure("Bu TC kimlik numarasına sahip bir personel zaten mevcut");

        Employee employee = request.Adapt<Employee>();
        employeeRepository.Add(employee);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Personel kaydı başarılı");
    }
}
