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
	/// Database operations related to 'Sutartis' entity.
	/// </summary>
	public class SutartisRepo
	{
		public static List<SutartisListVM> List()
		{
			var pardavimo_sutartys = new List<SutartisListVM>();

			var query =
				$@"SELECT
					sut.kodas,
                    sut.data,
                    sut.kaina,
                    sut.busena,
                    bus.name,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde AS dpavarde,
                    kli.asmens_kodas,
                    kli.vardas AS kvardas,
                    kli.pavarde AS kpavarde,
                    gyv.vardas AS gvardas,
                    gyv.kodas AS gyvunas
				FROM
					`{Config.TblPrefix}pardavimo_sutartys` sut
					LEFT JOIN `{Config.TblPrefix}sutarties_busenos` bus ON sut.busena=bus.id_sutarties_busenos
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON sut.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}klientai` kli ON sut.fk_KLIENTASasmens_kodas=kli.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON sut.fk_GYVUNASkodas=gyv.kodas
				ORDER BY sut.kodas ASC";

			var dt = Sql.Query(query);

			foreach( DataRow item in dt )
			{
				pardavimo_sutartys.Add(new SutartisListVM
				{
					ID = Convert.ToString(item["kodas"]),
					Data = Convert.ToDateTime(item["data"]),
					Kaina = Convert.ToDouble(item["kaina"]),
					Busena = Convert.ToString(item["name"]),
					FkDarbuotojas = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["dpavarde"]),
					FkKlientas = Convert.ToString(item["kvardas"]) + " " + Convert.ToString(item["kpavarde"]),
					FkGyvunas = Convert.ToString(item["gvardas"])
				});
			}

			return pardavimo_sutartys;
		}

        /*
		public static List<Sutartis> ListForMiestas(string miestas)
		{
			var result = new List<Sutartis>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}pardavimo_sutartys` WHERE fk_MIESTASpavadinimas=?miestas ORDER BY fk_MIESTASpavadinimas ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?miestas", MySqlDbType.VarChar).Value = miestas;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Sutartis
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"])
				});
			}

			return result;
		}

		public static List<Sutartis> ListForLytis(int lytis)
		{
			var result = new List<Sutartis>();

			var query = $@"SELECT * FROM `{Config.TblPrefix}pardavimo_sutartys` WHERE lytis=?lytis ORDER BY lytis ASC";

			var dt =
				Sql.Query(query, args => {
					args.Add("?lytis", MySqlDbType.Int32).Value = lytis;
				});

			foreach( DataRow item in dt )
			{
				result.Add(new Sutartis
				{
					AsmensKodas = Convert.ToString(item["asmens_kodas"]),
					Vardas = Convert.ToString(item["vardas"]),
					Pavarde = Convert.ToString(item["pavarde"]),
					GimimoData = Convert.ToDateTime(item["gimimo_data"]),
					Telefonas = Convert.ToString(item["telefonas"]),
					EPastas = Convert.ToString(item["e_pastas"]),
					Adresas = Convert.ToString(item["adresas"]),
					Lytis = Convert.ToInt32(item["lytis"]),
					FkMiestas = Convert.ToString(item["fk_MIESTASpavadinimas"])
				});
			}

			return result;
		}*/

		public static SutartisEditVM Find(string kodas)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}pardavimo_sutartys` WHERE kodas=?kodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?kodas", MySqlDbType.VarChar).Value = kodas;
				});

			if( dt.Count > 0 )
			{
				var sevm = new SutartisEditVM();

				foreach( DataRow item in dt )
				{
					sevm.Sutartis.ID = Convert.ToString(item["kodas"]);
					sevm.Sutartis.Data = Convert.ToDateTime(item["data"]);
					sevm.Sutartis.Kaina = Convert.ToDouble(item["kaina"]);
					sevm.Sutartis.Busena = Convert.ToInt32(item["busena"]);
					sevm.Sutartis.FkDarbuotojas = Convert.ToString(item["fk_DARBUOTOJASasmens_kodas"]);
					sevm.Sutartis.FkKlientas = Convert.ToString(item["fk_KLIENTASasmens_kodas"]);
					sevm.Sutartis.FkGyvunas = Convert.ToString(item["fk_GYVUNASkodas"]);
				}

				return sevm;
			}

			return null;
		}

		public static SutartisListVM FindForDeletion(string id)
		{
			var s = new SutartisListVM();

			var query =
				$@"SELECT
					sut.kodas,
                    sut.data,
                    sut.kaina,
                    sut.busena,
                    bus.name,
                    dar.asmens_kodas,
                    dar.vardas AS dvardas,
                    dar.pavarde AS dpavarde,
                    kli.asmens_kodas,
                    kli.vardas AS kvardas,
                    kli.pavarde AS kpavarde,
                    gyv.vardas AS gvardas,
                    gyv.kodas AS gyvunas
				FROM
					`{Config.TblPrefix}pardavimo_sutartys` sut
					LEFT JOIN `{Config.TblPrefix}sutarties_busenos` bus ON sut.busena=bus.id_sutarties_busenos
					LEFT JOIN `{Config.TblPrefix}darbuotojai` dar ON sut.fk_DARBUOTOJASasmens_kodas=dar.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}klientai` kli ON sut.fk_KLIENTASasmens_kodas=kli.asmens_kodas
                    LEFT JOIN `{Config.TblPrefix}gyvunai` gyv ON sut.fk_GYVUNASkodas=gyv.kodas
				WHERE sut.kodas=?kodas";

			var dt =
				Sql.Query(query, args => {
					args.Add("?kodas", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
				s.ID = Convert.ToString(item["kodas"]);
                s.Data = Convert.ToDateTime(item["data"]);
                s.Kaina = Convert.ToDouble(item["kaina"]);
                s.Busena = Convert.ToString(item["name"]);
                s.FkDarbuotojas = Convert.ToString(item["dvardas"]) + " " + Convert.ToString(item["dpavarde"]);
                s.FkKlientas = Convert.ToString(item["kvardas"]) + " " + Convert.ToString(item["kpavarde"]);
                s.FkGyvunas = Convert.ToString(item["gvardas"]);
			}

			return s;
		}

		public static void Insert(SutartisEditVM SutartisEVM)
		{
			var query =
				$@"INSERT INTO `{Config.TblPrefix}pardavimo_sutartys`
				(
					kodas,
                    data,
                    kaina,
                    busena,
                    fk_DARBUOTOJASasmens_kodas,
                    fk_KLIENTASasmens_kodas,
                    fk_GYVUNASkodas
				)
				VALUES(
					?kodas,
                    ?data,
                    ?kaina,
                    ?busena,
                    ?darbuotojas,
                    ?klientas,
                    ?gyvunas
				)";

			Sql.Insert(query, args => {
				args.Add("?kodas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.ID;
				args.Add("?data", MySqlDbType.Date).Value = SutartisEVM.Sutartis.Data;
				args.Add("?kaina", MySqlDbType.Double).Value = SutartisEVM.Sutartis.Kaina;
				args.Add("?busena", MySqlDbType.Int32).Value = SutartisEVM.Sutartis.Busena;
				args.Add("?darbuotojas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.FkDarbuotojas;
				args.Add("?klientas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.FkKlientas;
				args.Add("?gyvunas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.FkGyvunas;
			});
		}

		public static void Update(SutartisEditVM SutartisEVM)
		{
			var query =
				$@"UPDATE `{Config.TblPrefix}pardavimo_sutartys`
				SET
					kodas=?kodas,
                    data=?data,
                    kaina=?kaina,
                    busena=?busena,
                    fk_DARBUOTOJASasmens_kodas=?darbuotojas,
                    fk_KLIENTASasmens_kodas=?klientas,
                    fk_GYVUNASkodas=?gyvunas
				WHERE
					kodas=?kodas";

			Sql.Update(query, args => {
				args.Add("?kodas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.ID;
				args.Add("?data", MySqlDbType.Date).Value = SutartisEVM.Sutartis.Data;
				args.Add("?kaina", MySqlDbType.Double).Value = SutartisEVM.Sutartis.Kaina;
				args.Add("?busena", MySqlDbType.Int32).Value = SutartisEVM.Sutartis.Busena;
				args.Add("?darbuotojas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.FkDarbuotojas;
				args.Add("?klientas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.FkKlientas;
				args.Add("?gyvunas", MySqlDbType.VarChar).Value = SutartisEVM.Sutartis.FkGyvunas;
			});
		}

		public static void Delete(string id)
		{
			var query = $@"DELETE FROM `{Config.TblPrefix}pardavimo_sutartys` WHERE kodas=?id";
			Sql.Delete(query, args => {
				args.Add("?id", MySqlDbType.VarChar).Value = id;
			});
		}

        public static bool Exists(string id)
		{
			var query = $@"SELECT * FROM `{Config.TblPrefix}pardavimo_sutartys` WHERE kodas=?id";
			var dt = 
				Sql.Query(query, args => {
					args.Add("?id", MySqlDbType.VarChar).Value = id;
				});

			foreach( DataRow item in dt )
			{
                return true;
			}

			return false;
		}
	}
}