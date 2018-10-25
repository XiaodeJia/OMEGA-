using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOMEGA
{
   
    class Manager: Employees
    {
        private int vId;
        private string mk;
        private string md;
        private string mt;
        private string color;
        private string odo;
        private string ds;
        private string cat;
        private string sta;
        
        private Vehicle vec;

   

        Controller ctr = new Controller();

        public int VehicleId
        {
            get
            {
                return vId;
            }

            set
            {
                vId = value;
            }
        }
        public string Maker
        {
            get
            {
                return mk;
            }

            set
            {
                mk = value;
            }
        }
        public string Mode
        {
            get
            {
                return md;
            }

            set
            {
                md = value;
            }
        }
        public string MakeTime
        {
            get
            {
                return mt;
            }

            set
            {
                mt = value;
            }
        }

        public string Colour
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public string Odometer
        {
            get
            {
                return odo;
            }

            set
            {
                odo = value;
            }
        }

        public string Discription
        {
            get
            {
                return ds;
            }

            set
            {
                ds = value;
            }
        }

        public string Status
        {
            get
            {
                return sta;
            }

            set
            {
                sta = value;
            }
        }

        public string Categery
        {
            get
            {
                return cat;
            }

            set
            {
                cat = value;
            }
        }
        public Vehicle Vehicle
        {
            get
            {
                return vec;
            }

            set
            {
                vec = value;
            }
        }

        //Vehicle
        public override void addVehicle()
        {
           vId = ctr.addVehicle(mk, md,  mt, color, odo, ds, cat, sta);
        }
        public override void delVehicle()
        {
            ctr.deleteVehicle(vId);
        }
        public override void updateVehicle()
        {
            ctr.updateVehicle(vId, mk, md, mt, color, odo, ds,cat, sta);
        }

        public override void searchVehicle()
        {
            vec = ctr.getVehiclebyId(vId);
        }


    }
}
