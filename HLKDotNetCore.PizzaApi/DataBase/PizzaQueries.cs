namespace HLKDotNetCore.PizzaApi.DataBase
{
    public class PizzaQueries
    {
        public const string pizzaOrderConstant = @"select po.*,p.Pizza,p.Price from Tbl_PizzaOrder po
                            inner join Tbl_Pizza p on p.PizzaID = po.PizzaID
                            where po.PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";

        public const string pizzaOrderDetailConstant = @"select pod.*, pe.PizzaExtra,pe.Price from Tbl_PizzaOrderDetail pod
                            inner join Tbl_PizzaExtra pe on pe.PizzaExtraID = pod.PizzaExtraID
                            where pod.PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";
    }
}
