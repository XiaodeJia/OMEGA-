using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOMEGA
{
    abstract class  Employees
    {
        private int emId;
        private string fName;
        private string lName;
        private string sex;
        private string birthday;
        private string uType;
        private string passwd;

        public int employeeId
        {
            get
            {
                return emId;
            }

            set
            {
                emId = value;
            }
        }
        public string firstName
        {
            get
            {
                return fName;
            }

            set
            {
                fName = value;
            }
        }

        public string lastName
        {
            get
            {
                return lName;
            }

            set
            {
                lName = value;
            }
        }
        public string gender
        {
            get
            {
                return sex;
            }

            set
            {
                sex = value ;
            }
        }
        public string dob
        {
            get
            {
                return birthday;
            }

            set
            {
                birthday = value;
            }
        }
        public string jobTitle
        {
            get
            {
                return uType;
            }

            set
            {
                uType = value;
            }
        }
        public string password
        {
            get
            {
                return passwd;
            }

            set
            {
                passwd = value;
            }
        }
        

        //Vehicle
        abstract public void addVehicle();
        abstract public void delVehicle();
        abstract public void updateVehicle();
        abstract public void searchVehicle();


    }
}
