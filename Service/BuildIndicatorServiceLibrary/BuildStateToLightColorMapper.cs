using BuildIndicatorCommon.ExceptionHandling;
using BuildIndicatorCommon.Model;
using BuildIndicatorControl;

namespace BuildIndicatorServiceLibrary
{
    public static class BuildStateToLightColorMapper
    {
        public static void SetLightColorFromBuildState(Build buildFromSource, Light light)
        {
            switch (buildFromSource.State)
            {
                case Build.BuildState.Success:
                    {
                        light.LightColor = Light.Color.Green;
                        light.TurnOn(buildFromSource.IsBuilding);
                        break;
                    }
                case Build.BuildState.Unstable:
                    {
                        light.LightColor = Light.Color.Yellow;
                        light.TurnOn(buildFromSource.IsBuilding);
                        break;
                    }
                case Build.BuildState.Failure:
                    {
                        light.LightColor = Light.Color.Red;
                        light.TurnOn(buildFromSource.IsBuilding);
                        break;
                    }
                case Build.BuildState.Aborted:
                case Build.BuildState.Unknown:
                    {
                        light.TurnOff();
                        break;
                    }
                default:
                    {
                        throw new UnknownBuildStateException(buildFromSource.State.ToString());
                    }
            }
        }
    }
}