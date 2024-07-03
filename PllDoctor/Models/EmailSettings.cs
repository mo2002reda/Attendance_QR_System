using static QRCoder.PayloadGenerator;
using System.Net.Mail;
using System.Net;

namespace PllDoctor.Models
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);

			Client.EnableSsl = true; //for more Security
			Client.Credentials = new NetworkCredential("mr2438844@gmail.com", "nlvcuxjvrgccbmya");
			Client.Send("mr2438844@gmail.com", email.To, email.Title, email.body);

		}
	}
}
