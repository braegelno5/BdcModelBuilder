// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebberCross.BdcModelBuilder.WcfBikeService
{
    [ServiceContract]
    public interface IBikeService
    {
        // Basic methods

        [OperationContract]
        List<MountainBike> GetMountainBikes();

        [OperationContract]
        MountainBike GetMountainBike(int id);
               
        [OperationContract]
        MountainBike AddMountainBike(MountainBike bike);

        [OperationContract]
        void DeleteMountainBike(int id);

        // Complex nested object methods

        [OperationContract]
        BusinessDataEntity GetMountainBikesComplex();

        [OperationContract]
        BusinessDataEntity GetMountainBikeComplex(BusinessDataEntity bde);

        [OperationContract]
        BusinessDataEntity GetMountainBikeComplexComposite(BusinessDataEntity bde);

        [OperationContract]
        BusinessDataEntity AddMountainBikeComplex(BusinessDataEntity bde);

        [OperationContract]
        void DeleteMountainBikeComplex(BusinessDataEntity bde);

        [OperationContract]
        void DeleteMountainBikeComplexComposite(BusinessDataEntity bde);
    }
}
