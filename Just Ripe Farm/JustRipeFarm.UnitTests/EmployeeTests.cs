﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FarmControl;

namespace JustRipeFarm.UnitTests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void AddCropStock_CanFitInStorage_ReturnTrue()
        {
            bool result;
            Employee employee = new Employee();
            Storage storage = new Storage() { UsedCapacity = 1000, MaxCapacity = 2000 };
            short[] amountsToAdd = new short[] { 1000, 100, 523, 0, 000 };

            for (int i = 0; i < amountsToAdd.Length; i++)
            {
               result = employee.AddCropStock(storage, amountsToAdd[i]);
                Assert.IsTrue(result);
            }
        }
        [TestMethod]
        public void AddCropStock_CannotFitInStorage_ReturnFalse()
        {
            bool result;
            Employee employee = new Employee();
            Storage storage = new Storage() { UsedCapacity = 1000, MaxCapacity = 2000 };
            short[] amountsToAdd = new short[] { 1001, 5000, 1523, 10000 };

            for (int i = 0; i < amountsToAdd.Length; i++)
            {
                result = employee.AddCropStock(storage, amountsToAdd[i]);
                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void RemoveCropStock_AmountEnteredIsAvailable_ReturnTrue()
        {
            bool result;
            Employee employee = new Employee();
            Storage storage = new Storage() { UsedCapacity = 1000 };
            short[] amountsToRemove = new short[] { 1000, 200, 426, 0, 000 };

            for (int i = 0; i < amountsToRemove.Length; i++)
            {
                result = employee.RemoveCropStock(storage, amountsToRemove[i]);
                Assert.IsTrue(result);
            }
        }
        [TestMethod]
        public void RemoveCropStock_AmountEnteredIsNotAvailable_ReturnFalse()
        {
            bool result;
            Employee employee = new Employee();
            Storage storage = new Storage() { UsedCapacity = 1000 };
            short[] amountsToRemove = new short[] { 1001, 5000, 1523, 10000 };

            for (int i = 0; i < amountsToRemove.Length; i++)
            {
                result = employee.RemoveCropStock(storage, amountsToRemove[i]);
                Assert.IsFalse(result);
            }
        }
    }
}