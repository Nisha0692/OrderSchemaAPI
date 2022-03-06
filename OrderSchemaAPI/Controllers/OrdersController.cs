#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderSchemaAPI.Data;
using OrderSchemaAPI.Common;
using OrderSchemaAPI.Repository;
using OrderSchemaAPI.Models;
using FluentValidation.Results;

namespace OrderSchemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository _repository;

        public OrdersController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
        {
            var orders = await _repository.GetOrderAsync();
            var orderResponse = new OrderResponse();
            var orderList = new List<OrderView>();
            foreach(var order in orders)
            {
                var newOrder = new OrderView();
                newOrder.RequestedPickupTime= order.RequestedPickupTime;
                var pickup = new List<PickupAddressView>();
                foreach(var pickupAddressItem in order.PickupAddress)
                {
                    var pickupAddress = new PickupAddressView();
                    pickupAddress.Unit = pickupAddressItem.Unit;
                    pickupAddress.Street = pickupAddressItem.Street;
                    pickupAddress.Suburb = pickupAddressItem.Suburb;
                    pickupAddress.City = pickupAddressItem.City;
                    pickupAddress.Postcode = pickupAddressItem.Postcode;
                    pickup.Add(pickupAddress);
                }
                var delivery = new List<DeliveryAddressView>();
                foreach(var deliveryAddressItem in order.DeliveryAddress)
                {
                    var deliveryAddress = new DeliveryAddressView();
                    deliveryAddress.Unit = deliveryAddressItem.Unit;
                    deliveryAddress.Street = deliveryAddressItem.Street;
                    deliveryAddress.Suburb = deliveryAddressItem.Suburb;
                    deliveryAddress.City = deliveryAddressItem.City;
                    deliveryAddress.Postcode = deliveryAddressItem.Postcode;
                    delivery.Add(deliveryAddress);
                }
                var items = new List<ItemView>();
                foreach(var i in order.Items)
                {
                    var newItem = new ItemView();
                    newItem.ItemCode = i.ItemCode;
                    newItem.Quantity = i.Quantity;
                    items.Add(newItem);
                }
                newOrder.PickupInstructions = order.PickupInstructions;
                newOrder.DeliveryInstructions = order.DeliveryInstructions;
                newOrder.PickupAddress = pickup;
                newOrder.DeliveryAddress = delivery;
                newOrder.Items = items;
                orderList.Add(newOrder);
            }
            orderResponse.Orders = orderList;

            if (orderResponse == null)
            {
                return NotFound();
            }
            return Ok(orderResponse);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var order = await _repository.GetOrdersById(id);
            var response = new OrderView();
            response.RequestedPickupTime = order.RequestedPickupTime;
            var pickup = new List<PickupAddressView>();
            foreach (var pickupAddressItem in order.PickupAddress)
            {
                var pickupAddress = new PickupAddressView();
                pickupAddress.Unit = pickupAddressItem.Unit;
                pickupAddress.Street = pickupAddressItem.Street;
                pickupAddress.Suburb = pickupAddressItem.Suburb;
                pickupAddress.City = pickupAddressItem.City;
                pickupAddress.Postcode = pickupAddressItem.Postcode;
                pickup.Add(pickupAddress);
            }
            var delivery = new List<DeliveryAddressView>();
            foreach (var deliveryAddressItem in order.DeliveryAddress)
            {
                var deliveryAddress = new DeliveryAddressView();
                deliveryAddress.Unit = deliveryAddressItem.Unit;
                deliveryAddress.Street = deliveryAddressItem.Street;
                deliveryAddress.Suburb = deliveryAddressItem.Suburb;
                deliveryAddress.City = deliveryAddressItem.City;
                deliveryAddress.Postcode = deliveryAddressItem.Postcode;
                delivery.Add(deliveryAddress);
            }
            var items = new List<ItemView>();
            foreach (var i in order.Items)
            {
                var newItem = new ItemView();
                newItem.ItemCode = i.ItemCode;
                newItem.Quantity = i.Quantity;
                items.Add(newItem);
            }
            response.PickupInstructions = order.PickupInstructions;
            response.DeliveryInstructions = order.DeliveryInstructions;
            response.PickupAddress = pickup;
            response.DeliveryAddress = delivery;
            response.Items = items;
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
       
        // POST: api/Orders
       
        [HttpPost]
        public async Task<ActionResult<OrderView>> PostOrder(OrderView order)
        {
            var pickupAddressValidator = new PickupAddressValidator();
            var deliveryAddressValidator = new DeliveryAddressValidator();
            var orderValidator = new OrderValidator();
            var itemValidator = new ItemValidator();

            var orderToAdd = new Order();
            orderToAdd.RequestedPickupTime = order.RequestedPickupTime;
            var pickup = new List<PickupAddress>();
            foreach (var pickupAddressItem in order.PickupAddress)
            {
                var pickupAddress = new PickupAddress();
                pickupAddress.Unit = pickupAddressItem.Unit;
                pickupAddress.Street = pickupAddressItem.Street;
                pickupAddress.Suburb = pickupAddressItem.Suburb;
                pickupAddress.City = pickupAddressItem.City;
                pickupAddress.Postcode = pickupAddressItem.Postcode;
                pickup.Add(pickupAddress);
                ValidationResult pickUpAddressresult = pickupAddressValidator.Validate(pickupAddress);
                if (pickUpAddressresult.IsValid == false)
                {
                    return BadRequest(pickUpAddressresult.Errors[0].ErrorMessage);
                }
            }
            var delivery = new List<DeliveryAddress>();
            foreach (var deliveryAddressItem in order.DeliveryAddress)
            {
                var deliveryAddress = new DeliveryAddress();
                deliveryAddress.Unit = deliveryAddressItem.Unit;
                deliveryAddress.Street = deliveryAddressItem.Street;
                deliveryAddress.Suburb = deliveryAddressItem.Suburb;
                deliveryAddress.City = deliveryAddressItem.City;
                deliveryAddress.Postcode = deliveryAddressItem.Postcode;
                delivery.Add(deliveryAddress);
                ValidationResult deliveryAddressresult = deliveryAddressValidator.Validate(deliveryAddress);
                if (deliveryAddressresult.IsValid == false)
                {
                    return BadRequest(deliveryAddressresult.Errors[0].ErrorMessage);
                }
            }
            var items = new List<Item>();
            foreach (var i in order.Items)
            {
                var newItem = new Item();
                newItem.ItemCode = i.ItemCode;
                newItem.Quantity = i.Quantity;
                items.Add(newItem);
                ValidationResult itemResult = itemValidator.Validate(newItem);
                if (itemResult.IsValid == false)
                {
                    return BadRequest(itemResult.Errors[0].ErrorMessage);
                }
            }
            orderToAdd.PickupInstructions = order.PickupInstructions;
            orderToAdd.DeliveryInstructions = order.DeliveryInstructions;
            orderToAdd.PickupAddress = pickup;
            orderToAdd.DeliveryAddress = delivery;
            orderToAdd.Items = items;
            ValidationResult orderResult = orderValidator.Validate(orderToAdd);
            if (orderResult.IsValid == false)
            {
                return BadRequest(orderResult.Errors[0].ErrorMessage);
            }
           

            await _repository.SaveChangesAsync(orderToAdd);

            return CreatedAtAction("GetOrder", new { id = orderToAdd.OrderId }, orderToAdd);
        }
    }
}
