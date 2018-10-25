using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Globalization;

namespace NOMEGA
{
    class Controller
    {
        //create db
        OMEGADBDataContext db = new OMEGADBDataContext();

        //check ID/Name/phone/password/
        public bool idValidator(string text)
        {
            return Regex.Match(text, "^[\\d]{1,6}$").Success;
        }
        public bool dateValidator(string text)
        {
            DateTime parseDate;
            if (!DateTime.TryParseExact(text, "MM/dd/yyyy", null, DateTimeStyles.None, out parseDate))
            {
                return false;
            }
            return true;
        }

        public bool nameValidator(string text)
        {
            return Regex.Match(text, "^[A-Z]{1}[a-z]{1,19}$").Success;
        }
        public bool phoneValidator(string text)
        {
            return Regex.Match(text, "^[\\d]{8,15}$").Success;
        }
        public bool passwordValidator(string text)
        {
            return Regex.Match(text, "^[\\dA-Za-z(!@#$%&)]{6,22}$").Success;
        }
        public bool emailValidator(string text)
        {
            return Regex.Match(text, "^[\\dA-Za-z(!#$%&)]{1,32}[@]{1}[dA-Za-z]{1,32}[.]{1}[a-z]{2,5}$").Success;
        }
        public bool confirmPasswordValidator(string text1, string text2)
        {
            return text1.Equals(text2);
        }
        public int dateCalculator(DateTime date)
        {
            return (date - DateTime.Now.Date).Days;
        }
        public string GetMd5Hash(string input)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));


            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public bool checkUser(int uid, string password, string type)
        {
            string passwordMD5 = GetMd5Hash(password);
            var user = from u in db.Employees
                       where u.employeeId == uid && u.password == passwordMD5
                       && u.jobTitle == type
                       select u;
            if (user.Any())
            {
                return true;
            }
                
            return false;
        }
        //add/upudate/del/search employees
        public Employee getUser(int uid)
        {
            return db.Employees.First(u => u.employeeId == uid);
        }
        public int addUser(string fname, string lname, string sex, string birthday, string type, string password)
        {
            string passwordMD5 = GetMd5Hash(password);

            Employee user = new Employee
            {
                firstName = fname,
                lastName = lname,
                gender = sex,
                dob = birthday,
                jobTitle = type,
                password = passwordMD5
            };
            db.Employees.InsertOnSubmit(user);
            db.SubmitChanges();
            return user.employeeId;
        }

        public void updateUser(int uid, string fname, string lname, string sex, string birthday, string type, string password)
        {
            string passwordMD5 = GetMd5Hash(password);
            Employee user = getUser(uid);
            user.firstName = fname;
            user.lastName = lname;
            user.gender = sex;
            user.dob = birthday;
            user.jobTitle = type;
            user.password = password;
            db.SubmitChanges();
        }
    
        public void deleteUser(int uid)
        {
            db.Employees.DeleteOnSubmit(getUser(uid));
            db.SubmitChanges();
            //deleteSalaryByUserID(uid);
            //deleteTimeSheetByUserID(uid);
            //deleteEmployeeWorkTypeByUserID(uid);
            
        }
        //get/add/update/del salary
        public Salary getSalary(int sid)
        {
            return db.Salaries.First(u => u.salaryId == sid);
        }
        public object getSalaryByUserId(int uId)
        {
            var salList = from sal in db.Salaries where sal.employeeId == uId select sal;

            return salList.Distinct();
        }
        public int addSalary(string emId, string tsId, string ws, string os, string bs, string tal, string wt)
        {
            Salary sal = new Salary
            {
                employeeId = Convert.ToInt32(emId.Trim()),
                timeSheetId = Convert.ToInt32(tsId.Trim()),
                workSalary = ws,
                overtimeSalary = os,
                bonus = bs,
                total = tal,
                salaryDate = wt
            };
            db.Salaries.InsertOnSubmit(sal);
            db.SubmitChanges();
            return sal.salaryId;
        }

        public void updateSalary(int sId, string emId, string tsId,string ws,string os, string bs, string tal, string wt)
        {
            Salary sal = getSalary(sId);
            sal.employeeId = Convert.ToInt32(emId.Trim());
            sal.timeSheetId = Convert.ToInt32(tsId.Trim());
            sal.workSalary = ws;
            sal.overtimeSalary = os;
            sal.bonus = bs;
            sal.total = tal;
            sal.salaryDate = wt;
            db.SubmitChanges();
        }
        public void updateSalaryForTimesheet(int sId, string emId, string tsId)
        {
            Salary sal = getSalary(sId);
            sal.employeeId = Convert.ToInt32(emId.Trim());
            sal.timeSheetId = Convert.ToInt32(tsId.Trim());
        
            db.SubmitChanges();
        }
        public void deleteSalaryByUserID(int uId)
        {
            foreach(var sal in db.Salaries)
            {
                if (sal.employeeId == uId)
                    db.Salaries.DeleteOnSubmit(sal);
            }
            db.SubmitChanges();
        }
        public void deleteSalaryBySalaryID(int sId)
        {
            db.Salaries.DeleteOnSubmit(getSalary(sId));
            db.SubmitChanges();
        }

        //get/add/update/del timesheet
        public TimeSheet getTimeSheet(int tid)
        {
            return db.TimeSheets.First(u => u.timeSheetId == tid);
        }
        public object getTimeSheetByUserId(int uid)
        {
            var tsu = from ts in db.TimeSheets where ts.employeeId == uid select ts;
            return tsu.Distinct();
        }
        public void delTimeSheetByUserId(int uId)
        {
            foreach (var t in db.TimeSheets)
            {
                if (t.employeeId == uId)
	            {
		            db.TimeSheets.DeleteOnSubmit(t);
	            }
            }
            db.SubmitChanges();
        }
        public int addTimeSheet(string emId, string ws, string os)
        {
            TimeSheet ts = new TimeSheet
            {
                employeeId = Convert.ToInt32(emId.Trim()),
                workingHours = ws,
                overTimeHours = os,  
            };
            db.TimeSheets.InsertOnSubmit(ts);
            db.SubmitChanges();
            return ts.timeSheetId;
        }

        public void updateTimeSheet(int tId, string emId, string ws, string os)
        {
            TimeSheet ts = getTimeSheet(tId);
            ts.employeeId = Convert.ToInt32(emId.Trim());
            ts.workingHours = ws;
            ts.overTimeHours = os;
            db.SubmitChanges();
        }
        public void deleteTimeSheetByUserID(int td)
        {
            db.TimeSheets.DeleteOnSubmit(getTimeSheet(td));
            db.SubmitChanges();
        }

        //get/add/update/del Booking list
        public Booking getBooking(int bId)
        {
            return db.Bookings.First(u => u.bookingId == bId);
        }
        public object getBookListByCustomeId(int cusId)
        {
            var bookList = from bk in db.Bookings where bk.customerId == cusId select bk;
            return bookList.Distinct();
        }
        public int addBooking(string vId, string emId, string cusId, string bTime, string rtime, string sta)
        {
            Booking bk = new Booking
            {
                vehicleId = Convert.ToInt32(vId.Trim()),
                employeeId = Convert.ToInt32(emId.Trim()),
                customerId = Convert.ToInt32(cusId.Trim()),
                bookTime = bTime,
                rentTime = rtime,
                status = sta
            };
            db.Bookings.InsertOnSubmit(bk);
            db.SubmitChanges();
            return bk.bookingId;
        }

        public void updateBooking(int bId, string vId, string emId, string cusId, string bTime, string rtime, string sta)
        {
            Booking ws = getBooking(bId);
            ws.vehicleId = Convert.ToInt32(vId.Trim());
            ws.employeeId = Convert.ToInt32(emId.Trim());
            ws.customerId = Convert.ToInt32(cusId.Trim());
            ws.bookTime = bTime;
            ws.rentTime = rtime;
            ws.status = sta;
            db.SubmitChanges();
        }
        public void closeBooking(int bId)
        {
            Booking ws = getBooking(bId);
            ws.status = "Close";
            db.SubmitChanges();
        }
        public void deleteBooking(int bId)
        {
            db.Bookings.DeleteOnSubmit(getBooking(bId));
            db.SubmitChanges();
        }


        //get/add/update/del category
        public Category getCategory(int cId)
        {
            return db.Categories.First(u => u.categoryId == cId);
        }

        public Category getCategorybyType(string type)
        {
            return db.Categories.First(u => u.categoryType == type);
        }
        public int addCategory(int cateId, string cType, string rate)
        {
            Category ct = new Category
            {
                categoryId = cateId,
                categoryType = cType,
                rentalRate = rate
            };
            db.Categories.InsertOnSubmit(ct);
            db.SubmitChanges();
            return ct.categoryId;
        }

        public void updateCategory(int cId, string cType, string rate)
        {
            Category ct = getCategory(cId);
            ct.categoryType = cType;
            ct.rentalRate = rate;
            db.SubmitChanges();
        }
        public void deleteCategory(int cId)
        {
            db.Categories.DeleteOnSubmit(getCategory(cId));
            db.SubmitChanges();
        }

        //get/add/update/del customer
        public Customer getCustomer(int cId)
        {
            return db.Customers.First(u => u.customerId == cId);
        }
        public int addCustomer(string fname, string lname, string birth, string mb, string eM, string addr, string sta)
        {
            Customer ct = new Customer
            {
                firstName  = fname,
                lastName = lname,
                dob = birth,
                mobile = mb,
                email = eM,
                address = addr,
                status = sta 
            };
            db.Customers.InsertOnSubmit(ct);
            db.SubmitChanges();
            return ct.customerId;
        }

        public void updateCustomer(int cId, string fname, string lname, string birth, string mb, string eM, string addr, string sta)
        {
            Customer ct = getCustomer(cId);
            ct.firstName = fname;
            ct.lastName = lname;
            ct.dob = birth;
            ct.mobile = mb;
            ct.email = eM;
            ct.address = addr;
            ct.status = sta;
            db.SubmitChanges();
        }
        public void deleteCustomer(int cId)
        {
            db.Customers.DeleteOnSubmit(getCustomer(cId));
            db.SubmitChanges();
        }
        //get/add/update/del expenditure
        public object getExpenditureByCustomerId(int cusId)
        {
            var expList = from rent in db.Rentals
                          join exp in db.Expenditures on rent.expenditureId equals exp.expenditureId
                          where rent.customerId == cusId
                          select exp;
            return expList.Distinct();
        }
        public Expenditure getExpenditure(int exId)
        {
            return db.Expenditures.First(u => u.expenditureId == exId);
        }
        public int addExpenditure(string vId, string fcost, string pcost, string toffence, string dep, string rt, string tal, string sta, string staDate, string endDate)
        {
            Expenditure ex = new Expenditure
            {
                vehicleId = Convert.ToInt32(vId.Trim()),
                fuelCost = Convert.ToInt32(fcost.Trim()),
                parkOffence = Convert.ToInt32(pcost.Trim()),
                trafficeOffence = Convert.ToInt32(toffence.Trim()),
                deposit = Convert.ToInt32(dep.Trim()),
                rental = Convert.ToInt32(rt.Trim()),
                total = Convert.ToInt32(tal.Trim()),
                status = sta,
                StartDate = staDate,
                EndDate = endDate
            };
            db.Expenditures.InsertOnSubmit(ex);
            db.SubmitChanges();
            return ex.expenditureId;
        }

        public void updateExpenditure(int exId,string vId, string fcost, string pcost, string toffence, string dep, string rt, string tal, string sta, string staDate, string endDate)
        {
            Expenditure ex = getExpenditure(exId);
            ex.vehicleId = Convert.ToInt32(vId.Trim());
            ex.fuelCost = Convert.ToInt32(fcost.Trim());
            ex.parkOffence = Convert.ToInt32(pcost.Trim());
            ex.trafficeOffence = Convert.ToInt32(toffence.Trim());
            ex.deposit = Convert.ToInt32(dep.Trim());
            ex.rental = Convert.ToInt32(rt.Trim());
            ex.total = Convert.ToInt32(tal.Trim());
            ex.status = sta;
            ex.StartDate = staDate;
            ex.EndDate = endDate;
            db.SubmitChanges();
        }
        public void deleteExpenditure(int exId)
        {
            db.Expenditures.DeleteOnSubmit(getExpenditure(exId));
            db.SubmitChanges();
        }

        //get/add/update/del Rental
        public Rental getRental(int rId)
        {
            return db.Rentals.First(u => u.RentalId == rId);
        }
        public object getRentalByCustomerID(int cusId)
        {
            var customerList = from cus in db.Rentals where cus.customerId == cusId select cus;

            return customerList.Distinct();
        }
        public int addRental(string emId, string cId, string vId, string exId, string bId, string sta)
        {
            Rental ex = new Rental
            {
                employeeId = Convert.ToInt32(emId.Trim()),
                customerId = Convert.ToInt32(cId.Trim()),
                vehicleId = Convert.ToInt32(vId.Trim()),
                expenditureId = Convert.ToInt32(exId.Trim()),
                bookingId = Convert.ToInt32(bId.Trim()),
                status = sta
            };
            db.Rentals.InsertOnSubmit(ex);
            db.SubmitChanges();
            return ex.RentalId;
        }

        public void updateRental(int rId, string emId, string cId, string vId, string exId, string bId, string sta)
        {
            Rental ex = getRental(rId);
            ex.employeeId = Convert.ToInt32(emId.Trim());
            ex.customerId = Convert.ToInt32(cId.Trim());
            ex.vehicleId = Convert.ToInt32(vId.Trim());
            ex.expenditureId = Convert.ToInt32(exId.Trim());
            ex.bookingId = Convert.ToInt32(bId.Trim());
            ex.status = sta;
            db.SubmitChanges();
        }
        public void deleteRental(int exId)
        {
            db.Rentals.DeleteOnSubmit(getRental(exId));
            db.SubmitChanges();
        }

        //get/add/update/del Vehicle
        public Vehicle getVehiclebyId(int vId)
        {
            return db.Vehicles.First(u => u.vehicleId == vId);
        }
        public object getVehiclebyMaker(string mk)
        {
            var vehilelist = from b in db.Vehicles
                             where SqlMethods.Like(b.maker, "%" + mk + "%")
                             select b;
            return vehilelist.Distinct();
        }

        public object getVehiclebyMode(string md)
        {
            var vehilelist = from b in db.Vehicles
                             where SqlMethods.Like(b.mode, "%" + md + "%")
                             select b;
            return vehilelist.Distinct();
        }
        public int addVehicle(string mk, string md, string mt, string color, string odo, string ds, string cat, string sta)
        {
            Vehicle ex = new Vehicle
            {
                maker = mk,
                mode = md,
                makeTime = mt,
                color = color,
                odometer = odo,
                description= ds,
                categoryType = cat,
                status = sta
            };
            db.Vehicles.InsertOnSubmit(ex);
            db.SubmitChanges();
            return ex.vehicleId;
        }

        public void updateVehicle(int vId, string mk, string md, string mt, string color, string odo, string ds, string cat, string sta)
        {
            Vehicle ex = getVehiclebyId(vId);
                    ex.maker = mk;
                    ex.mode = md;
                    ex.makeTime = mt;
                    ex.color = color;
                    ex.odometer = odo;
                    ex.description= ds;
                    ex.categoryType = cat;
                    ex.status = sta;
                    db.SubmitChanges();
        }
        public void deleteVehicle(int vId)
        {
            db.Vehicles.DeleteOnSubmit(getVehiclebyId(vId));
            db.SubmitChanges();
        }

        public void updateVehicleByStaff(int vId,  string odo,  string sta)
        {
            Vehicle ex = getVehiclebyId(vId);
            ex.odometer = odo;
            ex.status = sta;
            db.SubmitChanges();
        }


    }
}
