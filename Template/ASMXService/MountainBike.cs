// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WebberCross.BdcModelBuilder.ASMXBikeService
{
    [Serializable]
    public class MountainBike
    {
        private int _id = 0;
        private string _manufacturer = "";
        private string _model = "";
        private string _forks = "";
        private string _rearShock = "";
        private string _brakeSet = "";
        private int _quantityInStock = 0;

        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public string Manufacturer
        {
            get { return this._manufacturer; }
            set { this._manufacturer = value; }
        }

        public string Model
        {
            get { return this._model; }
            set { this._model = value; }
        }

        public string Forks
        {
            get { return this._forks; }
            set { this._forks = value; }
        }

        public string RearShock
        {
            get { return this._rearShock; }
            set { this._rearShock = value; }
        }

        public string BrakeSet
        {
            get { return this._brakeSet; }
            set { this._brakeSet = value; }
        }

        public int QuantityInStock
        {
            get { return this._quantityInStock; }
            set { this._quantityInStock = value; }
        }

        public MountainBike()
        {

        }
    }
}