using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace WakeYourPcWebApp.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

    }
}
