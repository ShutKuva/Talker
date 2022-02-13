using System;
using System.Collections.Generic;
using MimeKit;
using MailKit.Net.Smtp;

namespace BLL.Services
{
    class MailSender
    {
        public Dictionary<string, string> Errors;
        public bool HasErrors = false;
        public readonly string VerificationCode;
        private readonly string _target;
        private readonly string _targetUsername;

        public MailSender(string targetMail, string targetUsername)
        {

            VerificationCode = new Random().Next(1000000, 9999999).ToString();
            _target = targetMail;
        }

        public void SendMail()
        {
            var message = GenerateMessage();
            message.From.Add(new MailboxAddress("Talker LLC.", "talkerllc@gmail.com"));
            try
            {
                message.To.Add(MailboxAddress.Parse(_target));
            }
            catch (Exception ex)
            {
                Errors[nameof(SendMail)] = ex.Message;
                HasErrors = true;
                return;
            }
            Send(message);
        }

        private MimeMessage GenerateMessage()
        {
            var message = new MimeMessage();
            message.Subject = "Verification code";
            message.Body = new TextPart("plain")
            {
                Text = $@"HI, {_targetUsername}!
your verification code is {VerificationCode}"
            };

            return message;
        }

        private void Send(MimeMessage message)
        {
            var client = new SmtpClient();

            try
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("talkerllc@gmail.com", "TalkerMSE");
                client.Send(message);
            }
            catch (Exception ex)
            {
                Errors[nameof(Send)] = ex.Message;
                HasErrors = true;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
