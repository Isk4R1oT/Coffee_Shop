namespace Coffee_Shop.Models
{
    public abstract class Payment
    {
        public abstract Payment PaymentType { get; set; }

        public abstract bool DoPay();

        public Payment Factory(Payment payment)
        {
            switch(PaymentType)
            {
                case PayPal: return new PayPal();
                case CreditCard: return new CreditCard();
                case BankTransfer: return new BankTransfer();
                default: return null;                   
            }
        }        
    }

    class  PayPal : Payment
    {
        public override Payment PaymentType { get; set; }

        public override bool DoPay()
        {
            return true;
        }
    }
    class CreditCard : Payment
    {
        public override Payment PaymentType { get; set; }
        public override bool DoPay()
        {
            return true;
        }
    }

    class BankTransfer : Payment
    {
        public override Payment PaymentType { get; set; }
        public override bool DoPay()
        {
            return true;
        }
    }
}
