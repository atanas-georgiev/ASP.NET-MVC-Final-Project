namespace Planex.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /*                                                                      *
     *      This extension was derive from Brad Christie's answer           *
     *      on StackOverflow.                                               *
     *                                                                      *
     *      The original code can be found at:                              *
     *      http://stackoverflow.com/a/18338264/998328                      *
     *                                                                      */
    public static class NotificationExtensions
    {
        private static IDictionary<string, string> NotificationKey = new Dictionary<string, string>
                                                                         {
                                                                             {
                                                                                 "Error", 
                                                                                 "App.Notifications.Error"
                                                                             }, 
                                                                             {
                                                                                 "Warning", 
                                                                                 "App.Notifications.Warning"
                                                                             }, 
                                                                             {
                                                                                 "Success", 
                                                                                 "App.Notifications.Success"
                                                                             }, 
                                                                             {
                                                                                 "Info", 
                                                                                 "App.Notifications.Info"
                                                                             }
                                                                         };

        public static void AddNotification(this ControllerBase controller, string message, string notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            ICollection<string> messages = controller.TempData[NotificationKey] as ICollection<string>;

            if (messages == null)
            {
                controller.TempData[NotificationKey] = messages = new HashSet<string>();
            }

            messages.Add(message);
        }

        public static IEnumerable<string> GetNotifications(this HtmlHelper htmlHelper, string notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            return htmlHelper.ViewContext.Controller.TempData[NotificationKey] as ICollection<string> ?? null;
        }

        private static string getNotificationKeyByType(string notificationType)
        {
            try
            {
                return NotificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
    }

    public static class NotificationType
    {
        public const string ERROR = "Error";

        public const string INFO = "Info";

        public const string SUCCESS = "Success";

        public const string WARNING = "Warning";
    }
}