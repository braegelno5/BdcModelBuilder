// Webber-Cross SharePoint Entity Service
// Automatically built by WebberCross.BDCModel

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WebberCross.BdcModelBuilder.Common;
using WebberCross.BdcModelBuilder.BdcModelTemplate;
using WebberCross.BdcModelBuilder.ServiceShim.WCFBikeServiceReference;

namespace WebberCross.BdcModelBuilder.TemplateModel1
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
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/TemplateWcfBikeService/BikeService.svc"));

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
		public static MountainBikeEntity ReadItem(System.Int32 param1)
		{
			// Return Item
			MountainBikeEntity entity = new MountainBikeEntity();

			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/TemplateWcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();
				Int32 _ID = new Int32();
				_bde.ID = _ID;

				// Map Id
				_bde.ID = param1;

				// Call method
				BusinessDataEntity retObj = _BikeServiceClient.GetMountainBikeComplex(_bde);

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
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/TemplateWcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();
				MountainBike _Bike = new MountainBike();
				_bde.Bike = _Bike;

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
		public static void Delete(System.Int32 param1)
		{
			try
			{
				// Instances
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/TemplateWcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();
				Int32 _ID = new Int32();
				_bde.ID = _ID;

				// Map Id
				_bde.ID = param1;

				// Call method
				_BikeServiceClient.DeleteMountainBikeComplex(_bde);
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
				BikeServiceClient _BikeServiceClient = new BikeServiceClient(new BasicHttpBinding(),new EndpointAddress("http://localhost:8080/TemplateWcfBikeService/BikeService.svc"));

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();
				MountainBike _Bike = new MountainBike();
				_bde.Bike = _Bike;

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
