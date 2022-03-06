namespace OrderSchemaAPI.Common
{
    public class OrderView
    {
        public DateTime RequestedPickupTime { get; set; }
        public IEnumerable<PickupAddressView> PickupAddress { get; set; }
        public IEnumerable<DeliveryAddressView> DeliveryAddress { get; set; }
        public IEnumerable<ItemView> Items { get; set; }
        public string PickupInstructions { get; set; }
        public string DeliveryInstructions { get; set; }
    }
}
