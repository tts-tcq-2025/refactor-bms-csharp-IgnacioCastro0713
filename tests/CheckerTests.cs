using checker.src;
using Xunit;

namespace checker.tests;

public sealed class FakeAlert : IAlert
{
	public readonly List<string> Infos = new();
	public readonly List<string> Criticals = new();
	public int BlinkCalls;
	public (int times, int delayMs) LastBlink;

	public void Info(string message) => Infos.Add(message);
	public void Critical(string message) => Criticals.Add(message);
	public void Blink(int times, int delayMs)
	{
		BlinkCalls++;
		LastBlink = (times, delayMs);
	}
}

public class CheckerTests
{
	[Fact]
	public void Returns_False_And_Alerts_For_Each_Issue()
	{
		var fake = new FakeAlert();

		bool ok = Checker.VitalsOk(103f, 55, 85, fake);

		Assert.False(ok);
		Assert.Equal(3, fake.Criticals.Count);
		Assert.Equal(3, fake.BlinkCalls);
		Assert.Equal((6, 1000), fake.LastBlink);
	}

	[Fact]
	public void Returns_True_And_Logs_Info_When_Ok()
	{
		var fake = new FakeAlert();

		bool ok = Checker.VitalsOk(98.1f, 70, 98, fake);

		Assert.True(ok);
		Assert.Contains(fake.Infos, s => s.Contains("within normal range"));
		Assert.Contains(fake.Infos, s => s.Contains("Temperature:"));
		Assert.Empty(fake.Criticals);
		Assert.Equal(0, fake.BlinkCalls);
	}
}