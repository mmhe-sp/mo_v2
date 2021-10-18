using MMHE.MO.Business.Entities;
using MMHE.MO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Business.Repositories
{
    public class ProjectsRepository
    {
        public List<Option> FindProjects(string project)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Project", project);
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.FindProjects", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table.Rows.Cast<DataRow>().Select(d => new Option
            {
                Value = d.Field<string>("ProjectId"),
                Text = d.Field<string>("ProjectName")
            }).ToList();
        }

        public void UpdateUserProject(string projectId, string userId)
        {
            using (MOContext context = new MOContext())
            {
                //var user = context.tblUsers.Single(d => d.EmployeeName == userName);
                int id = int.Parse(userId);
                var session = context.tblUserSessionProjectInfoes.Where(d => d.EmployeeId == id).OrderByDescending(d => d.ModifiedDate).FirstOrDefault();
                if (session == null)
                {
                    context.tblUserSessionProjectInfoes.Add(new tblUserSessionProjectInfo
                    {
                        CreatedDate = DateTime.Now,
                        EmployeeId = id,
                        ModifiedDate = DateTime.Now,
                        ProjectID = projectId
                    });
                }
                else
                {
                    session.ProjectID = projectId;
                    session.ModifiedDate = DateTime.Now;
                }
                context.SaveChanges();
            }
        }
    }
}
