using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DGPub.Infra.CrossCutting.Identity.Authorization
{
    public class SigningCredentialsConfiguration
    {
        // poderia estar em um Azure Key Vault
        private const string SecretKey = "dgpub@developertest";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
