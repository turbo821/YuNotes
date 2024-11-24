﻿using System.Net.Mail;
using System.Net;
using YuNotes.Services.Interfaces;
using System;

namespace YuNotes.Services
{
    public class SimplePasswordRecoveryService : IPasswordRecoveryService
    {
        private NetworkCredential credential;
        private static Random random = new Random();
        public SimplePasswordRecoveryService(NetworkCredential cred)
        {
            credential = cred;
        }

        public async Task<string> SendCodeAsync(string email)
        {
            string to = email;
            string from = "admin@gmail.com";
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Код восстановления почты для YuNotes";
            string code = RandomString(10);
            message.Body = $"Код: {code}";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = credential;
            try
            {
                await client.SendMailAsync(message);
                return code;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return code;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
