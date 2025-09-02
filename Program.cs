using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using MimeKit.Utils;

var message = new MimeMessage();

var from = new MailboxAddress("Dylan", "dylan@dylanbeattie.net");
message.From.Add(from);
var to = new MailboxAddress("Nick", "nick@dometrain.com");
message.To.Add(to);
message.Subject = "Let's do a video about email!";

var bb = new BodyBuilder();
bb.TextBody = "hello world in plain text";

var imageEntity = bb.LinkedResources.Add("cat.jpg");
imageEntity.ContentId = MimeUtils.GenerateMessageId();
var htmlBody = $"""
<p>Hey, look - here's a picture of my cat!</p>
<img src="cid:{imageEntity.ContentId}" alt="Ginger cat!" />
""";

bb.HtmlBody = htmlBody;

message.Body = bb.ToMessageBody();

using var smtp = new SmtpClient();
await smtp.ConnectAsync("localhost", 1025);
await smtp.SendAsync(message);
await smtp.DisconnectAsync(true);
Console.WriteLine("Mail sent!");

