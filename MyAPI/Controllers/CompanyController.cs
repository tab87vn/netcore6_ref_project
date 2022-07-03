using Microsoft.AspNetCore.Mvc;
using tab.TestDotNet.API.Models;

namespace tab.TestDotNet.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly List<Company> ALL_COMPS = new List<Company>()
    {
        new Company { Id = Guid.Parse("d9c4df4e-d2ac-45d0-a258-af1fb77b5ad7"), Name = "com1", Address = "xyz" },
        new Company { Id = Guid.Parse("3fd5c316-451d-476b-8fb7-c1cba2de7abb"), Name = "com2", Address = "xyz" },
        new Company { Id = Guid.Parse("a22d07c3-b077-48dc-a26f-d29db563b0a5"), Name = "com3", Address = "xyz" },
        new Company { Id = Guid.Parse("aa64e363-b74a-4299-901a-8d44b63894a9"), Name = "com4", Address = "xyz" },
        new Company { Id = Guid.Parse("8a15f0d1-c894-494d-8f90-9f2caec9c229"), Name = "com5", Address = "xyz" },
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
    [HttpGet("{id}")]
    public Company GetOneCompany(Guid id)
    {
        return ALL_COMPS.FirstOrDefault(x => x.Id == id);
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
}