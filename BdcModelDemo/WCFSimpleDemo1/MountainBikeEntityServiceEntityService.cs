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

namespace WebberCross.BdcModelBuilder.WCFSimpleDemo1
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
				MountainBike[] retObj = _BikeServiceClient.GetMountainBikes();

				// Parse retObj
				foreach(MountainBike retEnt in retObj)
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
		public static MountainBikeEntity ReadItem(System.Int32 param1)
		{
			// Return Item
			MountainBikeEntity entity = new MountainBikeEntity();

			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Method parameters
				Int32 _id;

				// Map Id
				_id = param1;

				// Call method
				MountainBike retObj = _BikeServiceClient.GetMountainBike(_id);

				// Parse retObj
				ReflectionUtility.CopyByProperty(retObj, entity);

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
				MountainBike _bike = new MountainBike();

				// Map Entity
				_bike = new MountainBike();
				ReflectionUtility.CopyByProperty(param1, _bike);

				// Call method
				MountainBike retObj = _BikeServiceClient.AddMountainBike(_bike);

				// Parse retObj
				ReflectionUtility.CopyByProperty(retObj, entity);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}

			return entity;
		}
		public static void Delete(System.Int32 param1)
		{
			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

				// Method parameters
				Int32 _id;

				// Map Id
				_id = param1;

				// Call method
				_BikeServiceClient.DeleteMountainBike(_id);
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
				MountainBike _bike = new MountainBike();

				// Map Entity
				_bike = new MountainBike();
				ReflectionUtility.CopyByProperty(param1, _bike);

				// Call method
				_BikeServiceClient.AddMountainBike(_bike);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}
		}
	}
}
