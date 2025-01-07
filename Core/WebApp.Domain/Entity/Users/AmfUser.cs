using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Entity.Users
{
  public class AmfUser : IdentityUser<long>//, IBaseEntity
  {
    public AmfUser()
    {
    }
    public bool IsActive { get; set; }
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string FullName => $"{FirstName} {LastName}";
    public string? LastName { get; set; }
    [MaxLength(15)]
    public string? Mobile { get; set; }

    [MaxLength(100)]
    public string? Address { get; set; }

    /// <summary>
    /// کد ملی
    /// </summary>
    //[GoldMaxLength(10)]
    public long? NationalCode { get; set; }
    public bool IsRemoved { get; set; }
  }

  public class AmfRole : IdentityRole<long>
  {
    public AmfRole()
    {
    }

  }

  public class AmfUserClaim : IdentityUserClaim<long>
  {
  }

  public class AmfUserRole : IdentityUserRole<long>
  {
  }

  public class AmfUserLogin : IdentityUserLogin<long>
  {
  }

  public class AmfRoleClaim : IdentityRoleClaim<long>
  {
  }

  public class AmfUserToken : IdentityUserToken<long>//, IBaseEntity
  {
    public DateTime? Expiration { get; set; }
    public Guid? RefreshToken { get; set; }
    public DateTime? ExpirationRefresh { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool IsRemoved { get; set; }

    public string? SearchKey => "";

    [NotMapped]
    public long Id { get; set; }
  }

  public class AmfUserManager : UserManager<AmfUser>
  {
    public AmfUserManager(
          IUserStore<AmfUser> store,
          IOptions<IdentityOptions>
          optionsAccessor,
          IPasswordHasher<AmfUser> passwordHasher,
          IEnumerable<IUserValidator<AmfUser>> userValidators,
          IEnumerable<IPasswordValidator<AmfUser>> passwordValidators,
          ILookupNormalizer keyNormalizer,
          IdentityErrorDescriber errors,
          IServiceProvider services,
          ILogger<UserManager<AmfUser>> logger)

      : base(
          store,
          optionsAccessor,
          passwordHasher,
          userValidators,
          passwordValidators,
          keyNormalizer,
          errors,
          services,
          logger)
    {
    }

    public override Task<IdentityResult> AddToRoleAsync(AmfUser user, string role)
    {
      return base.AddToRoleAsync(user, role);
    }

    public override Task<AmfUser> FindByNameAsync(string userName)
    {
      return base.FindByNameAsync(userName);
    }
  }

  public class AmfRoleManager : RoleManager<AmfRole>
  {
    public AmfRoleManager(
          IRoleStore<AmfRole> store,
          IEnumerable<IRoleValidator<AmfRole>> roleValidators,
          ILookupNormalizer keyNormalizer,
          IdentityErrorDescriber errors,
          ILogger<RoleManager<AmfRole>> logger)

      : base(
          store,
          roleValidators,
          keyNormalizer,
          errors,
          logger)
    {
    }

    public override Task<IdentityResult> CreateAsync(AmfRole role)
    {
      return base.CreateAsync(role);
    }
  }
}
