// Webber-Cross SharePoint Entity
// Automatically built by WebberCross.BDCModel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebberCross.BdcModelBuilder.WCFCompositeID
{
	[Serializable]
	public class MountainBikeEntity
	{
		private String _BrakeSet;
		private String _Forks;
		private Int32 _ID;
		private String _Manufacturer;
		private String _Model;
		private Int32 _QuantityInStock;
		private String _RearShock;

		public String BrakeSet
		{
			get { return this._BrakeSet; }
			set { this._BrakeSet = value; }
		}
		public String Forks
		{
			get { return this._Forks; }
			set { this._Forks = value; }
		}
		public Int32 ID
		{
			get { return this._ID; }
			set { this._ID = value; }
		}
		public String Manufacturer
		{
			get { return this._Manufacturer; }
			set { this._Manufacturer = value; }
		}
		public String Model
		{
			get { return this._Model; }
			set { this._Model = value; }
		}
		public Int32 QuantityInStock
		{
			get { return this._QuantityInStock; }
			set { this._QuantityInStock = value; }
		}
		public String RearShock
		{
			get { return this._RearShock; }
			set { this._RearShock = value; }
		}


		public MountainBikeEntity()
		{

		}

	}
}
