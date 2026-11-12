using System.ComponentModel.DataAnnotations;
using HairSalon.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HairSalon_Website.Areas.Identity.Pages.Account;

[Authorize(Roles = "Admin")]
public class DeleteHairdresserAccountModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteHairdresserAccountModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "Въведете имейла на акаунта.")]
        [EmailAddress(ErrorMessage = "Въведете валиден имейл адрес.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Въведете паролата на администратора.")]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; } = string.Empty;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var admin = await _userManager.GetUserAsync(User);
        if (admin is null || !await _userManager.IsInRoleAsync(admin, "Admin"))
        {
            return Forbid();
        }

        if (!await _userManager.CheckPasswordAsync(admin, Input.AdminPassword))
        {
            ModelState.AddModelError("Input.AdminPassword", "Невалидна парола на администратора.");
            return Page();
        }

        var account = await _userManager.FindByEmailAsync(Input.Email.Trim());
        if (account is null)
        {
            ModelState.AddModelError("Input.Email", "Не беше намерен такъв потребител.");
            return Page();
        }

        if (account.Id == admin.Id || await _userManager.IsInRoleAsync(account, "Admin"))
        {
            ModelState.AddModelError("Input.Email", "Администраторски акаунт не може да бъде изтрит оттук.");
            return Page();
        }

        var accountName = account.UserName ?? account.Email ?? Input.Email.Trim();
        var result = await _userManager.DeleteAsync(account);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Акаунтът не можа да бъде изтрит. Опитайте отново.");
            return Page();
        }

        TempData["AccountDeleted"] = $"Успешно изтрихте акаунта {accountName}.";
        return RedirectToPage();
    }
}
