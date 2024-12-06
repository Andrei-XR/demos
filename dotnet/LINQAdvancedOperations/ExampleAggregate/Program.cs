//reduz uma coleção a um unico valor

example2();

void example1()
{
    var numbers = new[] { 1, 2, 3, 4 };

    int sum = numbers.Aggregate((total, num) => total + num);
    Console.WriteLine($"Sum: {sum}");
}

void example2()
{
    var numbers = new[] { 2, 3, 5, 7 };

    int mult = numbers.Aggregate((total, num) => total * num);
    Console.WriteLine($"Mult: {mult}");
}