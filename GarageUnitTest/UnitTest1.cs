using Garage_1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GarageUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public Vehicle[] CreateVehicles(int n)
        {
            var test_cars = VehicleHandler.GetTestVehicles();
            Array.Resize(ref test_cars, n);
            return test_cars;
        }

        [TestMethod]
        [DataRow(3, 2)]
        [DataRow(3, 3)]
        [DataRow(3, 4)]
        public void GarageAddVehicle_WithVehicles_ReturnOccupancy(int cap, int len)
        {
            //Arrange
            var garage=GarageHandler.GetGarageCopyForTest(cap);
            var cars = CreateVehicles(len);
            var expected = Math.Min(cars.Length, cap);

            //Act
            foreach (var car in cars) garage.AddVehicle(car);

            //Assert
            Assert.AreEqual(expected, garage.Occupancy);

        }

        //[TestMethod]
        //[DataRow(3, 0)]
        //[DataRow(3, 1)]
        //[DataRow(3, 2)]
        //[DataRow(3, 3)]
        //public void GarageAddVehicle_WithVehicles_ReturnSameObjects(int cap, int len)
        //{
        //    //Arrange
        //    var garage = GarageHandler.SetUpGarage(cap);
        //    var cars = CreateVehicles(len);

        //    //Act
        //    foreach (var car in cars) garage.AddVehicle(car);

        //    //Assert
        //    //Assert.AreEqual(cars, garage.vehicles);
        //    var g_v = (Vehicle[])garage.vehicles.Clone();
        //    Array.Resize(ref g_v, len);
        //    var object1Json = JsonConvert.SerializeObject(cars);
        //    var object2Json = JsonConvert.SerializeObject(g_v);

        //    Assert.AreEqual(object1Json, object2Json);
        //}

        [TestMethod]
        [DataRow(3, 3)]
        [DataRow(3, 4)]
        [DataRow(3, 6)]
        public void GarageAddVehicle_WithVehicles_ReturnIsFull(int cap, int len)
        {
            //Arrange
            var garage = GarageHandler.GetGarageCopyForTest(cap);
            var cars = CreateVehicles(len);

            //Act
            foreach (var car in cars) garage.AddVehicle(car);

            //Assert
            Assert.IsTrue(garage.IsFull);
        }


        [TestMethod]
        [DataRow(-1, 0)]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(Garage_1.List<Vehicle>.MAX_CAPACITY, Garage_1.List<Vehicle>.MAX_CAPACITY)]
        [DataRow(Garage_1.List<Vehicle>.MAX_CAPACITY + 1, Garage_1.List<Vehicle>.MAX_CAPACITY)]
        public void GarageCreateArray_WithCapacity_ReturnsCapacity(int init, int expected)
        {
            //Arrange
            var garage = GarageHandler.GetGarageCopyForTest(init);

            //Act
            var actual = 0;
            if (garage != null) actual=garage.Capacity;

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        [DataRow(3, 2)]
        [DataRow(3, 3)]
        [DataRow(3, 4)]
        public void GarageRemoveVehicle_ReturnOccupancy(int cap, int len)
        {
            //Arrange
            var garage = GarageHandler.GetGarageCopyForTest(cap);
            var vehicles_add = CreateVehicles(cap); //full house
            var vehicles_remove = CreateVehicles(len);
            var expected = Math.Max(cap - len, 0);

            //Act
            foreach (var v in vehicles_add) garage.AddVehicle(v);
            foreach (var v in vehicles_remove) garage.RemoveVehicle(v.RegNr);

            //Assert
            Assert.AreEqual(expected, garage.Occupancy);
        }

        public static int CountUntyped( IEnumerable source) //this
        {
            int count = 0;
            foreach (object obj in source) { count++; }
            return count;
        }


        [TestMethod]
        [DataRow(5, 0)]
        [DataRow(5, 3)]
        [DataRow(5, 5)]
        public void GarageIEnumerable_WithAddedVehicles_ReturnSameNoVehicles(int cap, int len)
        {
            //Arrange
            var garage = GarageHandler.GetGarageCopyForTest(cap);
            var cars = CreateVehicles(len);

            //Act
            foreach (var car in cars) garage.AddVehicle(car);

            //Assert
            //GetEnumerator
            Assert.AreEqual(len, CountUntyped(garage));
        }
    }
}
