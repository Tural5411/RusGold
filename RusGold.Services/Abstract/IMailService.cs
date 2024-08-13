using RusGold.Entities.DTOs;
using RusGold.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusGold.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto, string recaptchaToken);
        IResult SendContactEmail(EmailSendDto emailSendDto);
    }
}
