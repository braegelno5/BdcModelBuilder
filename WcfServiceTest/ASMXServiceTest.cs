// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WebberCross.BdcModelBuilder.Tests.ASMXBikeServiceReference;
using System.ServiceModel;

namespace WebberCross.BdcModelBuilder.Tests
{
    [TestClass]
    public class ServiceTest
    {
        [TestMethod]
        public void TestRead()
        {
            try
            {
                BikeService client = new BikeService();
                client.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
                client.UseDefaultCredentials = true;

                MountainBike[] bikes = client.GetMountainBikes();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            BikeService client = new BikeService();
            client.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
            client.UseDefaultCredentials = true;

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
        public void TestDelete()
        {
            BikeService client = new BikeService();
            client.Url = "http://localhost:8080/ASMXBikeService/BikeService.asmx";
            client.UseDefaultCredentials = true;

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
