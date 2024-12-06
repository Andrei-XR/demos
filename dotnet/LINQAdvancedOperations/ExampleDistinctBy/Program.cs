//remove duplicatas com base em uma chave especifica

example2();

void example1()
{
    var products = new[]
    {
        new { Name = "Laptop", Category = "Eletronics" },
        new { Name = "Phone", Category = "Eletronics" },
        new { Name = "Shirt", Category = "Clothing" },
    };

    var distinctCategories = products.DistinctBy(p => p.Category);

    foreach(var item in distinctCategories)
    {
        Console.WriteLine(item.Category);
    }
}

void example2()
{
    var products = new[]
    {
        new { Name = "Apple", Type = "Fruit" },
        new { Name = "Banana", Type = "Fruit" },
        new { Name = "Carrot", Type = "Vegetable" },
        new { Name = "Apple", Type = "Fruit" },
    };

    var distinctName = products.DistinctBy(p => p.Name);

    foreach (var item in distinctName)
    {
        Console.WriteLine(item.Name);
    }
}