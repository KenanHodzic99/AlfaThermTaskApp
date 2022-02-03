using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAccountService _dataService;
        private readonly IDataService<Permission> permissionService;
        private readonly IDataService<Employees> employeeService;
        public AuthenticateService(IAccountService dataService, IDataService<Permission> permissionService, IDataService<Employees> employeeService)
        {
            _dataService = dataService;
            this.permissionService = permissionService;
            this.employeeService = employeeService;
        }

        public async Task<Users> Login(string username, string password)
        {
            var Account = await _dataService.GetByUsername(username);

            if (Account != null)
            {
                if(Account.PasswordHash == GenerateHash(Account.PasswordSalt, password))
                {
                    return Account;
                }
                else
                {
                    throw new Exception("Incorrect username or password!");
                }
            }

            throw new Exception("Incorrect username or password!");
        }

        public async Task<bool> Register(Employees employee)
        {
            string Salt = GenerateSalt();

            var newEmployee = await employeeService.Get(employee.Id);

            Users newUser = new Users()
            {
                EmployeeId = newEmployee.Id,
                Employee = newEmployee,
                Username = newEmployee.Email,
                PasswordSalt = Salt,
                PasswordHash = GenerateHash(Salt, "ChangeMe123")
            };


            var user = await _dataService.Create(newUser);

            List<Permission> defaultPermissions = new List<Permission>
            {
                new Permission { ModuleName="Project", CanRead=true, CanDelete=false, CanEdit=false, CanWrite=false, UserId = user.Id, User = user },
                new Permission { ModuleName="Department", CanRead=true, CanDelete=false, CanEdit=false, CanWrite=false, UserId = user.Id, User = user },
                new Permission { ModuleName="Jobs", CanRead=true, CanDelete=false, CanEdit=false, CanWrite=false, UserId = user.Id, User = user },
                new Permission { ModuleName="Work card", CanRead=true, CanDelete=false, CanEdit=false, CanWrite=false, UserId = user.Id, User = user },
                new Permission { ModuleName="Employee", CanRead=true, CanDelete=false, CanEdit=false, CanWrite=false, UserId = user.Id, User = user }
            };

            defaultPermissions.ForEach(async (p) => await permissionService.Create(p));

            return true;
        }

        public async Task<Users> ChangePassword(Users user, string newPassword)
        {
            var User = await _dataService.Get(user.Id);

            User.PasswordSalt = GenerateSalt();
            User.PasswordHash = GenerateHash(User.PasswordSalt, newPassword);

            return await _dataService.Update(User.Id, User);

        }

        public async Task<Users> GetAccount(Employees employee)
        {
            return await _dataService.GetByEmployee(employee);
        }

        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
    }
}
