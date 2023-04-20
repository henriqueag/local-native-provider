using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LocalNativeProvider.BusinessObjects.SalesOrder;

public class SalesOrderData
{
    public const string TableName = "sales_orders_detailed";

    [DisplayName("Id do pedido")]
    [JsonPropertyName("OrderId")]
    public int OrderId { get; set; }

    [DisplayName("Status do pedido")]
    [JsonPropertyName("OrderStatus")]
    public string OrderStatus { get; set; }

    [DisplayName("Data do pedido")]
    [JsonPropertyName("OrderDate")]
    public DateTime OrderDate { get; set; }

    [DisplayName("Data de requerimento do pedido")]
    [JsonPropertyName("RequiredDate")]
    public DateTime RequiredDate { get; set; }

    [DisplayName("Data de envio do pedido")]
    [JsonPropertyName("ShippedDate")]
    public DateTime ShippedDate { get; set; }

    [DisplayName("Id do cliente")]
    [JsonPropertyName("CustomerId")]
    public int CustomerId { get; set; }

    [DisplayName("Nome do cliente")]
    [JsonPropertyName("CustomerName")]
    public string CustomerName { get; set; }

    [DisplayName("Telefone do cliente")]
    [JsonPropertyName("CustomerPhone")]
    public string CustomerPhone { get; set; }

    [DisplayName("Email do cliente")]
    [JsonPropertyName("CustomerEmail")]
    public string CustomerEmail { get; set; }

    [DisplayName("Endereço do cliente")]
    [JsonPropertyName("CustomerAddress")]
    public string CustomerAddress { get; set; }

    [DisplayName("Nome da loja")]
    [JsonPropertyName("StoreName")]
    public string StoreName { get; set; }

    [DisplayName("Telefone da loja")]
    [JsonPropertyName("StorePhone")]
    public string StorePhone { get; set; }

    [DisplayName("Email da loja")]
    [JsonPropertyName("StoreEmail")]
    public string StoreEmail { get; set; }

    [DisplayName("Endereço da loja")]
    [JsonPropertyName("StoreAddress")]
    public string StoreAddress { get; set; }

    [DisplayName("Nome do vendedor")]
    [JsonPropertyName("StaffName")]
    public string StaffName { get; set; }
}