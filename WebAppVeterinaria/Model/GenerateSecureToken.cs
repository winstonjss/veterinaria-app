using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Model
{
    public class GenerateSecureToken
    {
        public string Token { get; private set; }
        public string HashedToken { get; private set; }

        public GenerateSecureToken()
        {
            Token = GenerateSecureTokenForEmail();
            HashedToken = HashTokenEmail(Token);
        }

        private static string GenerateSecureTokenForEmail(int length = 64)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[length];
                rng.GetBytes(tokenData);

                // Codificar en Base64 y convertir al formato URL-safe
                string base64Token = Convert.ToBase64String(tokenData);
                string urlSafeToken = base64Token.Replace("+", "-").Replace("/", "_").TrimEnd('=');
                return urlSafeToken;
            }
        }

        private static string HashTokenEmail(string token)
        {
            using (var sha256 = SHA256.Create())
            {
                var tokenBytes = Encoding.UTF8.GetBytes(token);
                var hashedBytes = sha256.ComputeHash(tokenBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public class PasswordResetToken
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }

        public PasswordResetToken GeneratePasswordResetToken()
        {
            return new PasswordResetToken
            {
                Token = GenerateSecureTokenForEmail(),
                Expiration = DateTime.UtcNow.AddHours(1) // El token expira en 1 hora
            };
        }

        public void SendPasswordResetEmail(string email, string token)
        {
            string resetLink = $"https://localhost:44395/WFCambiarContrasena.aspx?token={token}";

            // Configuración del cliente SMTP
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("enviarcorreodemo@gmail.com", "mjvy bdfo udgu zzxf"), // Cambia a tus credenciales
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("enviarcorreodemo@gmail.com"),
                Subject = "Password Reset",
                Body = $"Hello,\n\nClick the link below to reset your password:\n{resetLink}\n\nThis link will expire in 1 hour.",
                IsBodyHtml = false
            };

            mailMessage.To.Add(email);

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Password reset email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }

}
