using ShopParallelism;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; 

        using var context = new AppDbContext();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var initialProduct = new Product { Name = "Ноутбук", Description = "Ігровий ноутбук", Price = 30000, AvailableQuantity = 5 };
        context.Products.Add(initialProduct);
        await context.SaveChangesAsync();
        Console.WriteLine("📦 Початковий товар додано до бази даних.");

        using var ctx1 = new AppDbContext();
        var productByAdmin1 = await ctx1.Products.FindAsync(1);

        using var ctx2 = new AppDbContext();
        var productByAdmin2 = await ctx2.Products.FindAsync(1);

        productByAdmin1!.Price = 35000;
        await ctx1.SaveChangesAsync();
        Console.WriteLine("👤 Адміністратор 1 змінив ціну на 35000 і успішно зберіг.");

        productByAdmin2!.Description = "Ультрабук для роботи та навчання";

        var serviceAdmin2 = new ProductService(ctx2);
        try
        {
            await serviceAdmin2.UpdateProductAsync(productByAdmin2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n⚠️ Результат обробки помилки: {ex.Message}");
        }

        using var ctxHistory = new AppDbContext();
        var history = await ctxHistory.ProductHistories.ToListAsync();

        Console.WriteLine($"\n📜 Перевірка історії змін. Кількість записів у логах: {history.Count}");
        foreach (var h in history)
        {
            Console.WriteLine($"- Товар ID: {h.ProductId}, Дія: {h.Action}, Ціна в історії: {h.Price}, Час: {h.ChangedAt}");
        }
    }
}