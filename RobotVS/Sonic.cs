using System;
using System.Device.Gpio;
using System.Device.Gpio.Drivers;
using System.Threading;
namespace RobotVS
{
    class Sonic
    {
        int TRIG=17;
         int ECHO=4;
         long distance{ get; set; }
public void Start(){
 using (GpioController gpio = new GpioController(PinNumberingScheme.Logical, new RaspberryPi3Driver()))
 for (int x=0; x<50;x++){
            {
                 gpio.OpenPin(TRIG, PinMode.Output);
                    gpio.Write(TRIG,0);

                 gpio.OpenPin(ECHO, PinMode.Input);

                Thread.Sleep(TimeSpan.FromSeconds(0.1));
                var startScanning = DateTime.Now.AddSeconds(2);
                
                gpio.Write(TRIG,1);
                 Thread.Sleep(TimeSpan.FromSeconds(0.1));
                 Boolean worked=true;
                gpio.Write(TRIG,0);
                

                    while (gpio.Read(ECHO)==0)
                    {
                        
                      
                        if( DateTime.Now>startScanning){
                             Console.WriteLine("Failed aftre 2 sec");
                            gpio.ClosePin(ECHO);
                             gpio.ClosePin(TRIG);
                             worked=false;
                             break;
                        }
                    }
                    if(worked){
                     var start = DateTime.Now;

                     while (gpio.Read(ECHO)==1)
                    {

                    }
                     var stop = DateTime.Now;
                            //Console.WriteLine(stop.Ticks);

                            distance = ((stop.Ticks - start.Ticks) * 17) / 10000;
                            Console.WriteLine(distance);

                            gpio.ClosePin(ECHO);
                   gpio.ClosePin(TRIG);
                    }
            }}

        }
      
    }
}