using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Model;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Org.Ktu.Isk.P175B602.Autonuoma.ViewModels;


namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories
{
    public class NaudotojoPrivilegijosRepo
    {
        public static List<NaudotojoPrivilegijos> List()
        {
            var naudotojoPriv = new List<NaudotojoPrivilegijos>();

            var query =
                $@"SELECT * FROM naudotojo_privilegijos";

            var dt = Sql.Query(query);

            foreach (DataRow item in dt)
            {
                naudotojoPriv.Add(new NaudotojoPrivilegijos
                {
                    pk_id = Convert.ToInt32(item["id"]),
                    Pavadinimas = Convert.ToString(item["name"])
                });
            }
            return naudotojoPriv;
        }
    }
}
