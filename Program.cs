using System.Collections.Generic;

void AddWord(Dictionary<string, int> frequencies, string word)
{
    if (frequencies.TryGetValue(word, out int count))
    {
        frequencies[word] = count + 1;
    }
    else
    {
        frequencies.Add(word, 1);
    }
}

int SumFrequencies(Dictionary<string, int> frequencies)
{
    int sum = 0;
    foreach (KeyValuePair<string, int> entry in frequencies)
    {
        sum += entry.Value;
    }
    return sum;
}

void PrintFrequencies(Dictionary<string, int> frequencies)
{
    if (frequencies.Count == 0)
    {
        Console.WriteLine("Нет слов для анализа");
        return;
    }

    foreach (KeyValuePair<string, int> entry in frequencies)
    {
        Console.WriteLine($"{entry.Key}: {entry.Value}");
    }
}

Console.WriteLine(">>> Анализ слов <<<");
Console.WriteLine();


HashSet<string> excludedWords = new HashSet<string> { "и", "в", "на" };


Console.Write("Введите слово для добавления в исключения: ");
string wordToAdd = Console.ReadLine()!.Trim().ToLowerInvariant();

if (excludedWords.Add(wordToAdd))
{
    Console.WriteLine("Исключение добавлено");
}
else
{
    Console.WriteLine("Такое исключение уже есть");
}


Console.Write("Введите слово для удаления из исключений: ");
string wordToRemove = Console.ReadLine()!.Trim().ToLowerInvariant();

if (excludedWords.Remove(wordToRemove))
{
    Console.WriteLine("Исключение удалено");
}
else
{
    Console.WriteLine("Такого исключения нет");
}

Console.WriteLine($"Количество исключений: {excludedWords.Count}");
Console.WriteLine();


Console.Write("Введите текст: ");
string text = Console.ReadLine() ?? "";

string[] words = text.ToLowerInvariant()
    .Split(' ', StringSplitOptions.RemoveEmptyEntries);


Dictionary<string, int> frequencies = new Dictionary<string, int>();

foreach (string word in words)
{
    if (excludedWords.Contains(word))
    {
        continue;
    }
    AddWord(frequencies, word);
}


int totalWords = words.Length;
int countedWords = SumFrequencies(frequencies);
int skippedWords = totalWords - countedWords;
int distinctWords = frequencies.Count;

Console.WriteLine();
Console.WriteLine($"Исходная строка: [{text}]");
Console.WriteLine($"Всего слов: {totalWords}");
Console.WriteLine($"Пропущено слов: {skippedWords}");
Console.WriteLine($"Учтено слов: {countedWords}");
Console.WriteLine($"Разных учтённых слов: {distinctWords}");
Console.WriteLine();
Console.WriteLine("Частоты слов:");
PrintFrequencies(frequencies);
Console.WriteLine();


Console.Write("Введите слово для поиска: ");
string searchWord = Console.ReadLine()!.Trim().ToLowerInvariant();

if (frequencies.TryGetValue(searchWord, out int found))
{
    Console.WriteLine($"Количество вхождений: {found}");
}
else
{
    Console.WriteLine("Слово не найдено");
}


Console.WriteLine();
Console.Write("Введите порог частоты (1..100): ");
int limit = int.Parse(Console.ReadLine() ?? "0");
Console.WriteLine($"Слов с частотой >= {limit}: {CountFrequentWords(frequencies, limit)}");

int CountFrequentWords(Dictionary<string, int> frequencies, int limit)
{
    int count = 0;
    foreach (KeyValuePair<string, int> entry in frequencies)
    {
        if (entry.Value >= limit)
        {
            count++;
        }
    }
    return count;
}
//самостоятельное задание 1 вариант