namespace Application.DTOs
{
    public record CreateProductDto(
        string Name,
        decimal Price,
        int StockQuantity
    );

}