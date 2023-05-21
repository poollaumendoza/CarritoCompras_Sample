using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras.BusinessLayer
{
    public class DashBoard
    {
        DataLayer.DashBoard dDashBoard = new DataLayer.DashBoard();

        public EntityLayer.DashBoard VerDashBoard()
        {
            return dDashBoard.VerDashBoard();
        }
    }
}