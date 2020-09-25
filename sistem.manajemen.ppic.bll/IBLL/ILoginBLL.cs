using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ILoginBLL
    {
        List<LoginDto> GetAll();
        LoginDto GetById(object Id);
        LoginDto VerificationLogin(LoginDto Login);
        void ChangePassword(string UserId, string PasswordBaru);
        void SetLastOnline(string UserId);
        DateTime? GetLastOnline(string UserId);
        LoginDto Save(LoginDto Login);
    }
}
