using Microsoft.EntityFrameworkCore;
using PlugApi.Data;
using PlugApi.Entities;
using PlugApi.Helpers;
using PlugApi.Interfaces;
using PlugApi.Models.Enums;
using PlugApi.Models.Requests.Customers;
using PlugApi.Models.Responses.Customers;

namespace PlugApi.Services
{
    public class CustomerService : ICustomerService
    {
        private CSPContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerService(CSPContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<GetCustomersResponse>> GetAllCustomersAsync()
        {
            var customers = await _dbContext.Customers
                .ToListAsync().ConfigureAwait(true);
            var customersResponse = new List<GetCustomersResponse>();

            foreach (var customer in customers)
            {
                var customerResponse = new GetCustomersResponse()
                {
                    Name = customer.CustomerName,
                    ApiKey = Guid.Parse(customer.ApiKey),
                    Created = customer.Created,
                    //Updated = customer.Updated,
                    DataBaseType = (DataBaseTypeEnum)customer.InstanceDatabaseId,
                    IsActive = customer.IsActive
                };
                customersResponse.Add(customerResponse);
            }

            return customersResponse;
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _getCustomerById(id).ConfigureAwait(true);
        }
        public async Task<int> CreateCustomer(CreateCustomerRequest model)
        {
            if (await _dbContext.Customers.AnyAsync(x => x.CustomerName == model.Name))
                throw new RepositoryException($"An author with the name {model.Name} already exists.");
            Customer customer = new Customer()
            {
                ApiKey = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                CustomerName = model.Name,
                InstanceDatabaseId = (int)model.DataBaseType,
                IsActive = true,
                //VER COM O TIME O MODELO DE concatenacao
                SchemaName = model.Name.Replace(' ', '_').Trim(),
                Updated = DateTime.Now,
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);

            var schema = _httpContextAccessor.HttpContext.Request.Headers["SchemaName"];
            if (customer != null)
            {

                _CreateSchema(customer);
                return customer.Id;
            }

            return 0;

        }
        public async Task UpdateCustomer(int id, UpdateCustomerRequest model)
        {
            Customer? customer = await _getCustomerById(id).ConfigureAwait(true);

            customer.CustomerName = model.Name;
            customer.Updated = DateTime.Now;
            customer.ApiKey = model.Api_Key.ToString();

            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();

        }
        public async Task UpdateIsActiveCustomer(int id)
        {
            Customer? customer = await _getCustomerById(id).ConfigureAwait(true);

            customer.IsActive = !customer.IsActive;
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();

        }
        /// <summary>
        /// Get a single customer.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>A single customer</returns>
        private async Task<Customer> _getCustomerById(int id)
        {
            Customer? customer = await _dbContext.Customers
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            return customer;
        }
        private void _CreateSchema(Customer customer)
        {
            var script1 = String.Format("\r\nCREATE SCHEMA \"{0}\";\r\n", customer.SchemaName);
            _dbContext.Database.ExecuteSqlRaw(script1);
            _CreateTablesInSchema(customer);
        }
        private void _CreateTablesInSchema(Customer customer)
        {
            var script = string.Format("\r\n\r\n\r\nCREATE TABLE \"{0}\".project (\r\n project_id integer GENERATED ALWAYS AS IDENTITY,\r\n    category character varying(255) NULL,\r\n    name character varying(255) NULL,\r\n    project_key character varying(255) NULL,\r\n    CONSTRAINT \"PK_project\" PRIMARY KEY (project_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".sprints (\r\n    sprint_id integer GENERATED ALWAYS AS IDENTITY,\r\n    complete_date date NULL,\r\n    end_date date NULL,\r\n    goal character varying(255) NULL,\r\n    sprint_name character varying(255) NULL,\r\n    start_date date NULL,\r\n    state character varying(255) NULL,\r\n    CONSTRAINT \"PK_sprints\" PRIMARY KEY (sprint_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".issues (\r\n    issue_id integer GENERATED ALWAYS AS IDENTITY,\r\n    business_original_estimate numeric(10,2) NULL,\r\n    business_original_estimate_with_subtasks numeric(10,2) NULL,\r\n    business_time_spent_with_subtasks numeric(10,2) NULL,\r\n    business_remaining_estimate numeric(10,2) NULL,\r\n    business_remaining_estimate_with_subtasks numeric(10,2) NULL,\r\n    business_time_spent numeric(10,2) NULL,\r\n    categoria character varying(255) NULL,\r\n    condicao_game character varying(255) NULL,\r\n    condicao_pos character varying(255) NULL,\r\n    condicao_pre character varying(255) NULL,\r\n    created timestamp without time zone NULL,\r\n    creator_account_id integer NULL,\r\n    creator_name character varying(255) NULL,\r\n    current_status character varying(255) NULL,\r\n    current_assignee_account_id integer NULL,\r\n    current_assignee_name character varying(255) NULL,\r\n    due_date date NULL,\r\n    issue_key character varying(255) NULL,\r\n    issue_status_id integer NULL,\r\n    issue_type_id integer NULL,\r\n    mes character varying(255) NULL,\r\n    nome_do_mes character varying(255) NULL,\r\n    original_estimate numeric(10,2) NULL,\r\n    original_estimate_with_subtasks numeric(10,2) NULL,\r\n    parent_issue_id integer NULL,\r\n    parent_issue_key character varying(255) NULL,\r\n    posicao integer NULL,\r\n    prioridade character varying(255) NULL,\r\n    project_id integer NULL,\r\n    project_key character varying(255) NULL,\r\n    remaining_estimate numeric(10,2) NULL,\r\n    remaining_estimate_with_subtasks numeric(10,2) NULL,\r\n    resolution character varying(255) NULL,\r\n    resolution_date date NULL,\r\n    status character varying(255) NULL,\r\n    status_agrupado character varying(255) NULL,\r\n    story_point_estimate_10016 numeric(10,2) NULL,\r\n    story_points_10028 numeric(10,2) NULL,\r\n    time_spent numeric(10,2) NULL,\r\n    time_spent_with_subtasks numeric(10,2) NULL,\r\n    tipo_de_item character varying(255) NULL,\r\n    CONSTRAINT \"PK_issues\" PRIMARY KEY (issue_id),\r\n    CONSTRAINT fk_project_issues FOREIGN KEY (project_id) REFERENCES \"{0}\".project (project_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".epic_issues (\r\n    epic_issues_id integer GENERATED ALWAYS AS IDENTITY,\r\n    business_original_estimate numeric(10,2) NULL,\r\n    business_original_estimate_with_subtasks numeric(10,2) NULL,\r\n    business_remaining_estimate numeric(10,2) NULL,\r\n    business_remaining_estimate_with_subtasks numeric(10,2) NULL,\r\n    business_time_spent numeric(10,2) NULL,\r\n    business_time_spent_with_subtasks numeric(10,2) NULL,\r\n    created timestamp without time zone NULL,\r\n    creator_account_id integer NULL,\r\n    creator_name character varying(255) NULL,\r\n    current_assignee_account_id integer NULL,\r\n    current_assignee_name character varying(255) NULL,\r\n    due_date date NULL,\r\n    issue_id integer NULL,\r\n    issue_key character varying(255) NULL,\r\n    issue_status_id integer NULL,\r\n    issue_status_name character varying(255) NULL,\r\n    issue_type_id integer NULL,\r\n    issue_type_name character varying(255) NULL,\r\n    original_estimate numeric(10,2) NULL,\r\n    original_estimate_with_subtasks numeric(10,2) NULL,\r\n    parent_issue_id integer NULL,\r\n    parent_issue_key character varying(255) NULL,\r\n    priority character varying(255) NULL,\r\n    sum_project_id integer NULL,\r\n    project_key character varying(255) NULL,\r\n    remaining_estimate numeric(10,2) NULL,\r\n    remaining_estimate_with_subtasks numeric(10,2) NULL,\r\n    resolution character varying(255) NULL,\r\n    resolution_date date NULL,\r\n    story_point_estimate_10016 numeric(10,2) NULL,\r\n    story_points_10028 numeric(10,2) NULL,\r\n    CONSTRAINT epic_issues_pkey PRIMARY KEY (epic_issues_id),\r\n    CONSTRAINT fk_issue_epic_issues FOREIGN KEY (issue_id) REFERENCES \"{0}\".issues (issue_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".flagged (\r\n    flagged_id integer GENERATED ALWAYS AS IDENTITY,\r\n    flagged boolean NULL,\r\n    issue_id integer NULL,\r\n    issue_key character varying(255) NULL,\r\n    CONSTRAINT \"PK_flagged\" PRIMARY KEY (flagged_id),\r\n    CONSTRAINT fk_issue_flagged FOREIGN KEY (issue_id) REFERENCES \"{0}\".issues (issue_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".sprints_issues (\r\n    sprint_issues_id integer GENERATED ALWAYS AS IDENTITY,\r\n    issue_key character varying(255) NULL,\r\n    sprint_id integer NULL,\r\n    issue_id integer NULL,\r\n    sprint_name character varying(255) NULL,\r\n    CONSTRAINT sprints_issues_pkey PRIMARY KEY (sprint_issues_id),\r\n    CONSTRAINT fk_issues_issues_sprints FOREIGN KEY (issue_id) REFERENCES \"{0}\".issues (issue_id),\r\n    CONSTRAINT fk_sprint_issues_sprints FOREIGN KEY (sprint_id) REFERENCES \"{0}\".sprints (sprint_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".timeinstatus (\r\n    time_in_status_id integer GENERATED ALWAYS AS IDENTITY,\r\n    categoria character varying(255) NULL,\r\n    chave_projeto character varying(255) NULL,\r\n    condicao_sp character varying(255) NULL,\r\n    duration_in_business_days_24h integer NULL,\r\n    duration_in_business_days_24h_formatted character varying(255) NULL,\r\n    duration_in_business_days_bh numeric(10,2) NULL,\r\n    duration_in_normal_hours numeric(10,2) NULL,\r\n    duration_in_normal_hours_formatted character varying(255) NULL,\r\n    first_transition_from_status timestamp without time zone NULL,\r\n    first_transition_to_status timestamp without time zone NULL,\r\n    issue_id integer NULL,\r\n    issue_in_status_count integer NULL,\r\n    issue_key character varying(255) NULL,\r\n    issue_status_id integer NULL,\r\n    issue_status_name character varying(255) NULL,\r\n    issues_tipo_de_item character varying(255) NULL,\r\n    last_transition_from_status timestamp without time zone NULL,\r\n    last_transition_to_status timestamp without time zone NULL,\r\n    posicao integer NULL,\r\n    CONSTRAINT \"PK_timeinstatus\" PRIMARY KEY (time_in_status_id),\r\n    CONSTRAINT fk_issue_time_in_status FOREIGN KEY (issue_id) REFERENCES \"{0}\".issues (issue_id)\r\n);" +
              "\r\n\r\n\r\nCREATE TABLE \"{0}\".worklogs (\r\n    worklog_id integer GENERATED ALWAYS AS IDENTITY,\r\n    author_account_id integer NULL,\r\n    author_name character varying(255) NULL,\r\n    created timestamp without time zone NULL,\r\n    issue_id integer NULL,\r\n    issue_key character varying(255) NULL,\r\n    logged numeric(10,2) NULL,\r\n    start_date timestamp without time zone NULL,\r\n    update_account_id integer NULL,\r\n    update_name character varying(255) NULL,\r\n    CONSTRAINT \"PK_worklogs\" PRIMARY KEY (worklog_id),\r\n    CONSTRAINT fk_issue_worklogs FOREIGN KEY (issue_id) REFERENCES \"{0}\".issues (issue_id)\r\n);\r\n\r\n\r\nCREATE INDEX \"IX_epic_issues_issue_id\" ON \"{0}\".epic_issues (issue_id);\r\n\r\n\r\nCREATE INDEX \"IX_flagged_issue_id\" ON \"{0}\".flagged (issue_id);" +
              "\r\n\r\n\r\nCREATE INDEX \"IX_issues_project_id\" ON \"{0}\".issues (project_id);" +
              "\r\n\r\n\r\nCREATE INDEX \"IX_sprints_issues_issue_id\" ON \"{0}\".sprints_issues (issue_id);" +
              "\r\n\r\n\r\nCREATE INDEX \"IX_sprints_issues_sprint_id\" ON \"{0}\".sprints_issues (sprint_id);" +
              "\r\n\r\n\r\nCREATE INDEX \"IX_timeinstatus_issue_id\" ON \"{0}\".timeinstatus (issue_id);" +
              "\r\n\r\n\r\nCREATE INDEX \"IX_worklogs_issue_id\" ON \"{0}\".worklogs (issue_id);\r\n\r\n\r\n\r\n", customer.SchemaName);
            _dbContext.Database.ExecuteSqlRaw(script);
            InsertFakeInTables(customer);
        }
        private void InsertFakeInTables(Customer customer)
        {
            var script = string.Format(@"

        -- Inserir dados na tabela 'project'
        INSERT INTO ""{0}"".project (category, ""name"", project_key) VALUES
        ('Categoria1', 'Projeto1', 'Chave1'),
        ('Categoria2', 'Projeto2', 'Chave2');

        -- Inserir dados na tabela 'sprints'
        INSERT INTO ""{0}"".sprints (complete_date, end_date, goal, sprint_name, start_date, state) VALUES
        ('2023-09-30', '2023-10-15', 'Meta1', 'Sprint1', '2023-09-15', 'Concluída'),
        ('2023-10-30', '2023-11-15', 'Meta2', 'Sprint2', '2023-10-15', 'Ativa');
    ", customer.SchemaName);

            _dbContext.Database.ExecuteSqlRaw(script);
        }

    }

}