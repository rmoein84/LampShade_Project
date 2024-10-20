using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthHelper _authHelper;
        public RoleApplication(IRoleRepository roleRepository, IAuthHelper authHelper)
        {
            _roleRepository = roleRepository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.Exists(x=>x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var role = new Role(command.Name, new List<Permission>());
            _roleRepository.Create(role);
            _roleRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var role = _roleRepository.Get(command.Id);
            if (role == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var permissions = new List<Permission>();
            command.Permissions.ForEach(code => permissions.Add(new Permission(code)));
            var accountRole = _authHelper.CurrentAccountInfo().Role;
            if (command.Name == accountRole)
                _authHelper.SetPermissions(permissions.Select(x => x.Code).ToList());
            role.Edit(command.Name, permissions);
            _roleRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<RoleViewModel> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> Search(string name)
        {
            return _roleRepository.Search(name);
        }
    }
}
