namespace checker.src;

public static class Checker
{
	public static bool VitalsOk(float temperature, int pulseRate, int spo2, IAlert alert)
	{
		List<VitalIssue> issues = Vitals.Evaluate(temperature, pulseRate, spo2);

		if (issues.Count == 0)
		{
			alert.Info("Vitals received within normal range");
			alert.Info($"Temperature: {temperature}  Pulse: {pulseRate}  SpO2: {spo2}");
			return true;
		}

		foreach (var issue in issues)
		{
			alert.Critical(MessageFor(issue));
			alert.Blink(times: 6, delayMs: 1000);
		}
		return false;
	}

	private static string MessageFor(VitalIssue issue) => issue switch
	{
		VitalIssue.TemperatureLow => "Temperature is LOW!",
		VitalIssue.TemperatureHigh => "Temperature is HIGH!",
		VitalIssue.PulseLow => "Pulse Rate is LOW!",
		VitalIssue.PulseHigh => "Pulse Rate is HIGH!",
		VitalIssue.SpO2Low => "Oxygen Saturation is LOW!",
		_ => "Unknown issue"
	};
}