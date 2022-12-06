using System.Data;
using MySql.Data.MySqlClient;

using StoresReport = Org.Ktu.Isk.P175B602.Autonuoma.ViewModels.StoresReport;

namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
	/// <summary>
	/// Database operations related to reports.
	/// </summary>
	#nullable enable
	public class AtaskaitaRepo
	{
		public static Dictionary<string, List<StoresReport.Ataskaita.Parduotuve>> GetStores(DateTime? dateFrom, DateTime? dateTo, int? rusis, double? kainaFrom, double? kainaTo)
		{
			var result = new Dictionary<string, List<StoresReport.Ataskaita.Parduotuve>>();

			var query =
				$@"SELECT 
				gyvunai.kodas AS gyvuno_id, 
				gyvunai.vardas AS gyvuno_vardas, 
				gyvunai.gimimo_data AS gyvuno_gimimo_data, 
				gyvunai.svoris, 
				gyvunai.lytis AS gyvuno_lytis, 
				lytys.name AS gyvuno_lytis_s, 
				gyvunai.rusis AS gyvuno_rusis, 
				gyvunu_rusys.name AS gyvuno_rusis_s, 
				gyvunai.dydis AS gyvuno_dydis, 
				gyvunu_dydziai.name AS gyvuno_dydis_s, 
				gyvunai.fk_PARDUOTUVEkodas AS parduotuve, 
				parduotuves.pavadinimas AS parduotuve_s, 
				gyvunai.kaina AS gyvuno_kaina, 
				pardavimo_sutartys.kodas AS sutartis, 
				klientai.asmens_kodas AS klientas, 
				klientai.vardas AS kliento_vardas, 
				klientai.pavarde AS kliento_pavarde, 
				ROUND(IFNULL(vakc.kaina_vakcinaciju, 0), 2) AS vakcinaciju_kaina, 	
				IFNULL(vakc.kiekis_vakcinaciju, 0) AS vakcinaciju_kiekis,
				ROUND(IFNULL(vakc2.kaina_vakcinaciju, 0), 2) AS bendra_vakcinaciju_kaina,
				IFNULL(vakc2.kiekis_vakcinaciju, 0) AS bendras_vakcinaciju_kiekis,
				ROUND(IFNULL(vaist.kaina_vaistu, 0), 2) AS vaistu_kaina,
				IFNULL(vaist.kiekis_vaistu, 0) AS vaistu_kiekis,
				ROUND(IFNULL(vaist2.kaina_vaistu, 0), 2) AS bendra_vaistu_kaina,
				IFNULL(vaist2.kiekis_vaistu, 0) AS bendras_vaistu_kiekis,
				DATEDIFF(CURDATE(), gyvunai.gimimo_data) as dienos
			FROM 
				gyvunai 
				LEFT JOIN 
					pardavimo_sutartys 
				ON 
					gyvunai.kodas = pardavimo_sutartys.fk_GYVUNASkodas
				INNER JOIN 
					parduotuves 
				ON 
					gyvunai.fk_PARDUOTUVEkodas = parduotuves.kodas
				LEFT JOIN 
					klientai 
				ON 
					klientai.asmens_kodas = pardavimo_sutartys.fk_KLIENTASasmens_kodas
				INNER JOIN 
					lytys 
				ON 
					gyvunai.lytis = lytys.id_lytys
				INNER JOIN 
					gyvunu_rusys 
				ON 
					gyvunai.rusis = gyvunu_rusys.id_gyvunu_rusys
				INNER JOIN 
					gyvunu_dydziai 
				ON 
					gyvunai.dydis = gyvunu_dydziai.id_gyvunu_dydziai
				LEFT JOIN
				(
					SELECT
						gyvunai1.kodas as gyv_kodas,
						gyvunai1.rusis,
						gyvunai1.kaina,
						gyvunai1.gimimo_data,
						SUM(vakcinacijos.kaina) as kaina_vakcinaciju,
						COUNT(*) as kiekis_vakcinaciju
					FROM
						`gyvunai` gyvunai1, `vakcinacijos`
					WHERE 
						gyvunai1.kodas = vakcinacijos.fk_GYVUNASkodas
						AND 
						gyvunai1.rusis=IFNULL(?rusis, gyvunai1.rusis)
						AND
						gyvunai1.kaina>=IFNULL(?kainaFrom, gyvunai1.kaina)
						AND
						gyvunai1.kaina<=IFNULL(?kainaTo, gyvunai1.kaina)
						AND
						gyvunai1.gimimo_data>=IFNULL(?dateFrom, gyvunai1.gimimo_data)
						AND
						gyvunai1.gimimo_data<=IFNULL(?dateTo, gyvunai1.gimimo_data)
					GROUP BY gyvunai1.kodas
				) AS vakc
				ON 
					vakc.gyv_kodas = gyvunai.kodas
				LEFT JOIN
				(
					SELECT
						parduotuves.kodas,
						gyvunai1.kodas as gyv_kodas,
						gyvunai1.rusis,
						gyvunai1.kaina,
						gyvunai1.gimimo_data,
						SUM(vakcinacijos.kaina) as kaina_vakcinaciju,
						COUNT(*) as kiekis_vakcinaciju
					FROM
						`gyvunai` gyvunai1, `vakcinacijos`, `parduotuves`
					WHERE 
						gyvunai1.fk_PARDUOTUVEkodas = parduotuves.kodas 
						AND 
						gyvunai1.kodas = vakcinacijos.fk_GYVUNASkodas
						AND 
						gyvunai1.rusis=IFNULL(?rusis, gyvunai1.rusis)
						AND
						gyvunai1.kaina>=IFNULL(?kainaFrom, gyvunai1.kaina)
						AND
						gyvunai1.kaina<=IFNULL(?kainaTo, gyvunai1.kaina)
						AND
						gyvunai1.gimimo_data>=IFNULL(?dateFrom, gyvunai1.gimimo_data)
						AND
						gyvunai1.gimimo_data<=IFNULL(?dateTo, gyvunai1.gimimo_data)
					GROUP BY parduotuves.kodas
				) AS vakc2
				ON 
					vakc2.kodas = gyvunai.fk_PARDUOTUVEkodas
				LEFT JOIN
				(
					SELECT
						gyvunai1.kodas as gyv_kodas,
						gyvunai1.rusis,
						gyvunai1.kaina,
						gyvunai1.gimimo_data,
						SUM(medikacijos.kaina) as kaina_vaistu,
						COUNT(*) as kiekis_vaistu
					FROM
						`gyvunai` gyvunai1, `medikacijos`
					WHERE 
						gyvunai1.kodas = medikacijos.fk_GYVUNASkodas
						AND 
						gyvunai1.rusis=IFNULL(?rusis, gyvunai1.rusis)
						AND
						gyvunai1.kaina>=IFNULL(?kainaFrom, gyvunai1.kaina)
						AND
						gyvunai1.kaina<=IFNULL(?kainaTo, gyvunai1.kaina)
						AND
						gyvunai1.gimimo_data>=IFNULL(?dateFrom, gyvunai1.gimimo_data)
						AND
						gyvunai1.gimimo_data<=IFNULL(?dateTo, gyvunai1.gimimo_data)
					GROUP BY gyvunai1.kodas
				) AS vaist
				ON 
					vaist.gyv_kodas = gyvunai.kodas  
				LEFT JOIN
				(
					SELECT
						parduotuves.kodas,
						gyvunai1.kodas as gyv_kodas,
						gyvunai1.rusis,
						gyvunai1.kaina,
						gyvunai1.gimimo_data,
						SUM(medikacijos.kaina) as kaina_vaistu,
						COUNT(*) as kiekis_vaistu
					FROM
						`gyvunai` gyvunai1, `medikacijos`, `parduotuves`
					WHERE 
						gyvunai1.fk_PARDUOTUVEkodas = parduotuves.kodas 
						AND 
						gyvunai1.kodas = medikacijos.fk_GYVUNASkodas
						AND 
						gyvunai1.rusis=IFNULL(?rusis, gyvunai1.rusis)
						AND
						gyvunai1.kaina>=IFNULL(?kainaFrom, gyvunai1.kaina)
						AND
						gyvunai1.kaina<=IFNULL(?kainaTo, gyvunai1.kaina)
						AND
						gyvunai1.gimimo_data>=IFNULL(?dateFrom, gyvunai1.gimimo_data)
						AND
						gyvunai1.gimimo_data<=IFNULL(?dateTo, gyvunai1.gimimo_data)
					GROUP BY parduotuves.kodas
				) AS vaist2
				ON 
					vaist2.kodas = gyvunai.fk_PARDUOTUVEkodas
			WHERE 
				gyvunai.rusis=IFNULL(?rusis, gyvunai.rusis)
				AND
				gyvunai.kaina>=IFNULL(?kainaFrom, gyvunai.kaina)
				AND
				gyvunai.kaina<=IFNULL(?kainaTo, gyvunai.kaina)
				AND
				gyvunai.gimimo_data>=IFNULL(?dateFrom, gyvunai.gimimo_data)
				AND
				gyvunai.gimimo_data<=IFNULL(?dateTo, gyvunai.gimimo_data)
			ORDER BY parduotuves.kodas ASC, gyvunai.kodas ASC;";

			var dt =
				Sql.Query(query, args => {
					args.Add("?dateFrom", MySqlDbType.DateTime).Value = dateFrom;
					args.Add("?dateTo", MySqlDbType.DateTime).Value = dateTo;
					args.Add("?kainaFrom", MySqlDbType.Double).Value = kainaFrom;
					args.Add("?kainaTo", MySqlDbType.Double).Value = kainaTo;
					args.Add("?rusis", MySqlDbType.Int32).Value = rusis;
				});

			#nullable disable
			foreach( DataRow item in dt )
			{
				string FkParduotuve = Convert.ToString(item["parduotuve"]);
				string FkParduotuveToString = Convert.ToString(item["parduotuve_s"]);
				if(!result.ContainsKey(FkParduotuve + " " + FkParduotuveToString))
				{
					result[FkParduotuve + " " + FkParduotuveToString] = new List<StoresReport.Ataskaita.Parduotuve>();
				}
				result[FkParduotuve + " " + FkParduotuveToString].Add(new StoresReport.Ataskaita.Parduotuve
				{
					GyvunoID = Convert.ToString(item["gyvuno_id"]),
					GyvunoVardas = Convert.ToString(item["gyvuno_vardas"]),
					GimimoData = Convert.ToDateTime(item["gyvuno_gimimo_data"]),
					Svoris = Convert.ToDouble(item["svoris"]),
					FkLytis = Convert.ToInt32(item["gyvuno_lytis"]),
					FkLytisToString = Convert.ToString(item["gyvuno_lytis_s"]),
					FkRusis = Convert.ToInt32(item["gyvuno_rusis"]),
					FkRusisToString = Convert.ToString(item["gyvuno_rusis_s"]),
					FkDydis = Convert.ToInt32(item["gyvuno_dydis"]),
					FkDydisToString = Convert.ToString(item["gyvuno_dydis_s"]),
					FkParduotuve = Convert.ToString(item["parduotuve"]),
					FkParduotuveToString = Convert.ToString(item["parduotuve_s"]),
					Kaina = Convert.ToDouble(item["gyvuno_kaina"]),
					FkSutartis = Convert.ToString(item["sutartis"]),
					FkKlientas = Convert.ToString(item["klientas"]),
					FkKlientasToString = Convert.ToString(item["kliento_vardas"]) + " " + Convert.ToString(item["kliento_pavarde"]),
					KiekisVaistu = Convert.ToInt32(item["vaistu_kiekis"]),
					KiekisVakcinaciju = Convert.ToInt32(item["vakcinaciju_kiekis"]),
					SumaVaistu = Convert.ToDouble(item["vaistu_kaina"]),
					SumaVakcinaciju = Convert.ToDouble(item["vakcinaciju_kaina"]),
					BendraKiekisVaistu = Convert.ToInt32(item["bendras_vaistu_kiekis"]),
					BendraKiekisVakcinaciju = Convert.ToInt32(item["bendras_vakcinaciju_kiekis"]),
					BendraSumaVaistu = Convert.ToDouble(item["bendra_vaistu_kaina"]),
					BendraSumaVakcinaciju = Convert.ToDouble(item["bendra_vakcinaciju_kaina"]),
					Dienos = Convert.ToInt32(item["dienos"])
				});
			}

			return result;
		}
	}
}