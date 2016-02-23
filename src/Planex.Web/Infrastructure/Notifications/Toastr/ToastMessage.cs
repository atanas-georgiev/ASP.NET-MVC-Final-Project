namespace Planex.Web.Infrastructure.Notifications.Toastr
{
    using System;

    [Serializable]
    public class ToastMessage
    {
        public bool IsSticky { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public ToastType ToastType { get; set; }
    }
}