using System;
using System.Threading;
using BuildIndicatorCommon.ExceptionHandling;
using BuildIndicatorControl;

namespace BuildIndicatorServiceLibrary
{
    static class BuildIndicatorExceptionHandler
    {
        public static void HandleException(Exception exception, BuildServiceManager manager, Light buildLight)
        {
            if (exception is UnknownBuildStateException)
            {

                buildLight.LightColor = Light.Color.Yellow;
                buildLight.TurnOn();
                Thread.Sleep(250);
                buildLight.LightColor = Light.Color.Green;
                buildLight.TurnOn();

                Thread.Sleep(3000);
                buildLight.LightColor = Light.Color.Yellow;
                manager.Reset();
                buildLight.LightColor = Light.Color.Green;
                manager.Reset();

                return;
            }

            if (exception is BuildNotFoundException)
            {
                buildLight.LightColor = Light.Color.Yellow;
                buildLight.TurnOn();
                Thread.Sleep(250);
                buildLight.LightColor = Light.Color.Red;
                buildLight.TurnOn(true);

                Thread.Sleep(3000);
                buildLight.LightColor = Light.Color.Yellow;
                manager.Reset();
                buildLight.LightColor = Light.Color.Red;
                manager.Reset();

                return;
            }

            if (exception is DeviceNotConnectedException)
            {
                //have to ignore
                return;
            }
            
            //default
            buildLight.LightColor = Light.Color.Yellow;
            buildLight.TurnOn(true);
            Thread.Sleep(250);
            buildLight.LightColor = Light.Color.Green;
            buildLight.TurnOn(true);
            Thread.Sleep(250);
            buildLight.LightColor = Light.Color.Red;
            buildLight.TurnOn(true);


            Thread.Sleep(3000);
            buildLight.LightColor = Light.Color.Red;
            manager.Reset();
            buildLight.LightColor = Light.Color.Yellow;
            manager.Reset();
            buildLight.LightColor = Light.Color.Green;
            manager.Reset();
        }

    }
}
