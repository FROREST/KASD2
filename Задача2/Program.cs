using System;

struct ComplexNumber
{
    public double Real { get; private set; }
    public double Imaginary { get; private set; }

    
    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

   
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real);
    }

  
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        if (denominator == 0)
        {
            throw new DivideByZeroException("Ошибка: Деление на ноль.");
        }
        return new ComplexNumber(
            (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator,
            (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
    }

    
    public double Modulus()
    {
        return Math.Sqrt(Real * Real + Imaginary * Imaginary);
    }

   
    public double Argument()
    {
        return Math.Atan2(Imaginary, Real);
    }

   
    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
}

class Program
{
    static void Main(string[] args)
    {
        ComplexNumber complex = new ComplexNumber(0, 0);
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Создать комплексное число");
            Console.WriteLine("2 - Сложить");
            Console.WriteLine("3 - Вычесть");
            Console.WriteLine("4 - Умножить");
            Console.WriteLine("5 - Разделить");
            Console.WriteLine("6 - Нахождение модуля");
            Console.WriteLine("7 - Нахождение аргумента");
            Console.WriteLine("8 - Вывод комплексного числа");
            Console.WriteLine("Q - Выход");

            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                    PerformOperation(ref complex, choice);
                    break;
                case 'Q':
                case 'q':
                    running = false;
                    break;
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
        }
    }

    static void PerformOperation(ref ComplexNumber complex, char choice)
    {
        try
        {
            switch (choice)
            {
                case '1':
                    Console.Write("Введите вещественную часть: ");
                    double real = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Введите мнимую часть: ");
                    double imaginary = Convert.ToDouble(Console.ReadLine());
                    complex = new ComplexNumber(real, imaginary);
                    break;
                case '2':
                    ComplexNumber addend = GetComplexNumberFromUser("Введите комплексное число для сложения:");
                    complex += addend;
                    break;
                case '3':
                    ComplexNumber subtrahend = GetComplexNumberFromUser("Введите комплексное число для вычитания:");
                    complex -= subtrahend;
                    break;
                case '4':
                    ComplexNumber multiplier = GetComplexNumberFromUser("Введите комплексное число для умножения:");
                    complex *= multiplier;
                    break;
                case '5':
                    ComplexNumber divisor = GetComplexNumberFromUser("Введите комплексное число для деления:");
                    complex /= divisor; // Исключение будет выброшено в случае деления на ноль
                    break;
                case '6':
                    Console.WriteLine($"Модуль: {complex.Modulus()}");
                    break;
                case '7':
                    Console.WriteLine($"Аргумент: {complex.Argument()}");
                    break;
                case '8':
                    Console.WriteLine($"Комплексное число: {complex}");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введите корректное число.");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static ComplexNumber GetComplexNumberFromUser(string message)
    {
        Console.WriteLine(message);
        Console.Write("Введите вещественную часть: ");
        double real = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите мнимую часть: ");
        double imaginary = Convert.ToDouble(Console.ReadLine());
        return new ComplexNumber(real, imaginary);
    }
}