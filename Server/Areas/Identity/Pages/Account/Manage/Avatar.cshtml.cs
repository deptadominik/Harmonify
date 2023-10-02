using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace Harmonify.Server.Areas.Identity.Pages.Account.Manage;

public class AvatarModel : PageModel
{
    private IHostingEnvironment _environment;
    
    public AvatarModel(IHostingEnvironment environment)
    {
        _environment = environment;
    }
    
    [BindProperty]
    public IFormFile Upload { get; set; }
    
    public async Task OnPostAsync()
    {
        var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);
        using (var fileStream = new FileStream(file, FileMode.Create))
        {
            await Upload.CopyToAsync(fileStream);
        }
    }
}