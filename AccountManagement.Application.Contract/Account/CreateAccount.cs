using _0_Framework.Application;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Fullname { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Username { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Password { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequire)]
        public string Mobile { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequire)]
        public long RoleId { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public List<RoleViewModel> Roles { get; set; }
    }
}
