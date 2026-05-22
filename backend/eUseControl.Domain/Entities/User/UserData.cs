using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.User;

public class UserData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(30)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(30)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(30, MinimumLength = 4)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(48)]
    public string Password { get; set; } = string.Empty;

    [StringLength(15)]
    public string Phone { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public DateTime RegisteredOn { get; set; }
}
