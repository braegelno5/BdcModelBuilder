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
using BdcModelBuilder.ServiceShim.ASMXBikeServiceReference;

namespace WebberCross.BdcModelBuilder.ASMXDemo1
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
				BikeService _BikeService = new BikeService();
				_BikeService.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
				_BikeService.UseDefaultCredentials = true;

				// Call method
				BusinessDataEntity retObj = _BikeService.GetMountainBikesComplex();

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
				BikeService _BikeService = new BikeService();
				_BikeService.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
				_BikeService.UseDefaultCredentials = true;

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Id
				_bde.ID = param1;

				// Call method
				BusinessDataEntity retObj = _BikeService.GetMountainBikeComplex(_bde);

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
				BikeService _BikeService = new BikeService();
				_BikeService.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
				_BikeService.UseDefaultCredentials = true;

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Entity
				_bde.Bike = new MountainBike();
				ReflectionUtility.CopyByProperty(param1, _bde.Bike);

				// Call method
				BusinessDataEntity retObj = _BikeService.AddMountainBikeComplex(_bde);

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
				BikeService _BikeService = new BikeService();
				_BikeService.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
				_BikeService.UseDefaultCredentials = true;

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Id
				_bde.ID = param1;

				// Call method
				_BikeService.DeleteMountainBikeComplex(_bde);
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
				BikeService _BikeService = new BikeService();
				_BikeService.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
				_BikeService.UseDefaultCredentials = true;

				// Method parameters
				BusinessDataEntity _bde = new BusinessDataEntity();

				// Map Entity
				_bde.Bike = new MountainBike();
				ReflectionUtility.CopyByProperty(param1, _bde.Bike);

				// Call method
				_BikeService.AddMountainBikeComplex(_bde);
			}
			catch(Exception ex)
			{
				Utilities utl = new Utilities();
				utl.HandleException(ex);
			}
		}
	}
}
