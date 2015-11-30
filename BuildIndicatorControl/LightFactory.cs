using BuildIndicatorCommon.ExceptionHandling;

namespace BuildIndicatorControl
{

    public class LightFactory : ILightFactory
    {
        private readonly IDeviceController _controller;

        public LightFactory() : this(new DelcomDeviceController())
        {
            //TODO: Know how to configure the default device controller
        }

        public LightFactory(IDeviceController deviceController)
        {
            _controller = deviceController;
        }

        public Light CreateLight()
        {
            if (!_controller.OpenDevice())
            {
                throw new DeviceNotConnectedException();
            }

            return new Light(_controller);
        }
    }
}