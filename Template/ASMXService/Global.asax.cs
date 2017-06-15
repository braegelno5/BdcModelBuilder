using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WebberCross.BdcModelBuilder.ASMXBikeService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Add some bikes
            Dictionary<int, MountainBike> bikes = new Dictionary<int, MountainBike>();

            bikes.Add(1, new MountainBike()
            {
                ID = 1,
                Manufacturer = "Commencal",
                Model = "Supreme DH",
                Forks = "Marzocchi 888",
                RearShock = "Marzocchi Roco",
                BrakeSet = "Hope Tech V2",
                QuantityInStock = 5
            });

            bikes.Add(2, new MountainBike()
            {
                ID = 2,
                Manufacturer = "Santa Cruz",
                Model = "V10 Carbon",
                Forks = "Fox 40",
                RearShock = "Fox DHX 5",
                BrakeSet = "Formula The One",
                QuantityInStock = 3
            });

            Application["Bikes"] = bikes;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}