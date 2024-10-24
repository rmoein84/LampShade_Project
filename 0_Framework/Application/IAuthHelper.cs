namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        bool IsAuthenticated();
        void SignOut();
        void Signin(AuthViewModel account);
        AuthViewModel CurrentAccountInfo();
        string CurrentAccountRoleId();
        List<int> GetPermissions();
        void SetPermissions(List<int> permissions);
        long CurrentAccountId();
        string CurrentAccountMobile();
    }
}
