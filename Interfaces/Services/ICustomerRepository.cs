using PlugApi.Entities;
using PlugApi.Models.Requests.Customers;
using PlugApi.Models.Responses.Customers;

namespace PlugApi.Interfaces.Services
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Get all customers in database.
        /// </summary>
        /// <returns>All customers in database</returns>
        Task<IEnumerable<GetCustomersResponse>> GetAllCustomersAsync();
        /// <summary>
        /// Get a single customer by Id.
        /// </summary>
        /// <param name="id">Id of Customer</param>
        /// <returns>A single Customer</returns>
        Task<Customer> GetCustomerByIdAsync(int id);
        /// <summary>
        /// Create a new customer in the database
        /// </summary>
        /// <param name="model">Create customer request model</param>
        Task<int> CreateCustomer(CreateCustomerRequest model);
        /// <summary>
        /// Update an customer in the database if the customer already exists.
        /// </summary>
        /// <param name="id">Id of Customer</param>
        /// <param name="model">Create customer request model</param>
        Task UpdateCustomer(int id, UpdateCustomerRequest model);

        /// <summary>
        /// Muda o status da entidade IsActive, define como falso caso verdadeiro e vice versa.
        /// </summary>
        /// <param name="id">Id do Customer para alteração</param>
        Task UpdateIsActiveCustomer(int id);
    }
}
