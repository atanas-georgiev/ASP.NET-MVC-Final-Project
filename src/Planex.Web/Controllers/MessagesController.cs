using System;
using System.Web.Mvc;
using Planex.Data.Models;
using Planex.Services.Messages;
using Planex.Services.Users;
using Planex.Web.Models.Messages;

namespace Planex.Web.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMessageService messageService;

        public MessagesController(IUserService userService, IMessageService messageService) : base(userService)
        {
            this.messageService = messageService;
        }

        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(MessageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToSend = userService.GetById(model.Receiver);
                var messageDb = new Message()
                {
                    From = UserProfile,
                    To = userToSend,
                    Subject = model.Subject,
                    Text = model.Text,
                    Date = DateTime.UtcNow
                };

                messageService.Add(messageDb);

                return RedirectToAction("Inbox");                
            }

            return View(model);
        }


        public ActionResult View(string id)
        {
            return View();
        }
    }
}