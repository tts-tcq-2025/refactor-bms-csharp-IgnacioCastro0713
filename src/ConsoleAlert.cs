namespace checker.src;

public interface IAlert
{
	void Info(string message);
	void Critical(string message);
	void Blink(int times, int delayMs);
}

public sealed class ConsoleAlert : IAlert
{
	public void Info(string message) => Console.WriteLine(message);

	public void Critical(string message) => Console.WriteLine(message);

	public void Blink(int times, int delayMs)
	{
		for (int i = 0; i < times; i++)
		{
			Console.Write("\r* ");
			Thread.Sleep(delayMs);
			Console.Write("\r *");
			Thread.Sleep(delayMs);
		}
		Console.WriteLine(); // end line after blinking
	}
}