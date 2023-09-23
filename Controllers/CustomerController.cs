using Microsoft.AspNetCore.Mvc;
using PlugApi.Entities;
using PlugApi.Interfaces.Services;
using PlugApi.Models.Requests.Customers;

namespace PlugApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private ICustomerRepository _customerService;
    public CustomerController(ICustomerRepository customerService)
    {
        _customerService = customerService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }
    // GET api/<CustomerController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        Customer customer = await _customerService.GetCustomerByIdAsync(id);
        return Ok(customer);
    }
    // POST api/<AuthorController>
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest model)
    {
        int customerId = await _customerService.CreateCustomer(model);

        if (customerId != 0)
        {
            return Ok(new { message = $"Customer was successfully created in database with the id {customerId}" });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, "The customer was not created in the database.");
    }

    // PUT api/<AuthorController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(int id, UpdateCustomerRequest model)
    {
        await _customerService.UpdateCustomer(id, model);
        return Ok(new { message = "Customer was successfully updated in database" });
    }

    // PUT api/<AuthorController>/5/UpdateStatus
    [HttpPut("{id}/UpdateStatus")]
    public async Task<IActionResult> UpdateIsActiveCustomer(int id)
    {
        await _customerService.UpdateIsActiveCustomer(id);
        return Ok(new { message = "Customer was successfully updated in database" });
    }
}
