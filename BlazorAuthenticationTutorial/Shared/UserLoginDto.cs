using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAuthenticationTutorial.Shared
{
    public class UserLoginDto
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set;}=string.Empty;   
        public string Password { get; set;} = string.Empty;
    }
}
