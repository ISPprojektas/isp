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
	/// Database operations related to 'DarbuotojoDarboDiena' entity.
	/// </summary>
	public class DarbuotojoDarboDienaRepo
	{
		public static List<DarbuotojoDarboDiena> List()
		{
			var darbuotojoDarboDienos = new List<DarbuotojoDarboDiena>();

			var query =
				$@"SELECT
					darb.id,
					darb.darbo_pradzia,
                    darb.darbo_pabaiga,
                    dienos.name,
					dienos.id_darbo_dienos,
					d.asmens_kodas,
                    d.vardas,
                    d.pavarde
				FROM
					`{Config.TblPrefix}darbuotojo_darbo_dienos` darb
					LEFT JOIN `{Config.TblPrefix}darbo_dienos` dienos ON darb.fk_DARBO_DIENAdiena=dienos.id_darbo_dienos
					LEFT JOIN `{Config.TblPrefix}darbuotojai` d ON darb.fk_DARBUOTOJASasmens_kodas=d.asmens_kodas
				ORDER BY d.vardas ASC, d.pavarde ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				darbuotojoDarboDienos.Add(new DarbuotojoDarboDiena
				{
					ID = Convert.ToInt32(item["id"]),
					DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"])),
                    DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"])),
					FkDarboDiena = Convert.ToInt32(item["id_darbo_dienos"]),
                    FkDarboDienaToString = Convert.ToString(item["name"]),
					FkDarbuotojas = Convert.ToString(item["asmens_kodas"]),
                    FkDarbuotojasToString = Convert.ToString(item["vardas"]) + " " + Convert.ToString(item["pavarde"])
				});
			}

			return darbuotojoDarboDienos;
		}

		public static List<DarbuotojoDarboDiena> ListForDarboDiena(int id)
		{
			var result = new List<DarbuotojoDarboDiena>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojo_darbo_dienos` WHERE id=?id ORDER BY id ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new DarbuotojoDarboDiena
				{
					DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"])),
                    DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"])),
                    FkDarboDiena = Convert.ToInt32(item["name"]),
                    FkDarbuotojas = Convert.ToString(item["vardas"]) + " " + Convert.ToString(item["pavarde"])
				});
			}

			return result;
		}

		public static List<DarbuotojoDarboDiena> ListForDarbuotojas(int id)
		{
			var result = new List<DarbuotojoDarboDiena>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojo_darbo_dienos` WHERE fk_DARBUOTOJASasmens_kodas=?id ORDER BY fk_DARBUOTOJASasmens_kodas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new DarbuotojoDarboDiena
				{
					DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"])),
                    DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"])),
                    FkDarboDiena = Convert.ToInt32(item["name"]),
                    FkDarbuotojas = Convert.ToString(item["vardas"]) + " " + Convert.ToString(item["pavarde"])
				});
			}

			return result;
		}

		public static DarbuotojoDarboDienaEditVM Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}darbuotojo_darbo_dienos` 
            WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var dddevm = new DarbuotojoDarboDienaEditVM();

				foreach( DataRow item in dt )
				{
					dddevm.DarbuotojoDarboDiena.ID = Convert.ToInt32(item["id"]);
					dddevm.DarbuotojoDarboDiena.DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"]));
					dddevm.DarbuotojoDarboDiena.DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"]));
					dddevm.DarbuotojoDarboDiena.FkDarboDiena = Convert.ToInt32(item["fk_DARBO_DIENAdiena"]);
					dddevm.DarbuotojoDarboDiena.FkDarbuotojas = Convert.ToString(item["fk_DARBUOTOJASasmens_kodas"]);
				}

				return dddevm;
			}

			return null;
		}

		public static DarbuotojoDarboDiena FindForDeletion(int id)
		{
			var query =
				$@"SELECT
					darb.id,
					darb.darbo_pradzia,
                    darb.darbo_pabaiga,
                    dienos.name,
					dienos.id_darbo_dienos,
					d.asmens_kodas,
                    d.vardas,
                    d.pavarde
				FROM
					`{Config.TblPrefix}darbuotojo_darbo_dienos` darb
					LEFT JOIN `{Config.TblPrefix}darbo_dienos` dienos ON darb.fk_DARBO_DIENAdiena=dienos.id_darbo_dienos
					LEFT JOIN `{Config.TblPrefix}darbuotojai` d ON darb.fk_DARBUOTOJASasmens_kodas=d.asmens_kodas
            	WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var d = new DarbuotojoDarboDiena();

				foreach( DataRow item in dt )
				{
					d.ID = Convert.ToInt32(item["id"]);
					d.DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"]));
					d.DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"]));
					d.FkDarboDiena = Convert.ToInt32(item["id_darbo_dienos"]);
					d.FkDarboDienaToString = Convert.ToString(item["name"]);
					d.FkDarbuotojas = Convert.ToString(item["asmens_kodas"]);
					d.FkDarbuotojasToString = Convert.ToString(item["vardas"]) + " " + Convert.ToString(item["pavarde"]);
				}

				return d;
			}

			return null;
		}

		public static void Insert(DarbuotojoDarboDienaEditVM DarbuotojoDarboDienaEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}darbuotojo_darbo_dienos`
				(
					darbo_pradzia,
                    darbo_pabaiga,
                    fk_DARBO_DIENAdiena,
                    fk_DARBUOTOJASasmens_kodas
				)
				VALUES(
					?pradzia,
					?pabaiga,
					?diena,
					?darbuotojas
				)";

			Sql.Insert(query, args => {
				args.Add("?pradzia", MySqlDbType.Time).Value = (new DateTime() + DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.DarboPradzia).TimeOfDay;
                args.Add("?pabaiga", MySqlDbType.Time).Value = (new DateTime() + DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.DarboPabaiga).TimeOfDay;
                args.Add("?diena", MySqlDbType.Int32).Value = DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.FkDarboDiena;
                args.Add("?darbuotojas", MySqlDbType.VarChar).Value = DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.FkDarbuotojas;
			});
		}

		public static void Insert(DarbuotojoDarboDienaEditVM.DarbuotojoDarboDienaM darbuotojoDarboDienaM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}darbuotojo_darbo_dienos`
				(
					darbo_pradzia,
                    darbo_pabaiga,
                    fk_DARBO_DIENAdiena,
                    fk_DARBUOTOJASasmens_kodas
				)
				VALUES(
					?pradzia,
					?pabaiga,
					?diena,
					?darbuotojas
				)";

			Sql.Insert(query, args => {
				args.Add("?pradzia", MySqlDbType.Time).Value = (new DateTime() + darbuotojoDarboDienaM.DarboPradzia).TimeOfDay;
                args.Add("?pabaiga", MySqlDbType.Time).Value = (new DateTime() + darbuotojoDarboDienaM.DarboPabaiga).TimeOfDay;
                args.Add("?diena", MySqlDbType.Int32).Value = darbuotojoDarboDienaM.FkDarboDiena;
                args.Add("?darbuotojas", MySqlDbType.VarChar).Value = darbuotojoDarboDienaM.FkDarbuotojas;
			});
		}

		public static void Update(DarbuotojoDarboDienaEditVM DarbuotojoDarboDienaEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}darbuotojo_darbo_dienos`
				SET
					darbo_pradzia=?pradzia,
                    darbo_pabaiga=?pabaiga,
                    fk_DARBO_DIENAdiena=?diena,
                    fk_DARBUOTOJASasmens_kodas=?darbuotojas
				WHERE
					id=?id";

			Sql.Update(query, args => {
				args.Add("?pradzia", MySqlDbType.Time).Value = (new DateTime() + DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.DarboPradzia).TimeOfDay;
                args.Add("?pabaiga", MySqlDbType.Time).Value = (new DateTime() + DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.DarboPabaiga).TimeOfDay;
                args.Add("?diena", MySqlDbType.Int32).Value = DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.FkDarboDiena;
                args.Add("?darbuotojas", MySqlDbType.VarChar).Value = DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.FkDarbuotojas;
				 args.Add("?id", MySqlDbType.Int32).Value = DarbuotojoDarboDienaEVM.DarbuotojoDarboDiena.ID;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}darbuotojo_darbo_dienos` 
                WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}

		public static void DeleteForDarbuotojas(string id)
		{
			var query =
				$@"DELETE FROM `{Config.TblPrefix}darbuotojo_darbo_dienos`
				WHERE darbuotojo_darbo_dienos.fk_DARBUOTOJASasmens_kodas=?id";

			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}
	}
}