// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WebberCross.BdcModelBuilder.WcfBikeService
{
    [DataContract]
    public class BusinessDataEntity
    {
        private int _id = 0;
        private MountainBike _bike = null;
        private List<MountainBike> _bikes = null;

        [DataMember]
        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        [DataMember]
        public MountainBike Bike
        {
            get { return this._bike; }
            set { this._bike = value; }
        }

        [DataMember]
        public List<MountainBike> Bikes
        {
            get { return this._bikes; }
            set { this._bikes = value; }
        }
    }
}