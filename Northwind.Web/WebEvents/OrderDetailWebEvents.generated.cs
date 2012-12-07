namespace Northwind.Web.WebEvents
{ 
    using System.Web.Management;
	using Northwind.Data;
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetailCreatedEvent" /> class
    /// </summary>
    /// <param name="orderDetail">The <see cref="OrderDetail" /> which the event relates to</param>
    public class OrderDetailCreatedEvent : WebBaseEvent
    {
        public OrderDetailCreatedEvent(OrderDetail orderDetail)
            : base(string.Format("OrderDetail: '{0}-{1}' was created.", orderDetail.OrderId, orderDetail.ProductId), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetailUpdatedEvent" /> class
    /// </summary>
    /// <param name="orderDetail">The <see cref="OrderDetail" /> which the event relates to</param>
    public class OrderDetailUpdatedEvent : WebBaseEvent
    {
        public OrderDetailUpdatedEvent(OrderDetail orderDetail)
            : base(string.Format("OrderDetail: '{0}-{1}' was updated.", orderDetail.OrderId, orderDetail.ProductId), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderDetailDeletedEvent" /> class
    /// </summary>
    /// <param name="orderDetail">The <see cref="OrderDetail" /> which the event relates to</param>
    public class OrderDetailDeletedEvent : WebBaseEvent
    {
        public OrderDetailDeletedEvent(OrderDetail orderDetail)
            : base(string.Format("OrderDetail: '{0}-{1}' was deleted.", orderDetail.OrderId, orderDetail.ProductId), "Northwind.Web", WebEventCodes.WebExtendedBase + 100)
        { }
    }
}