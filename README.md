# .NET Networked Test Equipment Library

This library allows you to easily connect to and control your electronics test equipment over a network connection. It is implemented entirely in native .NET, so it does not require any additional equipment libraries or vendor specific DLLs. The library can be used on all platforms that support .NET, including Windows, Mac, Linux, and embedded systems.

Using this library, you can perform a variety of tasks such as sending commands, reading measurements, and configuring settings on your test equipment. It is designed to be easy to use, with a straightforward API and detailed documentation to help you get started.


# Supported Equipment

Currently, the following equipment is supported:
* Rigol MSO5000 Series Oscilloscopes
* Rigol DP800 Series Power Supplies
* Rigol DL3000 Series DC Loads

However, the code **should** work with most oscilloscopes/supplies/loads with most functions.

# Using the Library

Connecting to a power supply instrument and setting the output to 3.3V:

    var dp800a = new RigolDP800Instrument();
    await dp800a.Connect("10.0.0.132");
    
    Console.WriteLine($"Connected to {dp800a.InstrumentType.Manufacturer} {dp800a.InstrumentType.Model}");
    
    await dp800a.SelectChannel(Channel.CH1);
    await dp800a.Output.Disable();
    await dp800a.Voltage.Set(3.3);
    await dp800a.WaitForOperationComplete(1);
    await dp800a.Output.Enable();
