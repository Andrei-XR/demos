//achata coleções aninhadas em uma única sequencia

example2();

void example1()
{
    var data = new[]
    {
        new { Name = "John", Scores = new[] { 90, 80 } },
        new { Name = "Anna", Scores = new[] { 85, 95 } },
    };

    var allScores = data.SelectMany(d => d.Scores);

    foreach(var score in allScores)
    {
        Console.WriteLine(score);
    }
}

void example2()
{
    var students = new[]
    {
        new { Name = "Alice", Grades = new[] { 90, 85 } },
        new { Name = "Bob", Grades = new[] { 78, 82, 88 } }
    };

    var allGrades = students.SelectMany(s => s.Grades);

    foreach(var grade in allGrades)
    {
        Console.WriteLine(grade);
    }
}