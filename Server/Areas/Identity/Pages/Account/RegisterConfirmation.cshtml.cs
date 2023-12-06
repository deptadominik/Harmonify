// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Harmonify.Shared.Models;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Harmonify.Server.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _sender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RegisterConfirmationModel> _logger;

        public RegisterConfirmationModel(
            UserManager<ApplicationUser> userManager,
            IEmailSender sender,
            IConfiguration configuration,
            ILogger<RegisterConfirmationModel> logger)
        {
            _userManager = userManager;
            _sender = sender;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool DisplayConfirmAccountLink { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            MailjetClient client = new MailjetClient(
                _configuration["MailjetApiKey"],
                _configuration["MailjetSecretKey"]);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var emailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            var request = new MailjetRequest
                {
                    Resource = SendV31.Resource
                }
                .Property(Send.Messages, new JArray
                {
                    new JObject
                    {
                        {
                            "From", new JObject
                            {
                                { "Email", "harmonify.wsb@gmail.com" },
                                { "Name", "Harmonify" }
                            }
                        },
                        {
                            "To", new JArray
                            {
                                new JObject
                                {
                                    { "Email", email }
                                }
                            }
                        },
                        { "TemplateID", 5387069 },
                        { "TemplateLanguage", true },
                        {
                            "Variables", new JObject
                            {
                                { "confirm_url", emailConfirmationUrl }
                            }
                        }
                    }
                });

            var response = await client.PostAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                _logger.LogInformation(response.GetData().ToString());
            }
            else
            {
                _logger.LogInformation(string.Format("StatusCode: {0}\n", response.StatusCode));
                _logger.LogInformation(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                _logger.LogInformation(response.GetData().ToString());
                _logger.LogInformation(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            return Page();
        }
    }
}
