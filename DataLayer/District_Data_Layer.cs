using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Thiru_Proj.Models;
using System.Data;

namespace Thiru_Proj.DataLayer
{
    public class District_Data_Layer : District
    {
        public string cnn = "";


        public District_Data_Layer()
        {
            cnn = "Data Source=DESKTOP-4JFHAO7\\SQLEXPRESS;Database = Sample;User Id= hr;Password=tiger;";
        }

        public List<District> GetAllDistricts()
        {

            List<District> ListOfDistricts = new List<District>();
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("select * from  district2", cn))
                {
                    cn.Open();
                    SqlDataAdapter reader = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    reader.Fill(ds);
                    ListOfDistricts = (from DataRow dr in ds.Tables[0].Rows
                                       select new District()
                                       {
                                           Id = int.Parse(dr["Id"].ToString()),
                                           Name = (dr["Name"].ToString()),
                                           IsActive = bool.Parse(dr["IsActive"].ToString()),
                                           Insert_Date = DateTime.Parse(dr["Insert_Date"].ToString()),
                                           Update_Date = DateTime.Parse(dr["Update_Date"].ToString())

                                       }
                        ).ToList();
                }
            }

            return ListOfDistricts;
        }
        public District GetDistrictById(int Id)
        {

            List<District> ListOfDistricts = new List<District>();
            ListOfDistricts = GetAllDistricts();
            District selectedDistrict = (from row in ListOfDistricts
                                         where row.Id == Id
                                         select row).FirstOrDefault();
            return selectedDistrict;
        }
        public void AddDistrict()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
           //string Id = this.Id.ToString();
           //string IsActive = this.IsActive.ToString();
            param.Add("@Id", this.Id.ToString());
            param.Add("@Name", this.Name);
            param.Add("@IsActive",this.IsActive.ToString());
            ExecuteSp("sp_AddDistrict", param);
          //  @Id,@Name,@IsActive
        }
        public void UpdateDistrict()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            //string Id = this.Id.ToString();
            //string IsActive = this.IsActive.ToString();
            param.Add("@Id", this.Id.ToString());
            param.Add("@Name", this.Name);
            param.Add("@IsActive", this.IsActive.ToString());
            ExecuteSp("Sp_UpdateDistrict", param);
        }
        public void DeleteDistrict()
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            //string Id = this.Id.ToString();
            //string IsActive = this.IsActive.ToString();
            param.Add("@Id", this.Id.ToString());
            ExecuteSp("Sp_DeleteDistrict", param);
        }


        public void ExecuteSp(string spName, Dictionary<string, string> parameters)

        {
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand(spName, cn))
                {
                    cn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    foreach (KeyValuePair<string, string> k in parameters)
                    {
                        param = new SqlParameter(k.Key, k.Value);
                        cmd.Parameters.Add(param);
                    }
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}