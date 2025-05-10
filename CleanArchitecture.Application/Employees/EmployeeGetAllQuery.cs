using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Employees;
using MediatR;

namespace CleanArchitecture.Application.Employees;
public sealed record EmployeeGetAllQuery() : IRequest<IQueryable<EmployeeGetAllQueryResponse>>;

public sealed class EmployeeGetAllQueryResponse : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public string TCNo { get; set; } = default!;
}

internal sealed class EmployeeGetAllQueryHandler(
    IEmployeeRepository employeeRepository) : IRequestHandler<EmployeeGetAllQuery, IQueryable<EmployeeGetAllQueryResponse>>
{
    public Task<IQueryable<EmployeeGetAllQueryResponse>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var employees = employeeRepository.GetAll().Select(s => new EmployeeGetAllQueryResponse
        {
            FirstName = s.FirstName,
            LastName = s.LastName,
            DateOfBirth = s.DateOfBirth,
            Salary = s.Salary,
            TCNo = s.PersonalInformation.TCNo,
            Id = s.Id,
            IsDeleted = s.IsDeleted,
            CreateAt = s.CreateAt,
            DeleteAt = s.DeleteAt,
            UpdateAt = s.UpdateAt
        }).AsQueryable();
        return Task.FromResult(employees);
    }
}