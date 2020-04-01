using Garage_1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GarageUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public Vehicle[] CreateVehicles(int n)
        {
            var test_cars = new Vehicle[]
             {
                   new Car {RegNr="ABC11",Color="Red",NoWheels=4},
                   new Car {RegNr="ABC222",Color="White",NoWheels=4},
                   new Car {RegNr="ABC333",Color="Grey",NoWheels=4},
                   new Car {RegNr="ABC444",Color="Blue",NoWheels=4},
                   new Boat {RegNr="B-111",Color="Grey",NoWheels=0},
                   new Boat {RegNr="B-222",Color="Blue",NoWheels=0}

             };
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
        [DataRow(-1, 1)]
        [DataRow(0, 1)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(Garage<Vehicle>.MAX_CAPACITY, Garage<Vehicle>.MAX_CAPACITY)]
        [DataRow(Garage<Vehicle>.MAX_CAPACITY + 1, Garage<Vehicle>.MAX_CAPACITY)]
        public void GarageCreateArray_WithCapacity_ReturnsCapacity(int init, int expected)
        {
            //Arrange
            var garage = GarageHandler.GetGarageCopyForTest(init);

            //Act
            var actual = garage.Capacity;

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
            var cars = CreateVehicles(len);
            var expected = Math.Min(cap - len, 0);

            //Act
            foreach (var car in cars) garage.RemoveVehicle(car.RegNr);

            //Assert
            Assert.AreEqual(expected, garage.Occupancy);

        }

    }
}
