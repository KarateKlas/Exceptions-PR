namespace ExceptionsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("=== Start av programmet ===");
                Console.WriteLine("Christofer was here");

                // Exempel 1: try-catch-finally
                try
                {
                    Console.WriteLine("Försöker läsa fil och räkna...");
                    var path = Path.Combine(AppContext.BaseDirectory, "numbers.txt");
                    var result = ProcessFile(path);
                  
                    Console.WriteLine($"\nResultat: {result}");
                }
                catch (FileNotFoundException ex)
                {
                    // Specifikt fel om filen inte finns
                    Console.WriteLine($"Filen hittades inte: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    // Specifikt fel om texten inte kan tolkas som tal
                    Console.WriteLine($"Formatfel: {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    // Specifikt fel om nolldivision
                    Console.WriteLine($"Kan inte dividera med noll: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Fallback för alla övriga obekanta fel
                    Console.WriteLine($"Okänt fel: {ex.Message}");
                }
                finally
                {
                    // Körs ALLTID, även om det blev undantag
                    Console.WriteLine("Cleanup: Logging avslutat anrop.");
                }

                Console.WriteLine("Programmet avslutas normalt.");
            }
            int Count()
            {
                return 5 + 5;
            }

            // Exempel på metod som själv kastar ett undantag (throw)
            static double ProcessFile(string fileName)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    throw new ArgumentException("Filnamn får inte vara tomt eller null.", nameof(fileName));

                // using-satsen garanterar att resursen stängs automatiskt
                // utan att vi behöver ett explicit finally-block för det
                using var reader = new StreamReader(fileName);

                string? line = reader.ReadLine();
                if (line == null)
                    throw new InvalidOperationException("Filen är tom.");

                // TryParse kastar inget undantag – den returnerar false vid ogiltigt format
                if (!int.TryParse(line, out int number))
                    throw new FormatException($"Kunde inte tolka '{line}' som ett heltal.");

                if (number == 0)
                    throw new DivideByZeroException("Talet i filen är noll – kan inte dividera.");

                return 100.0 / number;
            }

        }
    }
}

