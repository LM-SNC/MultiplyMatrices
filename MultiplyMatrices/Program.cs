var matrices = new List<List<List<int>>> { new List<List<int>>() { new List<int>() } };

//Спасибо SobolB за мини код-ревью :D

for (var i = 0;; i++)
{
    bool isEnd = false;

    Console.WriteLine($"Enter the {i + 1} matrix");

    while (true)
    {
        var matrixElement = Console.ReadLine()!;

        if (matrixElement.ToLower() == "n")
        {
            break;
        }

        if (matrixElement.ToLower() == "b")
        {
            isEnd = true;
            break;
        }

        if (string.IsNullOrEmpty(matrixElement))
        {
            matrices[^1].Add(new List<int>());
        }
        else
        {
            matrices[^1][^1].Add(int.Parse(matrixElement));
        }

        DisplayMatrices(matrices);
    }

    if (isEnd)
    {
        break;
    }

    matrices.Add(new List<List<int>> { new List<int>() });
}


if (matrices.Count < 2)
{
    Console.WriteLine("Eblan?");
    return;
}

DisplayMatrices(matrices);

Console.WriteLine("Result: ");
Console.WriteLine();


var resultHui = MultiplyMatrices(new List<List<List<int>>> { matrices[0], matrices[1] });
DisplayMatrix(resultHui);

for (var i = 2; i < matrices.Count; i++)
{
    resultHui = MultiplyMatrices(new List<List<List<int>>> { resultHui, matrices[i] });
    DisplayMatrix(resultHui);
}


List<List<int>> MultiplyMatrices(List<List<List<int>>> matrices)
{
    var result = new List<List<int>> { new List<int>() };

    for (var matrix = 0; matrix < matrices.Count - 1; matrix++)
    {
        if (matrices[matrix][0].Count != matrices[matrix + 1].Count)
        {
            throw new Exception(
                "The number of columns of the first matrix must be equal to the number of rows of the second matrix");
        }

        for (var row = 0; row < matrices[matrix].Count; row++)
        {
            int index = 0;

            for (int i = 0; i < matrices[matrix + 1][0].Count; i++)
            {
                int resultElement = 0;

                for (var element = 0; element < matrices[matrix][row].Count; element++)
                {
                    var tmpResult = matrices[matrix][row][element] * matrices[matrix + 1][element][index];
                    resultElement += tmpResult;
                }

                index++;
                result[^1].Add(resultElement);
            }

            result.Add(new List<int>());
        }
    }

    result.RemoveAt(result.Count - 1);

    return result;
}

void DisplayMatrices(List<List<List<int>>> matrices)
{
    Console.Clear();

    foreach (var matrix in matrices)
    {
        Console.WriteLine();
        DisplayMatrix(matrix);
    }
}

void DisplayMatrix(List<List<int>> matrix)
{
    Console.Write("┌");
    for (int j = 0; j < matrix[0].Count; j++)
    {
        Console.Write(AlignCentre("", 7));
    }

    Console.WriteLine("┐");

    for (int j = 0; j < matrix.Count; j++)
    {
        Console.Write("|");
        for (int k = 0; k < matrix[j].Count; k++)
        {
            Console.Write(AlignCentre(matrix[j][k].ToString(), 7));
        }

        Console.Write("|");

        Console.WriteLine();
    }

    Console.Write("└");

    for (int j = 0; j < matrix[0].Count; j++)
    {
        Console.Write(AlignCentre("", 7));
    }

    Console.WriteLine("┘");
}

static string AlignCentre(string text, int width)
{
    text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

    if (string.IsNullOrEmpty(text))
    {
        return new string(' ', width);
    }
    else
    {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}