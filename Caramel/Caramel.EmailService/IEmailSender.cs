// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEmail.cs" company="JustProtect">
//   Copyright (C) 2017. All rights reserved.
// </copyright>
// <summary>
//   The email interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace Caramel.EmailService
{
    public interface IEmailSender  
    {
        void SendEmail(Message message);
    }
}