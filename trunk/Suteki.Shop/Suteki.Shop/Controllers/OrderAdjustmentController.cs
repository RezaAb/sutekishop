using System;
using System.Web.Mvc;
using Suteki.Common.Filters;
using Suteki.Common.Repositories;

namespace Suteki.Shop.Controllers
{
    public class OrderAdjustmentController : ControllerBase
    {
        readonly IRepository<Order> orderRepository;

        public OrderAdjustmentController(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [AcceptVerbs(HttpVerbs.Head)]
        public void Add(int id)
        {
            // form binding target
        }

        [ChildActionOnly, HttpGet]
        public ViewResult Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            var adjustment = new OrderAdjustment {Order = order};
            return View("Add", adjustment);
        }

        [HttpPost, UnitOfWork]
        public ActionResult Add(OrderAdjustment adjustment)
        {
            if (adjustment == null)
            {
                throw new ArgumentNullException("adjustment");
            }

            var order = adjustment.Order;
            if (ModelState.IsValid)
            {
                order.AddAdjustment(adjustment);
                orderRepository.SaveOrUpdate(order);
            }
            return RedirectToAction("Item", "Order", new {order.Id});
        }

        [HttpPost, UnitOfWork]
        public ActionResult Delete(OrderAdjustment adjustment)
        {
            if (adjustment == null)
            {
                throw new ArgumentNullException("adjustment");
            }

            var order = adjustment.Order;
            order.RemoveAdjustment(adjustment);
            orderRepository.SaveOrUpdate(order);
            return RedirectToAction("Item", "Order", new { order.Id });
        }
    }
}