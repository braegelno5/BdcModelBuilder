// Webber-Cross SharePoint Entity Service
// Automatically built by WebberCross.BDCModel

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WebberCross.BdcModelBuilder.Common;
using WebberCross.BdcModelBuilder.DemoBdcProject;
using BdcModelBuilder.ServiceShim.WCFBikeServiceReference;

namespace WebberCross.BdcModelBuilder.WCFCompositeID
{
	[Serializable]
	public class MountainBikeEntityService
	{
		public static IEnumerable<MountainBikeEntity> ReadList()
		{
			// Return Item
			List<MountainBikeEntity> entityList = new List<MountainBikeEntity>();

			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Call method
				BusinessDataEntity retObj = _BikeServiceClient.GetMountainBikesComplex();

				// Parse retObj
				foreach(MountainBike retEnt in retObj.Bikes)
				{
					MountainBikeEntity ent = new MountainBikeEntity();
					ReflectionUtility.CopyByProperty(retEnt, ent);
					entityList.Add(ent);
				}
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}

			return entityList;
		}
		public static MountainBikeEntity ReadItem(System.String param1, System.String param2)
		{
			// Return Item
			MountainBikeEntity entity = new MountainBikeEntity();

			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Id
				_bde.ManufacturerID = param1;

				// Map Id
				_bde.ModelID = param2;

				// Call method
				BusinessDataEntity retObj = _BikeServiceClient.GetMountainBikeComplexComposite(_bde);

				// Parse retObj
				ReflectionUtility.CopyByProperty(retObj.Bike, entity);

				//Fix dates
				ReflectionUtility.FixDatesForSharePoint(entity);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}

			return entity;
		}
		public static MountainBikeEntity Create(MountainBikeEntity param1)
		{

			// Return Item
			MountainBikeEntity entity = new MountainBikeEntity();
			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Entity
				_bde.Bike = new MountainBike();
				ReflectionUtility.CopyByProperty(param1, _bde.Bike);

				// Call method
				BusinessDataEntity retObj = _BikeServiceClient.AddMountainBikeComplex(_bde);

				// Parse retObj
				ReflectionUtility.CopyByProperty(retObj.Bike, entity);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}

			return entity;
		}
		public static void Delete(System.String param1, System.String param2)
		{
			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Id
				_bde.ManufacturerID = param1;

				// Map Id
				_bde.ModelID = param2;

				// Call method
				_BikeServiceClient.DeleteMountainBikeComplexComposite(_bde);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}
		}
		public static void Update(MountainBikeEntity param1)
		{
			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Entity
				_bde.Bike = new MountainBike();
				ReflectionUtility.CopyByProperty(param1, _bde.Bike);

				// Call method
				_BikeServiceClient.AddMountainBikeComplex(_bde);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}
		}
	}
}
