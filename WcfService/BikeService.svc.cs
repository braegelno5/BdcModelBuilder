using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebberCross.BdcModelBuilder.WcfBikeService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BikeService : IBikeService
    {
        private Dictionary<int, MountainBike> _bikes = null;

        public BikeService()
        {
            // Add some bikes
            this._bikes = new Dictionary<int, MountainBike>();

            this._bikes.Add(1, new MountainBike()
            {
                ID = 1,
                Manufacturer = "Commencal",
                Model = "Supreme DH",
                Forks = "Marzocchi 888",
                RearShock = "Marzocchi Roco",
                BrakeSet = "Hope Tech V2",
                QuantityInStock = 5
            });

            this._bikes.Add(2, new MountainBike()
            {
                ID = 2,
                Manufacturer = "Santa Cruz",
                Model = "V10 Carbon",
                Forks = "Fox 40",
                RearShock = "Fox DHX 5",
                BrakeSet = "Formula The One",
                QuantityInStock = 3
            });
        }

        #region Simple

        public List<MountainBike> GetMountainBikes()
        {
            return this._bikes.Values.ToList<MountainBike>();
        }

        public MountainBike GetMountainBike(int id)
        {
            if (!_bikes.ContainsKey(id)) return null;

            return _bikes[id];
        }

        public MountainBike AddMountainBike(MountainBike bike)
        {
            // If ID is 0 allocate an ID
            if (bike.ID == 0)
            {
                int id = _bikes.Keys.Max() + 1;
                bike.ID = id;

                _bikes.Add(id, bike);
            }
            else if (_bikes.ContainsKey(bike.ID))
            {
                _bikes[bike.ID] = bike;
            }
            else
            {
                _bikes.Add(bike.ID, bike);
            }

            return bike;
        }

        public void DeleteMountainBike(int id)
        {
            if (_bikes.ContainsKey(id))
                _bikes.Remove(id);
        }

        #endregion

        #region Complex

        public BusinessDataEntity GetMountainBikesComplex()
        {
            BusinessDataEntity bde = new BusinessDataEntity();
            bde.Bikes = this._bikes.Values.ToList<MountainBike>();
            return bde;
        }

        public BusinessDataEntity GetMountainBikeComplex(BusinessDataEntity bde)
        {
            if (!_bikes.ContainsKey(bde.ID))
            {
                bde.Bike = null;
            }
            else
            {
                bde.Bike = _bikes[bde.ID];
            }

            return bde;
        }

        public BusinessDataEntity GetMountainBikeComplexComposite(BusinessDataEntity bde)
        {
            bde.Bike = _bikes.Values.Single(b => b.Manufacturer == bde.ManufacturerID
                && b.Model == bde.ModelID);

            return bde;
        }

        public BusinessDataEntity AddMountainBikeComplex(BusinessDataEntity bde)
        {
            // If ID is 0 allocate an ID
            if (bde.Bike.ID == 0)
            {
                int id = _bikes.Keys.Max() + 1;
                bde.Bike.ID = id;

                _bikes.Add(id, bde.Bike);
            }
            else if (_bikes.ContainsKey(bde.Bike.ID))
            {
                _bikes[bde.Bike.ID] = bde.Bike;
            }
            else
            {
                _bikes.Add(bde.Bike.ID, bde.Bike);
            }

            return bde;
        }

        public void DeleteMountainBikeComplex(BusinessDataEntity bde)
        {
            if (_bikes.ContainsKey(bde.ID))
                _bikes.Remove(bde.ID);
        }

        public void DeleteMountainBikeComplexComposite(BusinessDataEntity bde)
        {
            int id = _bikes.Values.Single(b => b.Manufacturer == bde.ManufacturerID
                && b.Model == bde.ModelID).ID;

            if (_bikes.ContainsKey(id))
                _bikes.Remove(id);
        }

        #endregion
    }
}
