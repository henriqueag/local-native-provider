using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LocalNativeProvider.BusinessObjects.SalesOrderItems;

public class SalesOrderItemData
{
    public const string TableName = "sales_order_items_detailed";

    [JsonPropertyName("OrderId")]
    [DisplayName("ID do Pedido")]
    public int OrderId { get; set; }

    [JsonPropertyName("ItemId")]
    [DisplayName("ID do Item")]
    public int ItemId { get; set; }

    [JsonPropertyName("ProductId")]
    [DisplayName("ID do Produto")]
    public int ProductId { get; set; }

    [JsonPropertyName("ProductName")]
    [DisplayName("Nome do Produto")]
    public string ProductName { get; set; }

    [JsonPropertyName("Quantity")]
    [DisplayName("Quantidade")]
    public int Quantity { get; set; }

    [JsonPropertyName("ListPrice")]
    [DisplayName("Preço de Lista")]
    public double ListPrice { get; set; }

    [JsonPropertyName("Discount")]
    [DisplayName("Desconto")]
    public double Discount { get; set; }
}