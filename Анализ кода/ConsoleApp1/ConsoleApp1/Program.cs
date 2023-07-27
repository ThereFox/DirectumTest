
using System.Globalization;
using System.Xml.Linq;
using System;
using System.Text;

var testCase = new KeyValuePair<int, string>[]
{
    new(331, "133"),
    new(123, "123"),
    new(22, "223"),
    new(13, "133"),

    new(121, "123"),
    new(244, "223"),
    new(311, "133"),

    new(1, "123"),
    new(22, "223"),
    new(3, "133"),
};

//Func1(ref testCase, 1, "231");

ArraySort(ref testCase);

Task.Delay(1000);

//добавление в коллекцию KeyValuePair<int, string>[] (с увеличением размера массива) новый элемент с последующей пересортировкой массива

//Недостатки:
//каждый раз создаётся новый массив и копирует данные из старого (O(n))
//неэффективная сортировка (O(n^2))

static void Func1(ref KeyValuePair<int, string>[] a, int key, string value)
{
    Array.Resize(ref a, a.Length + 1); //Увеличение размера массива

    var keyValuePair = new KeyValuePair<int, string>(key, value);
    a[a.Length - 1] = keyValuePair; //Добавление нового элемента в новый элемент массива

    for (int i = 0; i < a.Length; i++) //проход по всем элементам
    {
        for (int j = a.Length - 1; j > 0; j--) //проход по всем остальным элементам до основного
        {
            if (a[j - 1].Key > a[j].Key) //Если следующий ключ ниже текущего
            {
                KeyValuePair<int, string> x;
                x = a[j - 1];
                a[j - 1] = a[j];
                a[j] = x;
                //значения меняются местами
            }
        }
    }
}


//Вариант улучшения 1:
//Смена основной коллекции на динамически расширяемую, к примеру
//1)На List<T>, который больше оптимизирован для работы со структурами + смена алгоритма сортировки на поразрядная сортировка + сортировка подсчётом (О(n))
//2)Возможно, на бинарное дерево (т.к. вставка станет О(n) в наихудшем случае ), при таком условии возростёт доступ к элементу(из О(1) к О(n)),
//но при таком условии отпадает необходимость в гарантированной сортировке всех элементов при вставке нового элемента.

//Вариант улучшения 2:
// Без смены коллекции, но со сменой алгоритма сортировки
//Собственная реализация алгоритма сортировки (поразрядная сортировка + сортировка подсчётом (О(n) производительность)

//Более производительный алгоритм (O(n) по скорости)
static void ArraySort(ref KeyValuePair<int, string>[] initArray)
{
    var regexCount = (int)Math.Log10(initArray.Max(val => val.Key)) + 1;

    for (int i = 0; i < regexCount; i++)
    {
        CountingSort(ref initArray, i); // O(n)
    }
}
static void CountingSort(ref KeyValuePair<int, string>[] initArray, int regex)
{
    var outputeData = new KeyValuePair<int, string>[initArray.Length];

    var timebleArray = new int[10];

    for (int i = 0; i < 10; i++)
    {
        timebleArray[i] = 0;
    }

    for (int i = 0; i < initArray.Length; i++)
    {
        var regexValue = (initArray[i].Key / (int)Math.Pow(10, regex)) % 10;

        timebleArray[regexValue]++;
    }

    for (int i = 1; i < 10; i++)
    {
        timebleArray[i] = timebleArray[i] + timebleArray[i - 1];
    }

    for (int i = initArray.Length - 1; i >= 0; i--)
    {
        var regexValue = (initArray[i].Key / (int)Math.Pow(10, regex)) % 10;

        outputeData[timebleArray[regexValue] - 1] = initArray[i];
        timebleArray[regexValue]--;
    }

    for (int i = 0; i < initArray.Length; i++)
    {
        initArray[i] = outputeData[i];
    }

}
