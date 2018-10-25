using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOMEGA
{
    class staff:Employees
    {
        Controller ctr = new Controller();
        //vechile
        private int vId;
        private string odo ;    
        private string sta; 
        private Vehicle vec;
        //vehicle

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

        public override void addVehicle()
        {
            MessageBox.Show("you have no right,only manager has right");
            return;
        }

        public override void delVehicle()
        {
            MessageBox.Show("you have no right,only manager has right");
            return;
        }
        public override void updateVehicle()
        {
            ctr.updateVehicleByStaff(vId,odo,sta);
        }
        public override void searchVehicle()
        {
            vec = ctr.getVehiclebyId(vId);
        }

    }
}
