namespace checker.src;

public enum VitalIssue
{
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

	// CCN ≈ 1 here (no if/else in this method)
	public static List<VitalIssue> Evaluate(float temperatureF, int pulseRate, int spo2)
	{
		return new[]
			{
				CheckTemperature(temperatureF),
				CheckPulse(pulseRate),
				CheckSpO2(spo2)
			}
			.Where(v => v.HasValue)
			.Select(v => v!.Value)
			.ToList();
	}

	public static bool IsOk(float temperatureF, int pulseRate, int spo2) =>
		Evaluate(temperatureF, pulseRate, spo2).Count == 0;

	// Each helper has CCN ≈ 1.
	private static VitalIssue? CheckTemperature(float t) => t switch
	{
		< MinTempF => VitalIssue.TemperatureLow,
		> MaxTempF => VitalIssue.TemperatureHigh,
		_ => null
	};

	private static VitalIssue? CheckPulse(int p) => p switch
	{
		< MinPulse => VitalIssue.PulseLow,
		> MaxPulse => VitalIssue.PulseHigh,
		_ => null
	};

	private static VitalIssue? CheckSpO2(int s) => s < MinSpO2 ? VitalIssue.SpO2Low : null;
}