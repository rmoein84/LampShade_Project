using _0_Framework.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Register(RegisterAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var pictureName = _fileUploader.Upload(command.ProfilePhoto, "profilePhotos");

            var password = _passwordHasher.Hash(command.Password);

            var account = new Account(command.Fullname, command.Username, password,
                command.Mobile, command.RoleId, pictureName);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.Id != command.Id && (x.Username == command.Username || x.Mobile == command.Mobile)))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var pictureName = _fileUploader.Upload(command.ProfilePhoto, "profilePhotos");
            account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, pictureName);
            _accountRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.Username);
            if (account == null)
                return operation.Failed(ApplicationMessages.UserNotFound);
            (bool verified, bool needsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
            if (!result.verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);
            var permission = _roleRepository.Get(account.RoleId).Permissions.Select(x=> x.Code).ToList();
            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Role.Name,
                account.Fullname, account.Username, permission, account.Mobile);

            _authHelper.Signin(authViewModel);
            return operation.Succedded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public AccountViewModel GetAccountBy(long id)
        {
            var account = _accountRepository.Get(id);
            return new AccountViewModel
            {
                Fullname = account.Fullname,
                Mobile = account.Mobile,
            };
        }
    }
}
