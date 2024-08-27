using Coffee_Shop.Entities;
using System.ComponentModel.DataAnnotations;

namespace Coffee_Shop.Models
{

    interface IOrderBilder
    {
        IOrderBilder Adress(AdressOrder adressOrder);
        IOrderBilder CurrentState(IOrderState state);

        IOrderBilder OrderItem(OrderItem item);
        IOrderBilder Client(Client client);

        IOrderBilder PayMent(Payment payment);
        Order Bild();
    }
    public class CreateOrder : IOrderBilder
    {
        private readonly Order _order;

        public CreateOrder(Order order)
        {
            _order = order;
        }
        public Order Bild()
        {
            return _order;
        }

        IOrderBilder IOrderBilder.Adress(AdressOrder adressOrder)
        {
            _order.Adress = adressOrder;
            return this;
        }

        IOrderBilder IOrderBilder.Client(Client client)
        {
            _order.Client = client;
            return this;
        }

        IOrderBilder IOrderBilder.CurrentState(IOrderState state)
        {
            _order.CurrentState = state;
            return this;
        }

        IOrderBilder IOrderBilder.OrderItem(OrderItem item)
        {
            _order.OrderItem.Add(item);
            return this;
        }
        IOrderBilder IOrderBilder.PayMent(Payment payment)
        {
            _order.Payment = payment;
            return this;
        }
    }

    // Интерфейс состояния заказа
    public interface IOrderState
    {
        void ProcessOrder(Order order);
    }

    /// <summary>
    /// Заказ собираеться
    /// </summary>
    public class OrgerInAssembled : IOrderState
    {
        public void ProcessOrder(Order order)
        {

        }

    }
    /// <summary>
    /// Заказ доставляеться
    /// </summary>
    public class OrderInTransitState : IOrderState
    {
        public void ProcessOrder(Order order)
        {

        }
    }

    /// <summary>
    /// Заказ доставлен
    /// </summary>
    public class OrderDelivered : IOrderState
    {
        public void ProcessOrder(Order order)
        {

        }
    }

    // Класс заказа
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public AdressOrder Adress { get; set; }

        public IOrderState CurrentState { get; set; }

        public ICollection<OrderItem> OrderItem { get; set; } = new HashSet<OrderItem>();

        public Client Client { get; set; }

        public Payment Payment { get; set; }

        public void ChangeState(IOrderState newState)
        {
            CurrentState = newState;
        }

        public void ProcessOrder()
        {
            CurrentState.ProcessOrder(this);
        }
        public Order()
        {

        }
    }
    public class OrderItem
    {
            public int Id { get; set; }      
            public int Quantity { get; set; }

            public Order Order { get; set; }

            public Coffee Coffee { get; set; }
       

    }

   
}
