using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProduct.Core.DTOs
{
    public class RegRequestDto
    {
        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required][EmailAddress] public string Email { get; set; }

        [PasswordPropertyText]
        [Required] public string Password { get; set; }
    }
}
