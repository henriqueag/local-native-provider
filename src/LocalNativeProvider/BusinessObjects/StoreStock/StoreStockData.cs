using System.ComponentModel;
using System.Text.Json.Serialization;

namespace LocalNativeProvider.BusinessObjects.StoreStock;

public class StoreStockData
{
    public const string TableName = "store_stock_detailed";

    [JsonPropertyName("StoreId")]
    [DisplayName("Id da Loja")]
    public int StoreId { get; set; }

    [JsonPropertyName("StoreName")]
    [DisplayName("Nome da loja")]
    public string StoreName { get; set; }

    [JsonPropertyName("ProductId")]
    [DisplayName("Id do Produto")]
    public int ProductId { get; set; }

    [JsonPropertyName("ProductName")]
    [DisplayName("Nome do Produto")]
    public string ProductName { get; set; }

    [JsonPropertyName("BrandId")]
    [DisplayName("Id da Marca")]
    public int BrandId { get; set; }

    [JsonPropertyName("BrandName")]
    [DisplayName("Nome da Marca")]
    public string BrandName { get; set; }

    [JsonPropertyName("CategoryId")]
    [DisplayName("Id da Categoria")]
    public int CategoryId { get; set; }

    [JsonPropertyName("CategoryName")]
    [DisplayName("Nome da Categoria")]
    public string CategoryName { get; set; }

    [JsonPropertyName("ModelYear")]
    [DisplayName("Ano do Modelo")]
    public int ModelYear { get; set; }

    [JsonPropertyName("ListPrice")]
    [DisplayName("Preço")]
    public double ListPrice { get; set; }

    [JsonPropertyName("Quantity")]
    [DisplayName("Quantidade")]
    public int Quantity { get; set; }
}