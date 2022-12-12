﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;


namespace Org.Ktu.Isk.P175B602.Autonuoma.ViewModels
{
    /// <summary>
    /// Model of 'Modelis' entity used in creation and editing forms.
    /// </summary>
    public class NaudotojoEditVM
    {
        /// <summary>
        /// Entity data
        /// </summary>
        public class NaudotojoM
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
                set { _slaptazodis = EncryptPassword(value); }
            }

            [DisplayName("Gimimo data")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            [Required]
            public DateTime GimimoData { get; set; }

            [DisplayName("Slapyvardis")]
            [Required]
            public string Slapyvardis { get; set; }

            [DisplayName("Kortelės numeris")]
            [Required]
            public string KortelesNr { get; set; }

            [DisplayName("Nuotraukos nuoroda")]
            public string? NuotraukosLink { get; set; }

            [DisplayName("Telefono numeris")]
            public string TelNr { get; set; }

            [DisplayName("Miestas")]
            public string? Miestas { get; set; }

            [DisplayName("Registracijos data")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime RegistracijosData { get; set; }

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
                using (var sha256 = SHA256.Create())
                {
                    var slaptazodisHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(slaptazodis));
                    return Convert.ToBase64String(slaptazodisHash) == encryptedSlaptazodis;
                }
            }
        }

        /// <summary>
        /// Select lists for making drop downs for choosing values of entity fields.
        /// </summary>
        public class ListsM
        {
            public IList<SelectListItem> NaudotojoPrivilegijos { get; set; }
			public IList<SelectListItem> KlientoTipai { get; set; }
		}

        /// <summary>
        /// Entity view.
        /// </summary>
        public NaudotojoM Naudotojas { get; set; } = new NaudotojoM();

        /// <summary>
        /// Lists for drop down controls.
        /// </summary>
        public ListsM Lists { get; set; } = new ListsM();
    }
}