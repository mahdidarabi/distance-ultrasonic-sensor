using Distance.Utilities;

Console.WriteLine("App Started...");

DistanceUtility distanceUtility = new DistanceUtility();

while (true)
{
    try
    {
        var result = distanceUtility.GetDistance();
        System.Console.WriteLine(result);
    }
    catch (System.Exception)
    {
        System.Console.WriteLine("ERROR"); ;
    }
    Thread.Sleep(1000);
}