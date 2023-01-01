
# .NET Networked Test Equipment Library

This library allows you to easily connect to and control your electronics test equipment over a network connection. It is implemented entirely in native .NET, so it does not require any additional equipment libraries or vendor specific DLLs. The library can be used on all platforms that support .NET, including Windows, Mac, Linux, and embedded systems.

Using this library, you can perform a variety of tasks such as sending commands, reading measurements, and configuring settings on your test equipment. It is designed to be easy to use, with a straightforward API and detailed documentation to help you get started.


# Supported Equipment

Currently, the following equipment is supported:
* Rigol MSO5000 Series Oscilloscopes
* Rigol DP800 Series Power Supplies
* Rigol DL3000 Series DC Loads

However, the code **should** work with most oscilloscopes/supplies/loads with most functions.

# Examples

## Connecting to a DP800 Power Supply and Setting Channel Voltage
Connecting to a power supply instrument and setting the output to 3.3V:

```csharp
    var dp800a = new RigolDP800Instrument();
    await dp800a.Connect("10.0.0.132");
    
    Console.WriteLine($"Connected to {dp800a.InstrumentType.Manufacturer} {dp800a.InstrumentType.Model}");
    
    await dp800a.SelectChannel(Channel.CH1);
    await dp800a.Output.Disable();
    await dp800a.Voltage.Set(3.3);
    await dp800a.WaitForOperationComplete(1);
    await dp800a.Output.Enable();
```

# Oscilloscope Usage

The NetworkedTestEquipmentLibrary provides a class called `RigolMSO5000Instrument` for interacting with a Rigol MSO5000 oscilloscope.

The following example methods show how you can use the `RigolMSO5000Instrument` class to perform various tasks with an oscilloscope.

## Connect to an MSO5000 Oscilloscope

```csharp
var mso5000 = new RigolMSO5000Instrument();
await mso5000.Connect("10.0.0.74");
```

## Enabling/Disabling Oscilloscope Channel Display
You can use the SetDisplay method of the Channel object to enable or disable the display of a specific channel on the oscilloscope. The method takes a bool indicating whether to enable (true) or disable (false) the display, and a ScopeChannel representing the channel to enable or disable (e.g. CH1, CH2, etc.).

Here is an example of how to disable the display of all channels and then re-enable the display of channels 1 and 2:

```csharp
if (mso != null)
{
    // Disable display of all channels
    await mso.Channel.SetDisplay(false, ScopeChannel.CH1);
    await mso.Channel.SetDisplay(false, ScopeChannel.CH2);
    await mso.Channel.SetDisplay(false, ScopeChannel.CH3);
    await mso.Channel.SetDisplay(false, ScopeChannel.CH4);

    // Wait for all operations to complete before continuing
    await mso.WaitForOperationComplete();

    // Enable display of channels 1 and 2
    await mso.Channel.SetDisplay(true, ScopeChannel.CH1);
    await mso.Channel.SetDisplay(true, ScopeChannel.CH2);
}
```

## Displaying Measurements and Statistics

To display measurements or measurements with statistics on the oscilloscope, you can use the `EnableMeasurement` and `EnableStatistic` methods of the `Measure` object. These methods take a `MeasureFunction` representing the measurement function to display (e.g. `VPP`, `VMAX`, `VRMS`) and a `MeasureChannel` representing the channel to display the measurement for (e.g. `CHAN1`, `CHAN2`).

Here is an example of how to use these methods to display various measurements and statistics on the oscilloscope:

```csharp
if (mso != null)
{
    // Enable statistical measurements for VPP and VMAX on channel 1
    await mso.Measure.EnableStatistic(MeasureFunction.VPP, MeasureChannel.CHAN1);
    await mso.Measure.EnableStatistic(MeasureFunction.VMAX, MeasureChannel.CHAN1);

    // Enable statistical measurement for VMIN on channel 2
    await mso.Measure.EnableStatistic(MeasureFunction.VMIN, MeasureChannel.CHAN2);

    // Enable measurements for VRMS and VAVG on channel 2
    await mso.Measure.EnableMeasurement(MeasureFunction.VRMS, MeasureChannel.CHAN2);
    await mso.Measure.EnableMeasurement(MeasureFunction.VAVG, MeasureChannel.CHAN1);

    // Wait for all operations to complete before continuing
    await mso.WaitToContinue();
}
```

## Removing Measurements from the Display
To remove measurements from the display of the oscilloscope, you can use the `Clear` method of the `Measure` object. Passing in the value `MeasureItem.ALL` as the parameter will remove all measurements from the display, regardless of which channel or function they are for.

Here is an example of how to use the Clear method:

```csharp
if (mso != null)
{
	// Remove the first measurement item
	await scope.Measure.Clear(MeasureItem.ITEM1);
	
    // Remove all measurements from the display
    await mso.Measure.Clear(MeasureItem.ALL);
}
```


## Querying Measurements
You can use the `Measure.QueryMeasurement` method to retrieve the value of a specific measurement (e.g. VMAX, VPP, PERIOD, etc) on the specific channel (CH1, CH2, MATH1, etc.) from the oscilloscope. If you do not specify the channel, it will use the channel you selecting with the `Measure.SetSource` method. The method below returns a double representing the voltage measurement.

```csharp
internal async Task<double> QueryScopeMeasurement(MeasureFunction function, MeasureChannel channel)
{
    // Check if oscilloscope object is available
    if (mso == null)
        return double.NaN;

    // Initialize measurement and attempt counter
    double meas = double.NaN;
    int attempts = 0;

    // Make multiple attempts to retrieve measurement
    while (attempts++ < 20)
    {
        meas = await mso.Measure.QueryMeasurement(function, channel);

        // Return measurement if it is valid (not double.NaN)
        if (!double.IsNaN(meas))
            return meas;
    }

    // Return measurement (could be double.NaN if all attempts failed)
    return meas;
}
```

**Note:** A measurement may return `double.NaN` if the oscilloscope is busy updating the measurement when you attempt to read it. In this case, the method will make multiple attempts until a valid measurement is returned or the maximum number of attempts is reached.

## Querying Statistics
You can use the `Measure.QueryStatistic` method to retrieve the value of a specific measurement (e.g. VMAX, VPP, PERIOD, etc) on the specific channel (CH1, CH2, MATH1, etc.) from the oscilloscope. This will allow you to read the statistics from the measurement window (e.g. Maximum, Average, Deviation.) If you do not specify the channel, it will use the channel you selecting with the `Measure.SetSource` method. The method below returns a double representing the voltage measurement.

```csharp
internal async Task<double> QueryScopeStatistic(MeasureFunction function, MeasureType type, MeasureChannel channel)
{
    // Check if oscilloscope object is available
    if (mso == null)
        return double.NaN;

    // Initialize measurement and attempt counter
    double meas = double.NaN;
    int attempts = 0;

    // Make multiple attempts to retrieve measurement
    while (double.IsNaN(meas) && attempts++ < 20)
    {
        meas = await mso.Measure.QueryStatistic(function, type, channel);
    }

    // Return measurement (could be double.NaN if all attempts failed)
    return meas;
}
```

**Note:** A measurement may return `double.NaN` if the oscilloscope is busy updating the measurement when you attempt to read it. In this case, the method will make multiple attempts until a valid measurement is returned or the maximum number of attempts is reached.

## Capturing and Saving the Oscilloscope Display

To capture the display of the oscilloscope and save it to a file, you can use the `Capture` method of the `Display` object and the `SaveAsPngAsync` method of the `Image` object. The `Capture` method returns an `Image` object representing the current display of the oscilloscope. The `SaveAsPngAsync` method takes a file path as a parameter and saves the image to the specified location in PNG format.

Here is an example of how to use these methods to capture and save the oscilloscope display:

```csharp
if (mso != null)
{
    // Capture the oscilloscope display as an Image object
    var display = await mso.Display.Capture();

    // Save the image to a file if it was successfully captured
    if (display != null)
    {
        await display.SaveAsPngAsync("C:\\temp\\screenshot.png");
    }
}
```

**Note:** You will need to have the [SixLabors.ImageSharp](https://www.nuget.org/packages/SixLabors.ImageSharp/) NuGet package installed in order to use this code.

## Capturing Waveform Data from the Oscilloscope

To capture waveform data from the oscilloscope, you can use the `Data` method of the `Waveform` object. This method takes a `WaveformSource` representing the source of the waveform data (e.g. `CHAN1`, `CHAN2`) and a `WaveformMode` representing the mode of the waveform data (e.g. `NORM`, `MAX`, `RAW`). The `WaveformMode` can be one of the following:

-   `NORM`: Reads the waveform data currently displayed on the screen.
-   `MAX`: Reads the waveform data displayed on the screen when the oscilloscope is in the Run state; reads the waveform data in the internal memory when the oscilloscope is in the Stop state.
-   `RAW`: Reads the waveform data in the internal memory. **Note:** *The data in the internal memory can only be read when the oscilloscope is in the Stop state. You are not allowed to operate the instrument when it is reading data.*

The `Data` method returns a `List<WaveformDataPoint>` object, where each `WaveformDataPoint` object represents a point on the waveform with a `Time` and `Voltage` value.

Here is an example of how to use the `Data` method to capture waveform data from the oscilloscope:

```csharp
if (mso != null)
{
    // Capture waveform data from channel 1 in normal mode
    var scopeWave = await mso.Waveform.Data(WaveformSource.CHAN1, WaveformMode.NORM);
}
```
