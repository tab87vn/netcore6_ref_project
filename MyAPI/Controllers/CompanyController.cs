using Microsoft.AspNetCore.Mvc;
using tab.TestDotNet.API.Exceptions;
using tab.TestDotNet.API.Models;
using tab.TestDotNet.API.Shared.DTOs;

namespace tab.TestDotNet.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly List<Company> ALL_COMPS = new List<Company>()
    {
        new Company { Id = Guid.Parse("d9c4df4e-d2ac-45d0-a258-af1fb77b5ad7"), Name = "com1", Address = "xyz", Country = "BE" },
        new Company { Id = Guid.Parse("3fd5c316-451d-476b-8fb7-c1cba2de7abb"), Name = "com2", Address = "xyz", Country = "NL" },
        new Company { Id = Guid.Parse("a22d07c3-b077-48dc-a26f-d29db563b0a5"), Name = "com3", Address = "xyz", Country = "FR" },
        new Company { Id = Guid.Parse("aa64e363-b74a-4299-901a-8d44b63894a9"), Name = "com4", Address = "xyz", Country = "BE" },
        new Company { Id = Guid.Parse("8a15f0d1-c894-494d-8f90-9f2caec9c229"), Name = "com5", Address = "xyz", Country = "GB" },
    };

    private readonly List<Employee> ALL_EMPS = new List<Employee>()
    {
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("d9c4df4e-d2ac-45d0-a258-af1fb77b5ad7"), Name = "A" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("3fd5c316-451d-476b-8fb7-c1cba2de7abb"), Name = "B" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("d9c4df4e-d2ac-45d0-a258-af1fb77b5ad7"), Name = "C" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("3fd5c316-451d-476b-8fb7-c1cba2de7abb"), Name = "D" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("d9c4df4e-d2ac-45d0-a258-af1fb77b5ad7"), Name = "E" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("a22d07c3-b077-48dc-a26f-d29db563b0a5"), Name = "F" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("d9c4df4e-d2ac-45d0-a258-af1fb77b5ad7"), Name = "G" },
        new Employee { Id = Guid.NewGuid(), CompanyId = Guid.Parse("8a15f0d1-c894-494d-8f90-9f2caec9c229"), Name = "H" },
    };

    // /api/companies 
    [HttpGet]
    public IEnumerable<Company> GetAllCompanies()
    {
        return ALL_COMPS;
    }

    // /api/companies/{id}
    [HttpGet("{id:guid}", Name = "CompanyById")]
    public Company GetOneCompany(Guid id)
    {
        var company = ALL_COMPS.FirstOrDefault(x => x.Id == id);
        if (company == null)
        {
            throw new CompanyNotFoundException("Uh oh, not found for this id");
        }

        Console.Out.WriteAsync("I don't care.");

        return company;
    }

    // /api/companies/{id}/employees
    [HttpGet("{id}/employees")]
    public IEnumerable<Employee> GetEmployeesByCompany(Guid id)
    {
        Console.Out.WriteLine("eh: " + id.ToString());
        return ALL_EMPS.Where(x => x.CompanyId == id);
    }

    [HttpGet("exceptions")]
    public IActionResult TestException()
    {
        throw new Exception("Something really bad happen.");
    }

    [HttpGet("notfoundexceptions")]
    public IActionResult Test404Exception()
    {
        throw new CompanyNotFoundException("Uh oh, company not found.");
    }

    // TODO: Try to understand it later
    [HttpGet("collection/({ids})")]
    public ActionResult<IEnumerable<CompanyDto>> GetCompanyCollection(
        IEnumerable<Guid> ids)
    {
        Console.Out.WriteLine(ids);

        var value = ALL_COMPS
            .Where(x => ids.Contains(x.Id))
            .Select(x => new CompanyDto(
                Id: x.Id,
                Name: x.Name,
                FullAddress: x.Address + ", " + x.Country
            ))
            .ToList();

        return Ok(value);
    }

    [HttpPost("test")]
    public ActionResult<CompanyDto> CreateCompany(
        [FromBody] CompanyForCreationDto company) {

            Console.Out.WriteLine(company);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var newCompany = new CompanyDto(
                Id: Guid.NewGuid(),
                Name: company.Name,
                FullAddress: company.Address + ", " + company.Country);

        //  return Created("dsds", newCompany); 
        return CreatedAtRoute("CompanyById", new { id = newCompany.Id },newCompany);
    }
}