using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        //our access  to the DB is via the Context class

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<EmployeeClients> Employee_GetClientList()
        {
            using (var context = new ChinookContext())
            {

                var results = from employeeRow in context.Employees
                              where employeeRow.Title.Contains("Support")
                              orderby employeeRow.LastName, employeeRow.FirstName
                              select new
                              {
                                  Name = employeeRow.LastName + ", " + employeeRow.FirstName,
                                  //title = employeeRow.Title,
                                  ClientCount = employeeRow.Customers.Count(),
                                  ClientList = from customerRowOfemployeeRow in employeeRow.Customers
                                               orderby customerRowOfemployeeRow.LastName,
                                                        customerRowOfemployeeRow.FirstName
                                               select new ClientInfo
                                               {
                                                   Client = customerRowOfemployeeRow.LastName + ", " + customerRowOfemployeeRow.FirstName,
                                                   Phone = customerRowOfemployeeRow.Phone
                                               }

                              };
                return results.ToList();
            }

        }
    }
}
