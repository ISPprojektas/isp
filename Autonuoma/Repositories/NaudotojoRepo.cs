using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
    /// <summary>
    /// Database operations related to 'Prekes' entity.
    /// </summary>
    public class NaudotojoRepo
    {
        public static List<NaudotojoListVM> List()
        {
            var naudotojai = new List<NaudotojoListVM>();

            var query =
                $@"SELECT 
            id,
            Vardas,
            Pavardė,
            ElPaštas,
            Slaptažodis,
            GimimoData,
            Slapyvardis,
            KortelėsNr,
            NuotraukaLink,
            TelefonoNr,
            Miestas,
            Registracijos_Data, 
            kliento_tipai.name as 'Kliento tipas',
            naudotojo_privilegijos.name as 'Privilegijos'
            FROM `naudotojai`
            LEFT JOIN kliento_tipai ON kliento_tipai.id = naudotojai.Tipas
            LEFT JOIN naudotojo_privilegijos ON naudotojo_privilegijos.id = naudotojai.Privilegijos";

            var dt = Sql.Query(query);

            foreach (DataRow item in dt)
            {
                naudotojai.Add(new NaudotojoListVM
                {
                    pk_Id = Convert.ToInt32(item["id"]),
                    Vardas = Convert.ToString(item["Vardas"]),
                    Pavarde = Convert.ToString(item["Pavardė"]),
                    ElPastas = Convert.ToString(item["ElPaštas"]),
                    Slaptazodis = Convert.ToString(item["Slaptažodis"]),
                    GimimoData = Convert.ToDateTime(item["GimimoData"]),
                    Slapyvardis = Convert.ToString(item["Slapyvardis"]),
                    KortelesNr = Convert.ToString(item["KortelėsNr"]),
                    NuotraukosLink = Convert.ToString(item["NuotraukaLink"]),
                    TelNr = Convert.ToString(item["NuotraukosLink"]),
                    Miestas = Convert.ToString(item[columnName: "Miestas"]),
                    RegistracijosData = Convert.ToDateTime(item[columnName: "Registracijos_Data"]),
                    Tipas = Convert.ToInt32(item["Kliento tipas"]),
                    Privilegijos = Convert.ToInt32(item["Privilegijos"]),
                });
            }

            return naudotojai;
        }

        public static NaudotojoEditVM Find(string id)
        {
            var query = $@"SELECT * FROM `naudotojai` WHERE kodas=?id";

            var dt =
                Sql.Query(query, args => {
                    args.Add("?id", MySqlDbType.VarChar).Value = id;
                });

            if (dt.Count > 0)
            {
                var gevm = new NaudotojoEditVM();

                foreach (DataRow item in dt)
                {
                    gevm.Naudotojas.pk_Id = Convert.ToInt32(item["kodas"]);
                    gevm.Naudotojas.Vardas = Convert.ToString(item["Vardas"]);
                    gevm.Naudotojas.Pavarde = Convert.ToString(item["Pavardė"]);
                    gevm.Naudotojas.ElPastas = Convert.ToString(item["ElPaštas"]);
                    gevm.Naudotojas.Slaptazodis = Convert.ToString(item["Slaptažodis"]);
                    gevm.Naudotojas.GimimoData = Convert.ToDateTime(item["GimimoData"]);
                    gevm.Naudotojas.Slapyvardis = Convert.ToString(item["Slapyvardis"]);
                    gevm.Naudotojas.KortelesNr = Convert.ToString(item["KortelėsNr"]);
                    gevm.Naudotojas.NuotraukosLink = Convert.ToString(item["NuotraukaLink"]);
                    gevm.Naudotojas.TelNr = Convert.ToString(item["NuotraukosLink"]);
                    gevm.Naudotojas.Miestas = Convert.ToString(item[columnName: "Miestas"]);
                    gevm.Naudotojas.RegistracijosData = Convert.ToDateTime(item[columnName: "Registracijos_Data"]);
                    gevm.Naudotojas.Tipas = Convert.ToInt32(item["Kliento tipas"]);
                    gevm.Naudotojas.Privilegijos = Convert.ToInt32(item["Privilegijos"]);
                }

                return gevm;
            }

            return null;
        }

        public static NaudotojoListVM FindForDeletion(string id)
        {
            var g = new NaudotojoListVM();

            var query =
                $@"SELECT 
            id,
            Vardas,
            Pavardė,
            ElPaštas,
            Slaptažodis,
            GimimoData,
            Slapyvardis,
            KortelėsNr,
            NuotraukaLink,
            TelefonoNr,
            Miestas,
            Registracijos_Data, 
            kliento_tipai.name as 'Kliento tipas',
            naudotojo_privilegijos.name as 'Privilegijos'
            FROM `naudotojai`
            LEFT JOIN kliento_tipai ON kliento_tipai.id = naudotojai.Tipas
            LEFT JOIN naudotojo_privilegijos ON naudotojo_privilegijos.id = naudotojai.Privilegijos";

            var dt =
                Sql.Query(query, args => {
                    args.Add("?id", MySqlDbType.VarChar).Value = id;
                });

            foreach (DataRow item in dt)
            {
                g.pk_Id = Convert.ToInt32(item["id"]);
                g.Vardas = Convert.ToString(item["Vardas"]);
                g.Pavarde = Convert.ToString(item["Pavardė"]);
                g.ElPastas = Convert.ToString(item["ElPaštas"]);
                g.Slaptazodis = Convert.ToString(item["Slaptažodis"]);
                g.GimimoData = Convert.ToDateTime(item["GimimoData"]);
                g.Slapyvardis = Convert.ToString(item["Slapyvardis"]);
                g.KortelesNr = Convert.ToString(item["KortelėsNr"]);
                g.NuotraukosLink = Convert.ToString(item["NuotraukaLink"]);
                g.TelNr = Convert.ToString(item["NuotraukosLink"]);
                g.Miestas = Convert.ToString(item[columnName: "Miestas"]);
                g.RegistracijosData = Convert.ToDateTime(item[columnName: "Registracijos_Data"]);
                g.Tipas = Convert.ToInt32(item["Kliento tipas"]);
                g.Privilegijos = Convert.ToInt32(item["Privilegijos"]);
            }

            return g;
        }


        public static void Insert(NaudotojoEditVM NaudotojoEVM)
        {
            var query =
                $@"INSERT INTO `naudotojai` 
				(`Vardas`, `Pavardė`, `ElPaštas`, 
				`Slaptažodis`, `GimimoData`, 
				`Slapyvardis`, `KortelėsNr`, `NuotraukaLink`, `TelefonoNr`, 
				`Miestas`, `Registracijos_Data`, `Tipas`, 
				`Privilegijos`) 
				VALUES (
					?vard,
					?pavard,
					?elpastas,
					?slaptazod,
					?gimdata,
					?slapyvard,
					?kortnr,
					?nuotraukalink,
					?telnr,
					?miestas,
					?regdata,
					?tipas,
					?priv,
				)";

            var scc = Sql.Insert(query, args => {
                args.Add("?vard", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Vardas;
                args.Add("?pavard", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Pavarde;
                args.Add("?elpastas", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.ElPastas;
                args.Add("?slaptazod", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Slaptazodis;
                args.Add("?gimdata", MySqlDbType.DateTime).Value = NaudotojoEVM.Naudotojas.GimimoData;
                args.Add("?slapyvard", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Slapyvardis;
                args.Add("?kortnr", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.KortelesNr;
                args.Add("?nuotraukalink", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.NuotraukosLink;
                args.Add("?telnr", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.TelNr;
                args.Add("?miestas", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Miestas;
                args.Add("?regdata", MySqlDbType.DateTime).Value = NaudotojoEVM.Naudotojas.RegistracijosData;
                args.Add("?tipas", MySqlDbType.Int32).Value = NaudotojoEVM.Naudotojas.Tipas;
                args.Add("?priv", MySqlDbType.Int32).Value = NaudotojoEVM.Naudotojas.Privilegijos;
            });
        }

        public static void Update(NaudotojoEditVM NaudotojoEVM)
        {
            var query =
                $@"UPDATE naudotojai
				SET
					Vardas=?vard,
					Pavardė=?pavard,
					ElPaštas=?elpastas,
					Slaptažodis=?slaptazod,
					GimimoData=?gimdata,
					Slapyvardis=?slapyvard,
					KortelėsNr=?kortnr,
					NuotraukaLink=?nuotraukalink,
					TelefonoNr=?telnr,
					Miestas=?miestas,
					Registracijos_Data=?regdata,
					Tipas=?tipas,
					Privilegijos=?priv
				WHERE
					id=?id";

            var succ = Sql.Update(query, args => {
                args.Add("?vard", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Vardas;
                args.Add("?pavard", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Pavarde;
                args.Add("?elpastas", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.ElPastas;
                args.Add("?slaptazod", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Slaptazodis;
                args.Add("?gimdata", MySqlDbType.DateTime).Value = NaudotojoEVM.Naudotojas.GimimoData;
                args.Add("?slapyvard", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Slapyvardis;
                args.Add("?kortnr", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.KortelesNr;
                args.Add("?nuotraukalink", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.NuotraukosLink;
                args.Add("?telnr", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.TelNr;
                args.Add("?miestas", MySqlDbType.VarChar).Value = NaudotojoEVM.Naudotojas.Miestas;
                args.Add("?regdata", MySqlDbType.DateTime).Value = NaudotojoEVM.Naudotojas.RegistracijosData;
                args.Add("?tipas", MySqlDbType.Int32).Value = NaudotojoEVM.Naudotojas.Tipas;
                args.Add("?priv", MySqlDbType.Int32).Value = NaudotojoEVM.Naudotojas.Privilegijos;
                args.Add("?id", MySqlDbType.Int32).Value = NaudotojoEVM.Naudotojas.pk_Id;
            });
            Console.WriteLine(NaudotojoEVM.Naudotojas.pk_Id);
            Console.WriteLine("----");
        }

        public static void Delete(string id)
        {
            var query = $@"DELETE FROM `naudotojai` WHERE id=?id";
            Sql.Delete(query, args => {
                args.Add("?id", MySqlDbType.VarChar).Value = id;
            });
        }

        public static bool Exists(int id)
        {
            var query = $@"SELECT * FROM `naudotojai` WHERE id=?id";
            var dt =
                Sql.Query(query, args => {
                    args.Add("?id", MySqlDbType.Int32).Value = id;
                });

            foreach (DataRow item in dt)
            {
                return true;
            }

            return false;
        }
    }                
}