using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebberCross.BdcModelBuilder.ASMXBikeService
{
    /// <summary>
    /// Summary description for BikeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BikeService : System.Web.Services.WebService
    {
        private Dictionary<int, MountainBike> Bikes
        {
            get { return Application["Bikes"] as Dictionary<int, MountainBike>; }
        }

        [WebMethod]
        public List<MountainBike> GetMountainBikes()
        {
            return Bikes.Values.ToList<MountainBike>();
        }

        [WebMethod]
        public MountainBike GetMountainBike(int id)
        {
            if (!Bikes.ContainsKey(id)) return null;

            return Bikes[id];
        }

        [WebMethod]
        public MountainBike AddMountainBike(MountainBike bike)
        {
            // If ID is 0 allocate an ID
            if (bike.ID == 0)
            {
                int id = Bikes.Keys.Max() + 1;
                bike.ID = id;

                Bikes.Add(id, bike);
            }
            else if (Bikes.ContainsKey(bike.ID))
            {
                Bikes[bike.ID] = bike;
            }
            else
            {
                Bikes.Add(bike.ID, bike);
            }

            return bike;
        }

        [WebMethod]
        public void DeleteMountainBike(int id)
        {
            if (Bikes.ContainsKey(id))
                Bikes.Remove(id);
        }

        [WebMethod]
        public BusinessDataEntity GetMountainBikesComplex()
        {
            BusinessDataEntity bde = new BusinessDataEntity();
            bde.Bikes = this.Bikes.Values.ToList<MountainBike>();
            return bde;
        }

        [WebMethod]
        public BusinessDataEntity GetMountainBikeComplex(BusinessDataEntity bde)
        {
            if (!Bikes.ContainsKey(bde.ID))
            {
                bde.Bike = null;
            }
            else
            {
                bde.Bike = Bikes[bde.ID];
            }

            return bde;
        }

        [WebMethod]
        public BusinessDataEntity AddMountainBikeComplex(BusinessDataEntity bde)
        {
            // If ID is 0 allocate an ID
            if (bde.Bike.ID == 0)
            {
                int id = Bikes.Keys.Max() + 1;
                bde.Bike.ID = id;

                Bikes.Add(id, bde.Bike);
            }
            else if (Bikes.ContainsKey(bde.Bike.ID))
            {
                Bikes[bde.Bike.ID] = bde.Bike;
            }
            else
            {
                Bikes.Add(bde.Bike.ID, bde.Bike);
            }

            return bde;
        }

        [WebMethod]
        public void DeleteMountainBikeComplex(BusinessDataEntity bde)
        {
            if (Bikes.ContainsKey(bde.ID))
                Bikes.Remove(bde.ID);
        }
    }
}
