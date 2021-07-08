using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnergyPriceInspector.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private Command<string> _sendMailCommand;

        public AboutViewModel() => Title = Langs.Langs.AboutTitle;

        public ICommand SendMailCommand => _sendMailCommand ??= new Command<string>(async (address) => await SendMailAsync(address));

        private Task SendMailAsync(string address)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = Langs.Langs.MailSubject,
                    Body = Langs.Langs.MailBody,
                    To = new List<string>(new[] { address })
                };
                return Email.ComposeAsync(message);
            }
            catch (Exception) {
                return Task.CompletedTask;
            }

        }

    }
}
