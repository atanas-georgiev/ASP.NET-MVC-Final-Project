namespace Planex.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Planex.Data.Models;
    using Planex.Services.Messages;
    using Planex.Services.Users;
    using Planex.Web.Infrastructure.Mappings;
    using Planex.Web.Models.Messages;

    using Vereyon.Web;

    [Authorize]
    public class MessagesController : BaseController
    {
        private readonly IMessageService messageService;

        public MessagesController(IUserService userService, IMessageService messageService)
            : base(userService)
        {
            this.messageService = messageService;
        }

        public ActionResult Inbox()
        {
            return this.View();
        }

        public ActionResult Send()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(MessageCreateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var userToSend = this.UserService.GetById(model.Receiver);
                var messageDb = new Message()
                                    {
                                        From = this.UserProfile, 
                                        To = userToSend, 
                                        Subject = model.Subject, 
                                        Text = model.Text, 
                                        Date = DateTime.Now
                                    };

                this.messageService.Add(messageDb);

                return this.RedirectToAction("Inbox");
            }

            return this.View(model);
        }

        public ActionResult Details(string id)
        {
            var intId = int.Parse(id);
            var messageDb = this.messageService.GetAll().FirstOrDefault(x => x.Id == intId);
            if (messageDb != null)
            {
                if (messageDb.ToId != UserProfile.Id)
                {
                    return this.HttpNotFound();
                }

                messageDb.IsRead = true;
                this.messageService.Update(messageDb);
            }

            var message = this.messageService.GetAll().Where(x => x.Id == intId).To<MessageViewModel>().FirstOrDefault();

            var sanitizer = HtmlSanitizer.SimpleHtml5DocumentSanitizer();
            
            if (message != null)
            {
                message.Text = sanitizer.Sanitize(message.Text);                
            }

            return this.View(message);
        }

        public ActionResult Remove(string id)
        {
            var intId = int.Parse(id);
            var messageDb = this.messageService.GetAll().FirstOrDefault(x => x.Id == intId);
   
            if (messageDb != null)
            {
                if (messageDb.ToId != UserProfile.Id)
                {
                    return this.HttpNotFound();
                }

                this.messageService.Delete(messageDb.Id);
            }

            return this.RedirectToAction("Inbox");
        }
    }
}