using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderSchemaAPI;
using OrderSchemaAPI.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        private ItemValidator itemValidator;
        private OrderValidator orderValidator;
        private PickupAddressValidator pickupAddressValidator;

        [TestInitialize]
        public void TestInitialize()
        {
            itemValidator = new ItemValidator();
            orderValidator = new OrderValidator();
            pickupAddressValidator = new PickupAddressValidator();
        }

        [DataTestMethod]
        [DataRow("AND-90", 4, DisplayName = "Correct item code")]
        [DataRow( "JFH-57", 46, DisplayName = "Correct quantity")]
        public void Validate_Items(string itemCode, int quantity)
        {
            var item = new Item { ItemCode = itemCode, Quantity = quantity };
            var result = itemValidator.TestValidate(item);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [DataTestMethod]
        [DataRow("JFF", 5, DisplayName = "Incorrect item code")]
        [DataRow("87", 46, DisplayName = "Incorrect item code")]
        [DataRow("DJF-89", null , DisplayName = "Empty quatity")]
        [DataRow("KFJ89", null , DisplayName = "Incorrect item code and null quantity")]
        public void Validate_Items_Incorrect(string itemCode, int quantity)
        {
            var item = new Item { ItemCode = itemCode, Quantity = quantity };
            var result = itemValidator.TestValidate(item);

            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Validate_Order()
        {
            var order = new Order { RequestedPickupTime = System.DateTime.Now, PickupInstructions = "pickup at the aiport", DeliveryInstructions = "drop at the warehouse" };
            var result = orderValidator.TestValidate(order);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Validate_Order_Incorrect()
        {
            var order = new Order { RequestedPickupTime = System.DateTime.MinValue, PickupInstructions = null, DeliveryInstructions = null };
            var result = orderValidator.TestValidate(order);

            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Validate_Address()
        {
            var pickupAddress = new PickupAddress { Unit = 12, Street = "Mason street", Suburb = "Newlynn", City= "Auckland" , Postcode = 2735};
            var result = pickupAddressValidator.TestValidate(pickupAddress);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Validate_Address_Incorrect()
        {
            var pickupAddress = new PickupAddress { Unit = 789090909, Street = "Masonstreet6743", Suburb = "Newlynn&&*356", City = "&**hhjj", Postcode = 63 };
            var result = pickupAddressValidator.TestValidate(pickupAddress);

            result.ShouldHaveAnyValidationError();
        }
    }
}
