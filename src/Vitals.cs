namespace checker.src;

public enum VitalIssue
{
	None = 0,
	TemperatureLow,
	TemperatureHigh,
	PulseLow,
	PulseHigh,
	SpO2Low
}

public static class Vitals
{
	public const float MinTempF = 95f;
	public const float MaxTempF = 102f;
	public const int MinPulse = 60;
	public const int MaxPulse = 100;
	public const int MinSpO2 = 90;

	public static List<VitalIssue> Evaluate(float temperatureF, int pulseRate, int spo2)
	{
		var issues = new List<VitalIssue>(capacity: 3);

		if (temperatureF < MinTempF) issues.Add(VitalIssue.TemperatureLow);
		else if (temperatureF > MaxTempF) issues.Add(VitalIssue.TemperatureHigh);

		if (pulseRate < MinPulse) issues.Add(VitalIssue.PulseLow);
		else if (pulseRate > MaxPulse) issues.Add(VitalIssue.PulseHigh);

		if (spo2 < MinSpO2) issues.Add(VitalIssue.SpO2Low);

		return issues;
	}

	// returns true when no issues.
	public static bool IsOk(float temperatureF, int pulseRate, int spo2) =>
		Evaluate(temperatureF, pulseRate, spo2).Count == 0;
}