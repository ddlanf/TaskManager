using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataAccess.Data
{
    static public class StoredProcedures
    {
        static public string GetEmployeesByProjectId => "dbo.GetEmployeesByProjectId";
        static public string GetProjects => "dbo.GetProjects";
    }
}
