//une duas coleções com base em uma chave em comum

example2();

void example1()
{
    var customers = new[]
    {
        new { Id = 1, Name = "John"},
        new { Id = 2, Name = "Anna"}
    };

    var orders = new[]
    {
        new { CustomerId = 1, Product = "Laptop"},
        new { CustomerId = 2, Product = "Phone"},
    };

    var result = customers.Join(orders,
        customer => customer.Id,
        order => order.CustomerId,
        (customer, order) => new { customer.Name, order.Product });

    foreach(var item in result)
    {
        Console.WriteLine($"{item.Name} comprou um {item.Product}");
    }
}

void example2()
{
    var authors = new[]
    {
        new { Id = 1, Name = "Author A"},
        new { Id = 2, Name = "Author B"},
    };

    var books = new[]
    {
        new { AuthorId = 1, Title = "Book 1" },
        new { AuthorId = 1, Title = "Book 2" },
        new { AuthorId = 2, Title = "Book 3" },
    };

    var result = authors.GroupJoin(books,
        author => author.Id,
        book => book.AuthorId,
        (author, book) => new { author.Name, book });

    foreach(var item in result)
    {
        Console.WriteLine($"Author: {item.Name}");

        foreach(var book in item.book)
        {
            Console.WriteLine($" - {book.Title}");
        }
    }
}