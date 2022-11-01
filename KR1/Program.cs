using System;
using System.Numerics;
using System.Text;

int[,] GenerateMatrix(int n)
{
    Random r = new Random();

    int[,] matr = new int[n,n];
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            matr[i,j] = r.Next(-100, 100);

    return matr;
}

void PrintMatr(int[,] matr)
{
    for (int i = 0; i < matr.GetLength(0); i++)
    {
        for (int j = 0; j < matr.GetLength(1); j++)
        {
            Console.Write($"{matr[i, j]} ");
        }
        Console.WriteLine();
    }
}

async void WriteMatrix(int[,] matrix, string path)
{
    string text = "";
    int N=matrix.GetLength(0);
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            text += matrix[i, j].ToString();
            text += " ";
        }
        text.Trim();
        text += '\n';
    }
    using (FileStream fstream = new FileStream(path, FileMode.Create))
    {
        byte[] buffer = Encoding.Default.GetBytes(text);
        await fstream.WriteAsync(buffer, 0, buffer.Length);
    }
}

int[,] ReadMatrix(string path, int n)
{
    using (StreamReader reader = new StreamReader("note.txt"))
    {
        int[,] matr = new int[n,n];
        for (int i = 0; i < n; i++)
        {
            var row = reader.ReadLine().Split(' ');
            for (int j = 0; j < n; j++)
                matr[i, j] = int.Parse(row[j]);
        }
        return matr;
    }
}
string path = @"C:\Users\nazig\source\repos\1 семестр по темам\KR1\KR1\bin\Debug\net6.0\note.txt";
//Я умею записывать в файл только используя путь(
int N = 10;
int[,] matrix = new int[N, N];
matrix = GenerateMatrix(N);
//PrintMatr(matrix);
WriteMatrix(matrix,path);
//PrintMatr(ReadMatrix(path, N));

//2 вариант задание:

int[,] GenerateMatrix1(int n)//нечетные числа
{
    Random r = new Random();

    int[,] matr = new int[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            int t=r.Next(-100,100);
            if (t % 2 == 1)
                matr[i, j] = t;
            else
                matr[i, j] = t - 1;
        }
    }    

    return matr;
}

bool IsMagic(int[,] matr)
{
    bool flag=true;
    int n=matr.GetLength(0),sum=0,sumi=0,sumj=0;
    for (int i = 0; i < n; i++)
        sum+=matr[i,0];
    for (int i = 0; i < n; i++)
    {
        sumi = 0;
        sumj = 0;
        for (int j = 0; j < n; j++)
        {
            sumi+=matr[i,j];
            sumj += matr[j, i];
        }
        if (sumi!= sum)
            flag=false;
    }
    return flag;
}

int[,] SumMatrix(int[,] matr1, int[,] matr2)
{
    var summatr = matr1;
    int n = matr1.GetLength(0);
    for (int i = 0; i < n; i++)
    {
        for (int j=0; j<n; j++)
        {
            summatr[i,j] = matr2[i, j] + matr1[i,j];
        }
    }
    return summatr;
}

bool isPrime(int number)
{
    bool prime = true;
    int upper = (int)Math.Sqrt(number);
    for (int i = 2; i <= upper+1; i++)
    {
        if ((number % i) == 0)
        {
            prime = false;
            return prime;
        }
    }
    return prime;
}

var matrix1= GenerateMatrix1(N);
var matrix2 = ReadMatrix(path, N);
PrintMatr(matrix1);
PrintMatr(matrix2);
Console.WriteLine(IsMagic(matrix2));
PrintMatr(SumMatrix(matrix1,matrix2));
var matrix3 = SumMatrix(matrix1, matrix2);
int max = 0,maxi=0;
for (int i = 0; i < N; i++)
{
    int k = 0;
    for (int j=0; j < N; j++)
    {
        if (isPrime(matrix3[j,i]))
        {
            k++;
        }
    }
    if (k>max)
    {
        max = k;
        maxi = i;
    }
}
Console.WriteLine();
for (int i = 0; i < N; i++)
{
    Console.WriteLine(matrix3[i,maxi]);
}
Console.WriteLine();
