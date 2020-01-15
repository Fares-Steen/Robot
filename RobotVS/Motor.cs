using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;
namespace RobotVS
{
 class Motor
    {


        private int[] ControlPin = new int[] { 6, 13, 19, 26 };

        public void Start()
        {

            Light();
        }

     

        private void Light()
        {
            using (GpioController gpio = new GpioController(PinNumberingScheme.Logical, new RaspberryPi3Driver()))
            {
                int[,] seg = new int[,] {
                          {1, 0, 0, 0 },
                          {1, 1, 0, 0},
                          {0, 1, 0, 0},
                          {0, 1, 1, 0},
                          {0, 0, 1, 0},
                          {0, 0, 1, 1},
                          {0, 0, 0, 1},
                          {1, 0, 0, 1}
                    };



                for (int pin = 0; pin < 4; pin++)
                {
                    gpio.OpenPin(ControlPin[pin], PinMode.Output);

                }

                for (int i = 0; i < 128; i++)
                {
                    for (int halfstep = 0; halfstep < 8; halfstep++)
                    {
                        for (int pin = 0; pin < 4; pin++)
                        {

                            gpio.Write(ControlPin[pin], seg[halfstep, pin]);
                            Thread.Sleep(TimeSpan.FromSeconds(0.0011));
                        }

                    }

                }
                for (int pin = 0; pin < 4; pin++)
                {


                    gpio.ClosePin(ControlPin[pin]);

                }


            }


        }


    }
}