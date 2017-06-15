// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebberCross.BdcModelBuilder.Tests.WCFBikeServiceReference;
using System.ServiceModel;

namespace WebberCross.BdcModelBuilder.Tests
{
    [TestClass]
    public class WcfServiceTest
    {
        [TestMethod]
        public void TestBasicHttp()
        {
            try
            {
                BikeServiceClient client = new BikeServiceClient(
                    new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

                MountainBike[] bikes = client.GetMountainBikes();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestBasicHttpAdd()
        {
            BikeServiceClient client = new BikeServiceClient(
                new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

            MountainBike bike = client.AddMountainBike(new MountainBike()
            {
                Manufacturer = "Specialized",
                Model = "Demo 8",
                Forks = "Rockshox Boxxer",
                RearShock = "Fox DHX4",
                BrakeSet = "Shimano Saint",
                QuantityInStock = 1
            });

            // Check ID is set
            Assert.AreNotEqual(0, bike.ID);

            // Check singleton is working
            MountainBike[] bikes = client.GetMountainBikes();
            Assert.AreNotEqual(2, bikes.Length);

            BusinessDataEntity bde = client.GetMountainBikeComplexComposite(new BusinessDataEntity()
                {
                    ManufacturerID = "Specialized",
                    ModelID = "Demo 8"
                });

            Assert.AreEqual(bde.Bike.Manufacturer, "Specialized");

        }

        [TestMethod]
        public void TestBasicHttpDelete()
        {
            BikeServiceClient client = new BikeServiceClient(
                new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc"));

            // Add bike
            MountainBike bike = client.AddMountainBike(new MountainBike()
            {
                Manufacturer = "Specialized",
                Model = "Demo 8",
                Forks = "Rockshox Boxxer",
                RearShock = "Fox DHX4",
                BrakeSet = "Shimano Saint",
                QuantityInStock = 1
            });

            // Check ID is set
            Assert.AreNotEqual(0, bike.ID);

            // Delete bike
            client.DeleteMountainBike(bike.ID);

            // Check bike has been removed
            MountainBike[] bikes = client.GetMountainBikes();
            Assert.IsFalse(bikes.Contains(bike));
        }

        [TestMethod]
        public void TestWSHttp()
        {
            try
            {
                BikeServiceClient client = new BikeServiceClient(
                    new WSHttpBinding(), new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc/ws"));

                MountainBike[] bikes = client.GetMountainBikes();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestWSHttpAdd()
        {
            BikeServiceClient client = new BikeServiceClient(
                new WSHttpBinding(), new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc/ws"));

            MountainBike bike = client.AddMountainBike(new MountainBike()
                {
                    Manufacturer = "Specialized",
                    Model = "Demo 8",
                    Forks = "Rockshox Boxxer",
                    RearShock = "Fox DHX4",
                    BrakeSet = "Shimano Saint",
                    QuantityInStock = 1
                });

            // Check ID is set
            Assert.AreNotEqual(0, bike.ID);

            // Check singleton is working
            MountainBike[] bikes = client.GetMountainBikes();
            Assert.AreNotEqual(2, bikes.Length);
        }

        [TestMethod]
        public void TestWSHttpDelete()
        {
            BikeServiceClient client = new BikeServiceClient(
                new WSHttpBinding(), new EndpointAddress("http://localhost:8080/WcfBikeService/BikeService.svc/ws"));

            // Add bike
            MountainBike bike = client.AddMountainBike(new MountainBike()
            {
                Manufacturer = "Specialized",
                Model = "Demo 8",
                Forks = "Rockshox Boxxer",
                RearShock = "Fox DHX4",
                BrakeSet = "Shimano Saint",
                QuantityInStock = 1
            });

            // Check ID is set
            Assert.AreNotEqual(0, bike.ID);

            // Delete bike
            client.DeleteMountainBike(bike.ID);

            // Check bike has been removed
            MountainBike[] bikes = client.GetMountainBikes();
            Assert.IsFalse(bikes.Contains(bike));
        }
    }
}
