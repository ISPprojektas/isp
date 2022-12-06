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
	/// Database operations related to 'ParduotuvesDarboDiena' entity.
	/// </summary>
	public class ParduotuvesDarboDienaRepo
	{
		public static List<ParduotuvesDarboDiena> List()
		{
			var ParduotuvesDarboDienos = new List<ParduotuvesDarboDiena>();

			var query =
				$@"SELECT
					darb.id,
					darb.darbo_pradzia,
                    darb.darbo_pabaiga,
                    dienos.name,
					dienos.id_darbo_dienos,
					d.kodas,
					d.pavadinimas
				FROM
					`{Config.TblPrefix}Parduotuves_darbo_dienos` darb
					LEFT JOIN `{Config.TblPrefix}darbo_dienos` dienos ON darb.fk_DARBO_DIENAdiena=dienos.id_darbo_dienos
					LEFT JOIN `{Config.TblPrefix}parduotuves` d ON darb.fk_PARDUOTUVEkodas=d.kodas
				ORDER BY d.pavadinimas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				ParduotuvesDarboDienos.Add(new ParduotuvesDarboDiena
				{
					ID = Convert.ToInt32(item["id"]),
					DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"])),
                    DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"])),
					FkDarboDiena = Convert.ToInt32(item["id_darbo_dienos"]),
                    FkDarboDienaToString = Convert.ToString(item["name"]),
					FkParduotuve = Convert.ToString(item["kodas"]),
                    FkParduotuveToString = Convert.ToString(item["pavadinimas"])
				});
			}

			return ParduotuvesDarboDienos;
		}

		public static ParduotuvesDarboDienaEditVM Find(int id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}parduotuves_darbo_dienos` 
            WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var dddevm = new ParduotuvesDarboDienaEditVM();

				foreach( DataRow item in dt )
				{
					dddevm.ParduotuvesDarboDiena.ID = Convert.ToInt32(item["id"]);
					dddevm.ParduotuvesDarboDiena.DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"]));
					dddevm.ParduotuvesDarboDiena.DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"]));
					dddevm.ParduotuvesDarboDiena.FkDarboDiena = Convert.ToInt32(item["fk_DARBO_DIENAdiena"]);
					dddevm.ParduotuvesDarboDiena.FkParduotuve = Convert.ToString(item["fk_PARDUOTUVEkodas"]);
				}

				return dddevm;
			}

			return null;
		}

		public static ParduotuvesDarboDiena FindForDeletion(int id)
		{
			var query =
				$@"SELECT
					darb.id,
					darb.darbo_pradzia,
                    darb.darbo_pabaiga,
                    dienos.name,
					dienos.id_darbo_dienos,
					d.kodas,
                    d.pavadinimas
				FROM
					`{Config.TblPrefix}parduotuves_darbo_dienos` darb
					LEFT JOIN `{Config.TblPrefix}darbo_dienos` dienos ON darb.fk_DARBO_DIENAdiena=dienos.id_darbo_dienos
					LEFT JOIN `{Config.TblPrefix}parduotuves` d ON darb.fk_PARDUOTUVEkodas=d.kodas
            	WHERE id=?id";

			var dt =
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.Int32).Value = id;
				});

			if( dt.Count > 0 )
			{
				var d = new ParduotuvesDarboDiena();

				foreach( DataRow item in dt )
				{
					d.ID = Convert.ToInt32(item["id"]);
					d.DarboPradzia = TimeSpan.Parse(Convert.ToString(item["darbo_pradzia"]));
					d.DarboPabaiga = TimeSpan.Parse(Convert.ToString(item["darbo_pabaiga"]));
					d.FkDarboDiena = Convert.ToInt32(item["id_darbo_dienos"]);
					d.FkDarboDienaToString = Convert.ToString(item["name"]);
					d.FkParduotuve = Convert.ToString(item["kodas"]);
					d.FkParduotuveToString = Convert.ToString(item["pavadinimas"]);
				}

				return d;
			}

			return null;
		}

		public static void Insert(ParduotuvesDarboDienaEditVM ParduotuvesDarboDienaEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}parduotuves_darbo_dienos`
				(
					darbo_pradzia,
                    darbo_pabaiga,
                    fk_DARBO_DIENAdiena,
                    fk_PARDUOTUVEkodas
				)
				VALUES(
					?pradzia,
					?pabaiga,
					?diena,
					?parduotuve
				)";

			Sql.Insert(query, args => {
				args.Add("?pradzia", MySqlDbType.Time).Value = (new DateTime() + ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.DarboPradzia).TimeOfDay;
                args.Add("?pabaiga", MySqlDbType.Time).Value = (new DateTime() + ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.DarboPabaiga).TimeOfDay;
                args.Add("?diena", MySqlDbType.Int32).Value = ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.FkDarboDiena;
                args.Add("?parduotuve", MySqlDbType.VarChar).Value = ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.FkParduotuve;
			});
		}

		public static void Insert(ParduotuvesDarboDienaEditVM.ParduotuvesDarboDienaM parduotuvesDarboDienaM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}parduotuves_darbo_dienos`
				(
					darbo_pradzia,
                    darbo_pabaiga,
                    fk_DARBO_DIENAdiena,
                    fk_PARDUOTUVEkodas
				)
				VALUES(
					?pradzia,
					?pabaiga,
					?diena,
					?parduotuve
				)";

			Sql.Insert(query, args => {
				args.Add("?pradzia", MySqlDbType.Time).Value = (new DateTime() + parduotuvesDarboDienaM.DarboPradzia).TimeOfDay;
                args.Add("?pabaiga", MySqlDbType.Time).Value = (new DateTime() + parduotuvesDarboDienaM.DarboPabaiga).TimeOfDay;
                args.Add("?diena", MySqlDbType.Int32).Value = parduotuvesDarboDienaM.FkDarboDiena;
                args.Add("?parduotuve", MySqlDbType.VarChar).Value = parduotuvesDarboDienaM.FkParduotuve;
			});
		}

		public static void Update(ParduotuvesDarboDienaEditVM ParduotuvesDarboDienaEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}parduotuves_darbo_dienos`
				SET
					darbo_pradzia=?pradzia,
                    darbo_pabaiga=?pabaiga,
                    fk_DARBO_DIENAdiena=?diena,
                    fk_PARDUOTUVEkodas=?parduotuve
				WHERE
					id=?id";

			Sql.Update(query, args => {
				args.Add("?pradzia", MySqlDbType.Time).Value = (new DateTime() + ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.DarboPradzia).TimeOfDay;
                args.Add("?pabaiga", MySqlDbType.Time).Value = (new DateTime() + ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.DarboPabaiga).TimeOfDay;
                args.Add("?diena", MySqlDbType.Int32).Value = ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.FkDarboDiena;
                args.Add("?parduotuve", MySqlDbType.VarChar).Value = ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.FkParduotuve;
				 args.Add("?id", MySqlDbType.Int32).Value = ParduotuvesDarboDienaEVM.ParduotuvesDarboDiena.ID;
			});
		}

		public static void Delete(int id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}parduotuves_darbo_dienos` 
                WHERE id=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.Int32).Value = id;
			});
		}

		public static void DeleteForParduotuve(string id)
		{
			var query =
				$@"DELETE FROM `{Config.TblPrefix}parduotuves_darbo_dienos`
				WHERE parduotuves_darbo_dienos.fk_PARDUOTUVEkodas=?id";

			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}
	}
}