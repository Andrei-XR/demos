//agrupa elementos de uma coleção com base em uma chave

example2();

void example1(){

    var employees = new[]
    {
        new { Name = "John", Department = "HR"},
        new { Name = "Anna", Department = "IT"},
        new { Name = "Mike", Department = "IT"},
        new { Name = "Sara", Department = "HR"},
    };

    var grouped = employees.GroupBy(e => e.Department);

    foreach (var group in grouped)
    {
        Console.WriteLine($"Department: {group.Key}");

        foreach (var employee in group)
        {
            Console.WriteLine($" - {employee.Name}");
        }
    }
}

void example2(){

    var transactions = new[]
    {
        new { Id = 1, Amount = 100, Category = "Food" },
        new { Id = 2, Amount = 200, Category = "Transport" },
        new { Id = 3, Amount = 150, Category = "Food" },
        new { Id = 4, Amount = 300, Category = "Shopping" },
        new { Id = 5, Amount = 100, Category = "Transport" },
    };

    var grouped = transactions.GroupBy(t => t.Category);

    foreach(var group in grouped)
    {
        Console.WriteLine($"Category: {group.Key}");
        Console.WriteLine($"Total: {group.Sum(t => t.Amount)}");
        Console.WriteLine();
    }
}