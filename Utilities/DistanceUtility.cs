using System.Device.Gpio;
using System.Diagnostics;

namespace Distance.Utilities;

class DistanceUtility
{
    //this values are in BCM mode   23, 24
    //in Board mode those are       16, 18
    private int _triggerPin = 23;
    private int _echoPin = 24;
    private GpioController _controller;
    public DistanceUtility()
    {
        _controller = new GpioController();
        _controller.OpenPin(_triggerPin, PinMode.Output);
        _controller.OpenPin(_echoPin, PinMode.Input);

        _controller.Write(_triggerPin, PinValue.Low);
    }

    public string GetDistance()
    {
        ManualResetEvent mre = new ManualResetEvent(false);
        mre.WaitOne(500);
        Stopwatch pulseLength = new Stopwatch();

        //Send pulse
        _controller.Write(_triggerPin, PinValue.High);
        mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
        _controller.Write(_triggerPin, PinValue.Low);

        //Receive pulse
        while (_controller.Read(_echoPin) == PinValue.Low)
        {
        }
        pulseLength.Start();

        while (_controller.Read(_echoPin) == PinValue.High)
        {
        }
        pulseLength.Stop();

        //Calculating distance
        TimeSpan timeBetween = pulseLength.Elapsed;
        Debug.WriteLine(timeBetween.ToString());
        double distance = timeBetween.TotalSeconds * 17150;

        return distance.ToString();
    }
}