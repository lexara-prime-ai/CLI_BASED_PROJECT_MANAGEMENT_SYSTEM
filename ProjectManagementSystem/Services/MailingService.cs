using System.Net.Http.Headers;
using System.Text;

namespace ProjectManagementSystem.Services
{
  public class MailingService
  {
    public static async Task SEND_MAIL(string rdx_USER_EMAIL)
    {
      /*
         Default fields : Path to connection string
     */
      string rdx_PROJECT_PATH = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
      string rdx_ACCESS_TOKEN_PATH = $"{rdx_PROJECT_PATH}\\Services\\AccessToken.txt";

      string accessToken = File.ReadAllText(rdx_ACCESS_TOKEN_PATH);

      string emailEndpoint = "https://graph.microsoft.com/v1.0/me/sendMail";

      string emailJson = $@"
            {{
            ""message"": {{
                ""subject"": ""Thank you for Registering..."",
                ""body"": {{
                    ""contentType"": ""Text"",
                    ""content"": ""Thank you for registering. I really don't get what all the fuss is about Rust. I mean, C also forces you to write memory safe code. The only difference is that it is runtime enforced.""
                }},
                ""toRecipients"": [
                    {{
                        ""emailAddress"": {{
                            ""address"": ""{rdx_USER_EMAIL}""
                        }}
                    }}
                ]
            }}
        }}
        ";

      using HttpClient client = new();
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

      StringContent content = new(emailJson, Encoding.UTF8, "application/json");

      HttpResponseMessage response = await client.PostAsync(emailEndpoint, content);

      if (response.IsSuccessStatusCode)
      {
        Console.WriteLine("Email sent successfully.");
      }
      else
      {
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error sending email: {responseContent}");
      }
    }
  }
}