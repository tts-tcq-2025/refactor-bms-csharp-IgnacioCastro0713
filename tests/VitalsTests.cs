using checker.src;
using Xunit;

namespace checker.tests;

public class VitalsTests
{
	[Fact]
	public void Ok_When_All_Are_Inclusive_Bounds()
	{
		Assert.True(Vitals.IsOk(95f, 60, 90));
		Assert.True(Vitals.IsOk(102f, 100, 90));
	}

	[Theory]
	[InlineData(98.6f, 70, 98)]
	[InlineData(99f, 80, 95)]
	[InlineData(97f, 60, 90)]
	public void Ok_When_Typical_Normal(float t, int p, int s)
	{
		Assert.True(Vitals.IsOk(t, p, s));
		Assert.Empty(Vitals.Evaluate(t, p, s));
	}

	[Fact]
	public void Temperature_Low_High()
	{
		var low = Vitals.Evaluate(94.9f, 70, 95);
		var high = Vitals.Evaluate(102.1f, 70, 95);

		Assert.Contains(VitalIssue.TemperatureLow, low);
		Assert.DoesNotContain(VitalIssue.TemperatureHigh, low);

		Assert.Contains(VitalIssue.TemperatureHigh, high);
		Assert.DoesNotContain(VitalIssue.TemperatureLow, high);
	}

	[Fact]
	public void Pulse_Low_High()
	{
		var low = Vitals.Evaluate(98.6f, 59, 95);
		var high = Vitals.Evaluate(98.6f, 101, 95);

		Assert.Contains(VitalIssue.PulseLow, low);
		Assert.Contains(VitalIssue.PulseHigh, high);
	}

	[Fact]
	public void SpO2_Low()
	{
		var issues = Vitals.Evaluate(98.6f, 70, 89);
		Assert.Contains(VitalIssue.SpO2Low, issues);
	}

	[Fact]
	public void Multiple_Issues_Are_Reported()
	{
		var issues = Vitals.Evaluate(103f, 55, 85);
		Assert.Equal(3, issues.Count);
		Assert.Contains(VitalIssue.TemperatureHigh, issues);
		Assert.Contains(VitalIssue.PulseLow, issues);
		Assert.Contains(VitalIssue.SpO2Low, issues);
	}

	[Fact]
	public void Boundaries_Are_OK()
	{
		// temperature
		Assert.True(Vitals.IsOk(95f, 70, 95));
		Assert.True(Vitals.IsOk(102f, 70, 95));

		// pulse
		Assert.True(Vitals.IsOk(98.6f, 60, 95));
		Assert.True(Vitals.IsOk(98.6f, 100, 95));

		// spo2
		Assert.True(Vitals.IsOk(98.6f, 70, 90));
	}
}