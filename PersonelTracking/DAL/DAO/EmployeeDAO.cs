using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
			try
			{
				db.EMPLOYEEs.InsertOnSubmit(employee); // To insert changes
                db.SubmitChanges(); // To save changes
            }
			catch (Exception ex)
			{

				throw ex;
			}
        }
        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEEs.Where(x => x.UserNo == v).ToList();
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeeList = new List<EmployeeDetailDTO>();
            var list = (from e in db.EMPLOYEEs
                        join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                        join p in db.POSITIONs on e.PositionID equals p.ID
                        select new
                        { 
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.Surname,
                            EmployeeID = e.ID,
                            Password = e.Password,
                            DepartmentName = d.DepartmentName,
                            PositionName = p.PositionName,
                            DepartmentID = e.DepartmentID,
                            PositionID = e.PositionID,
                            IsAdmin = e.isAdmin,
                            Salary = e.Salary,
                            ImagePath = e.ImagePath, // Added by DA
                            BirthDay = e. Birthday,
                            Adress =e.Address,
                        }).OrderBy(x => x.UserNo).ToList(); // .Where(x => x.UserNo == 1)

            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                
                dto.Name = item.Name;
                dto.UserNo = item.UserNo;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmployeeID;
                dto.Password = item.Password;
                dto.DepartmentID = item.DepartmentID;
                dto.DepartmentName = item.DepartmentName;
                dto.PositonID = item.PositionID;
                dto.PositionName = item.PositionName;
                dto.IsAdmin = item.IsAdmin;
                dto.Salary = item.Salary;
                dto.ImagePath = item.ImagePath; // Added by DA
                dto.BirthDay = item.BirthDay;
                dto.Adress = item.Adress;
                employeeList.Add(dto);
            }
            return employeeList;
        }

        public static List<EMPLOYEE> GetEmployees(int userNo, string userPass)
        {
            try
            {
                List<EMPLOYEE> list = db.EMPLOYEEs.Where(x => x.UserNo == userNo && x.Password == userPass).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            try
            {
                EMPLOYEE employee = db.EMPLOYEEs.First(x => x.ID == employeeID);
                employee.Salary = amount;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEEs.First(x => x.ID == employee.ID);
                emp.UserNo = employee.UserNo;
                emp.Name = employee.Name;
                emp.Surname = employee.Surname;
                emp.Password = employee.Password;
                emp.isAdmin = employee.isAdmin;
                emp.Birthday = employee.Birthday;
                emp.Address = employee.Address;
                emp.DepartmentID = employee.DepartmentID;
                emp.PositionID = employee.PositionID;
                emp.Salary = employee.Salary;
                emp.ImagePath = employee.ImagePath;// Added by DA
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            List<EMPLOYEE> list = db.EMPLOYEEs.Where(x => x.PositionID == position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SubmitChanges();
        }

        // When deleting Employee, we have also to delete:
        // all tasks, permissions and salaries for this employee
        public static void DeleteEmployee(int employeeID)
        {
            EMPLOYEE emp = db.EMPLOYEEs.First(x => x.ID == employeeID);
            db.EMPLOYEEs.DeleteOnSubmit(emp);
            db.SubmitChanges();

            /*
            List<TASK> tasks = db.TASKs.Where(x => x.EmployeeID == employeeID).ToList();
            db.TASKs.DeleteAllOnSubmit(tasks);
            db.SubmitChanges();

            List<SALARY> salaries = db.SALARies.Where(x =>x.EmployeeID == employeeID).ToList();
            db.SALARies.DeleteAllOnSubmit(salaries);
            db.SubmitChanges();

            List<PERMISSION> permissions = db.PERMISSIONs.Where(x => x.EmployeeID == employeeID).ToList();
            db.PERMISSIONs.DeleteAllOnSubmit(permissions);
            db.SubmitChanges();
            */

            // We will use Database Trigger (stored procedure) instead of commneted code above
            //Trigger Syntax:
            /* 
               CREATE TRIGGER delete_employee ON EMPLOYEE
               AFTER DELETE AS 
               BEGIN
               declare @id int
               select @id = ID FROM deleted
               delete from TASK where EmployeeID = @id
               delete from SALARY where EmployeeID = @id
               delete from PERMISSION where EmployeeID = @id
               END
            */



        }
    }
}
