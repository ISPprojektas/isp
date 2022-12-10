using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Model
{
    public class Naudotojai
    {
        private string _slaptazodis;

        [DisplayName("Vardas")]
        [Required]
        public string Vardas { get; set; }

        [DisplayName("Pavardė")]
        [Required]
        public string Pavarde { get; set; }

        [DisplayName("Elektroninis paštas")]
        [Required]
        public string ElPastas { get; set; }

        [DisplayName("Slaptažodis")]
        [Required]
        public string Slaptazodis 
        {
            get { return _slaptazodis; }
            set { _slaptazodis = value;}
        }

        [DisplayName("Gimimo data")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateOnly GimimoData { get; set; }

        [DisplayName("Slapyvardis")]
        [Required]
        public string Slapyvardis { get; set; }

        [DisplayName("Kortelės numeris")]
        [Required]
        public string KortelesNr { get; set; }

        [DisplayName("Nuotraukos nuoroda")]
        [Required]
        public string NuotraukosLink { get; set; }

        [DisplayName("Telefono numeris")]
        public string TelNr { get; set; }

        [DisplayName("Miestas")]
        public string Miestas { get; set; }

        [DisplayName("Registracijos data")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateOnly RegistracijosData { get; set; }

        [DisplayName("Tipas")]
        [Required]
        public int Tipas { get; set; }

        [DisplayName(displayName: "Privilegijos")]
        [Required]
        public int Privilegijos { get; set; }

        [DisplayName("Id")]
        [Required]
        public int pk_Id { get; set; }

        // Enryptina slaptažodį naudojant sha256 hashing algoritmą ir grąžina kaip stringą.
        public string EncryptPassword(string slaptazodis)
        {
            using (var sha256 = SHA256.Create())
            {
                var slaptazodisHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(slaptazodis));
                return Convert.ToBase64String(slaptazodisHash);
            }
        }
        // Įvestą slaptažodį užencryptina ir palygina su jau esamu encrypted slaptažodžiu ir grąžina true arba false
        public bool VerifyPassword(string slaptazodis, string encryptedSlaptazodis)
        {
            var passwordHash = EncryptPassword(slaptazodis);
            return passwordHash == encryptedSlaptazodis;
        }
    }
}
